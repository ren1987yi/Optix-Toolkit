using System;
using System.Linq;
using System.Runtime.InteropServices;
using FTOptix.HMIProject;
using UAManagedCore;

using System.Collections.Generic;
using System.ComponentModel;
//namespace GOptixLib.Extensions;
using GOptixLib;
public static class UANodeExtensions
{
	/// <summary>
	/// 构建数据Object的树形结构
	/// </summary>
	/// <param name="obj">数据对象</param>
	/// <param name="root">树形结构的根</param>
	/// <param name="clear">是否清除树形结构</param> 
	public static void BuildTreeNode(this IUAObject obj, IUANode root, bool clear = true)
	{
		if (clear)
		{

			root.Children.Clear();
		}

		var node = InformationModel.MakeObject<GOptix_Type_TreeNode>(obj.BrowseName);
		node.Text = new LocalizedText(obj.BrowseName, "en-US");
		node.Tag = obj.NodeId;
		root.Add(node);

		foreach (var _obj in obj.Children.OfType<IUAObject>())
		{
			// BuildTreeNode(_obj,node.Nodes);
			_obj.BuildTreeNode(node.Nodes, false);
		}


	}


	/// <summary>
	/// 根据对象内的变量构建树结构
	/// </summary>
	/// <param name="obj"></param>
	/// <param name="treeroot"></param>
	/// <param name="clear"></param>
	public static void BuildVariableTreeNode(this IUANode obj, IUANode treeroot, bool clear = true)
	{
		if (clear)
		{
			treeroot.Children.Clear();
		}

		foreach (var n in obj.Children)
		{
			if (n.GetType().IsSubclassOf(typeof(UAVariable)) || n.GetType() == typeof(UAVariable))
			{
				var tvNode = InformationModel.MakeObject<GOptix_Type_TreeNode>(n.BrowseName);
				tvNode.Text = new LocalizedText(n.BrowseName, "en-US");
				tvNode.Tag = n.NodeId;

				treeroot.Add(tvNode);
			}
			else if (n.GetType().IsSubclassOf(typeof(UAObject)) || n.GetType() == typeof(UAObject))
			{
				var tvNode = InformationModel.MakeObject<GOptix_Type_TreeNode>(n.BrowseName);
				tvNode.Text = new LocalizedText(n.BrowseName, "en-US");
				tvNode.Tag = n.NodeId;
				treeroot.Add(tvNode);
				n.BuildVariableTreeNode(tvNode.Nodes, false);
			}
		}

	}

	/// <summary>
	/// 注册 内元素 加入 和 移除的事件观察
	/// </summary>
	/// <param name="model">父模型</param>
	/// <param name="logicObject">脚本对象</param>
	/// <param name="onAdded_callback">加入事件响应</param>
	/// <param name="onRemoved_callback">移除事件响应</param>
	/// <returns></returns>
	public static ModelEventObserver RegisterAddAndRemoveObserver(this IUANode model
									, IUAObject logicObject
									, Action<IUANode, IUANode, NodeId, ulong> onAdded_callback
									, Action<IUANode, IUANode, NodeId, ulong> onRemoved_callback
									)
	{
		return new ModelEventObserver(model, logicObject, onAdded_callback, onRemoved_callback);

	}


	public static NodeId GetTypeNodeId(this IUANode node){
		 var t = node.GetType();
        var s = t.Assembly.FullName.Split(",").FirstOrDefault();

        var aaaa = $"{s}.ObjectTypes";
        var tObjectTypes = t.Assembly.GetType(aaaa);
        if(tObjectTypes!= null){
            var fInfo = tObjectTypes.GetField(t.Name);
            var val = fInfo.GetValue(null);
			if(val != null){
				return val as NodeId;
			}
         
        }

		return null;
	}


}