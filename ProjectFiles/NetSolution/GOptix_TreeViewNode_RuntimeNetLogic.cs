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
using FTOptix.SQLiteStore;
#endregion
using GOptix.Widget;
using FTOptix.OPCUAServer;
using FTOptix.Recipe;
using FTOptix.WebUI;
public class GOptix_TreeViewNode_RuntimeNetLogic : BaseNetLogic
{
    GOptix_Type_TreeNode TreeNode;
    GOptix_TreeView TreeView; 

    IUAVariable Expanded;
    IUAVariable Selected;


    GOptix.Widget.TreeViewNode treeViewNode;

    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started
        TreeNode = Owner.GetAlias(nameof(TreeNode)) as GOptix_Type_TreeNode;
        TreeView = Owner.GetAlias(nameof(TreeView)) as GOptix_TreeView;

        Expanded = Owner.GetVariable(nameof(Expanded));
        Selected = Owner.GetVariable(nameof(Selected));

        treeViewNode = new GOptix.Widget.TreeViewNode(
            TreeView
            ,Owner
            ,LogicObject.GetAlias("Container") as Item);

        treeViewNode.Build(TreeNode);
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
    }

    [ExportMethod]
    public void ClickNode(){
        // if(TreeNode != null){
        //     if(TreeNode.Nodes.Children.Count > 0){
        //         Expanded.Value = !((bool)Expanded.Value);
        //     }        
        // }

        // var v = TreeView.GetVariable("ClickedNode");
        // if(v != null){
        //     v.Value = Owner.NodeId;
        // }

        treeViewNode.OnClick_Handle();
    }



    [ExportMethod]
    public void OnExpand_Handle(){
        treeViewNode.OnExpand_Handle();
    }
}
