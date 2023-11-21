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
using FTOptix.Report;
#endregion

public class GOptix_ApexchartViewer_RuntimeNetLogic : BaseNetLogic
{


    IUAVariable RootPath;
    IUAVariable BackgoundColor;
    IUAVariable Blob;

    WebBrowser Web;

    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started
        RootPath = Owner.GetVariable(nameof(RootPath));
        BackgoundColor = Owner.GetVariable(nameof(BackgoundColor));
        Blob = Owner.GetVariable(nameof(Blob));

        RootPath.VariableChange += OnVarChange;
        BackgoundColor.VariableChange += OnVarChange;
        Blob.VariableChange += OnVarChange;


        Web = Owner as WebBrowser;
        // var uri_string = RootPath.Value.Value;
        // var uri = new ResourceUri(RootPath.Value);
        Refresh();
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped


        RootPath.VariableChange -= OnVarChange;
        BackgoundColor.VariableChange -= OnVarChange;
        Blob.VariableChange -= OnVarChange;
    }

    void OnVarChange(object sender, VariableChangeEventArgs args)
    {
        Refresh();

    }

    void Refresh()
    {
        string url = string.Empty;
        var uri = new ResourceUri(RootPath.Value);
        var _uri = string.Empty;
        var bg = (string)BackgoundColor.Value;
        var blob = (string)Blob.Value;


        var _url = uri.ConvertToURL();

        url = $"{_url}?bg={bg}&&blob={blob}";
        Web.URL = url;
        Log.Info(url);
    }

}
