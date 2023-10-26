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
using System.Linq;
using WorkflowCore.Models;
#endregion


namespace GOptixLib
{

	/// <summary>
	/// 模型事件观察者
	/// </summary>
	public class ModelEventObserver : IDisposable
	{

		/// <summary>
		/// 模型观察者
		/// </summary>
		ModelObjectObserver observer;
		/// <summary>
		/// 事件注册
		/// </summary>
		IEventRegistration eventRegistration;

		uint affinityId = 0;

		/// <summary>
		///  
		/// </summary>
		/// <param name="model"></param>
		/// <param name="logicObject"></param>
		/// <param name="onAdded_callback"></param>
		/// <param name="onRemoved_callback"></param> 
		public ModelEventObserver(
			IUANode model
			, IUAObject logicObject
			, Action<IUANode, IUANode, NodeId, ulong> onAdded_callback
			, Action<IUANode, IUANode, NodeId, ulong> onRemoved_callback

		)
		{
			var context = logicObject.Context;

			affinityId = context.AssignAffinityId();

			observer = new ModelObjectObserver(onAdded_callback, onRemoved_callback);

			eventRegistration = model.RegisterEventObserver(
								 observer
								, EventType.ForwardReferenceAdded | EventType.ForwardReferenceRemoved
								, affinityId
								);
		}


		public void Dispose()
		{
			eventRegistration?.Dispose();
			eventRegistration = null;

			observer = null;
		}
	}


	class ModelObjectObserver : IReferenceObserver
	{
		private Action<IUANode, IUANode, NodeId, ulong> _onAdded_callback;
		private Action<IUANode, IUANode, NodeId, ulong> _onRemoved_callback;
		public ModelObjectObserver(
									Action<IUANode, IUANode, NodeId, ulong> onAdded_callback
									, Action<IUANode, IUANode, NodeId, ulong> onRemoved_callback
									)
		{
			_onAdded_callback = onAdded_callback;
			_onRemoved_callback = onRemoved_callback;

		}


		public void OnReferenceAdded(IUANode sourceNode, IUANode targetNode, NodeId referenceTypeId, ulong senderId)
		{
			//Log.Info($"model added : sourceNode:{sourceNode.BrowseName}  targetNode:{targetNode.BrowseName}");
			if (_onAdded_callback != null)
			{
				_onAdded_callback.Invoke(sourceNode, targetNode, referenceTypeId, senderId);
			}
		}

		public void OnReferenceRemoved(IUANode sourceNode, IUANode targetNode, NodeId referenceTypeId, ulong senderId)
		{
			//Log.Info($"model removed : sourceNode:{sourceNode.BrowseName}  targetNode:{targetNode.BrowseName}");

			if (_onRemoved_callback != null)
			{
				_onRemoved_callback.Invoke(sourceNode, targetNode, referenceTypeId, senderId);
			}
		}
	}
}