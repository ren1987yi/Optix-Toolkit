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
using FTOptix.Recipe;
using FTOptix.SQLiteStore;
#endregion

public class test_Treeview_RuntimeNetLogic : BaseNetLogic
{
    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
    }

    [ExportMethod]
    public void Test1(){
        var vv = Owner.Get("GOptix_TreeView1");
        var node = vv.GetAlias("SelectedNode");

    }

    [ExportMethod]
    public void Test2(){
        var obj = Project.Current.Get("Model/Data/TestTreeModel");
        obj?.Children.Clear();
    }
}
