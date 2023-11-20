# Optix Toolkit

这个项目是Optix的工具包，包含 可视化组件(Widget),脚本代码(c#)，程序库 和 数据类型

C# 代码的主命名空间为 : **GOptix** .

命名规则：
- 1.所有Widget Type 的前缀为 **Goptix_**
- 2.所有运行时脚本 前缀为 **Goptix_**，后缀为 **RuntimeNetLogic**
- 3.所有设计时脚本 前缀为 **Goptix_**，后缀为 **DesigntimeNetLogic**

Optix Version : **V1.2.0.272**

项目路径 : [github](https://github.com/ren1987yi/Optix-Toolkit)


---

**目录**


- [Optix Toolkit](#optix-toolkit)
	- [0.前言](#0前言)
		- [0.1 自建 Nuget库](#01-自建-nuget库)
			- [添加Nuget库](#添加nuget库)
		- [0.2 引用的库](#02-引用的库)
		- [0.3 项目结构](#03-项目结构)
	- [1.Widgets - 可视化组件](#1widgets---可视化组件)
		- [1.1 GOptix\_RungSummaryViewer](#11-goptix_rungsummaryviewer)
			- [Browse Name](#browse-name)
			- [Script Name](#script-name)
			- [Super Type](#super-type)
			- [参数](#参数)
		- [1.2 GOptix\_TreeView](#12-goptix_treeview)
			- [Browse Name](#browse-name-1)
			- [Script Name](#script-name-1)
			- [Super Type](#super-type-1)
			- [Data Type](#data-type)
			- [参数](#参数-1)
		- [1.3 GOptix\_ModelViewer](#13-goptix_modelviewer)
			- [Browse Name](#browse-name-2)
			- [Script Name](#script-name-2)
			- [Super Type](#super-type-2)
			- [参数](#参数-2)
		- [1.4 GOptix\_CalendarMonth](#14-goptix_calendarmonth)
			- [Browse Name](#browse-name-3)
			- [Script Name](#script-name-3)
			- [Super Type](#super-type-3)
			- [参数](#参数-3)
		- [1.5 GOptix\_ObjectViewer](#15-goptix_objectviewer)
			- [Browse Name](#browse-name-4)
			- [Script Name](#script-name-4)
			- [Super Type](#super-type-4)
			- [参数](#参数-4)
		- [1.6 GOptix\_Dlg\_VariableBrowser](#16-goptix_dlg_variablebrowser)
			- [Browse Name](#browse-name-5)
			- [Script Name](#script-name-5)
			- [Super Type](#super-type-5)
			- [Data Type](#data-type-1)
			- [参数](#参数-5)
		- [1.7 GOptix\_CarouselLoader](#17-goptix_carouselloader)
			- [Browse Name](#browse-name-6)
			- [Script Name](#script-name-6)
			- [Super Type](#super-type-6)
			- [Data Type](#data-type-2)
			- [参数](#参数-6)
		- [1.8 GOptix\_BarcodeViewer](#18-goptix_barcodeviewer)
			- [Browse Name](#browse-name-7)
			- [Script Name](#script-name-7)
			- [Super Type](#super-type-7)
			- [Data Type](#data-type-3)
			- [参数](#参数-7)
		- [1.9 GOptix\_QRcodeViewer](#19-goptix_qrcodeviewer)
			- [Browse Name](#browse-name-8)
			- [Script Name](#script-name-8)
			- [Super Type](#super-type-8)
			- [Data Type](#data-type-4)
			- [参数](#参数-8)
		- [1.10 GOptix\_EchartViewer](#110-goptix_echartviewer)
			- [Browse Name](#browse-name-9)
			- [Script Name](#script-name-9)
			- [Super Type](#super-type-9)
			- [Data Type](#data-type-5)
			- [参数](#参数-9)
		- [1.11 GOptix\_VerticalCollection / GOptix\_HorizontalCollection](#111-goptix_verticalcollection--goptix_horizontalcollection)
			- [Browse Name](#browse-name-10)
			- [Script Name](#script-name-10)
			- [Super Type](#super-type-10)
			- [Data Type](#data-type-6)
			- [参数](#参数-10)
		- [1.12 GOptix\_GridCollection](#112-goptix_gridcollection)
			- [Browse Name](#browse-name-11)
			- [Script Name](#script-name-11)
			- [Super Type](#super-type-11)
			- [Data Type](#data-type-7)
			- [参数](#参数-11)
		- [1.13 GOptix\_MML\_Viewer](#113-goptix_mml_viewer)
			- [Browse Name](#browse-name-12)
			- [Script Name](#script-name-12)
			- [Super Type](#super-type-12)
			- [Data Type](#data-type-8)
			- [参数](#参数-12)
		- [1.14 GOptix\_PanelLoader](#114-goptix_panelloader)
			- [Browse Name](#browse-name-13)
			- [Script Name](#script-name-13)
			- [Super Type](#super-type-13)
			- [Data Type](#data-type-9)
			- [参数](#参数-13)
		- [1.15 GOptix\_FavoriteButton](#115-goptix_favoritebutton)
			- [Browse Name](#browse-name-14)
			- [Script Name](#script-name-14)
			- [Super Type](#super-type-14)
			- [Data Type](#data-type-10)
			- [参数](#参数-14)
			- [用法](#用法)
		- [1.16 GOptix\_FavoritesViewer](#116-goptix_favoritesviewer)
			- [Browse Name](#browse-name-15)
			- [Script Name](#script-name-15)
			- [Super Type](#super-type-15)
			- [Data Type](#data-type-11)
			- [参数](#参数-15)
	- [2 脚本](#2-脚本)
		- [2.1 随机数发生器](#21-随机数发生器)
			- [名称](#名称)
			- [参数](#参数-16)
	- [3 高级报表](#3-高级报表)
		- [3.1 流程逻辑](#31-流程逻辑)
		- [3.2 关键代码](#32-关键代码)
			- [Python](#python)
		- [Template To Word](#template-to-word)
			- [Word To PDF](#word-to-pdf)






---

## 0.前言

### 0.1 自建 Nuget库 

地址: http://52.130.64.23:50109/nuget

![](doc/my_nuget.png)



#### 添加Nuget库

微软官方流程 :https://learn.microsoft.com/zh-cn/nuget/consume-packages/install-use-packages-visual-studio

---

### 0.2 引用的库

|名称|版本|说明|
|--|--|--|
|OptixHelper|>=0.0.1.3|一个Optix的帮助库|
|GFlow.OptixWrapper|>=1.0.65|一个基于Optix的工作流引擎|
|GFlow.Steps|>=1.0.66|一个基于Optix的工作流引擎组件|
|MagneMotion.DSL|>=0.0.1|MagneMotion 的DSL组件|


---
### 0.3 项目结构

- **Optix_Toolkit** (Project Main Folder)
	- **Nodes** (Design file)
		- **UI**
			- **MainWindow**
			- **Screens**
				- **NativeScreen** : 本地呈现的窗体
			- **Widgets** : 存放所有可视化部件
			- **Test**	: 存放可浏览的画面
		- **Model**
			- **DataTypes** : 公用数据类型
			- **Data**	: 数据
			- **UA** : OPC UA Server 发布根节点
	- **ProjectFiles** (Resource)
		- **images** : 图片
		- **ICT** : ICT / MML 相关图片
		- **StaticHtml** : 借助WEB技术实现的功能的固定页面
---

## 1.Widgets - 可视化组件



### 1.1 GOptix_RungSummaryViewer

梯形图预览，可根据 Program name,Routine name,Rung Number ，检索到梯形图的内容。
并把阶梯内所有引用的标签和状态显示在画面中。
每个标签和状态的显示元素，可自定义。

![](doc/rungviewer.png)

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

![](doc/treeview.png)

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

![](doc/3dviewer.png)

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

![](doc/calendar.png)


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

![](doc/modelviewer.png)


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

![](doc/variable_browser.png)




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

![](doc/barcode.png)

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

![](doc/qrcode.png)

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


![](doc/echart.png)

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

![](doc/vlcollection.png)

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

![](doc/gridcollection.png)


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

![](doc/mml.png)




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

![](doc/panelloader.png)

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

### 1.15 GOptix_FavoriteButton

可视化部件收藏按钮，通过点击按钮，对感兴趣的部件进行收藏，并可在收藏夹中集中查看

![](doc/favorite_button.png)

对应收藏的两种状态: 无收藏 / 收藏，组件内有两个图片控件进行切换显示。


注：
为了保证收藏夹内容能保存，需要把收藏夹节点加入到 **Retentivity** 中.

![](doc/favorite_storage.png)



#### Browse Name
GOptix_FavoriteButton 


#### Script Name
GOptix_FavoriteButton_RuntimeNetLogic


#### Super Type
Panel

#### Data Type
无

#### 参数

|Name|DataType|Description|
|--|--|--|
|Favorites|Alias|保存收藏夹内容的父节点,必须为 **`Object`** |
|IsAdded|bool|此部件是否已经收藏|

#### 用法

`如下图所示：`

![](doc/favorite_case1.png)

1. 建立自定义的可视化部件 :**_NumEditor**

2. 在 **_NumEditor** 中的 第1层内加入 **收藏按钮 (FavoriteButton)**

3. 定义好 **收藏按钮** 中的参数 **Favorites**




`例子：`
![](doc/favorite_case2.png)



---


### 1.16 GOptix_FavoritesViewer

收藏夹的查看器，可清除收藏夹 和 刷新

![](doc/favorite_case3.png)

上图是显示已收藏的所有可视化组件，并自动完成部件内所有自定义变量的赋值。


注：
为了保证收藏夹内容能保存，需要把收藏夹节点加入到 **Retentivity** 中.

![](doc/favorite_storage.png)



#### Browse Name
GOptix_FavoritesViewer 


#### Script Name
GOptix_FavoritesViewer_RuntimeLogic


#### Super Type
Panel

#### Data Type
无

#### 参数

|Name|DataType|Description|
|--|--|--|
|Favorites|Alias|保存收藏夹内容的父节点,必须为 **`Object`** |








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





---

## 3 高级报表 

这个目前针对 Optix 中 Report 功能不全的暂时方案

![](doc/report_x1.png)


工具链详细介绍:
1. request_html : https://github.com/psf/requests-html 
	离线渲染HTML,并获取HTML内容

2. MiniWord : https://github.com/mini-software/MiniWord
	MiniWord .NET Word模板引擎，藉由Word模板和数据简单、快速生成文件。

3. LibreOffice : LibreOffice 便携版

	命令行帮助

```BASH
	C:\Users\liqiang>
LibreOffice 6.0.6.2 0c292870b25a325b5ed35f6b45599d2ea4458e77

Usage: soffice [argument...]
       argument - switches, switch parameters and document URIs (filenames).

Using without special arguments:
Opens the start center, if it is used without any arguments.
   {file}              Tries to open the file (files) in the components
                       suitable for them.
   {file} {macro:///Library.Module.MacroName}
                       Opens the file and runs specified macros from
                       the file.

Getting help and information:
   --help | -h | -?    Shows this help and quits.
   --helpwriter        Opens built-in or online Help on Writer.
   --helpcalc          Opens built-in or online Help on Calc.
   --helpdraw          Opens built-in or online Help on Draw.
   --helpimpress       Opens built-in or online Help on Impress.
   --helpbase          Opens built-in or online Help on Base.
   --helpbasic         Opens built-in or online Help on Basic scripting
                       language.
   --helpmath          Opens built-in or online Help on Math.
   --version           Shows the version and quits.
   --nstemporarydirectory
                       (MacOS X sandbox only) Returns path of the temporary
                       directory for the current user and exits. Overrides
                       all other arguments.

General arguments:
   --quickstart[=no]   Activates[Deactivates] the Quickstarter service.
   --nolockcheck       Disables check for remote instances using one
                       installation.
   --infilter={filter} Force an input filter type if possible. For example:
                       --infilter="Calc Office Open XML"
                       --infilter="Text (encoded):UTF8,LF,,,"
   --pidfile={file}    Store soffice.bin pid to {file}.
   --display {display} Sets the DISPLAY environment variable on UNIX-like
                       platforms to the value {display} (only supported by a
                       start script).

User/programmatic interface control:
   --nologo            Disables the splash screen at program start.
   --minimized         Starts minimized. The splash screen is not displayed.
   --nodefault         Starts without displaying anything except the splash
                       screen (do not display initial window).
   --invisible         Starts in invisible mode. Neither the start-up logo nor
                       the initial program window will be visible. Application
                       can be controlled, and documents and dialogs can be
                       controlled and opened via the API. Using the parameter,
                       the process can only be ended using the taskmanager
                       (Windows) or the kill command (UNIX-like systems). It
                       cannot be used in conjunction with --quickstart.
   --headless          Starts in "headless mode" which allows using the
                       application without GUI. This special mode can be used
                       when the application is controlled by external clients
                       via the API.
   --norestore         Disables restart and file recovery after a system crash.
   --safe-mode         Starts in a safe mode, i.e. starts temporarily with a
                       fresh user profile and helps to restore a broken
                       configuration.
   --accept={UNO-URL}  Specifies an UNO-URL connect-string to create an UNO
                       acceptor through which other programs can connect to
                       access the API. UNO-URL is string the such kind
                   uno:connection-type,params;protocol-name,params;ObjectName.
   --unaccept={UNO-URL} Closes an acceptor that was created with --accept. Use
                       --unaccept=all to close all open acceptors.
   --language={lang}   Uses specified language, if language is not selected
                       yet for UI. The lang is a tag of the language in IETF
                       language tag.

Developer arguments:
   --terminate_after_init
                       Exit after initialization complete (no documents loaded).
   --eventtesting      Exit after loading documents.

New document creation arguments:
The arguments create an empty document of specified kind. Only one of them may
be used in one command line. If filenames are specified after an argument,
then it tries to open those files in the specified component.
   --writer            Creates an empty Writer document.
   --calc              Creates an empty Calc document.
   --draw              Creates an empty Draw document.
   --impress           Creates an empty Impress document.
   --base              Creates a new database.
   --global            Creates an empty Writer master (global) document.
   --math              Creates an empty Math document (formula).
   --web               Creates an empty HTML document.

File open arguments:
The arguments define how following filenames are treated. New treatment begins
after the argument and ends at the next argument. The default treatment is to
open documents for editing, and create new documents from document templates.
   -n                  Treats following files as templates for creation of new
                       documents.
   -o                  Opens following files for editing, regardless whether
                       they are templates or not.
   --pt {Printername}  Prints following files to the printer {Printername},
                       after which those files are closed. The splash screen
                       does not appear. If used multiple times, only last
                       {Printername} is effective for all documents of all
                       --pt runs. Also, --printer-name argument of
                       --print-to-file switch interferes with {Printername}.
   -p                  Prints following files to the default printer, after
                       which those files are closed. The splash screen does
                       not appear. If the file name contains spaces, then it
                       must be enclosed in quotation marks.
   --view              Opens following files in viewer mode (read-only).
   --show              Opens and starts the following presentation documents
                       of each immediately. Files are closed after the showing.
                       Files other than Impress documents are opened in
                       default mode , regardless of previous mode.
   --convert-to OutputFileExtension[:OutputFilterName]
     [--outdir output_dir] [--convert-images-to]
                       Batch convert files (implies --headless). If --outdir
                       isn't specified, then current working directory is used
                       as output_dir. If --convert-images-to is given, its
                       parameter is taken as the target MIME format for *all*
                       images written to the output format. If --convert-to is
                       used more than once, the last value of OutputFileExtension
                       [:OutputFilterName] is effective. If --outdir is used more
                       than once, only its last value is effective. For example:
                   --convert-to pdf *.odt
                   --convert-to epub *.doc
                   --convert-to pdf:writer_pdf_Export --outdir /home/user *.doc
                   --convert-to "html:XHTML Writer File:UTF8" *.doc
                   --convert-to "txt:Text (encoded):UTF8" *.doc
   --print-to-file [--printer-name printer_name] [--outdir output_dir]
                       Batch print files to file. If --outdir is not specified,
                       then current working directory is used as output_dir.
                       If --printer-name or --outdir used multiple times, only
                       last value of each is effective. Also, {Printername} of
                       --pt switch interferes with --printer-name.
   --cat               Dump text content of the following files to console
                       (implies --headless). Cannot be used with --convert-to.
   --script-cat        Dump text content of any scripts embedded in the files to console
                       (implies --headless). Cannot be used with --convert-to.
   -env:<VAR>[=<VALUE>] Set a bootstrap variable. For example: to set
                       a non-default user profile path:
                       -env:UserInstallation=file:///tmp/test

Ignored switches:
   -psn                Ignored (MacOS X only).
   -Embedding          Ignored (COM+ related; Windows only).
   --nofirststartwizard Does nothing, accepted only for backward compatibility.
   --protector {arg1} {arg2}
                       Used only in unit tests and should have two arguments.
```


### 3.1 流程逻辑

1. 使用 Python , Request_html 库 对图表进行渲染，等到图表的 **SVG** 的文件，保存在本地
2. 使用 MiniWord，结合 **Word 模板** ，进行最终报表的渲染
3. 使用 LibreOffice 的命令行工具，把 Word 文件 输出为 PDF 文件

### 3.2 关键代码

#### Python 

```python
# 获取 图表 格式为 svg
import sys,os

from requests_html import HTMLSession,UserAgent


'''
argv
0: python file
1: render url
2: svg output absolut path
3: fix svg format
'''
if __name__ == '__main__':


    _count = len(sys.argv)
    filename = ''
    url = 'http://127.0.0.1:5501/echart_ssr.html'

    fixSvg = False

    out_file=''
    if _count > 0:
        filename = sys.argv[0]

    if _count > 1:
        url = sys.argv[1]
        pass

    if _count > 2:
        out_file = sys.argv[2]

    if _count > 3:
        fixSvg = True


    print('filename"',filename)
    print('url:',url)
    print('out_file:',out_file)


    session = HTMLSession()
    user_agent = UserAgent().random
    header = {"User-Agent": user_agent}
    r = session.get(url,headers=header)

    r.html.render()  # 首次使用，自动下载chromium


    x = r.html.xpath("//svg")

    _svg = ''
    for _ in x:
        # print(_.html)
        if fixSvg:
            el = _.element

            vbox = el.attrib['viewbox']
            vboxs = vbox.split(' ')


            width = el.attrib['width']

            el.attrib['width'] = vboxs[2]
            el.attrib['height'] = vboxs[3]
            el.attrib['xmlns'] = "http://www.w3.org/2000/svg"
        _svg = _.html
        break

    print(_svg)




    if out_file != '':
        with open(out_file, 'w') as f:
            f.write(_svg)
        print("save svg")
    pass

```


### Template To Word

```csharp

	//查询数据库 并 构建 word 渲染值
 	private object QueryAndBuildValue()
    {
       
        var st = (DateTime)StartTime.Value.Value;
        var et = (DateTime)EndTime.Value.Value;


        var _st = st.ToString("yyyy-MM-ddTHH:mm:ss");
        var _et = et.ToString("yyyy-MM-ddTHH:mm:ss");


        var store = Project.Current.GetObject("DataStores").Get<Store.Store>("LocalDB");
        var logger = Project.Current.GetObject("Loggers/DataLogger1") as DataLogger;
        var sql = @$"select LocalTimestamp as DT,V1,V2,V3 from DataLogger1 where LocalTimestamp >= '{_st}' and LocalTimestamp <= '{_et}' order by LocalTimestamp";
        Log.Info(sql);
       

        var res = StoreHelpr.Query(store, sql);


    
        var s2 = res;

        sql = @$"select 
        MAX(V1) AS maxV1
        ,MIN(V1) AS minV1
        ,MAX(V2) AS maxV2
        ,MIN(V2) AS minV2
        ,MAX(V3) AS maxV3
        ,MIN(V3) AS minV3
         from DataLogger1 where LocalTimestamp >= '{_st}' and LocalTimestamp <= '{_et}' order by LocalTimestamp";
        res = StoreHelpr.Query(store, sql);

        var s1 = new List<Dictionary<string, object>>();

        s1.Add(new Dictionary<string, object>(){
            { "Name","V1"},
            { "MaxValue",res.First()["maxV1"]},
            { "MinValue",res.First()["minV1"]},
        });

        s1.Add(new Dictionary<string, object>(){
            { "Name","V2"},
            { "MaxValue",res.First()["maxV2"]},
            { "MinValue",res.First()["minV2"]},
        });

        s1.Add(new Dictionary<string, object>(){
            { "Name","V3"},
            { "MaxValue",res.First()["maxV3"]},
            { "MinValue",res.First()["minV3"]},
        });



        
        var option_path = @"D:\Work\Optix\Optix_Toolkit\ProjectFiles\StaticHtml\chartdata\options\echart_ssr_data.js";
        EChartTrend.SaveSSROption(store,logger,st,et,new string[]{"V1","V3","V4"},option_path); //保存 echart 的Option
        GenerateTrend("d:\\aaa.svg",900,540); //服务端渲染SVG


        var qr_url = @$"http://127.0.0.1/OPtixWeb/qrcode_viewer.html?value={DateTime.Now}f&useSVG=true";
        ServerSiderRenderSVG(qr_url,@"d:\aaa1.svg",true); //服务端渲染SVG



        var value = new Dictionary<string, object>()
        {
            ["StartTime"] = st,
            ["EndTime"] = et,
            ["S1"] = s1,
            ["S2"] = s2,
            ["img"] = new MiniWordPicture() { Path = @"d:\aaa.svg", Width = 900, Height = 540 },
            ["qrcode"] = new MiniWordPicture() { Path = @"d:\aaa1.svg", Width = 200, Height = 200 }
        };



        return value;
    }


    /// <summary>
    /// 生成word
    /// </summary>
    /// <param name="tmpFilepath">模板文件</param>
    /// <param name="optFilepath">输出路径</param>
    /// <param name="value">渲染值</param>
    private void GenerateWord(string tmpFilepath, string optFilepath, object value)
    {
        MiniWord.SaveAsByTemplate(optFilepath, tmpFilepath, value);
    }


```


#### Word To PDF

```csharp


    /// <summary>
    /// Word Convert to PDF
    /// </summary>
    /// <param name="wordfile">word file</param>
    /// <param name="outFolder">输出目录</param>
    private int Word2Pdf(string wordfile, string outFolder)
    {
        string libreOfficePath = @"D:\Download\Software\LibreOfficePortablePrevious\App\libreoffice\program\soffice.exe";

        // FIXME: file name escaping: I have not idea how to do it in .NET.
        ProcessStartInfo procStartInfo = new ProcessStartInfo(libreOfficePath, string.Format("--headless --convert-to pdf --nologo \"{0}\" --outdir \"{1}\"", wordfile, outFolder));
        procStartInfo.RedirectStandardOutput = true;
        procStartInfo.UseShellExecute = false;
        procStartInfo.CreateNoWindow = true;
        // procStartInfo.WorkingDirectory = Environment.CurrentDirectory;

        Process process = new Process() { StartInfo = procStartInfo, };
        process.Start();
        process.WaitForExit();

        // Check for failed exit code.
        if (process.ExitCode != 0)
        {
            Log.Error(string.Format("LibreOffice has failed with {0}", process.ExitCode));
            return process.ExitCode;
            // throw new LibreOfficeFailedException(process.ExitCode);
        }else{
            return 0;
        }
    }

```
