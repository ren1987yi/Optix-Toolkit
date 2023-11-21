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
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Collections.Generic;
using OptixHelper;
using FTOptix.Report;



#endregion
using GOptixLib;
public class GOptix_HistoryTrend_RuntimeNetLogic : BaseNetLogic
{
    Store store;
    DataLogger logger;
    Item ChannelContainer;

    IUAVariable varBlob;

    IUAVariable QueryStartTime;
    IUAVariable QueryEndTime;

    List<ChannelMapper> chMapper;

    LongRunningTask _task_export;


    Report expReport;
DialogType dialogType;
    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started
        store = Owner.GetAlias("Store") as Store;
        logger = Owner.GetAlias("Logger") as DataLogger;

        dialogType = Owner.GetAlias("DlgPdf") as DialogType;

        ChannelContainer = LogicObject.GetAlias(nameof(ChannelContainer)) as Item;

        varBlob = LogicObject.GetAlias("Blob") as IUAVariable;


        QueryStartTime = LogicObject.GetVariable("QueryStartTime") as IUAVariable;
        QueryEndTime = LogicObject.GetVariable("QueryEndTime") as IUAVariable;
        _task_export = new LongRunningTask(Task_Export,LogicObject);

        chMapper = new List<ChannelMapper>();


        expReport = Project.Current.Get("Reports/ReportTrend") as Report;
        expReport.UAEvent += (s,e) => {OnPdfGenerated_Handle();};


        BuildChannelUI();
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped

        expReport.UAEvent -= (s,e) => {OnPdfGenerated_Handle();};
        _task_export.Dispose();
    }


    private void BuildChannelUI()
    {
        foreach (var ui in ChannelContainer.Children.OfType<Item>())
        {
            ui.Delete();
        }
        chMapper.Clear();

        foreach (var v in logger.VariablesToLog)
        {
            var ui = InformationModel.Make<CheckBox>(v.BrowseName);
            ui.Checked = false;
            ui.TopMargin = 10;
            ui.BottomMargin = 10;
            ui.Text = v.BrowseName;
            ChannelContainer.Add(ui);
            chMapper.Add(new ChannelMapper() { UI = ui, ChannelName = v.BrowseName });
        }
    }


    [ExportMethod]
    public void Render()
    {
        //   var project = FTOptix.HMIProject.Project.Current;
        // var myStore = project.GetObject("DataStores").Get<Store>("LocalDB");


        var myStore = this.store;
        var tbName = string.IsNullOrWhiteSpace(this.logger.TableName) ? this.logger.BrowseName : this.logger.TableName;

        var st = (DateTime)QueryStartTime.Value.Value;
        var et = (DateTime)QueryEndTime.Value.Value;

        var channels = chMapper.Where(c => c.UI.Checked == true).Select(cc => cc.ChannelName).ToList();

        var _chs = string.Join(",", channels);




        var _st = st.ToString("yyyy-MM-ddTHH:mm:ss");
        var _et = et.ToString("yyyy-MM-ddTHH:mm:ss");
        var sql = $"SELECT LocalTimestamp as RecordTime,{_chs} FROM {tbName} where LocalTimestamp >= '{_st}' AND LocalTimestamp <= '{_et}' ";
        // myStore.Query("SELECT LocalTimestamp,V1,V2,V3 FROM DataLogger1 where LocalTimestamp >= '2023-11-17T14:00:00' AND LocalTimestamp <= '2023-11-17T14:30:00' ", out header, out resultSet);
        Log.Info(sql);
        var res = StoreHelpr.Query(myStore, sql);

        var xdata = new List<string>();

        var Series = new List<Serie>();
        foreach (var ch in channels)
        {

            Series.Add(new Serie() { Name = ch });

        }

        foreach (var row in res)
        {
            // xdata.Add("'" + ((DateTime)row["RecordTime"]).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            xdata.Add("'" + row["RecordTime"] + "'");


            for (var i = 0; i < channels.Count; i++)
            {
                var ch = channels[i];
                Series[i].Data.Add(row[ch]);
                // Series[1].Data.Add(row["V3"]);
                // Series[2].Data.Add(row["V4"]);
            }

        }

        var option_1 = EChart_OPTION_Template.Replace("(%Xs%)", string.Join(",", xdata));

        var _ss = new List<string>();
        foreach (var s in Series)
        {
            _ss.Add(s.ToString());
        }


        var option = option_1.Replace("(%Ys%)", string.Join(",", _ss));
        Log.Info(option);

        var blob = GOptixLib.Utils.Encode.Base64Encrypt(option);
        varBlob.Value = blob;
    }



    [ExportMethod]
    public void Export(){
      _task_export.Start();
    }

    ResourceUri pdf_out;
    private void OnPdfGenerated_Handle(){
      Item a = Owner as Item;

      a.OpenDialog(dialogType);
      
      
    }


    private void Task_Export(){
      var st = (DateTime)QueryStartTime.Value.Value;
      var et = (DateTime)QueryEndTime.Value.Value;

     var channels = chMapper.Where(c => c.UI.Checked == true).Select(cc => cc.ChannelName).ToList();

      var echart_option = EChartTrend.BuildOption(store,logger,st,et,channels,false);
      var uri = ResourceUri.FromProjectRelativePath("PDFs\\tmp\\t1.svg");
      Log.Info(uri.Uri);
      var client = new ServerSiderRenderChart(10000000,"http://localhost:18080");
      var svg = client.GetEChartSvg(900,540,echart_option,out_file:uri.Uri);


      var rpt = Project.Current.Get("Reports/ReportTrend") as Report;
      var pdf_uri = ResourceUri.FromProjectRelativePath("PDFs\\trend.pdf");
      pdf_out = pdf_uri;
      rpt.GeneratePdf(pdf_uri,"",out Guid oid);
    }





    const string EChart_OPTION_Template = @"{
  xAxis: {
    type: 'category',
    data: [(%Xs%)]
  },
  yAxis: {
    type: 'value'
  }, 
   toolbox: {
    feature: {
      dataZoom: {
        yAxisIndex: 'none'
      },
      restore: {}
    }
  },
    tooltip: {
    trigger: 'axis'
  },
 legend: {
  show:true
  },
  dataZoom: [
    {
      type: 'inside',
      start: 0,
      end: 20
    },
    {
      start: 0,
      end: 20
    }
  ],
  series: [
    (%Ys%)
  ]
}";




    class ChannelMapper
    {
        public FTOptix.UI.CheckBox UI { get; set; }
        public string ChannelName { get; set; }

    }

    class Serie
    {
        static string EChart_SERIES_TEMPLATE = @"
    {
      data:[(%Data%)],
      name:'(%Name%)',
      type: 'line',
      smooth: true
    }";
        public string Name { get; set; }

        private List<object> _data = new List<object>();
        public List<object> Data
        {
            get { return _data; }
            set { _data = value; }
        }

        public override string ToString()
        {
            var _op1 = EChart_SERIES_TEMPLATE.Replace("(%Name%)", Name);


            return _op1.Replace("(%Data%)", string.Join(",", _data.Select(t => t.ToString())));


        }
    }
}
