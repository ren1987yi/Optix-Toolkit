using System;
using System.Linq;
using FTOptix.HMIProject;
using UAManagedCore;

//namespace GOptixLib.Extensions;

public static class UANodeExtensions{
	/// <summary>
	/// 构建数据Object的树形结构
	/// </summary>
	/// <param name="obj">数据对象</param>
	/// <param name="root">树形结构的根</param>
	/// <param name="clear">是否清除树形结构</param> 
	public static void BuildTreeNode(this IUAObject obj,IUANode root,bool clear=true){
		if(clear){

			root.Children.Clear();
		}

		var node = InformationModel.MakeObject<GOptix_Type_TreeNode>(obj.BrowseName);
		node.Text = obj.BrowseName;
		node.Tag = obj.NodeId;
		root.Add(node);

		foreach(var _obj in obj.Children.OfType<IUAObject>()){
			// BuildTreeNode(_obj,node.Nodes);
			_obj.BuildTreeNode(node.Nodes,false);
		}


	}

}