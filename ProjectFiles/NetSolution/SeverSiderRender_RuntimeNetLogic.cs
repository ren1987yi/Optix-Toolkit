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
using FTOptix.DataLogger;
using FTOptix.SQLiteStore;
using FTOptix.Store;
using FTOptix.OPCUAServer;
using FTOptix.RAEtherNetIP;
using FTOptix.Retentivity;
using FTOptix.CoreBase;
using FTOptix.CommunicationDriver;
using FTOptix.Core;
#endregion
using System.Diagnostics;
using FTOptix.Report;

public class SeverSiderRender_RuntimeNetLogic : BaseNetLogic
{
    Process server;
    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started
        StartServer();
        Log.Info("SSR running...");
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
        server.Kill();
        server.Close();
        Log.Info("SSR exit()");
    }


    private void StartServer()
    {
        string libreOfficePath = @"C:\Users\yren6\Desktop\index-win.exe";

        // FIXME: file name escaping: I have not idea how to do it in .NET.
        ProcessStartInfo procStartInfo = new ProcessStartInfo(libreOfficePath, "18080");
        procStartInfo.RedirectStandardOutput = false;
        procStartInfo.UseShellExecute = false;
        procStartInfo.CreateNoWindow = false;
        // procStartInfo.WorkingDirectory = Environment.CurrentDirectory;

        Process process = new Process() { StartInfo = procStartInfo, };
        server = process;
        process.Start();
        
    }
}
