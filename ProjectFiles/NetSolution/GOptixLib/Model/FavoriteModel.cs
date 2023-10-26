
using UAManagedCore;
using System;
using System.Linq;
using System.Reflection.Metadata;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using FTOptix.HMIProject;
using FTOptix.Core;

namespace GOptixLib.Model;
[Serializable]
class FavoriteItem
{
	public NodeId UI;
	public List<ParamMapper> Params { get; private set; }

	public string Serialize()
	{

		string value = string.Empty;
		BinaryFormatter serializer = new BinaryFormatter();

		using (MemoryStream memStream = new MemoryStream())
		{
			serializer.Serialize(memStream, this);
			value = Convert.ToBase64String(memStream.ToArray());
			// value = System.Text.Encoding.Unicode.GetString(memStream.ToArray());
		}
		return value;
		// memStream.Close();
	}


	public FavoriteItem(){

	}

	public FavoriteItem(IUANode node){
		var nodeId = node.GetTypeNodeId();
		UI = nodeId;
 		Params = RecordNodeParam(node);
	}

  private List<ParamMapper> RecordNodeParam(IUANode node)
    {
        var ps = new List<ParamMapper>();
        foreach (var obj in node.Children)
        {
            if (obj.GetType() == typeof(Alias) || obj.GetType().IsSubclassOf(typeof(Alias)))
            {
                var aliasNode = node.GetAlias(obj.BrowseName);
                ps.Add(new ParamMapper()
                {
                    Name = obj.BrowseName,
                    Type = ParamType.Alias,
                    Value = aliasNode?.NodeId
                });
            }
            else if (obj.GetType() == typeof(UAVariable) || obj.GetType().IsSubclassOf(typeof(UAVariable)))
            {
                ps.Add(new ParamMapper()
                {
                    Name = obj.BrowseName,
                    Type = ParamType.Variable,
                    Value = (obj as IUAVariable)?.Value.Value
                });
            }
            //Log.Info($"{obj.BrowseName} {obj.GetType().Name}");
        }

        return ps;
    }



	public IUANode MakeNode(string name){
		bool missParam = false;
		var ui = InformationModel.MakeObject(name,UI);
		foreach(var p in Params){
			switch(p.Type){
				case ParamType.Variable:
					var v = ui.GetVariable(p.Name);
					if(v != null){
						v.Value = new UAValue(p.Value);
					}else{
						missParam = true;
						break;
					}
					break;
				case ParamType.Alias:
					var nodeId = p.Value as NodeId;
					if(nodeId != null){

						var aliasNode = InformationModel.Get(nodeId);
						if(aliasNode != null){
							ui.SetAlias(p.Name,aliasNode );
						}else{
							missParam = true;
							break;
						}
					}else{
						missParam = true;
						break;
					}
					break;
			}
		}
		if(!missParam){

			return ui;
		}else{
			return null;
		}
	}

	public static FavoriteItem Deserialize(string value)
	{

		FavoriteItem obj = null;
		BinaryFormatter serializer = new BinaryFormatter();
		var bytes = Convert.FromBase64String(value);
		using (var memStream = new MemoryStream(bytes))
		{

			obj = serializer.Deserialize(memStream) as FavoriteItem;
		}
		return obj;

	}


}

[Serializable]
class ParamMapper
{
	public string Name { get; set; }
	public ParamType Type { get; set; }
	public object Value { get; set; }

	public override string ToString()
	{
		return $"{Name} {Type} {Value}";
	}
}

[Serializable]
enum ParamType
{
	Variable,
	Alias
}