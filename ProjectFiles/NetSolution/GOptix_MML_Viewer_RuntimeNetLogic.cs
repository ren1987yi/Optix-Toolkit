

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
using FTOptix.CoreBase;
using FTOptix.Store;
using FTOptix.Core;
#endregion

using GOptixLib.MagneMotionLite;
using FTOptix.Recipe;
using FTOptix.SQLiteStore;
using FTOptix.WebUI;
using FTOptix.OPCUAServer;
using FTOptix.System;
using FTOptix.Report;
using Microsoft.Extensions.Configuration;



public class GOptix_MML_Viewer_RuntimeNetLogic : BaseNetLogic
{
    LayoutViewer2D layoutViewer;
    IUAVariable varTrackFile;


    IUANode VehicleModel;

    string _trackFilePath;
    LongRunningTask _taskBuildTrack;
    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started
        var container = LogicObject.GetAlias("Container") as Item;

        varTrackFile = Owner.GetVariable("TrackFile");

        VehicleModel = Owner.GetAlias("VehicleModel");


        var config = new LayoutViewer2DConfiguration(){
            Motor250mmTypeId= Owner.GetAlias("Motor250mmType").NodeId,
            Motor1000mmTypeId= Owner.GetAlias("Motor1000mmType").NodeId,
            MotorCurveLeftTypeId= Owner.GetAlias("MotorCurveLeftType").NodeId,
            MotorCurveRightTypeId= Owner.GetAlias("MotorCurveRightType").NodeId,
            MotorSwitchLeftTypeId= Owner.GetAlias("MotorSwitchLeftType").NodeId,
            MotorSwitchRightTypeId= Owner.GetAlias("MotorSwitchRightType").NodeId,
            VehicleTypeId= Owner.GetAlias("VehicleType").NodeId,
        };

        // var filepath = "D:\\Work\\Projects\\PLC\\MML\\optix_test\\t1.mmlopt";
        // var filepath = "D:\\Work\\Projects\\PLC\\MML\\optix_test\\slovt2.mmlopt";
        // var filepath = "D:\\aaaaaa.bin";


        var pullingTime = 100;
        var v = Owner.GetVariable("StatePullingTime");
        if(v != null){
            pullingTime = (int)v.Value;
        }
        layoutViewer = new LayoutViewer2D(container,config,LogicObject,pullingTime);

        _taskBuildTrack = new LongRunningTask(TaskBuildTrack,LogicObject);
        _trackFilePath = (string)varTrackFile.Value;
        _taskBuildTrack.Start();

        

    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
        layoutViewer?.Dispose();
    }




    [ExportMethod]
    public void BuildLayout(){
         _trackFilePath = (string)varTrackFile.Value;
        _taskBuildTrack.Start();
    }


    public void TaskBuildTrack(){
        layoutViewer.BuildTrack(_trackFilePath,VehicleModel,null);
    }

    [ExportMethod]
    public void AddVehicle(){
        layoutViewer.AddVehicle();
    }

    [ExportMethod]
    public void SetVehicleLocation(int vehicleNo,int pathId,float position){
        layoutViewer.MoveVehicle(vehicleNo,pathId,position);
    }

    
}
