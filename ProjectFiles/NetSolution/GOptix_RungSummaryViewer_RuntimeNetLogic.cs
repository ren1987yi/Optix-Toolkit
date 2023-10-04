#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.EventLogger;
using FTOptix.HMIProject;
using FTOptix.Retentivity;
using FTOptix.NativeUI;
using FTOptix.UI;
using FTOptix.Alarm;
using FTOptix.Store;
using FTOptix.CoreBase;
using FTOptix.Core;
using FTOptix.NetLogic;
#endregion

using System.Xml;
using System.Xml.Serialization;

using System.Collections.Generic;
using System.IO;
using FTOptix.RAEtherNetIP;
using FTOptix.CommunicationDriver;
using System.Linq;
using UAManagedCore.OpcUa;
using System.Security;
public class GOptix_RungSummaryViewer_RuntimeNetLogic : BaseNetLogic
{


    RungDoc doc;
    string RungFilePath;

    IUANode FilteredPrograms;
    IUANode FilteredRoutines;
    IUANode FilteredRungs;

    IUANode ItemUIType;


    Item UIContainer;

    IUANode TagsFolder;

    string _selectedPrg;
    string _selectedRoutine;
    string _selectedRung;


    string _openedPrg;
    string _openedRoutine;
    string _openedRung;




    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started

        FilteredPrograms =  LogicObject.Get(nameof(FilteredPrograms));
        FilteredRoutines =  LogicObject.Get(nameof(FilteredRoutines));
        FilteredRungs =  LogicObject.Get(nameof(FilteredRungs));

        var v = Owner.GetVariable("ItemUIType");
        var pv = v.Value.Value;
        if(pv != null){

            ItemUIType = InformationModel.Get(pv as NodeId);
        }

        UIContainer = LogicObject.GetAlias("UIBody") as Item;

        TagsFolder = Owner.GetAlias(nameof(TagsFolder));

        if(
            FilteredPrograms == null
            || FilteredRoutines == null
            || FilteredRungs == null
            || ItemUIType == null
            || UIContainer == null
            || TagsFolder == null
        ){
            Log.Error(this.GetType().Name,"Initial error");
            return;
        }







        LoadRungFile("d:\\123456789.xml");
   


        v = Owner.GetVariable("Variable1");
        var dlink = v.Get("DynamicLink") as DynamicLink;
        
