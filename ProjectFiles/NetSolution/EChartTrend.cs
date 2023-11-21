#region Using directives
using System;
using UAManagedCore;
using FTOptix.DataLogger;

#endregion
using Store = FTOptix.Store;

using System.Collections.Generic;
using System.IO;

using OptixHelper;

using System.Linq;



    public class EChartTrend
    {
        const string EChart_OPTION_Template = @"{
        xAxis: {
            type: 'category',
            data: [(%Xs%)]
        },
        yAxis: {
            type: 'value'
        }, 
       
           
        legend: {
            show:true
        },
       
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
        public static string BuildOption(Store.Store store, DataLogger logger,DateTime start_time,DateTime end_time,IEnumerable<string> chs,bool isBase64=true)
        {
            var myStore = store;
            var tbName = string.IsNullOrWhiteSpace(logger.TableName) ? logger.BrowseName : logger.TableName;

            var st = (DateTime)start_time;
            var et = (DateTime)end_time;

            var channels = chs.ToList();

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

            if(isBase64){

                var blob = GOptixLib.Utils.Encode.Base64Encrypt(option);
                return blob;
            }else{
                return option;
            }
        }

        public static void SaveSSROption(Store.Store store, DataLogger logger,DateTime start_time,DateTime end_time,IEnumerable<string> chs,string output_file){
            var option = BuildOption(store,logger,start_time,end_time,chs,false);

            File.WriteAllText(output_file,option);
        }
    }
