///=============================================================================
/// name:LayoutViewer2D 的文件,含有相关类
/// author:Renyi
/// email:ren1987yi@163.com
/// license:MIT
/// version:0.0.1-alpha
/// description:
/// 
///=============================================================================



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
using MagneMotion.ML;
using System.Linq;
using System.Collections.Generic;
using System.Numerics;



namespace GOptixLib.MagneMotionLite{

	/// <summary>
	/// 布局查看器
	/// </summary>
	public class LayoutViewer2D :IDisposable {
		const string _VEHICLE_NAME_PREFIX_ = "Vehicle_";
		const string _PLACEHOLDER_OBJECT_NAME_ = "a6ad371a0950da0e1d6741b6b2383b2bb5657203851fb55cd21f6f62d7b320f6eee11f2696403d330131c44349abb9b870f44c6f7ed60e71ff4601a79a6cc535";
		readonly Item _container;// viewer 容器 scrollerviewer
		readonly LayoutViewer2DConfiguration _config; //配置
		Map _map; //地图
		TrackPath _trackPath; //轨迹路径
		
		readonly List<Item> _vehicles = new List<Item>(); //车 ui
		readonly List<IUANode> _motors = new List<IUANode>(); // motor ui

		readonly IUAObject LogicObject; //optix logic object, this host
		public LayoutViewer2D(Item container,LayoutViewer2DConfiguration configure,IUAObject logicObject,int update_period=100){
			_container = container;
			_config = configure;
			LogicObject = logicObject;
			_taskUpdateVehicleLocation = new PeriodicTask(UpdateVehicleLocation,update_period,LogicObject);
			
			
		}

		public void Dispose(){
			try{
				_taskUpdateVehicleLocation.Cancel();
				_taskUpdateVehicleLocation?.Dispose();

			}catch{

			}
		}

		public void BuildTrack(string filepath,IUANode vehicleModel,int update_period=100){
			if(_map != null){

				_taskUpdateVehicleLocation.Cancel();
			}
			
			
			ClearContainer(_container);
			
			var map = FileHelper.LoadMMLTrackFile(filepath);
			_map = map;

#if DEBUG
			Log.Info("ICT","OLD===================================================");
			foreach(var motor in map.MotorEntities){
				Log.Info("ICT", $"{motor.TypeName} angle:{motor.Rotation} x:{motor.Location.X} y:{motor.Location.Y}");
			}
#endif


			var res = FileHelper.TrackToOptix(map);
			var wrl = res[0];
			var box = res[1];
		

			


			var idx = 0; //motor index

			#if DEBUG
			Log.Info("ICT","NEW===================================================");
			#endif

			

			var ui_qmeter = _config.Motor250mmTypeId;
			var ui_1meter = _config.Motor1000mmTypeId;
			var ui_l_curve = _config.MotorCurveLeftTypeId;
			var ui_r_curve = _config.MotorCurveRightTypeId;
			var ui_l_switch = _config.MotorSwitchLeftTypeId;
			var ui_r_switch = _config.MotorSwitchRightTypeId;
			var ui_vehicle = _config.VehicleTypeId;

			
			

			//build motor ,add to container
			foreach(var motor in map.MotorEntities){
				#if DEBUG
				Log.Info("ICT", $"{motor.TypeName} angle:{motor.Rotation} x:{motor.Location.X} y:{motor.Location.Y}");
				#endif

				//motor 里由 path 的 信息

				switch(motor.TypeName){
					case "motor_l_curve":
						
						var m_l_curve = InformationModel.MakeObject($"{idx}",ui_l_curve) as Item;
						m_l_curve.LeftMargin = motor.Location.X;
						m_l_curve.TopMargin = motor.Location.Y;
						m_l_curve.Rotation = motor.Rotation;
						
						_container.Add(m_l_curve);
						_motors.Add(m_l_curve);
						break;
					case "motor_r_curve":
						var m_r_curve = InformationModel.MakeObject($"{idx}",ui_r_curve) as Item;
						m_r_curve.LeftMargin = motor.Location.X;
						m_r_curve.TopMargin = motor.Location.Y;
						m_r_curve.Rotation = motor.Rotation;
						_container.Add(m_r_curve);
						_motors.Add(m_r_curve);
						break;
					case "motor_q_meter":
						var m_q_meter = InformationModel.MakeObject($"{idx}",ui_qmeter) as Item;
						m_q_meter.LeftMargin = motor.Location.X;
						m_q_meter.TopMargin = motor.Location.Y;
						m_q_meter.Rotation = motor.Rotation;
						_container.Add(m_q_meter);
						_motors.Add(m_q_meter);
						break;
					case "motor_1_meter":
						var m_1_meter = InformationModel.MakeObject($"{idx}",ui_1meter) as Item;
						m_1_meter.LeftMargin = motor.Location.X;
						m_1_meter.TopMargin = motor.Location.Y;
						m_1_meter.Rotation = motor.Rotation;
						_container.Add(m_1_meter);
						_motors.Add(m_1_meter);
						break;
					case "motor_l_switch":
						var m_l_switch = InformationModel.MakeObject($"{idx}",ui_l_switch) as Item;
						m_l_switch.LeftMargin = motor.Location.X;
						m_l_switch.TopMargin = motor.Location.Y;
						m_l_switch.Rotation = motor.Rotation;
						_container.Add(m_l_switch);
						_motors.Add(m_l_switch);
						break;
					case "motor_r_switch":
						var m_r_switch = InformationModel.MakeObject($"{idx}",ui_r_switch) as Item;
						m_r_switch.LeftMargin = motor.Location.X;
						m_r_switch.TopMargin = motor.Location.Y;
						m_r_switch.Rotation = motor.Rotation;
						_container.Add(m_r_switch);
						_motors.Add(m_r_switch);
						break;
						
				}


				idx++;
			}

			
			//add a hide box
			var _rect = InformationModel.Make<Rectangle>(_PLACEHOLDER_OBJECT_NAME_);
			_rect.LeftMargin = box.X +1000 ;
			_rect.TopMargin = box.Y+1000;
			_container.Add(_rect);


		
		

			//build track path
			var track = new TrackPath(map.Paths);
			track.WorldLoc = new Vector2(wrl.X,wrl.Y);
			_trackPath = track;


			// add vehicle from model
			if(vehicleModel != null){
				foreach(var vehicle in vehicleModel.Children.OfType<IUAObject>()){
					AddVehicle(1,0,vehicle);
				}
			}


			_taskUpdateVehicleLocation.Start();


		}


