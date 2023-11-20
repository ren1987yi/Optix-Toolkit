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
using System.Collections.Generic;
using FTOptix.DataLogger;
using FTOptix.SQLiteStore;
public class RuntimeNetLogic2 : BaseNetLogic
{
     IUAVariable varBlob;

    
    string template;
    PeriodicTask _task;
    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started

        varBlob =  LogicObject.GetAlias("VV1") as IUAVariable;

       

        var a = @"
        {
          series: [{
          data: [(%1%)]
        }],
          chart: {
          type: 'bar',
          height: 350
        },
        plotOptions: {
          bar: {
            borderRadius: 4,
            horizontal: true,
          }
        },
        dataLabels: {
          enabled: false
        },
        xaxis: {
          categories: ['South Korea', 'Canada', 'United Kingdom', 'Netherlands', 'Italy', 'France', 'Japan',
            'United States', 'China', 'Germany'
          ],
        }
        }
        ";

        template = a;

        Log.Info(a);


        _task = new PeriodicTask(OnTask,5000,LogicObject);
        _task.Start();

    }



    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
        _task.Cancel();
        _task.Dispose();
        _task = null;
    }

    Random rnd  = new Random();
    public void OnTask(){
        var vals = new List<string>();
        for(var i=0;i<10;i++){
            vals.Add(rnd.Next(100).ToString());
        }

        var _val = string.Join(",",vals);
        var _blob = template.Replace("(%1%)",_val);
        var blob = GOptixLib.Utils.Encode.Base64Encrypt(_blob);
        varBlob.Value = blob;
    }

     
}
