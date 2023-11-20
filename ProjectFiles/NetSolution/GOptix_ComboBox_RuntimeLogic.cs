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
#endregion

public class GOptix_ComboBox_RuntimeLogic : BaseNetLogic
{
    UADataType EnumType;
    IUANode Options;
    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started
        EnumType = Owner.GetAlias(nameof(EnumType)) as UADataType;
        Options = LogicObject.Get(nameof(Options));



        if(EnumType != null){
            if(!EnumType.EnumDefinition.IsEmpty){
                foreach(var field in EnumType.EnumDefinition.Fields){
                    var v = InformationModel.MakeVariable(field.Name,OpcUa.DataTypes.Int32);
                    v.Value = field.Value;
                    v.DisplayName = field.DisplayName;
                    v.Description = field.Description;
                    Options.Add(v);
                }
            }
        }
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
    }
}
