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
using FTOptix.System;
using FTOptix.Report;





public class GOptix_TreeView_RuntimeNetLogic : BaseNetLogic
{
    TreeView treeView;
    Item Container;
    IUANode Model;



    // NodeObserver nodeObserver;
    // CCObserver observer;
    // uint affinityId;

    public override void Start()
    {
        Model = Owner.GetAlias(nameof(Model));
        Container = LogicObject.GetAlias("Container") as Item;

        var nodeType = Owner.GetAlias("TreeNodeUIType");
        if (nodeType != null)
        {

            treeView = new TreeView(Container, Owner, nodeType.NodeId);
        }
        else
        {

            var nn = Project.Current.GetByType<GOptix_TreeViewNode>();
            if (nn != null)
            {

                treeView = new TreeView(Container, Owner, nn.NodeId);
            }
            else
            {

                Log.Error("this tree node ui type is null");
            }

            return;
        }



        Refresh();





    }

    public override void Stop()
    {
        // nodeObserver?.Dispose();
    }




    [ExportMethod]
    public void Refresh()
    {
        treeView.Build(Model);
    }

 



}

