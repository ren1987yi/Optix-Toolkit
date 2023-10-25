#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.EventLogger;
using FTOptix.HMIProject;
using FTOptix.NetLogic;
using FTOptix.NativeUI;
using FTOptix.UI;
using FTOptix.WebUI;
using FTOptix.Report;
using FTOptix.OPCUAServer;
using FTOptix.RAEtherNetIP;
using FTOptix.Retentivity;
using FTOptix.Alarm;
using FTOptix.CommunicationDriver;
using FTOptix.CoreBase;
using FTOptix.Store;
using FTOptix.Core;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Buffers.Text;
using FTOptix.System;
#endregion

public class RuntimeNetLogic1 : BaseNetLogic
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
	xAxis: {
	  type: 'category',
	  data: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun']
	},
	yAxis: {
	  type: 'value'
	},
	series: [
	  {
		data: [(%1%)],
		type: 'bar',
		showBackground: true,
		color:'red',
		backgroundStyle: {
		  color: 'rgba(180, 180, 180, 0.2)'
		}
	  }
	]
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
