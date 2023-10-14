#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.EventLogger;
using FTOptix.HMIProject;
using FTOptix.NetLogic;
using FTOptix.NativeUI;
using FTOptix.UI;
using FTOptix.RAEtherNetIP;
using FTOptix.Retentivity;
using FTOptix.Alarm;
using FTOptix.CommunicationDriver;
using FTOptix.Store;
using FTOptix.CoreBase;
using FTOptix.Core;
#endregion

using System.Collections.Generic;
using System.Linq;
using FTOptix.SQLiteStore;
using FTOptix.OPCUAServer;
using FTOptix.Recipe;

public class GOptix_RandomVariable_RuntimeLogic : BaseNetLogic
{
   List<SimVariable> vars = new List<SimVariable>();

    PeriodicTask _task;
    System.Random random = new System.Random();
    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started
        foreach(var child in Owner.Children.OfType<IUAVariable>()){
            var range = child.Children.OfType<FTOptix.Core.Range>().FirstOrDefault();
            _Range_ r ;
            if(range == null){
                //range = InformationModel.MakeObject<FTOptix.Core.Range>();
                r = new _Range_();
                r.High = 100;
                r.Low = 0;

                
                //child.Add(range);
            }else{
                r = new _Range_();
                r.High = range.High;
                r.Low = range.Low;

                
            }

            vars.Add(new SimVariable(){Var = child,Range = r});
        }


        var v = LogicObject.GetVariable("Period");
        var period = (int)v.Value;

        _task = new PeriodicTask(TaskPeriod,period,LogicObject);

        _task.Start();

    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
        _task.Cancel();
        _task.Dispose();
    }

    private void TaskPeriod(){
        foreach(var child in vars){
            
            var v = random.NextSingle(); 
            var vvv = v * (child.Range.High - child.Range.Low)  + child.Range.Low;
            child.Var.Value = vvv;
        }
    }


    class SimVariable{
        public IUAVariable Var { get; set; }
        public _Range_ Range { get; set; }
    }

    class _Range_{
        public double High { get; set; }
        public double Low { get; set; }
    }
}
