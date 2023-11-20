#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.HMIProject;
using FTOptix.NetLogic;
using FTOptix.NativeUI;
using FTOptix.UI;
using FTOptix.WebUI;
using FTOptix.Alarm;
using FTOptix.EventLogger;
using FTOptix.OPCUAServer;
using FTOptix.RAEtherNetIP;
using FTOptix.Retentivity;
using FTOptix.CoreBase;
using FTOptix.CommunicationDriver;
using FTOptix.Store;
using FTOptix.Core;
#endregion


using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml.Linq;
using DocumentFormat.OpenXml.Packaging;
using OpenXmlPowerTools;
using MiniSoftware;
using System.Collections.Generic;
using FTOptix.DataLogger;
using FTOptix.SQLiteStore;

public class GOptix_WordViewer_RuntimeNetLogic : BaseNetLogic
{
    IUAVariable FilePath;
    WebBrowser browser;
    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started
        FilePath = Owner.GetVariable(nameof(FilePath));
        browser = LogicObject.GetAlias("Browser") as WebBrowser;

        var path1 = @"D:\Work\Optix\Optix_Toolkit\ProjectFiles\WordSources\1.docx";
        var path2 = @"D:\Work\Optix\Optix_Toolkit\ProjectFiles\WordSources\t1.docx";
        GenerateWord(path1, path2);

        Docx2Html();
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
    }

    [ExportMethod]
    public void RefreshHandle()
    {
        Docx2Html();
    }


    public void Docx2Html()
    {

        var path = FilePath.Value.Value as string;

        Log.Info(path);

        var u = ResourceUri.FromProjectRelativePath(path);
        Log.Info(u.Uri);

        var folder = @"D:\Work\Optix\Optix_Toolkit\ProjectFiles\WordSources\output";
        ConvertToHtml(u.Uri, folder);

        var ofile = Path.Combine(folder, "1.html");

        browser.URL = ResourceUri.FromAbsoluteFilePath(ofile);

    }



    public void ConvertToHtml(string file, string outputDirectory)
    {
        var fi = new FileInfo(file);
        Console.WriteLine(fi.Name);
        byte[] byteArray = File.ReadAllBytes(fi.FullName);
        using (MemoryStream memoryStream = new MemoryStream())
        {
            memoryStream.Write(byteArray, 0, byteArray.Length);
            using (WordprocessingDocument wDoc = WordprocessingDocument.Open(memoryStream, true))
            {
                var destFileName = new FileInfo(fi.Name.Replace(".docx", ".html"));
                if (outputDirectory != null && outputDirectory != string.Empty)
                {
                    DirectoryInfo di = new DirectoryInfo(outputDirectory);
                    if (!di.Exists)
                    {
                        throw new OpenXmlPowerToolsException("Output directory does not exist");
                    }
                    destFileName = new FileInfo(Path.Combine(di.FullName, destFileName.Name));
                }
                var imageDirectoryName = destFileName.FullName.Substring(0, destFileName.FullName.Length - 5) + "_files";
                int imageCounter = 0;

                var pageTitle = fi.FullName;
                var part = wDoc.CoreFilePropertiesPart;
                if (part != null)
                {
                    pageTitle = (string)part.GetXDocument().Descendants(DC.title).FirstOrDefault() ?? fi.FullName;
                }

                // TODO: Determine max-width from size of content area.
                WmlToHtmlConverterSettings settings = new WmlToHtmlConverterSettings()
                {
                    AdditionalCss = "body { margin: 1cm auto; max-width: 20cm; padding: 0; }",
                    PageTitle = pageTitle,
                    FabricateCssClasses = true,
                    CssClassPrefix = "pt-",
                    RestrictToSupportedLanguages = false,
                    RestrictToSupportedNumberingFormats = false,
                    ImageHandler = imageInfo =>
                    {
                        DirectoryInfo localDirInfo = new DirectoryInfo(imageDirectoryName);
                        if (!localDirInfo.Exists)
                            localDirInfo.Create();
                        ++imageCounter;
                        string extension = imageInfo.ContentType.Split('/')[1].ToLower();
                        ImageFormat imageFormat = null;
                        if (extension == "png")
                            imageFormat = ImageFormat.Png;
                        else if (extension == "gif")
                            imageFormat = ImageFormat.Gif;
                        else if (extension == "bmp")
                            imageFormat = ImageFormat.Bmp;
                        else if (extension == "jpeg")
                            imageFormat = ImageFormat.Jpeg;
                        else if (extension == "tiff")
                        {
                            // Convert tiff to gif.
                            extension = "gif";
                            imageFormat = ImageFormat.Gif;
                        }
                        else if (extension == "x-wmf")
                        {
                            extension = "wmf";
                            imageFormat = ImageFormat.Wmf;
                        }

                        // If the image format isn't one that we expect, ignore it,
                        // and don't return markup for the link.
                        if (imageFormat == null)
                            return null;

                        string imageFileName = imageDirectoryName + "/image" +
                            imageCounter.ToString() + "." + extension;
                        try
                        {
                            imageInfo.Bitmap.Save(imageFileName, imageFormat);
                        }
                        catch (Exception ex)
                        {
                            Log.Error(ex.Message);
                            return null;
                        }
                        string imageSource = localDirInfo.Name + "/image" +
                            imageCounter.ToString() + "." + extension;

                        XElement img = new XElement(Xhtml.img,
                            new XAttribute(NoNamespace.src, imageSource),
                            imageInfo.ImgStyleAttribute,
                            imageInfo.AltText != null ?
                                new XAttribute(NoNamespace.alt, imageInfo.AltText) : null);
                        return img;
                    }
                };
                XElement htmlElement = WmlToHtmlConverter.ConvertToHtml(wDoc, settings);

                // Produce HTML document with <!DOCTYPE html > declaration to tell the browser
                // we are using HTML5.
                var html = new XDocument(
                    new XDocumentType("html", null, null, null),
                    htmlElement);

                // Note: the xhtml returned by ConvertToHtmlTransform contains objects of type
                // XEntity.  PtOpenXmlUtil.cs define the XEntity class.  See
                // http://blogs.msdn.com/ericwhite/archive/2010/01/21/writing-entity-references-using-linq-to-xml.aspx
                // for detailed explanation.
                //
                // If you further transform the XML tree returned by ConvertToHtmlTransform, you
                // must do it correctly, or entities will not be serialized properly.

                var htmlString = html.ToString(SaveOptions.DisableFormatting);
                File.WriteAllText(destFileName.FullName, htmlString, System.Text.Encoding.UTF8);
            }
        }
    }


    public void GenerateWord(string out_filepath, string tmp_filepath)
    {
        var value = new Dictionary<string, object>()
        {
            ["Title"] = "aaaa",
            ["Subtitle"] = "埃弗拉发23收到两份简历数据发",
            ["ss"] = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    { "a",1},
                    { "b",2},
                    { "d",2},
                    { "c","Discussion requirement part1"},
                },
                new Dictionary<string, object>
                {
                    { "a",4},
                    { "b",5},
                    { "d",222},
                    { "c","结构设计管理司法机构的监管"},
                },
            }
        };

        MiniWord.SaveAsByTemplate(out_filepath, tmp_filepath, value);
    }
}
