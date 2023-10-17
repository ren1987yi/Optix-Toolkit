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
using FTOptix.RAEtherNetIP;
using FTOptix.Retentivity;
using FTOptix.Alarm;
using FTOptix.CommunicationDriver;
using FTOptix.CoreBase;
using FTOptix.Store;
using FTOptix.Core;
using System.Collections.Generic;
using System.Linq;
#endregion
using GOptixLib.Utils;
using System.Reflection;
using FTOptix.OPCUAServer;
public class GOptix_CalendarMonth_RuntimeNetLogic : BaseNetLogic
{

    CalendarViewer  calendar;
    IUAVariable varSelectedDateTime;
    IUAVariable varUICurrentDateTimeString;

    private DateTime _currentDatetime;
    private DateTime currentDatetime
    {
        get { return _currentDatetime; }
        set { 
            _currentDatetime = value; 
            if(varUICurrentDateTimeString != null){
                varUICurrentDateTimeString.Value = $"{value.Year} / {value.Month}";
            }
        }
    }
    


    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started

        var model = Owner.GetAlias("Model");
        var dayType = Owner.GetAlias("UIDayType")?.NodeId;
        varSelectedDateTime = Owner.GetVariable("SelectedDateTime");
        calendar = new CalendarViewer(model,dayType,OnDayMouseDown_Handle);

        varUICurrentDateTimeString = Owner.GetVariable("UICurrentDateTimeString");


        var container = LogicObject.GetAlias("Container");
        var idx = 0;
        foreach(var item in container.Children.OfType<Item>()){
            var ui = item.Get("VL") as Item;
            calendar.AddWeekViewer(idx,item,ui);
            idx++;
        }

