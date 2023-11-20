#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.HMIProject;
using FTOptix.NetLogic;
using FTOptix.NativeUI;
using FTOptix.UI;
using FTOptix.WebUI;
using FTOptix.Alarm;
using FTOptix.EventLogger;
using FTOptix.OPCUAServer;
using FTOptix.RAEtherNetIP;
using FTOptix.Retentivity;
using FTOptix.CoreBase;
using FTOptix.CommunicationDriver;
using FTOptix.Store;
using FTOptix.Core;
#endregion

using GOptixLib.Model;
using System.Linq;
using GOptixLib;
using System.Collections.Generic;
using FTOptix.DataLogger;
using FTOptix.SQLiteStore;

public class GOptix_FavoritesViewer_RuntimeLogic : BaseNetLogic
{
    IUANode Favorites;
    Item Container;
    ModelEventObserver observer;
    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started
       
        GetLogicParameter();
        BuildUI();
        observer = new ModelEventObserver(Favorites
        , LogicObject
        , (s, e, t, l) => { ModelChanged(); }
        , (s, e, t, l) => { ModelChanged(); });
       
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
        if(observer != null){

            observer.Dispose();
        }
    }

    private void GetLogicParameter()
    {
        Favorites = Owner.GetAlias("Favorites");

        if (Favorites == null)
        {
            Log.Error(this.GetType().Name, "logic parameter is null");
            throw new Exception("abort");
        }

        Container = LogicObject.GetAlias("Container") as Item;
        if (Container == null)
        {
            Log.Error(this.GetType().Name, "ui container is null");
            throw new Exception("abort");
        }

    }

    private void ClearUI()
    {
        foreach (var v in Container.Children.OfType<Item>())
        {
            v.Delete();
        }
    }

    private void BuildUI()
    {
        ClearUI();
        var delObjs = new List<IUANode>();
        foreach (var obj in Favorites.Children.OfType<IUAVariable>())
        {
            var v = (string)obj.Value;
            if (v != null)
            {
                var item = FavoriteItem.Deserialize(v);
                if (item != null)
                {
                    var ui = item.MakeNode(obj.BrowseName);
                    if (ui == null)
                    {
                        delObjs.Add(obj);
                    }
                    else
                    {
                        Container.Add(ui);
                    }
                }
            }
        }


        //删除错误的收藏夹项目
        for (var i = 0; i < delObjs.Count; i++)
        {
            delObjs[i].Delete();
        }
    }

    private void ModelChanged()
    {
        BuildUI();
    }



    [ExportMethod]
    public void Clear()
    {
        Favorites.Children.Clear();

        ClearUI();
    }

    [ExportMethod]
    public void Refresh()
    {
        BuildUI();
    }

}
