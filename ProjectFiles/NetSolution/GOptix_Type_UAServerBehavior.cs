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
#endregion
using GOptixLib;
using GOptixLib.OpcUA;
using Newtonsoft.Json;
using FTOptix.System;
using FTOptix.Report;
[CustomBehavior]
public class GOptix_Type_UAServerBehavior : BaseNetBehavior
{
    public override void Start()
    {
        // Insert code to be executed when the user-defined behavior is started
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined behavior is stopped
    }

    [ExportMethod]
    public void GetNodeInfo(string nodepath, out string result)
    {
        // result = "1";


        var res = new MethodExecuteResult()
        {
            Success = false,
            ErrorMessage = "node not exists",
            Result = null
        };

        var node = Project.Current.Get(nodepath);
        if (node != null)
        {

            var nodeinfo = new NodeInfoDto();

            nodeinfo.TypeName = node.GetType().Name;
            if (node.GetType().IsSubclassOf(typeof(UAVariable)))
            {
                nodeinfo.NodeType = NodeType.Variable;
            }
            else if (node.GetType().IsSubclassOf(typeof(UAObject)))
            {
                nodeinfo.NodeType = NodeType.Object;
            }
            else if (node.GetType().IsSubclassOf(typeof(Folder)))
            {
                nodeinfo.NodeType = NodeType.Folder;
            }
            else if (node.GetType().IsSubclassOf(typeof(UANode)))
            {
                nodeinfo.NodeType = NodeType.Node;
            }

            nodeinfo.BrowseName = node.BrowseName;
            nodeinfo.Description = node.Description.Text;


            res.Result = nodeinfo;
            res.Success = true;
            res.ErrorMessage = string.Empty;

        }

        result = JsonConvert.SerializeObject(res);


    }






    #region Auto-generated code, do not edit!
    protected new GOptix_Type_UAServer Node => (GOptix_Type_UAServer)base.Node;
    #endregion
}
