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
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using Jint.Native;
using FTOptix.System;
#endregion

public class GOptix_PanelLoader_RuntimeNetLogic : BaseNetLogic
{
    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started
        var root = Owner.GetAlias("Panels");
        var obj = LogicObject.GetObject("TreeNode");
        if(root != null && obj != null){

            BuildTreeNode(root,obj);
        }
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
    }

    void BuildTreeNode(IUANode obj,IUANode root,bool clear=true){
        if(clear){
            root.Children.Clear();
        }

     
        foreach(var n in obj.Children){
            if(n.GetType().IsSubclassOf(typeof(ContainerType)) || n.GetType() == typeof(ContainerType)){
                var tvNode = InformationModel.MakeObject<GOptix_Type_TreeNode>(n.BrowseName);
                if(!string.IsNullOrWhiteSpace(n.DisplayName?.Text)){
                    tvNode.Text = n.DisplayName;
                }else{
                    tvNode.Text = new LocalizedText(n.BrowseName,"en-US");
                }

                var _ = n.GetVariable("Icon");
                if(_ != null){
                    tvNode.ImageVariable.Value = _.Value;
                }
                
                tvNode.Tag = n.NodeId;
                
                root.Add(tvNode);
            }else if(n.GetType().IsSubclassOf(typeof(Folder)) || n.GetType() == typeof(Folder)){
                var tvNode = InformationModel.MakeObject<GOptix_Type_TreeNode>(n.BrowseName);
                if(!string.IsNullOrWhiteSpace(n.DisplayName?.Text)){
                    tvNode.Text = n.DisplayName;
                }else{
                    tvNode.Text = new LocalizedText(n.BrowseName,"en-US");
                }
                //tvNode.Tag = n.NodeId;

                var _ = n.GetVariable("Icon");
                if(_ != null){
                    tvNode.ImageVariable.Value = _.Value;
                }
                
                root.Add(tvNode);
                BuildTreeNode(n,tvNode.Nodes,false);
            }
        }

    }
}
