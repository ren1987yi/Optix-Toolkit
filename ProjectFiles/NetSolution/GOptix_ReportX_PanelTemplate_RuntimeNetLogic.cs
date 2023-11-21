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
using FTOptix.DataLogger;
using FTOptix.SQLiteStore;
using DocumentFormat.OpenXml.ExtendedProperties;
#endregion
using Store = FTOptix.Store;
using MiniSoftware;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using OptixHelper;
using OpenXmlPowerTools;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Linq;
using GOptixLib;
using FTOptix.Report;
public class GOptix_ReportX_PanelTemplate_RuntimeNetLogic : BaseNetLogic
{
    ServerSiderRenderChart client;

    PdfViewer Viewer;

    IUAVariable StartTime;
    IUAVariable EndTime;

    IUAVariable TemplateFilePath;
    IUAVariable OutputFolder;

    IUAVariable Busy;

    LongRunningTask _taskGenerate;

    bool reRender = false;
    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started

        TemplateFilePath = Owner.GetVariable(nameof(TemplateFilePath));
        OutputFolder = Owner.GetVariable(nameof(OutputFolder));


        Viewer = LogicObject.GetAlias(nameof(Viewer)) as PdfViewer;

        StartTime = LogicObject.GetVariable(nameof(StartTime));
        EndTime = LogicObject.GetVariable(nameof(EndTime));

        StartTime.Value = DateTime.Now;
        EndTime.Value = DateTime.Now;

        Busy = LogicObject.GetVariable(nameof(Busy));

        client = new ServerSiderRenderChart(10000000,"http://localhost:18080");

