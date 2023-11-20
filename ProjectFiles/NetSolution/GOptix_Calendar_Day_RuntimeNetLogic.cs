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
using FTOptix.OPCUAServer;
using FTOptix.System;
using FTOptix.Report;
using FTOptix.DataLogger;
using FTOptix.SQLiteStore;
#endregion

public class GOptix_Calendar_Day_RuntimeNetLogic : BaseNetLogic
{
    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started
        var v = Owner.GetVariable("LocalTime");
        var now = (DateTime)v.Value;

        v = Owner.GetVariable("DatetimeFormatString");
        var format = (string)v.Value;

        v = Owner.GetVariable("UILocalTime");
        if (!string.IsNullOrWhiteSpace(format))
        {
            v.Value = string.Format(now.ToString(format));
        }


    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
    }
}
