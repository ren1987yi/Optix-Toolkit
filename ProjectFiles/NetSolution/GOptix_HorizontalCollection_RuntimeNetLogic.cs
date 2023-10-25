
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
#endregion

using System.Collections.Generic;
using System.Linq;
using GOptixLib;
using FTOptix.System;
public class GOptix_HorizontalCollection_RuntimeNetLogic : BaseNetLogic
{
   /// <summary>
    /// 数据模型
    /// </summary>
    IUANode Model;

    /// <summary>
    /// 呈现的UI
    /// </summary>
    IUANode UIType;

    /// <summary>
    /// 放UI的容器
    /// </summary> 
    Item Container;

    /// <summary>
    /// 行偏移 bottom margin
    /// </summary>
    IUAVariable ColumnOffset;

    /// <summary>
    /// 数据模型 观察者
    /// </summary>
    ModelEventObserver observer;

    /// <summary>
    /// 模型和UI的字典
    /// </summary> 
    Dictionary<IUANode, IUANode> mapModelUI;

    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started
        Model = Owner.GetAlias(nameof(Model));
        UIType = Owner.GetAlias(nameof(UIType));
        Container = Owner as Item;
        if (Container == null)
        {
            Log.Error("Owner must be a UI Type");
            return;
        }

        ColumnOffset = Owner.GetVariable(nameof(ColumnOffset));

        mapModelUI = new Dictionary<IUANode, IUANode>();



        observer = Model.RegisterAddAndRemoveObserver(
                LogicObject
                , ModelObserverOnAdded_Handle
                , ModelObserverOnRemoved_Handle
        );

        BuildUI();

    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped

        observer?.Dispose();
        observer = null;

    }

    private void BuildUI()
    {
        var offset = 0;
        if (this.ColumnOffset != null)
        {
            offset = (int)ColumnOffset.Value;
        }
        foreach (var ui in Container.Children.OfType<Item>())
        {
            ui.Delete();
        }

        foreach (var model in Model.Children)
        {
            var ui = InformationModel.MakeObject(model.BrowseName, UIType.NodeId) as Item;
            if (ui == null)
            {
                break;
            }
            foreach (var child in ui.Children)
            {
                if (child.GetType() == typeof(Alias))
                {
                    ui.SetAlias(child.BrowseName, model);
                    break;
                }
            }
            ui.RightMargin = offset;

            Container.Add(ui);

            mapModelUI.Add(model, ui);
        }
    }

    public void ModelObserverOnAdded_Handle(IUANode sourceNode, IUANode targetNode, NodeId referenceTypeId, ulong senderId)
    {
        var model = targetNode;
        var ui = InformationModel.MakeObject(model.BrowseName, UIType.NodeId);

        foreach (var child in ui.Children)
        {
            if (child.GetType() == typeof(Alias))
            {
                ui.SetAlias(child.BrowseName, model);
                break;
            }
        }

        Container.Add(ui);
        mapModelUI.Add(model, ui);
    }

    public void ModelObserverOnRemoved_Handle(IUANode sourceNode, IUANode targetNode, NodeId referenceTypeId, ulong senderId)
    {
        var model = targetNode;
        if (mapModelUI.TryGetValue(model, out var ui))
        {
            ui.Delete();
            mapModelUI.Remove(model);
        }
        //mapModelUI.Add(model,ui);
    }


}
