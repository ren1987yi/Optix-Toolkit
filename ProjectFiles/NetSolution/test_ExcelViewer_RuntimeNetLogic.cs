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
using MiniExcelLibs;
using System.Collections.Generic;
using FTOptix.DataLogger;
using FTOptix.SQLiteStore;
using FTOptix.Report;
public class test_ExcelViewer_RuntimeNetLogic : BaseNetLogic
{
    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
    }


    Random rnd = new Random();
    [ExportMethod]
    public void GenerateExcel(){
        var t_path = @"D:\Work\Optix\Optix_Toolkit\ProjectFiles\ExcelSources\Templates\t1.xlsx";
        var out_path = @"D:\Work\Optix\Optix_Toolkit\ProjectFiles\StaticHtml\lib\luckysheet\demoData\3.xlsx";


        // var ss = () =>{
        //     var s = new List<object>();
        //     for(var i=0;i<length;i++){
        //         s.Add(new {name=1,department=i.ToString()} );
        //     }

        //     return s.ToArray();

        // };

            var length = rnd.Next(100);

        var ss = new List<ttt>();
        for(var i=0;i<length;i++){
            ss.Add(new ttt(){name=$"aa{i}bb",department=i} );
        }


        var value = new
        {
            A = "AA",
            B = rnd.Next(999),
            C = 0.11f,
            D = DateTime.Now,
            // employees = new[] {
            //     new {name="Jack",department="HR"},
            //     new {name="Lisa",department="HR"},
            //     new {name="John",department="HR"},
            //     new {name="Mike",department="IT"},
            //     new {name="Neo",department="IT"},
            //     new {name="Loan",department="IT"}
                
            // }
            employees = ss,
        };
        MiniExcel.SaveAsByTemplate(out_path,t_path,value);
    }


    class ttt{
        public string name { get; set; }
        public int department { get; set; }
    }
}
