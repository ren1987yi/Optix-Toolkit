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
using System.Collections.Generic;
using System.Linq;
using FTOptix.System;
using FTOptix.Report;
#endregion

public class GOptix_CarouselLoader_RuntimeNetLogic : BaseNetLogic
{

    int _curIndex = 0;
    List<IUANode> _playList =  new List<IUANode>();
    PeriodicTask _task ;
    PanelLoader _loader;
    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started

        _loader = Owner as PanelLoader;
        if(_loader == null){
            Log.Error("owner must be panelloader");
            return;
        }

        // Panel a;
        // Container ba;
        var list = Owner.GetAlias("PlayList");
        if(list != null){
            _playList.Clear();
            foreach(var n in list.Children){
                if(n.GetType().IsSubclassOf(typeof(ContainerType)) ){
                    _playList.Add(n);
                }
            }


            //_playList.AddRange( list.Children.Where(n=>n.GetType().IsSubclassOf(typeof(BaseUIObject))));
            #if DEBUG
            Log.Info($"play list count:{_playList.Count}");
            #endif
        }
        var p = Owner.GetVariableValue<int>("Period");
        if(p <= 0){
            Log.Warning("Period <= 0,so set default value:60000");
            p = 60000;
        }


        _curIndex = 0;

        _task = new PeriodicTask(OnTimer_Handle,p,LogicObject);
        _task.Start();
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
        try{
            _task?.Cancel();
            _task?.Dispose();
            _task = null;

        }catch{

        }
    }

    int ChangePanel(int idx){
        idx++;
        if(idx >= _playList.Count){
            idx = 1;
        }

        if(idx > _playList.Count){

        }else{
            _loader.Panel = _playList[idx-1].NodeId;
        }
        return idx;
    }

    public void OnTimer_Handle(){
        _curIndex = ChangePanel(_curIndex);
    }
}