        GoToday();
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
    }


    [ExportMethod]
    public void GoToday(){
        var dt = DateTime.Now;
        calendar.BuildUIWithDatetime(dt);
        currentDatetime = dt;
    }


    [ExportMethod]
    public void GoLastMonth(){
        var dt = currentDatetime.AddMonths(-1);
        calendar.BuildUIWithDatetime(dt);
        currentDatetime = dt;
    }

    [ExportMethod]
    public void GoNextMonth(){
        var dt = currentDatetime.AddMonths(1);
        calendar.BuildUIWithDatetime(dt);
        currentDatetime = dt;
    }


    [ExportMethod]
    public void GoLastYear(){
        var dt = currentDatetime.AddYears(-1);
        calendar.BuildUIWithDatetime(dt);
        currentDatetime = dt;
    }

    [ExportMethod]
    public void GoNextYear(){
        var dt = currentDatetime.AddYears(1);
        calendar.BuildUIWithDatetime(dt);
        currentDatetime = dt;
    }


    private void OnDayMouseDown_Handle(DateTime dateTime){
        if(varSelectedDateTime != null){
            varSelectedDateTime.Value = dateTime;
            currentDatetime = dateTime;
        }
    }


    class CalendarViewer{
        private List<WeekViewer> _weeks = new List<WeekViewer>();
        public List<WeekViewer> Weeks
        {
            get { return _weeks; }
            set { _weeks = value; }
        }
        
        readonly IUANode _model; //主数据模型
        readonly NodeId _dayTypeId; //天的ui typeid
 
        readonly Action<DateTime> _callback_SelectedDatetime;


        Dictionary<Panel,EventHandler<MouseDownEvent>> _onMousedown_handles = new Dictionary<Panel, EventHandler<MouseDownEvent>>();


        public CalendarViewer(IUANode model,NodeId dayTypeId,Action<DateTime> callback_SelectedDatetime){
            _model = model;
            _dayTypeId = dayTypeId;
            _callback_SelectedDatetime = callback_SelectedDatetime;
           
        }


        public void AddWeekViewer(int week,Item master,Item ui){
            _weeks.Add( new WeekViewer(week,master,ui));
        }

        public void ShowWeekChannel(int weekday=-1){
            if(weekday < 0){
                foreach(var _week in _weeks){
                    _week.MasterContainer.Visible = true;
                }
            }else{
                var week = _weeks.Where(w=>w.Weekday == weekday).FirstOrDefault();
                if(week != null){
                    week.MasterContainer.Visible = true;
                }

            }
        }

        


        public void BuildUIWithDatetime(DateTime dt){
            ClearUI();


            var month_first_day = DateTimeHelper.GetMonthFirstDay(dt); //一个月中第一天
            var month_first_day_week = month_first_day.DayOfWeek; //第一天的星期数


            var before_month_first_day = month_first_day.AddDays((int)month_first_day_week*-1);

            var month_last_day = DateTimeHelper.GetMonthLastDay(dt);
            var month_last_day_week = month_last_day.DayOfWeek;
            var after_month_last_day = month_last_day.AddDays(6-(int)month_last_day_week);

            //DayOfWeek a;

            var dt_start = before_month_first_day;
            var dt_end = after_month_last_day;
            var diff_day = (after_month_last_day - before_month_first_day).Days + 1; //天数差


            Dictionary<int,List<DateTime>> _mapper = new Dictionary<int, List<DateTime>>();

            foreach(var weekday in _weeks.Select(w=>w.Weekday)){
                _mapper.Add(weekday,new List<DateTime>());
            }

            for(var i=0;i<diff_day;i++){
                var d = dt_start.AddDays(i);
                var week = ((int)d.DayOfWeek);
                _mapper[week].Add(d);
            }

            foreach(var kv in _mapper){
                var week = kv.Key;
                var viewer = _weeks.Where(w=>w.Weekday == week).FirstOrDefault();
                if(viewer != null){
                    var uis = viewer.AddDays(month_first_day, kv.Value,_dayTypeId,_model);
                    for(var i=0;i<kv.Value.Count;i++){
                        var dateTime = kv.Value[i];
                        var ui = uis[i] as Panel;
                        if(ui != null){
                            EventHandler<MouseDownEvent> aa = (s,e) => {_callback_SelectedDatetime(dateTime);};
                            ui.OnMouseDown += aa;
                            _onMousedown_handles.Add(ui,aa);
                        }
                    }
                }
            }


        }


        public void ClearUI(){

            foreach(var kv in _onMousedown_handles){
                kv.Key.OnMouseDown -= kv.Value;
            }
            _onMousedown_handles.Clear();


            foreach(var week in Weeks){
                week.Clear();
            }
        }

        private void OnDayMouseDown_Handle(object sender,MouseDownEvent args){

        }



    }


    class WeekViewer{
        public int Weekday{get; private set;}
        public Item MasterContainer{get;private set;}
        public Item UIContainer{get;private set;}




        public WeekViewer(int weekday,Item master,Item ui){
            Weekday = weekday;
            MasterContainer = master;
            UIContainer = ui;

            Clear();
        }


        public void Clear(){
            


            foreach(var item in  UIContainer.Children.OfType<Item>()){
             
                item.Delete();
            }
        }

   
        /// <summary>
        /// 
        /// </summary>
        /// <param name="curMonth">当前月</param>
        /// <param name="days"></param>
        /// <param name="uiTypeId"></param>
        /// <param name="model"></param>
        /// <returns></returns> 
        
        public List<Item> AddDays(DateTime curMonth,IEnumerable<DateTime> days,NodeId uiTypeId,IUANode model){
            List<Item> uis = new List<Item>();
            foreach(var day in days){
                var ui = BuildUI(day,uiTypeId,model);
                if(day.Month != curMonth.Month || day.Year != curMonth.Year){
                    ui.Enabled = false;
                }
               
                UIContainer.Add(ui);
                uis.Add(ui);
            }

            return uis;

        }




        Item BuildUI(DateTime day,NodeId uiTypeId,IUANode model){
            
            var ui = InformationModel.MakeObject(day.ToString(),uiTypeId) as Item;
            ui.HorizontalAlignment = HorizontalAlignment.Stretch;
            ui.VerticalAlignment = VerticalAlignment.Stretch;
            if(model != null){

                ui.SetAlias("Model",model);
            }
            var v = ui.GetVariable("UTCTime");
            if(v != null){
                v.Value = day;
                // Log.Info(day.ToString());
            }
            v = ui.GetVariable("LocalTime");
            if(v != null){
                v.Value = day.ToLocalTime();
                Log.Info(day.ToString());
            }
             


            return ui;
        }
    }
}
