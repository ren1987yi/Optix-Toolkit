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
using System.Linq;
#endregion
using System.Collections.Generic;
using GOptix;
using GOptix.Widget;
using FTOptix.OPCUAServer;
using FTOptix.Recipe;
using FTOptix.SQLiteStore;
using FTOptix.WebUI;





public class GOptix_TreeView_RuntimeNetLogic : BaseNetLogic
{
    TreeView treeView;
    Item Container;
    IUANode Model;



    // NodeObserver nodeObserver;
    // CCObserver observer;
    // uint affinityId;

    public override void Start(){
        Model = Owner.GetAlias(nameof(Model));
        Container = LogicObject.GetAlias("Container") as Item;
        treeView = new TreeView(Container,Owner);
        
        if(1==2){

            // var context = LogicObject.Context;
            // affinityId = context.AssignAffinityId();
            // observer = new CCObserver(()=>{Refresh();});
            // nodeObserver = new NodeObserver(Model,observer,affinityId);
        }
        
        
        Refresh();





    }

    public override void Stop(){
        // nodeObserver?.Dispose();
    }


   

    [ExportMethod]
    public void Refresh(){
        treeView.Build(Model);
    }

    //  public class CCObserver : IEventObserver{
    

    //     public Action _callback;
    //     public CCObserver(Action callbadk){
    //         _callback = callbadk;
    //     }
    //     // Action<IUANode,IUANode,NodeId,ulong> callbackHandler;
    //     // public TreeNodeObserver(Action<IUANode,IUANode,NodeId,ulong> callback){
    //     //     callbackHandler = callback;
    //     // }
    //     public void OnReferenceAdded(IUANode sourceNode, IUANode targetNode, NodeId referenceTypeId, ulong senderId)
    //     {
    //        //callbackHandler(sourceNode,targetNode,referenceTypeId,senderId);
    //        Log.Info("add");
    //     }

    //     public void OnReferenceChanged(){

    //     }

    //     public void OnReferenceRemoved(IUANode sourceNode, IUANode targetNode, NodeId referenceTypeId, ulong senderId)
    //     {
    //        //callbackHandler(sourceNode,targetNode,referenceTypeId,senderId);
    //       Log.Info("move");
    //       _callback.Invoke();
    //     }

    // }



}


/*
public class GOptix_TreeView_RuntimeNetLogic : BaseNetLogic
{
    uint affinityId=0;

    IUANode Model;

    IEventRegistration treenodeRegistration;
    TreeNodeObserver treenodeObserver;

    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started
         var context = LogicObject.Context;

        affinityId = context.AssignAffinityId();




        Model = Owner.GetAlias(nameof(Model));
        if(Model != null){
            TreeNodeObserver.Register(Model,affinityId,
            (sn,tn,r,s) => {OnTreeNodeModelChange_Handle(sn,tn,r,s);}
            ,out treenodeRegistration
            ,out treenodeObserver
            );
        }
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
        treenodeRegistration?.Dispose();
        treenodeRegistration = null;

        treenodeObserver = null;
    }

    [ExportMethod]
    public void OnClickNode_Handle(NodeId nodeId){
        var vv = Owner.GetVariable("ClickedNode");
        nodeId = (NodeId)vv.Value;
        var item = InformationModel.GetObject(nodeId);
    }

    [ExportMethod]
    public void Refresh(){

    }


    private void OnTreeNodeModelChange_Handle(IUANode sourceNode, IUANode targetNode, NodeId referenceTypeId, ulong senderId){

    }

    private void BuildTreeView(){
        var nodes = Model.Children.OfType<GOptix_Type_TreeNode>();
        Item a;
        LinkedListNode<int> aa;
        
    }




    public class TreeView:IDisposable{
        private List<TreeViewNode> _nodes = new List<TreeViewNode>();
        public List<TreeViewNode> Nodes
        {
            get { return _nodes; }
            set { _nodes = value; }
        }

        private Item _container;
        public Item Container
        {
            get { return _container; }
            // set { _container = value; }
        }
        
        public TreeView(Item container){
            _container = container;
        }


        public void Refresh(IUANode model){
            Clear();

            var nodes = model.Children.OfType<GOptix_Type_TreeNode>().ToList();

            // var currentPath = string.Empty;
            
            foreach(var node in nodes){
                var path = node.Path;
                var paths = path.Split("/");

            }


        }

        public void Clear(){
            _nodes.Clear();
        }

        public void Dispose(){
            Clear();
        }
    }

    public class TreeViewNode:IDisposable{
        public GOptix_TreeViewNode UI { get; set; }
        public GOptix_Type_TreeNode TreeNode {get;set;}
        public NodeId TreeView {get;set;}
        public string Path { get; set; }
        public int Level {get;}
        
        private List<TreeViewNode> _nodes = new List<TreeViewNode>();
        public List<TreeViewNode> Nodes
        {
            get { return _nodes; }
            set { _nodes = value; }
        }
        
        public void Dispose(){
            _nodes.Clear();

            if(UI != null){
                UI.Delete();
            }
        }

    }

    public class TreeNodeObserver : IReferenceObserver{
        public static void Register(IUANode node,uint affinityId, Action<IUANode,IUANode,NodeId,ulong> callback,out IEventRegistration reg,out TreeNodeObserver observer){
            observer = new TreeNodeObserver(callback);
            reg = node.RegisterEventObserver(
             observer, EventType.ForwardReferenceAdded | EventType.ForwardReferenceRemoved , affinityId);

              
        }


        Action<IUANode,IUANode,NodeId,ulong> callbackHandler;
        public TreeNodeObserver(Action<IUANode,IUANode,NodeId,ulong> callback){
            callbackHandler = callback;
        }
        public void OnReferenceAdded(IUANode sourceNode, IUANode targetNode, NodeId referenceTypeId, ulong senderId)
        {
           callbackHandler(sourceNode,targetNode,referenceTypeId,senderId);
        }

        public void OnReferenceRemoved(IUANode sourceNode, IUANode targetNode, NodeId referenceTypeId, ulong senderId)
        {
           callbackHandler(sourceNode,targetNode,referenceTypeId,senderId);
          
        }

    }

}
*/
