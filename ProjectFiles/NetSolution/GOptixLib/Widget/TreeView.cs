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


namespace GOptix.Widget{
    public class TreeView : IDisposable{
        
        readonly Item _container;
        readonly IUANode _treeview;
        IUAVariable _ClickedNode;
        IUAVariable _varSelectedNode;
        IUAVariable _varSelectedTag;


        IUAVariable _varColorNoraml;
        IUAVariable _varColorSelected;
        IUAVariable _varColorArrow;


        private GOptix_TreeViewNode _selectedNode;
        public GOptix_TreeViewNode SelectedNode
        {
            get { return _selectedNode; }
            set {
                if(_selectedNode != null){
                    var v = _selectedNode.GetVariable("Selected");
                    if(v != null){
                        v.Value = false;
                    }
                } 

                if(value != null){
                    var v = value.GetVariable("Selected");
                    if(v != null){
                        v.Value = true;
                    }
                } 

                _selectedNode = value; 

                if(value != null){
                    

                    var treeNode = value.GetAlias("TreeNode") as GOptix_Type_TreeNode;



                    if(treeNode != null){
                        if(value != null){
                            // var v = _treeview.GetVariable(nameof(SelectedNode));
                            // if(v != null){

                            //     v.Value = treeNode.NodeId;
                            // }

                            if(_varSelectedNode != null){
                                _varSelectedNode.Value = treeNode.NodeId;
                            }

                            //_treeview.SetAlias(nameof(SelectedNode),treeNode);
                            if(treeNode?.Tag != null){
                                // v = _treeview.GetVariable("SelectedTag");
                                // if(v != null){

                                //     v.Value = treeNode.Tag;
                                //     // _treeview.SetAlias("SelectedTag",treeNode?.Tag);
                                // }

                                _varSelectedTag.Value = treeNode.Tag;
                            }
                        }
                    }

                }

            }
        }
        
        public TreeView(Item container,IUANode treeview){
            _container = container;
            _treeview = treeview;
            if(treeview != null){
                _ClickedNode = treeview.GetVariable("ClickedNode");
                if(_ClickedNode != null){
                    _ClickedNode.VariableChange += OnClickedNodeChanged_Handle;
                }


                _varSelectedNode = treeview.GetVariable("SelectedNode");
                _varSelectedTag = treeview.GetVariable("SelectedTag");

                _varColorNoraml = treeview.GetVariable("colorNoraml");
                _varColorSelected = treeview.GetVariable("colorSelected");
                _varColorArrow = treeview.GetVariable("colorArrow");


            }



        }

        public void Dispose(){
             if(_ClickedNode != null){
                    _ClickedNode.VariableChange -= OnClickedNodeChanged_Handle;
                }
        }

        public void Clear(){
            // _container.Children.Clear();

            foreach(var item in _container.Children.OfType<Item>()){
                item.Delete();
            }
        }

        public void Build(IUANode model){
            Clear();
            if(model== null){
                return;
            }

            var nodes = model.Children.OfType<GOptix_Type_TreeNode>();
            foreach(var node in nodes){
                var ui = InformationModel.Make<GOptix_TreeViewNode>(node.BrowseName);
                ui.SetAlias("TreeView",_treeview);
                ui.SetAlias("TreeNode",node);

                var v = ui.GetVariable("colorNormal");
                if(v != null){
                    v.Value = _varColorNoraml == null ? Colors.Transparent : _varColorNoraml.Value;
                }

                v = ui.GetVariable("colorSelected");
                if(v != null){
                    v.Value = _varColorSelected == null ? Colors.Black : _varColorSelected.Value;
                }

                v = ui.GetVariable("colorArrow");
                if(v != null){
                    v.Value = _varColorArrow == null ? Colors.Black : _varColorArrow.Value;
                }

                
                
                _container.Add(ui);
            }
        }


        private void OnClickedNodeChanged_Handle(object sender,VariableChangeEventArgs args){
            var nodeId = (NodeId)args.NewValue;

            var viewNode = InformationModel.Get<GOptix_TreeViewNode>(nodeId);
            if(viewNode == null){

            }else{
                SelectedNode = viewNode;
            }

        }





    }


    public class TreeViewNode{

        GOptix_Type_TreeNode _treeNode;
        IUANode _treeView;
        IUANode Owner;
        Item _nodeContainer;
        IUAVariable Expanded;



        
        IUAVariable _varColorNoraml;
        IUAVariable _varColorSelected;
        IUAVariable _varColorArrow;

        public TreeViewNode(IUANode treeview,IUANode owner, Item nodeContainer){

            _treeView = treeview;
            _nodeContainer = nodeContainer;
            Owner = owner;

            _varColorNoraml = treeview.GetVariable("colorNoraml");
            _varColorSelected = treeview.GetVariable("colorSelected");
            _varColorArrow = treeview.GetVariable("colorArrow");

            Expanded = Owner.GetVariable(nameof(Expanded));
        }

         public void Clear(){
            foreach(var item in _nodeContainer.Children.OfType<Item>()){
                item.Delete();
            }
        }

        public void Build(GOptix_Type_TreeNode model){
            Clear();
            if(model== null){
                return;
            }
            _treeNode = model;
            var nodes = model.Nodes.Children.OfType<GOptix_Type_TreeNode>();
            foreach(var node in nodes){
                var ui = InformationModel.Make<GOptix_TreeViewNode>(node.BrowseName);
                ui.SetAlias("TreeView",_treeView);
                ui.SetAlias("TreeNode",node);

                var v = ui.GetVariable("colorNormal");
                if(v != null){
                    v.Value = _varColorNoraml == null ? Colors.Transparent : _varColorNoraml.Value;
                }

                v = ui.GetVariable("colorSelected");
                if(v != null){
                    v.Value = _varColorSelected == null ? Colors.Black : _varColorSelected.Value;
                }

                v = ui.GetVariable("colorArrow");
                if(v != null){
                    v.Value = _varColorArrow == null ? Colors.Black : _varColorArrow.Value;
                }

                _nodeContainer.Add(ui);
            }
            var _hasChildren = _nodeContainer.Children.OfType<Item>().Count() > 0;
            var _v = Owner.GetVariable("HasExpand");
            if(_v != null){
                _v.Value = _hasChildren;
            }
        }

        public void OnClick_Handle(){


            var v = _treeView.GetVariable("ClickedNode");
            if(v != null){
                v.Value = Owner.NodeId;
            }
        }

        public void OnExpand_Handle(){
            if(_treeNode != null){
                if(_treeNode.Nodes.Children.Count > 0){
                    Expanded.Value = !((bool)Expanded.Value);
                    Log.Info(Expanded.Value);
                }        
            }
        }

    }
}
