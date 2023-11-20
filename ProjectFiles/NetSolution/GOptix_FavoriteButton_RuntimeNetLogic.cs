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
using System.Collections.Generic;
using GOptixLib.Model;
using GOptixLib;
using FTOptix.DataLogger;
using FTOptix.SQLiteStore;

public class GOptix_FavoriteButton_RuntimeNetLogic : BaseNetLogic
{
    /// <summary>
    /// 收藏夹对象
    /// </summary>
    IUANode Favorites;

    /// <summary>
    /// 是否加入文件夹
    /// </summary>
    IUAVariable IsAdded;


    /// <summary>
    /// 宿主节点 必须是Item
    /// </summary>
    IUANode HostNode;


    /// <summary>
    /// 宿主的信息值
    /// </summary>
    string HostValue;


    /// <summary>
    /// 信息值的SHA256 
    /// </summary>
    string HostSHA256;


    ModelEventObserver observer;
    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started
        GetLogicParameter();
        HostValue = GetHostInfo();
        HostSHA256 = GOptixLib.Utils.Encode.SHA256EncryptString(HostValue);

        IsAdded.Value = FindFavority(HostSHA256);


        observer = new ModelEventObserver(
            Favorites
            , LogicObject
            , (s, e, t, a) => { IsAdded.Value = FindFavority(HostSHA256); }
            , (s, e, t, a) => { IsAdded.Value = FindFavority(HostSHA256); }
        );


    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
        observer.Dispose();
    }

    private void GetLogicParameter()
    {
        Favorites = Owner.GetAlias("Favorites");
        IsAdded = Owner.GetVariable("IsAdded");



        if (Favorites == null || IsAdded == null)
        {
            Log.Error(this.GetType().Name, "logic parameter is null");
            throw new Exception("abort");
        }

        HostNode = (Owner as Item)?.Parent as Item;
        if (HostNode == null)
        {
            Log.Error(this.GetType().Name, "favorite host must be a UI Item");
            throw new Exception("abort");
        }


    }

    private bool FindFavority(string hashcode)
    {
        var node = Favorites.Get(hashcode);
        return node == null ? false : true;
    }


    private string GetHostInfo()
    {

        var rec = new FavoriteItem(HostNode);
        var vv = rec.Serialize();
        return vv;
    }


    [ExportMethod]
    public void OnClick_Handle()
    {

        if ((bool)IsAdded.Value)
        {
            var node = Favorites.Get(HostSHA256.ToString());
            node.Delete();
            IsAdded.Value = false;
        }
        else
        {
            var v = InformationModel.MakeVariable(HostSHA256.ToString(), OpcUa.DataTypes.String);
            v.Value = HostValue;
            Favorites.Add(v);
            IsAdded.Value = true;
        }


    }


}
