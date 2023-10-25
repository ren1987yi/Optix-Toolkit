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
using FTOptix.OPCUAServer;
using FTOptix.RAEtherNetIP;
using FTOptix.Retentivity;
using FTOptix.Alarm;
using FTOptix.CommunicationDriver;
using FTOptix.CoreBase;
using FTOptix.Store;
using FTOptix.Core;
using System.Linq;
using FTOptix.System;
using FTOptix.Report;
#endregion

public class GOptix_Dlg_VariableBrowser_RuntimeNetLogic : BaseNetLogic
{

    IUAVariable _varSelectedTag;
    IUAVariable _varSelectedNode;

    GOptix_Type_VariableBrowserParameter _param;
    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started
        _param = Owner.GetAlias("Parameter") as GOptix_Type_VariableBrowserParameter;
        var treeroot = LogicObject.GetObject("TreeNode");


        _varSelectedTag = LogicObject.GetVariable("TreeViewerSelectedTag");
        _varSelectedNode = LogicObject.GetVariable("TreeViewerSelectedNode");

        var model = InformationModel.Get(_param.Model);
        if (model != null && treeroot != null)
        {

            model.BuildVariableTreeNode(treeroot);
        }
        else
        {
            #if DEBUG
            Log.Error("参数不对");
            #endif
        }



    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
    }


    [ExportMethod]
    public void OnOK_Handle()
    {
        var nodeId = (NodeId)_varSelectedTag.Value;
        if (nodeId != null)
        {
            var node = InformationModel.Get(nodeId) as IUAVariable;
            if (node != null)
            {
#if DEBUG
                Log.Info($"{node.BrowseName} -- {node.DisplayName.Text} -- {node.Description.Text} -- {node.Value}");
#endif
                if (_param != null)
                {
                    _param.TargetNodePoint = nodeId;

                }
            }
        }
    }





}