        v = Owner.GetVariable("Variable2");
        var dlink2 = v.Get("DynamicLink") as DynamicLink;
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
    }



    private void LoadRungFile(string filepath = null)
    {

        try
        {
            if (string.IsNullOrWhiteSpace(filepath))
            {
                doc = LoadFromFile<RungDoc>(RungFilePath);
            }
            else
            {
                doc = LoadFromFile<RungDoc>(filepath);
                RungFilePath = filepath;
                BuildProgramDataSource();
                //FillComboxProgram();
            }
        }catch(Exception ex)
        {
            Log.Error(this.GetType().Name,ex.Message);
        }
        
    }



    [ExportMethod]
    public void Program_SelectChangeHandle(NodeId item){
        var obj = InformationModel.Get(item) as IUAVariable;
        if(obj == null){
            _selectedPrg = string.Empty;
        
            return;
        }
 
        var prg = (string)obj.Value;
        _selectedPrg = prg;
        BuildRoutineDataSource(prg);
    }

    [ExportMethod]
    public void Routine_SelectChangeHandle(NodeId item){
         var obj = InformationModel.Get(item) as IUAVariable;

        if(obj == null){
            _selectedRoutine = string.Empty;
        
            return;
        }


        _selectedRoutine = (string)obj.Value;
        BuildRungDataSource(_selectedPrg,_selectedRoutine);

    }


    [ExportMethod]
    public void Rung_SelectChangeHandle(NodeId item){
         var obj = InformationModel.Get(item) as IUAVariable;
         
        if(obj == null){
            _selectedRung = string.Empty;
        
            return;
        }


        _selectedRung = (string)obj.Value;
        
    }


    [ExportMethod]
    public void OpenRung(){
        LoadRungContent(_selectedPrg,_selectedRoutine,_selectedRung);
    }




    [ExportMethod]
    public void ExOpenRung(string prg,string routine,string rung){
        LoadRungContent(prg,routine,rung);
    }






    /// <summary>
    /// 载入Rung内容
    /// </summary>
    /// <param name="prg"></param>
    /// <param name="routine"></param>
    /// <param name="rung"></param>
    private void LoadRungContent(string prg,string routine,string rung){

        if(_openedPrg == prg
            && _openedRoutine == routine
            && _openedRung == rung
        ){
            return;

        }

        


        //clear ui
        UIContainer.Children.Clear();


        var res = uint.TryParse(rung,out var rung_number);

        if(string.IsNullOrWhiteSpace(prg)
            ||string.IsNullOrWhiteSpace(routine)
            ||string.IsNullOrWhiteSpace(rung)
            || res == false
        ){

        
            return;
        }



        _openedPrg = prg;
        _openedRoutine = routine;
        _openedRung = rung;

        var data = doc.Rungs.Where(r => r.ProgramName == prg && r.RoutineName == routine && r.RungNumber == rung_number).FirstOrDefault();

        if(data == null){
            return;
        }


        foreach(var item in data.InternalSymbols){
            var tagpath = item.isGlobal ? $"Controller Tags.{item.Name}" : $"Program:{prg}.{item.Name}";


            //var tag = TagsFolder.Find(tagpath);
            var tag = FindNodeByNodePath(TagsFolder,tagpath,out var arrayIndex ,out var bitIndex);
            if(tag == null){
                Log.Error(this.GetType().Name,$"tag : {tagpath} is not exists");
                continue;
            }


            var ui = InformationModel.MakeObject(item.Name,ItemUIType.NodeId);
            var model = InformationModel.MakeObject<GOptix_RungSummaryItemModel>(item.Name);

            model.Comment = string.IsNullOrWhiteSpace(item.Comment.Value) ? item.Name : item.Comment.Value;
            
            if(arrayIndex == null){
                if(bitIndex == null){

                    model.ValueVariable.SetDynamicLink(tag as IUAVariable,DynamicLinkMode.Read);
                }else{
                    var dlink = InformationModel.Make<DynamicLink>("DynamicLink");
                    //dlink.Value = tag.NodeId.ToString();
                    dlink.Value = $"{{NodeId:ns={tag.NodeId.NamespaceIndex};{tag.NodeId.IdTypeShortString}={tag.NodeId.Id.ToString()}}}.{bitIndex}";
                    model.ValueVariable.Add(dlink);
                }
            }else{
                model.ValueVariable.SetDynamicLink(tag as IUAVariable,(uint)arrayIndex,DynamicLinkMode.Read);
                
            }

            Log.Info(tagpath);
            ui.Add(model);
            ui.SetAlias("Item",model);

            UIContainer.Add(ui);
        }

    }


    private IUANode FindNodeByNodePath(IUANode root,string path,out uint? arrayIndex,out uint? bitIndex){
        arrayIndex = null;
        bitIndex = null;
        string spliter = ".";
        var paths = path.Split(spliter);
        var node = root;
        foreach(var p in paths){
            if(uint.TryParse(p,out var prefix)){
                bitIndex = prefix;
                break;
            }
            
            string text = p;
            var _path = p;
            int num = text.LastIndexOf('[');
            int num2 = text.LastIndexOf(']');
            if (num == -1 || num2 == -1)
            {
            }else{
                _path = p.Substring(0,num);
                string text2 = text.Substring(num + 1, num2 - num - 1);
                if(uint.TryParse(text2,out var idx)){
                    arrayIndex = idx;
                }
            }


            node = node.Get(_path);
            if(node == null){
                
                return null;
            }

            if(arrayIndex != null){
                break;
            }
        }
        return node;


    }

    private void BuildProgramDataSource(){
        var prgs = doc.Rungs.Select(r=>r.ProgramName).Distinct().ToArray();

        FilteredPrograms.Children.Clear();

        var i=0;
        foreach(var prg in prgs){
            var v = InformationModel.MakeVariable($"i",OpcUa.DataTypes.String);
            
            v.Value = (string)prg;
            FilteredPrograms.Add(v);
            i++;
        }



    }


    private void BuildRoutineDataSource(string prg){
        var rs = doc.Rungs.Where(d => d.ProgramName == prg.ToString()).Select(p => p.RoutineName).Distinct().ToArray();
        FilteredRoutines.Children.Clear();
        var i=0;
        foreach(var r in rs){
            var v = InformationModel.MakeVariable($"i",OpcUa.DataTypes.String);
            v.Value = (string)r;
            FilteredRoutines.Add(v);
            i++;
        }

        _selectedRoutine = string.Empty;
    }


    private void BuildRungDataSource(string prg,string routine){
        var rs = doc.Rungs.Where(d => d.ProgramName == prg.ToString() && d.RoutineName == routine.ToString()).Select(p => p.RungNumber.ToString()).Distinct().ToArray();
        FilteredRungs.Children.Clear();
        var i=0;
        foreach(var r in rs){
            var v = InformationModel.MakeVariable($"i",OpcUa.DataTypes.String);
            v.Value = (string)r;
            FilteredRungs.Add(v);
            i++;
        }

        _selectedRung = string.Empty;
    }

    










    private static T LoadFromFile<T>(string filepath)
    {

        // Construct an instance of the XmlSerializer with the type
        // of object that is being deserialized.
        var mySerializer = new XmlSerializer(typeof(T));
        // To read the file, create a FileStream.
        var myFileStream = new FileStream(filepath, FileMode.Open);
        // Call the Deserialize method and cast to the object type.
        var myObject = (T)mySerializer.Deserialize(myFileStream);

        myFileStream.Close();
        return myObject;
    }

    private static T LoadFromString<T>(string xml_string)
    {
        var mySerializer = new XmlSerializer(typeof(T));
        var fs = new StringReader(xml_string);
        //byte[] byteArray = Encoding.Unicode.GetBytes(xml_string);
        //var fs = new MemoryStream(byteArray);
        var myObject = (T)mySerializer.Deserialize(fs);

        return myObject;
    }



    public class RungDoc
    {
        private List<RungSummary> _rungs = new List<RungSummary>();
        [XmlArray("Rungs")]
        [XmlArrayItem("Rung")]
        public List<RungSummary> Rungs
        {
            get { return _rungs; }
            set { _rungs = value; }
        }

    }

    public class RungSummary
    {
        [XmlAttribute]
        public string ProgramName { get; set; }
        [XmlAttribute]
        public string RoutineName { get; set; }
        [XmlAttribute]
        public uint RungNumber { get; set; }

        private XmlCDATA _code = new XmlCDATA();
        [XmlElement]
        public XmlCDATA Code
        {
            get { return _code; }
            set { _code = value; }
        }

        [XmlArray("symbols")]
        [XmlArrayItem("symbol")]
        public List<Symbol> InternalSymbols { get; set; }


    }

    public class Symbol
    {
        [XmlAttribute]
        public string Name { get; set; }



        private XmlCDATA _comment = new XmlCDATA();
        [XmlElement]
        public XmlCDATA Comment
        {
            get { return _comment; }
            set { _comment = value; }
        }
        [XmlAttribute]
        public bool isGlobal { get; set; }



    }

    [System.Serializable]
    public class XmlCDATA
    {
        private string ValueField;


        [XmlIgnore]
        public string Value
        {
            get => ValueField;
            set
            {

                ValueField = value;


            }

        }


        /// <summary>
        /// 行代码XML导出是CDATA
        /// </summary>
        [XmlText]
        public XmlNode[] CDataContent
        {
            get
            {
                var dummy = new XmlDocument();
                return new XmlNode[] { dummy.CreateCDataSection(ValueField) };
            }
            set
            {
                if (value == null)
                {
                    ValueField = null;
                    return;
                }
                if (value.Length != 1)
                {
                    throw new InvalidOperationException(
                        String.Format(
                            "Invalid array length {0}", value.Length));
                }


                ValueField = value[0].Value;
            }
        }




        public XmlCDATA()
        {
            ValueField = "";

        }

        public XmlCDATA(string value)
        {
            ValueField = value;


        }
    }
}
