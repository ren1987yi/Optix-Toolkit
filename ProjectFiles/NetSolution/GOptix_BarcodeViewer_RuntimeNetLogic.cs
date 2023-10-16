#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.EventLogger;
using FTOptix.HMIProject;
using FTOptix.NetLogic;
using FTOptix.NativeUI;
using FTOptix.UI;
using FTOptix.RAEtherNetIP;
using FTOptix.Retentivity;
using FTOptix.Alarm;
using FTOptix.CommunicationDriver;
using FTOptix.Store;
using FTOptix.CoreBase;
using FTOptix.Core;
#endregion
using OptixHelper;
using System.Linq;
using System.Collections.Generic;
using System.Web;
using System.ComponentModel.DataAnnotations;
using FTOptix.SQLiteStore;
using FTOptix.OPCUAServer;
using FTOptix.Recipe;
using FTOptix.WebUI;
public class GOptix_BarcodeViewer_RuntimeNetLogic : BaseNetLogic
{
    string WebRootPath;

    List<IUAVariable> _option_vars = new List<IUAVariable>();

    IUAVariable VarValue;
    IUAVariable URL;

    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started
        if(Owner.GetType().Name != nameof(GOptix_BarcodeViewer)){

            throw new Exception("Panel type is error,must be :" + nameof(GOptix_BarcodeViewer));
        }



        
        foreach(var v in Owner.Children.OfType<IUAVariable>()){
            //v.BrowseName
            if(v.BrowseName.StartsWith('_')){
                _option_vars.Add(v);
                if(v.BrowseName == "_value"){
                    v.VariableChange += OnBarcodeChange_Handle;
                    VarValue = v;
                }
            }
        }


        var _path = ObjectHelper.GetVariabeleValue<string>(Owner,"RootPath");

        URL = Owner.GetVariable("URL");
        // var url = ResourceUri.FromUri(URL.Value.Value as string);

        var url = ResourceUri.FromProjectRelativePath(_path);
       var _p = url.Uri.Replace("\\","/");
        _p = _p.Replace(" ","%20");
        WebRootPath = _p;
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
        if(VarValue != null){
            VarValue.VariableChange -= OnBarcodeChange_Handle;
        }
    }


    private void OnBarcodeChange_Handle(object sender,VariableChangeEventArgs args){
        Refresh();
        
    }



    [ExportMethod]
    public void Refresh(){
        var options = new List<string>();

        foreach(var v in _option_vars){
            var val = (string)v.Value;
            if(!string.IsNullOrWhiteSpace(val)){
                var key = v.BrowseName.Substring(1);
                var _val = HttpUtility.UrlEncode(val,System.Text.Encoding.UTF8);

                var kp = $"{key}={_val}";
                options.Add(kp);
            }
        }


        var option_string = string.Join('&',options);
        //option_string = HttpUtility.UrlEncode(option_string,System.Text.Encoding.UTF8);

        var url = $"{this.WebRootPath}?{option_string}";
        URL.Value = url;

        #if DEBUG
        Log.Info(this.GetType().Name,url);
        #endif
        }
}
