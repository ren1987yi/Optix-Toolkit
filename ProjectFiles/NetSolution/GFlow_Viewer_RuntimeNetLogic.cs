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

using GFlow.Models;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;
using FTOptix.SQLiteStore;
using FTOptix.OPCUAServer;
using FTOptix.Recipe;
using FTOptix.WebUI;
using FTOptix.System;
using FTOptix.Report;
using FTOptix.DataLogger;
public class GFlow_Viewer_RuntimeNetLogic : BaseNetLogic
{
    IUANode WorkflowsObject;

    const char PATH_SPLIT_CHAR = ';';
    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started


        WorkflowsObject = LogicObject.GetAlias("WorkflowsObject") ;


        string[] folders = null;
        string[] files = null;

        var workflowFiles = new List<string>();

        var v = Owner.GetVariable("FolderPaths");
        if(v != null){
            var _ = (string)v.Value ;
            folders = _.Split(PATH_SPLIT_CHAR);
        }

        v = Owner.GetVariable("FilePaths");
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


        InitialWorkflowsObject(workflowFiles.Where(f=>!string.IsNullOrWhiteSpace(f)).Distinct().ToArray(),WorkflowsObject);
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
    }


     void InitialWorkflowsObject(string[] filepaths,IUANode node){
        foreach(var item in node.Children){
            item.Delete();
        }


        if(filepaths == null){
            return;
        }

        var i=0;

        foreach(var filepath in filepaths){
            if(File.Exists(filepath)){
                var output_json = File.ReadAllText(filepath.Trim());
                var _output = JsonConvert.DeserializeObject<WorkflowOutput>(output_json, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All, TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Full });
                if(_output == null)
                {

                    Log.Error(this.Owner.GetType().Name, "workflow output is error");
                    continue;
                }
                else
                {
                    var workflow_json = _output.JsonDefinition;
                    var event_json = _output.JsonEvent;
                    var diagramJson = _output.JsonDiagram;

                    var obj = InformationModel.MakeObject(i.ToString());
                    var v = InformationModel.MakeVariable("Id",OpcUa.DataTypes.String);
                    v.Value = Path.GetFileNameWithoutExtension(filepath);
                    obj.Add(v);

                    v = InformationModel.MakeVariable("Version",OpcUa.DataTypes.Int32);
                    v.Value = 1;
                    obj.Add(v);


                    v = InformationModel.MakeVariable("Diagram",OpcUa.DataTypes.String);
                    v.Value = diagramJson;
                    obj.Add(v);

                    node.Add(obj);


                    i++;
                }
            
            }
        }
     }










    
}
