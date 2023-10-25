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
#endregion

public class GOptix_ObjectViewer_RuntimeNetLogic : BaseNetLogic
{
    IUANode model;
    IUAObject treeNode;

    GOptix_TreeView treeView;



    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started
        model = Owner.GetAlias("Object") ;

        treeNode = LogicObject.GetObject("TreeNode");
        treeView = LogicObject.GetAlias("TreeViewer") as GOptix_TreeView;
        if(model != null){
            (model as IUAObject).BuildTreeNode(treeNode);
            var v = treeView.GetVariable("TriggerRefresh");
            v.Value = !(bool)v.Value;
        }

    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
    }


}
