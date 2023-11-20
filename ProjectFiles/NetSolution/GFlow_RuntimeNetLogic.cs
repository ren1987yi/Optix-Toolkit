#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.EventLogger;
using FTOptix.HMIProject;
using FTOptix.Retentivity;
using FTOptix.NativeUI;
using FTOptix.UI;
using FTOptix.RAEtherNetIP;
using FTOptix.Alarm;
using FTOptix.CommunicationDriver;
using FTOptix.NetLogic;
using FTOptix.Store;
using FTOptix.CoreBase;
using FTOptix.Core;
#endregion
using GFlow.OptixWrapper;
using System.Collections.Generic;
using System.IO;
using GFlow.Services;
using System.Linq;
using FTOptix.SQLiteStore;
using FTOptix.OPCUAServer;
using FTOptix.Recipe;
using FTOptix.WebUI;
using FTOptix.System;
using FTOptix.Report;
using FTOptix.DataLogger;
public class GFlow_RuntimeNetLogic : BaseNetLogic
{
    IServiceProvider serviceProvider;
    WorkerCollection workers;
    const char PATH_SPLIT_CHAR = ';';
    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started



        string[] folders = null;
        string[] files = null;


        if(this.Owner.GetType() != typeof(FTOptix.Core.Folder) ||
            this.Owner.BrowseName != "NetLogic"){
            
            Log.Error(this.GetType().Name,"this script must be install in NetLogic");
            return;
        }

        var workflowFiles = new List<string>();

        var v = LogicObject.GetVariable("FolderPaths");
        if(v != null){
            var _ = (string)v.Value ;
            folders = _.Split(PATH_SPLIT_CHAR);
        }

        v = LogicObject.GetVariable("FilePaths");
        if(v != null){
            var _ = (string)v.Value ;
            files = _.Split(PATH_SPLIT_CHAR);
            workflowFiles.AddRange(files);
        }
        
        foreach(var folder in folders){
            if(Directory.Exists(folder)){

                var _files = Directory.EnumerateFiles(folder);
                workflowFiles.AddRange(_files);
            }
        }

        serviceProvider = GFlowServiceExtensions.ConfigureServices(implOptixServiceType:typeof(OptixBahaviorService),ptype:PersistenceProviderType.MEMORY);
        workers = new WorkerCollection(serviceProvider);

        foreach(var filepath in workflowFiles.Where(f=>!string.IsNullOrWhiteSpace(f)).Distinct()){

            workers.Add(System.IO.File.ReadAllText(filepath));
        }


        var objs = LogicObject.GetObject("Workflows");
        InitialWorkflowsObject(objs);
        


        serviceProvider.StartWorkflowHost();
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped


        if(serviceProvider != null){
            serviceProvider.StopWorkflowHost();
        }
    }


    void InitialWorkflowsObject(IUANode node){
        foreach(var item in node.Children){
            item.Delete();
        }

        foreach(var wk in workers){
            var obj = InformationModel.MakeObject(wk.WorkerID);
            var v = InformationModel.MakeVariable("Id",OpcUa.DataTypes.String);
            v.Value = wk.WorkerID;
            obj.Add(v);
            
            v = InformationModel.MakeVariable("Version",OpcUa.DataTypes.Int32);
            v.Value = wk.WorkerVersion;
            obj.Add(v);


            v = InformationModel.MakeVariable("Diagram",OpcUa.DataTypes.String);
            v.Value = wk.DiagramJson;
            obj.Add(v);

            node.Add(obj);
            
        }
    }


    [ExportMethod]
    public void StartWorkflow(string id){
        if(serviceProvider != null){
            workers.ExecuteWork(id);
        }
    }

    [ExportMethod]
    public void SetWorkflowTimer(string id,bool is_start){
        if(serviceProvider != null){
            workers.SetWorkTimer(id,is_start);
        }
    }


}