		private void ClearContainer(Item container){
			_vehicles.Clear();
			_motors.Clear();
			_vehicleMapper.Clear();
			foreach(var item in  container.Children.OfType<Item>()){
				item.Delete();
			}


		}


		/// <summary>
		/// 加车,车序号往后排
		/// 如果空模型，新建一个，挂在车 UI上
		/// </summary>
		/// <param name="pathId">location:PathId</param>
		/// <param name="position">location:Position</param>
		/// <param name="model">车辆的数据模型</param> 
		public void AddVehicle(int pathId=1,float position=0,IUANode model=null){

			var m_vehicle = InformationModel.MakeObject($"{_VEHICLE_NAME_PREFIX_}{_vehicles.Count +1}",_config.VehicleTypeId) as Item;
			if(model != null){

				m_vehicle.SetAlias("Model",model);
			}else{
				var _model = InformationModel.Make<GOptix_Type_MML_VehicleStatus>(m_vehicle.BrowseName);
				model = _model;
				_model.Index = _vehicles.Count + 1;
				m_vehicle.Add(_model);
				m_vehicle.SetAlias("Model",model);
				
			}

			if(model.GetType() == typeof(GOptix_Type_MML_VehicleStatus)){
				var status = model as GOptix_Type_MML_VehicleStatus;
				_vehicleMapper.Add(status.LocationVariable,m_vehicle);
			}
			_trackPath?.MoveVehicle(pathId,position,m_vehicle);
			_container?.Add(m_vehicle);
			_vehicles?.Add(m_vehicle);
		}

		/// <summary>
		/// move vehicle with pathid and position 
		/// </summary>
		/// <param name="id">vehicle id > 0</param>
		/// <param name="pathId"></param>
		/// <param name="position">uint mm</param>
		public void MoveVehicle(int id,int pathId,float position){
			if(id > 0 && id <= _vehicles.Count){
				var m_vehicle = _vehicles[id-1];
				_trackPath?.MoveVehicle(pathId,position,m_vehicle);
			}
		}




		
		/// <summary>
		/// 位置变量 和 vehicle ui 的对应关系
		/// </summary>
		/// <typeparam name="IUAVariable">vehicle location variable ,type is string</typeparam>
		/// <typeparam name="Item">vehicle ui</typeparam>
		/// <returns></returns>
		Dictionary<IUAVariable,Item> _vehicleMapper = new Dictionary<IUAVariable, Item>();

		
		/// <summary>
		/// update vehicle location optix period task
		/// </summary>
		PeriodicTask _taskUpdateVehicleLocation; 
		
		/// <summary>
		/// update vehicle location in ui
		/// location is string ,format is {PathId};{Position}
		/// </summary> 
		void UpdateVehicleLocation(){
			foreach(var kv in _vehicleMapper){
				var loc = (string)kv.Key.Value;
				var locs = loc.Split(";");
				if(locs.Length == 2){
					
					if(int.TryParse(locs[0],out var pathId) && float.TryParse(locs[1],out var position)){
						_trackPath?.MoveVehicle(pathId,position,kv.Value);
					}
				}
				
			}
		}


	}



	/// <summary>
	/// MML Layout viewer 配置项
	/// </summary> 
	public class LayoutViewer2DConfiguration{
		public NodeId Motor250mmTypeId { get; set; }
		public NodeId Motor1000mmTypeId { get; set; }
		public NodeId MotorCurveLeftTypeId { get; set; }
		public NodeId MotorCurveRightTypeId { get; set; }
		public NodeId MotorSwitchLeftTypeId { get; set; }
		public NodeId MotorSwitchRightTypeId { get; set; }
		public NodeId VehicleTypeId { get; set; }
	}






}