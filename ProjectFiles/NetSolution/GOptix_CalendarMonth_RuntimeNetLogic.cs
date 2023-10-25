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

using GOptixLib.Widget;

public class GOptix_CalendarMonth_RuntimeNetLogic : BaseNetLogic
{

    CalendarViewer calendar;
    IUAVariable varSelectedDateTime;
    IUAVariable varUICurrentDateTimeString;

    private DateTime _currentDatetime;
    private DateTime currentDatetime
    {
        get { return _currentDatetime; }
        set
        {
            _currentDatetime = value;
            if (varUICurrentDateTimeString != null)
            {
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
        calendar = new CalendarViewer(model, dayType, OnDayMouseDown_Handle);

        varUICurrentDateTimeString = Owner.GetVariable("UICurrentDateTimeString");


        var container = LogicObject.GetAlias("Container");
        var idx = 0;
        foreach (var item in container.Children.OfType<Item>())
        {
            var ui = item.Get("VL") as Item;
            calendar.AddWeekViewer(idx, item, ui);
            idx++;
        }

        GoToday();
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
    }


    [ExportMethod]
    public void GoToday()
    {
        var dt = DateTime.Now;
        calendar.BuildUIWithDatetime(dt);
        currentDatetime = dt;
    }


    [ExportMethod]
    public void GoLastMonth()
    {
        var dt = currentDatetime.AddMonths(-1);
        calendar.BuildUIWithDatetime(dt);
        currentDatetime = dt;
    }

    [ExportMethod]
    public void GoNextMonth()
    {
        var dt = currentDatetime.AddMonths(1);
        calendar.BuildUIWithDatetime(dt);
        currentDatetime = dt;
    }


    [ExportMethod]
    public void GoLastYear()
    {
        var dt = currentDatetime.AddYears(-1);
        calendar.BuildUIWithDatetime(dt);
        currentDatetime = dt;
    }

    [ExportMethod]
    public void GoNextYear()
    {
        var dt = currentDatetime.AddYears(1);
        calendar.BuildUIWithDatetime(dt);
        currentDatetime = dt;
    }


    private void OnDayMouseDown_Handle(DateTime dateTime)
    {
        if (varSelectedDateTime != null)
        {
            varSelectedDateTime.Value = dateTime;
            currentDatetime = dateTime;
        }
    }


}
