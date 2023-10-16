

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



public class GOptix_MML_Viewer_RuntimeNetLogic : BaseNetLogic
{
    LayoutViewer2D layoutViewer;
    IUAVariable varTrackFile;


    IUANode VehicleModel;


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
        layoutViewer = new LayoutViewer2D(container,config,LogicObject);

        var filepath = "D:\\Work\\Projects\\PLC\\MML\\optix_test\\slovt.mmtrk";
        layoutViewer.BuildTrack(filepath,VehicleModel);

    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
        layoutViewer?.Dispose();
    }

    public void BuildTrack(){

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
