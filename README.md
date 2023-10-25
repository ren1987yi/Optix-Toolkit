# Optix Toolkit

这个项目是Optix的工具包，包含 可视化组件(Widget),脚本代码(c#)，程序库 和 数据类型

C# 代码的主命名空间为 : **GOptix** .

命名规则：
- 1.所有Widget Type 的前缀为 **Goptix_**
- 2.所有运行时脚本 前缀为 **Goptix_**，后缀为 **RuntimeNetLogic**
- 3.所有设计时脚本 前缀为 **Goptix_**，后缀为 **DesigntimeNetLogic**

Optix Version : **V1.2.0.272**

项目路径 : [github](https://github.com/ren1987yi/Optix-Toolkit)


## Catalog

- 0.前言
- 1.Widgets
- 2.脚本
- 3.代码库


## 0.前言



### 0.1 自建 Nuget库 
	地址: http://52.130.64.23:50109/nuget

![](doc\my_nuget.png)



**添加流程：**

官方流程 :https://learn.microsoft.com/zh-cn/nuget/consume-packages/install-use-packages-visual-studio

1.若要更改 Visual Studio 从中加载包元数据的源，请从 包源 选择器中选择源。
![](https://learn.microsoft.com/zh-cn/nuget/consume-packages/media/package-source-selector.png)

2.若要管理包源，请选择“设置”图标或选择“工具>选项”。
![](https://learn.microsoft.com/zh-cn/nuget/consume-packages/media/package-source-settings.png)

3.在 “选项” 窗口中，展开 NuGet 包管理器 节点，然后选择 “包源”。
![](https://learn.microsoft.com/zh-cn/nuget/consume-packages/media/package-sources.png)

4.要添加源，请选择 +，编辑名称，在“源”控件中输入 URL 或路径，然后选择“更新”。

源现在显示在 “包源 ”下拉列表中。

5.若要更改包源，请选中它，在“名称”和“源”框中进行编辑，然后选择“更新”。

6.若要禁用包源，请清除列表中名称左侧的框。

7.要删除包源，请选中它，然后选择“X”按钮。

如果在删除某个包源后，该包源重新出现，则它可能会列在计算机级或用户级 文件中。 有关这些文件的位置，请参阅 通用 NuGet 配置。 通过手动编辑包源或使用 nuget 源命令删除文件中的包源。

---

### 0.2 引用的库

|名称|版本|说明|
|--|--|--|
|OptixHelper|>=0.0.1.3|一个Optix的帮助库|
|GFlow.OptixWrapper|>=1.0.65|一个基于Optix的工作流引擎|
|GFlow.Steps|>=1.0.66|一个基于Optix的工作流引擎组件|
|MagneMotion.DSL|>=0.0.1|MagneMotion 的DSL组件|


---

## 1.Widgets - 可视化组件



### 1.1 GOptix_RungSummaryViewer

梯形图预览，可根据 Program name,Routine name,Rung Number ，检索到梯形图的内容。
并把阶梯内所有引用的标签和状态显示在画面中。
每个标签和状态的显示元素，可自定义。

![](doc\rungviewer.png)

梯形图信息构建工具地址 ： [在线工具](http://52.130.64.23:8078/ab/routine_summary)


#### Browse Name
GOptix_RungSummaryViewer

#### Script Name
GOptix_RungSummaryViewer_RuntimeNetLogic

#### Super Type
Panel

#### 参数

|Name|DataType|Description|
|--|--|--|
|RungFilepath|ResourceUri|梯形图信息文件路径|
|TriigerLoadRungFile|bool|加载梯形图文件的触发变量，变化时触发|
|ItemUIType|Alias|每个标签的显示UI|
|TagsFolder|Alias|实际标签值的来源，主要链接到驱动的总节点|


---

### 1.2 GOptix_TreeView

树形控件
![](doc\treeview.png)

#### Browse Name
GOptix_TreeView

#### Script Name
GOptix_TreeView_RuntimeNetLogic

#### Super Type
Panel

#### Data Type
GOptix_Type_TreeNode

- Text	: 显示文本
- Image	: 显示图标
- Tag : 链接的实体节点
- Path : 路径
- Nodes : 子节点

#### 参数

|Name|DataType|Description|
|--|--|--|
|Model|Alias|树形结构数据节点|
|ClickedNode|NodePointer|用户点击的树节点|
|SelectedNode|NodePointer|选中的树节点|
|SelectedTag|NodePointer|选中的树节点中的附带标签|
|colorNormal|Color|树节点平时的背景色|
|colorSelect|Color|树节点选中的背景色|
|colorArrow|Color|箭头的颜色|
|TriggerRefresh|bool|刷新的触发，变化时触发|
|TreeNodeUIType|Alias|树节点的可视化的类型，默认：GOptix_TreeViewNode|

---

### 1.3 GOptix_ModelViewer

3维模型查看，通过使用浏览器控件，浏览web页面。web页面使用 threejs库，渲染模型。
可支持 缩放、自动旋转、线框模式等功能特性。

![](doc\3dviewer.png)

#### Browse Name
GOptix_ModelViewer

#### Script Name
无

#### Super Type
Web browser

#### 参数

|Name|DataType|Description|
|--|--|--|
|ModelName|string|模型的名称|
|Scale|float|模型的缩放比例|
|Wireframe|bool|线框模式|
|RootUrl|string|浏览的主地址，必须时 http / https|
|BackgourndColor|string|场景的背景色|


---

### 1.4 GOptix_CalendarMonth

月日历控件,点击单元格，选中日期。可跳转到今天，前后一个月，前后一年

![](doc\calendar.png)


#### Browse Name
GOptix_CalendarMonth

#### Script Name
GOptix_CalendarMonth_RuntimeNetLogic

#### Super Type
Panel

#### 参数

|Name|DataType|Description|
|--|--|--|
|Model|Alias|数据集合，可用于每一天呈现时进行检索|
|UIDayType|Alias|每一天的UI类别|
|SelectedDateTime|Datetime|选中的日期|
|UICurrentDateTimeString|string|UI选中的当前日期字符串|


---

### 1.5 GOptix_ObjectViewer

节点查看器，可根据节点的数据结构，通过树形控件逐层展开。选中某个节点后，可查看节点内的元素(Variable)，并可修改。

![](doc\modelviewer.png)


#### Browse Name
GOptix_ObjectViewer

#### Script Name
GOptix_ObjectViewer_RuntimeNetLogic

#### Super Type
Panel

#### 参数

|Name|DataType|Description|
|--|--|--|
|Object|Alias|查看的节点|

---

### 1.6 GOptix_Dlg_VariableBrowser

变量查看对话框，通过树形控件查看节点内的变量，并可在选中变量后，把选中的变量信息，反馈到调用方。

![](doc\variable_browser.png)




#### Browse Name
GOptix_Dlg_VariableBrowser

#### Script Name
GOptix_Dlg_VariableBrowser_RuntimeNetLogic

#### Super Type
Dialog

#### Data Type
GOptix_Type_VariableBrowserParameter

- Model : 需要查看变量的节点
- TargetNodePoint : 反馈变量节点的目标地址



#### 参数

|Name|DataType|Description|
|--|--|--|
|Parameter|Alias|窗体弹出时的参数|

---

### 1.7 GOptix_CarouselLoader

轮播的画面加载器

#### Browse Name
GOptix_CarouselLoader

#### Script Name
GOptix_CarouselLoader_RuntimeNetLogic

#### Super Type
Panel Loader

#### Data Type
无

#### 参数

|Name|DataType|Description|
|--|--|--|
|Period|int|轮播周期 ms|
|Playlist|Alias|需要轮播画面的父节点，程序会把这个节点下所有的可播放UI 定时切换显示|


---

### 1.8 GOptix_BarcodeViewer

条码查看器，通过浏览器，加载条码生成器。参数通过URL传递

![](doc\barcode.png)

#### Browse Name
GOptix_BarcodeViewer

#### Script Name
GOptix_BarcodeViewer_RuntimeNetLogic

#### Super Type
Web browser

#### Data Type
无

#### 参数

|Name|DataType|Description|
|--|--|--|
|_format|string|条码格式 `auto`|
|_width|string|宽度 `number`|
|_height|string|高度 `number`|
|_value|string|条码值|
|_displayValue|string|是否显示数值 `true`/`false`|
|_font|string|字体 `monospace`|
|_textAlign|string|数值对齐 `center`|
|_textPosition|string|数值位置 `bottom`|
|_textMargin|string| 默认值`2`|
|_fontSize|string|字体大小 `20`|
|_background|string|背景色 `#ffffff` css color|
|_lineColor|string|前景色 `#000000` css color|
|_margin|string|`10`|
|RootPath|ResourceUri||
|TriggerRefresh|bool|刷新的触发变量|

---


### 1.9 GOptix_QRcodeViewer

二维码查看器，通过浏览器，加载二维码码生成器。参数通过URL传递

![](doc\qrcode.png)

#### Browse Name
GOptix_QRcodeViewer

#### Script Name
GOptix_QRcodeViewer_RuntimeNetLogic

#### Super Type
Web browser

#### Data Type
无

#### 参数

|Name|DataType|Description|
|--|--|--|
|_width|string|宽度 `number`|
|_height|string|高度 `number`|
|_value|string|条码值|
|_displayValue|string|是否显示数值 `true`/`false`|
|_dark|string|背景色 `#ffffff` css color|
|_light|string|前景色 `#000000` css color|
|RootPath|ResourceUri||
|TriggerRefresh|bool|刷新的触发变量|


---


### 1.10 GOptix_EchartViewer

echart 查看器，通过URL传递数据，echart option 数据编码为base64


![](doc\echart.png)

#### Browse Name
GOptix_EchartViewer

#### Script Name
GOptix_EchartViewer_RuntimeNetLogic

#### Super Type
Web browser

#### Data Type
无

#### 参数

|Name|DataType|Description|
|--|--|--|
|RootPath|ResourceUri|echart 主文件路径|
|BackgoundColor|string|背景色|
|Blob|string|echart option (js 对象)，经过base64编码的数据|




*使用例子:*

```C#

var template = @"
        {
	xAxis: {
	  type: 'category',
	  data: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun']
	},
	yAxis: {
	  type: 'value'
	},
	series: [
	  {
		data: [(%1%)],
		type: 'bar',
		showBackground: true,
		color:'red',
		backgroundStyle: {
		  color: 'rgba(180, 180, 180, 0.2)'
		}
	  }
	]
  }
  ";

  void OnChangeEchart(){
	var vals = new List<string>();
	for(var i=0;i<10;i++){
		vals.Add(rnd.Next(100).ToString());
	}

	var _val = string.Join(",",vals);
	var _blob = template.Replace("(%1%)",_val);
	var blob = Base64Encrypt(_blob);
	//set echart weidget blob variable


  }


	/// <summary>
	/// Base64编码，采用utf8编码
	/// </summary>
	/// <param name="strPath">待编码的明文</param>
	/// <returns>Base64编码后的字符串</returns>
	public  string Base64Encrypt(string strPath)
	{
		string returnData;
		System.Text.Encoding encode = System.Text.Encoding.UTF8;
		byte[] bytedata = encode.GetBytes(strPath);
		try
		{
			returnData = Convert.ToBase64String(bytedata, 0, bytedata.Length);
		}
		catch
		{
			returnData = strPath;
		}
		return returnData;
	}


```


---



### 1.11 GOptix_VerticalCollection / GOptix_HorizontalCollection

可根据主数据节点内元素的个数，自动填充控件，并完成数据绑定。主数据节点内元素变更时自动更新界面UI

- VerticalCollection : 垂直排布
- HorizontalCollection : 水平排布

![](doc\vlcollection.png)

#### Browse Name
GOptix_VerticalCollection / GOptix_HorizontalCollection


#### Script Name
GOptix_VerticalCollection_RuntimeNetLogic
GOptix_HorizontalCollection_RuntimeNetLogic

#### Super Type
Panel

#### Data Type
无

#### 参数

|Name|DataType|Description|
|--|--|--|
|Model|Alias|主数据节点|
|UIType|Alias|自动填充的UI类型|
|RowOffset|int|行距离|
|ColumnOffset|int|列距离|

---

### 1.12 GOptix_GridCollection

可根据主数据节点内元素的个数，自动填充控件，并完成数据绑定。主数据节点内元素变更时自动更新界面UI。排布方式为 二维表格



![](doc\gridcollection.png)


#### Browse Name
GOptix_GridCollection 


#### Script Name
GOptix_GridCollection_RuntimeNetLogic


#### Super Type
Panel

#### Data Type
无

#### 参数

|Name|DataType|Description|
|--|--|--|
|Model|Alias|主数据节点|
|UIType|Alias|自动填充的UI类型|
|RowOffset|int|行距离|
|ColumnOffset|int|列距离|
|ColumnCount|int|列个数|
|FillMode|int|垂直方向填满 ?|
|RowHeight|int|行高度 -1为自动|


---

### 1.13 GOptix_MML_Viewer

MagneMotion 查看器，可监视车辆，路径，电机。轨道坐标经过 mmtrk文件转化后得出，转换工具路径 :[在线工具](http://52.130.64.23:8078/ab/mmltoui).

车辆位置的关键参数 
 - PathId (int)
 - Position (float)

![](doc\mml.png)




#### Browse Name
GOptix_MML_Viewer 


#### Script Name
GOptix_MML_Viewer_RuntimeNetLogic


#### Super Type
Panel

#### Data Type
GOptix_Type_MML_VehicleStatus

- PathId
- Position
- ID
- Index
- Location

#### 参数

|Name|DataType|Description|
|--|--|--|
|TrackFile|string|轨道文件路径|
|MotorModel|Alias||
|VehicleModel|Alias||
|PathModel|Alias||
|SelectedItem|NodePointer||
|ToolbarVisible|bool|工具栏可见性|
|Motor250mmType|Alias||
|Motor1000mmType|Alias||
|MotorCurveLeftType|Alias||
|MotorCurveRightmmType|Alias||
|MotorSwitchLeftmmType|Alias||
|MotorSwitchRightType|Alias||
|VehicleType|Alias||
|StatePullingTime|int|状态更新周期 ms|

---

### 1.14 GOptix_PanelLoader

根据Optix 设计时的目录结构，自动完成导航侧边栏的建立，选中后显示对应的画面 Panel

![](doc\panelloader.png)

每个panel 需定义两个属性：
- Icon : 显示的图标
- Display Name : 显示的文本信息


#### Browse Name
GOptix_PanelLoader 


#### Script Name
GOptix_PanelLoader_RuntimeNetLogic


#### Super Type
Panel

#### Data Type
无

#### 参数

|Name|DataType|Description|
|--|--|--|
|Panels|Alias|需要导航的Panel 父节点|

---

## 2 脚本

### 2.1 随机数发生器


#### 名称
GOptix_RandomVariable_RuntimeLogic

脚本对Owner中的所有Variable，进行随机数计算。范围默认为 0 - 100.如果变量配置了 Range,则用 Range 中的范围。


#### 参数
|Name|DataType|Description|
|--|--|--|
|Period|int|随机变换周期|