        _taskGenerate = new LongRunningTask(GenerateHandle, LogicObject);
        Busy.Value = false;
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
        if (_taskGenerate != null)
        {


            _taskGenerate.Dispose();
        }
    }

    [ExportMethod]
    public void GenerateReport()
    {
        Busy.Value = true;
        reRender = true;
        _taskGenerate.Start();
    }

    [ExportMethod]
    public void ViewTemplate()
    {
        Busy.Value = true;
        reRender = false;
        _taskGenerate.Start();
    }


    private void GenerateHandle()
    {
        var tmp = ResourceUri.FromProjectRelativePath(TemplateFilePath.Value.Value as string);
        var folder = ResourceUri.FromProjectRelativePath(OutputFolder.Value.Value as string);
        Viewer.Path = "";
        if (reRender)
        {
            var outfile = Path.Combine(folder.Uri, "oo.docx");

            var val = QueryAndBuildValue();

            Log.Info($"template:{tmp.Uri}");
            Log.Info($"outfile:{outfile}");
            GenerateWord(tmp.Uri, outfile, val);
            Word2Pdf(outfile, Path.GetDirectoryName(outfile));
            Log.Info("OK");



            Busy.Value = false;

            var pdffile = Path.Combine(Path.GetDirectoryName(outfile), "oo.pdf");
            // Viewer.Path = "";
            Viewer.Path = pdffile;
        }
        else
        {
            var outfile = Path.Combine(folder.Uri, "oo.docx");
            Word2Pdf(tmp.Uri, folder.Uri);
            Log.Info("OK");



            Busy.Value = false;

            var pdffile = Path.Combine(Path.GetDirectoryName(outfile), Path.GetFileNameWithoutExtension(tmp.Uri) + ".pdf");
            
            Viewer.Path = pdffile;
        }
    }


    Random rnd = new Random();
    private object QueryAndBuildValue()
    {
       
        var st = (DateTime)StartTime.Value.Value;
        var et = (DateTime)EndTime.Value.Value;


        var _st = st.ToString("yyyy-MM-ddTHH:mm:ss");
        var _et = et.ToString("yyyy-MM-ddTHH:mm:ss");


        var store = Project.Current.GetObject("DataStores").Get<Store.Store>("LocalDB");
        var logger = Project.Current.GetObject("Loggers/DataLogger1") as DataLogger;
        var sql = @$"select LocalTimestamp as DT,V1,V2,V3 from DataLogger1 where LocalTimestamp >= '{_st}' and LocalTimestamp <= '{_et}' order by LocalTimestamp";
        Log.Info(sql);
       

        var res = StoreHelpr.Query(store, sql);


    
        var s2 = res;

        sql = @$"select 
        MAX(V1) AS maxV1
        ,MIN(V1) AS minV1
        ,MAX(V2) AS maxV2
        ,MIN(V2) AS minV2
        ,MAX(V3) AS maxV3
        ,MIN(V3) AS minV3
         from DataLogger1 where LocalTimestamp >= '{_st}' and LocalTimestamp <= '{_et}' order by LocalTimestamp";
        res = StoreHelpr.Query(store, sql);

        var s1 = new List<Dictionary<string, object>>();

        s1.Add(new Dictionary<string, object>(){
            { "Name","V1"},
            { "MaxValue",res.First()["maxV1"]},
            { "MinValue",res.First()["minV1"]},
        });

        s1.Add(new Dictionary<string, object>(){
            { "Name","V2"},
            { "MaxValue",res.First()["maxV2"]},
            { "MinValue",res.First()["minV2"]},
        });

        s1.Add(new Dictionary<string, object>(){
            { "Name","V3"},
            { "MaxValue",res.First()["maxV3"]},
            { "MinValue",res.First()["minV3"]},
        });



        
        // var option_path = @"D:\Work\Optix\Optix_Toolkit\ProjectFiles\StaticHtml\chartdata\options\echart_ssr_data.js";
        // EChartTrend.SaveSSROption(store,logger,st,et,new string[]{"V1","V3","V4"},option_path); //保存 echart 的Option
        // GenerateTrend("d:\\aaa.svg",900,540); //服务端渲染SVG

        var echart_option = EChartTrend.BuildOption(store,logger,st,et,new string[]{"V1","V3","V4"},false);
        var svg = client.GetEChartSvg(900,540,echart_option,out_file:@"d:\aaa.svg");
        // File.WriteAllText(@"d:\aaa.svg",svg);
        Log.Info(svg);

        svg = client.GetQRcodeSvg(DateTime.Now.ToString(),200,200,out_file:@"d:\aaa1.svg");
        // File.WriteAllText(@"d:\aaa1.svg",svg);
        Log.Info(svg);


        svg = client.GetBarcodeSvg(DateTime.Now.ToString(),out_file:@"d:\aaa2.svg");
        // File.WriteAllText(@"d:\aaa2.svg",svg);
        Log.Info(svg);
        // var qr_url = @$"http://127.0.0.1/OPtixWeb/qrcode_viewer.html?value={DateTime.Now}f&useSVG=true";
        // ServerSiderRenderSVG(qr_url,@"d:\aaa1.svg",true); //服务端渲染SVG



        var value = new Dictionary<string, object>()
        {
            ["StartTime"] = st,
            ["EndTime"] = et,
            ["S1"] = s1,
            ["S2"] = s2,
            ["img"] = new MiniWordPicture() { Path = @"d:\aaa.svg", Width = 900, Height = 540 },
            ["qrcode"] = new MiniWordPicture() { Path = @"d:\aaa1.svg", Width = 200, Height = 200 },
            ["barcode"] = new MiniWordPicture() { Path = @"d:\aaa2.svg", Width = 600, Height = 200 }
        };



        return value;
    }

    /// <summary>
    /// 生成word
    /// </summary>
    /// <param name="tmpFilepath">模板文件</param>
    /// <param name="optFilepath">输出路径</param>
    /// <param name="value">渲染值</param>
    private void GenerateWord(string tmpFilepath, string optFilepath, object value)
    {
        MiniWord.SaveAsByTemplate(optFilepath, tmpFilepath, value);
    }


    private void GenerateTrend(string output_file,int width=1024,int height=768){
        var filename = "python";
        var args = new string[]{
            @"""C:\Users\yren6\PycharmProjects\testDemo\get_echart_svg.py""",
            @$"""http://127.0.0.1/OptixWeb/echart_ssr.html?w={width}&h={height}""",
            output_file,

        };
        CallCommand(filename,string.Join(" ",args));


        
    }

    private void ServerSiderRenderSVG(string url,string output_file,bool fixSvg=false){
        var filename = "python";
        var args = new List<string>(){
            @"""C:\Users\yren6\PycharmProjects\testDemo\get_echart_svg.py""",
            url,
            output_file,


        };

        if(fixSvg){
            args.Add("true");
        }

        
        CallCommand(filename,string.Join(" ",args));
    }



    private void CallCommand(string filename,string args){
        Process p = new Process();//设定调用的程序名，不是系统目录的需要完整路径 
        p.StartInfo.FileName = filename;//执行的外部软件全路径
        p.StartInfo.Arguments = args; // 
        p.StartInfo.UseShellExecute = false;//是否重定向标准输入 
        p.StartInfo.RedirectStandardInput = false;//是否重定向标准转出 
        p.StartInfo.RedirectStandardOutput = false;//是否重定向错误 
        p.StartInfo.RedirectStandardError = false;//执行时是不是显示窗口 
        p.StartInfo.CreateNoWindow = true;//启动 
        p.Start();
        p.WaitForExit();
        p.Close();
    }


    /// <summary>
    /// Word Convert to PDF
    /// </summary>
    /// <param name="wordfile">word file</param>
    /// <param name="outFolder">输出目录</param>
    private int Word2Pdf(string wordfile, string outFolder)
    {
        string libreOfficePath = @"D:\Download\Software\LibreOfficePortablePrevious\App\libreoffice\program\soffice.exe";

        // FIXME: file name escaping: I have not idea how to do it in .NET.
        ProcessStartInfo procStartInfo = new ProcessStartInfo(libreOfficePath, string.Format("--headless --convert-to pdf --nologo \"{0}\" --outdir \"{1}\"", wordfile, outFolder));
        procStartInfo.RedirectStandardOutput = true;
        procStartInfo.UseShellExecute = false;
        procStartInfo.CreateNoWindow = true;
        // procStartInfo.WorkingDirectory = Environment.CurrentDirectory;

        Process process = new Process() { StartInfo = procStartInfo, };
        process.Start();
        process.WaitForExit();

        // Check for failed exit code.
        if (process.ExitCode != 0)
        {
            Log.Error(string.Format("LibreOffice has failed with {0}", process.ExitCode));
            return process.ExitCode;
            // throw new LibreOfficeFailedException(process.ExitCode);
        }else{
            return 0;
        }
    }
    public class LibreOfficeFailedException : Exception
    {
        public LibreOfficeFailedException(int exitCode)
            : base(string.Format("LibreOffice has failed with {}", exitCode))
        { }
    }






}
