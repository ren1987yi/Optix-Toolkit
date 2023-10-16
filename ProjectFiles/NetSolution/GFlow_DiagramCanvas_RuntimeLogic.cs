#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.HMIProject;
using FTOptix.NetLogic;
using FTOptix.UI;
using FTOptix.NativeUI;
using FTOptix.CoreBase;
using FTOptix.Core;
#endregion
using GFlow.OptixWrapper;
using FTOptix.SQLiteStore;
using FTOptix.OPCUAServer;
using FTOptix.Recipe;
using FTOptix.WebUI;



public class GFlow_DiagramCanvas_RuntimeLogic : BaseNetLogic
{
    Diagram diagram;
    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started

        var nodeUIType = Owner.GetAlias("NodeUIType");
        if(nodeUIType == null){
            throw new Exception("NodeUI Type is null");
        }

        var v = Owner.GetVariable("NodeUIAlias");
        if(v == null){
            throw new Exception("NodeUI Alias is null");
        }

        var nodeUIAlisa = (string)v.Value;


        var canvas = LogicObject.GetAlias("Canvas") as Item;
        if(canvas == null){
            throw new Exception("Canvas is null");

        }


        v  = Owner.GetVariable("LineColor");
        var line_color = v.Value;
        
        v = Owner.GetVariable("LineThickness");
        var line_thickness = v.Value;


        diagram = new Diagram(canvas,nodeUIType.NodeId,nodeUIAlisa,(Color)line_color,(float)line_thickness);


    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
    }


    [ExportMethod]
    public void BuildUI(){
        var v = Owner.GetVariable("DiagramJson");
        if(v != null){
            var json = (string)v.Value;
            diagram.BuildUIFromJson(json);
        }
    }
}
