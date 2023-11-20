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
using FTOptix.Report;
using FTOptix.OPCUAServer;
using FTOptix.RAEtherNetIP;
using FTOptix.Retentivity;
using FTOptix.Alarm;
using FTOptix.CommunicationDriver;
using FTOptix.CoreBase;
using FTOptix.Store;
using FTOptix.Core;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
#endregion

using GOptixLib.Widget;
using FTOptix.DataLogger;
using FTOptix.SQLiteStore;
public class GOptix_GridCollection_RuntimeNetLogic : BaseNetLogic
{
    IUANode Model;
    IUANode UIType;
    IUAVariable RowOffset;
    IUAVariable RowHeight;
    IUAVariable ColumnOffset;
    IUAVariable ColumnCount;
    IUAVariable FillMode;
    Item Container;


    GridLayout gridLayout;

    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started
        Model = Owner.GetAlias(nameof(Model));
        UIType = Owner.GetAlias(nameof(UIType));
        RowOffset = Owner.GetVariable(nameof(RowOffset));
        RowHeight = Owner.GetVariable(nameof(RowHeight));
        ColumnOffset = Owner.GetVariable(nameof(ColumnOffset));
        ColumnCount = Owner.GetVariable(nameof(ColumnCount));
        FillMode = Owner.GetVariable(nameof(FillMode));

        Container = LogicObject.GetAlias(nameof(Container)) as Item;


        gridLayout = new GridLayout(
            new LayoutConfigure()
            {
                FillMode = FillMode.Value,
                RowOffset = RowOffset.Value,
                RowHeight = RowHeight.Value,
                ColOffset = ColumnOffset.Value,
                ColCount = ColumnCount.Value
            }
            , Container
            , Model
            , UIType
            , LogicObject


        );



        BuildUI();
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
        gridLayout.Dispose();
    }

    private void BuildUI()
    {
        gridLayout.BuildUI();
    }



}
