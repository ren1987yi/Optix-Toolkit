

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
#endregion
using MagneMotion;
using System.Linq;
using System.Collections.Generic;

using System.Numerics;

namespace GOptixLib.MagneMotionLite;
	public class TrackPath{
         private Vector2 _world ;
         
		 /// <summary>
       /// 世界坐标偏移位置
       /// </summary>
       /// <value></value>
        public Vector2 WorldLoc
        {
            get { return _world; }
            set { _world = value; }
        }
        
        private List<BakedPath> _paths = new List<BakedPath>();
        public List<BakedPath> Paths
        {
            get { return _paths; }
            private set { _paths = value; }
        }


		public TrackPath(IEnumerable<Path> paths){
			Paths.Clear();

			foreach(var path in paths){
				path.BakeOffset = 1;
				var _path = new BakedPath(path.BakePoints);
				_path.Length = path.Length;
				_path.ID = path.ID;
				//_path.AddSegment(path.BakePoints);
				Paths.Add(_path);
			}

		}

		/// <summary>
		/// move vehicle 找到对应 pathId,position的location
		/// </summary>
		/// <param name="_pathNo"></param>
		/// <param name="_pathPos"></param>
		/// <param name="vehicle"></param> 
        public void MoveVehicle(int _pathNo,float _pathPos,Item vehicle){
            var wrl = _world;
             _pathNo-=0;

            var path = Paths.Where(p=>p.ID == _pathNo).FirstOrDefault();
            if(path == null){
				//throw new Exception("move vehicle error");

                return;
            }
            try{


				var loc = path.GetLocation(_pathPos);
				//转到OPTIX 坐标系
				vehicle.LeftMargin = loc.Position.X + wrl.X;
                vehicle.TopMargin = wrl.Y - loc.Position.Y;
				vehicle.Rotation = loc.RotationAngle * -1;


            }catch(Exception ex){
				Log.Info("GetLocation",$" error:{_pathNo} -- {_pathPos} \n details:{ex}");
            }
        }


      
        
    }
