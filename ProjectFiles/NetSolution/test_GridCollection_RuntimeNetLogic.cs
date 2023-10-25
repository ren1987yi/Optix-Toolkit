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
using FTOptix.Report;
using FTOptix.OPCUAServer;
using FTOptix.RAEtherNetIP;
using FTOptix.Retentivity;
using FTOptix.CoreBase;
using FTOptix.CommunicationDriver;
using FTOptix.Store;
using FTOptix.Core;
using System.Linq;
using FTOptix.System;
#endregion

public class test_GridCollection_RuntimeNetLogic : BaseNetLogic
{
    IUANode _object;
    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started
        _object = Owner.Get("Object1");
        if(_object == null){
            throw new Exception("error object1 not exists");
        }
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
    }



    Random rnd = new Random();
    [ExportMethod]
    public void AddObject(){
        var v = InformationModel.MakeVariable(rnd.NextDouble().ToString(),OpcUa.DataTypes.Int32);
        _object.Add(v);
    }
    [ExportMethod]
    public void DelObject(){
        
        var v = _object.Children.LastOrDefault();
        if(v != null){
            v.Delete();
        }
    }
}
