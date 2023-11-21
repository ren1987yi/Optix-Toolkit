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
using FTOptix.Report;
#endregion
using GOptixLib;
using System.Collections.Generic;
public class test_ReportWithChart_RuntimeNetLogic : BaseNetLogic
{

    LongRunningTask _task;
    ServerSiderRenderChart client;

    Report rpt;

    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started
         client = new ServerSiderRenderChart(10000000,"http://localhost:18080");
        
         rpt = Project.Current.Get("Reports/ReportsWithChart") as Report;
         rpt.UAEvent += (s,e) => {OnPdfGenerated_Handle();};
        
        _task= new LongRunningTask(Task_GenerateReport,LogicObject);


    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
         var rpt = Project.Current.Get("Reports/ReportsWithChart") as Report;
         rpt.UAEvent -= (s,e) => {OnPdfGenerated_Handle();};

         _task.Dispose();

         
    }

    [ExportMethod]
    public void GenerateReport(){
        _task.Start();
    }

    Random rnd = new Random();
    ResourceUri pdf_out;

    private void OnPdfGenerated_Handle(){
        // args.OutputPath
        var viewer =  LogicObject.GetAlias("PDFViewer") as PdfViewer;
        viewer.Path = "";
        viewer.Path = pdf_out;
        // viewer.Path = args.OutputPath;

    }

    private void Task_GenerateReport(){
        

        var uri = ResourceUri.FromProjectRelativePath("PDFs\\tmp\\qrcode.svg");

        Log.Info(uri.Uri);
        //生成 二维码 svg
        var svg = client.GetQRcodeSvg(DateTime.Now.ToString(),200,200,out_file:uri.Uri);
        Log.Info(svg);

        uri = ResourceUri.FromProjectRelativePath("PDFs\\tmp\\barcode.svg");
        Log.Info(uri.Uri);
        svg = client.GetBarcodeSvg(DateTime.Now.ToString(),out_file:uri.Uri);
        // File.WriteAllText(@"d:\aaa2.svg",svg);
        Log.Info(svg);



         var store = Project.Current.GetObject("DataStores").Get<Store>("LocalDB");
        var logger = Project.Current.GetObject("Loggers/DataLogger1") as DataLogger;


          var st = new DateTime(2023,11,1);
        var et = DateTime.Now;

        var echart_option = EChartTrend.BuildOption(store,logger,st,et,new string[]{"V1","V3","V4"},false);
        uri = ResourceUri.FromProjectRelativePath("PDFs\\tmp\\chart1.svg");
        Log.Info(uri.Uri);
        svg = client.GetEChartSvg(900,540,echart_option,out_file:uri.Uri);
        // File.WriteAllText(@"d:\aaa.svg",svg);
        Log.Info(svg);




        echart_option = @"
 {
  legend: {
    top: 'bottom'
  },
  toolbox: {
    show: true,
    feature: {
      mark: { show: true },
      dataView: { show: true, readOnly: false },
      restore: { show: true },
      saveAsImage: { show: true }
    }
  },
  series: [
    {
      name: 'Nightingale Chart',
      type: 'pie',
      radius: [50, 250],
      center: ['50%', '50%'],
      roseType: 'area',
      itemStyle: {
        borderRadius: 8
      },
      data: [
        {%1%}
      ]
    }
  ]
}";


        var ss = new List<string>();
        for(var i=0;i<rnd.Next(10) + 5;i++){
            ss.Add($"{{ value: {rnd.Next(100)}, name: 'rose {i}' }}");
        }

        echart_option  = echart_option.Replace("{%1%}",string.Join(",",ss));




        uri = ResourceUri.FromProjectRelativePath("PDFs\\tmp\\chart2.svg");
        Log.Info(uri.Uri);
        svg = client.GetEChartSvg(900,540,echart_option,out_file:uri.Uri);
        // File.WriteAllText(@"d:\aaa.svg",svg);
        Log.Info(svg);

        //生成报表
        // var rpt = Project.Current.Get("Reports/ReportsWithChart") as Report;
        
        var pdf_uri = ResourceUri.FromProjectRelativePath("PDFs\\aa.pdf");
        pdf_out = pdf_uri;
        rpt.GeneratePdf(pdf_uri,"",out Guid oid);


     
        

    }



}
