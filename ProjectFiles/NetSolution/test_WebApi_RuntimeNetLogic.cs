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
using FTOptix.DataLogger;
using FTOptix.SQLiteStore;
using FTOptix.Store;
using FTOptix.OPCUAServer;
using FTOptix.RAEtherNetIP;
using FTOptix.Retentivity;
using FTOptix.CoreBase;
using FTOptix.CommunicationDriver;
using FTOptix.Core;
#endregion
using GOptixLib;
using FTOptix.Report;
public class test_WebApi_RuntimeNetLogic : BaseNetLogic
{
    ServerSiderRenderChart client;
    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started
        client = new ServerSiderRenderChart(10000000,"http://localhost:18080");
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
    }


    [ExportMethod]
    public void T1(){
        // var url = "http://localhost:18080/echart";
        var body = @"{
			animation:false,
			xAxis: {
				type: 'category',
				data: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun']
			},
			yAxis: {
				type: 'value'
			},
			series: [
				{
					data: [1150, 230, 224, 218, 135, 147, 260],
					type: 'line'
				}
			]

		}";
        
        var response = client.GetEChartSvg(1000,800,body);
        // client.Post(url,body,"","",out var response,out int code);
        Log.Info(response);


        // url = "http://localhost:18080/barcode";
        response = client.GetBarcodeSvg(DateTime.Now.ToString());
        Log.Info(response);

        // url = "http://localhost:18080/qrcode";
        response = client.GetQRcodeSvg(DateTime.Now.ToString());
        Log.Info(response);

        
    }
}
