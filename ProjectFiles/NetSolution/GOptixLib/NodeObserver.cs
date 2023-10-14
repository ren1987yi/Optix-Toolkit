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


namespace GOptix{
	public class NodeObserver : IDisposable{

		IUANode _node;

    	IEventRegistration _registration;
    	IEventObserver _observer;


		public void Dispose(){
			_registration?.Dispose();
			_registration = null;
			_observer = null;
		}


		public NodeObserver(IUANode node, IEventObserver observer, uint affinityId){
			try{

				_observer = observer;
				_registration = node.RegisterEventObserver(
             		observer
					, EventType.ForwardReferenceAdded | EventType.ForwardReferenceRemoved | EventType.ReferenceChanged | EventType.InverseReferenceRemoved
					, affinityId);

			}catch(Exception ex){

			}
		}

	}
}