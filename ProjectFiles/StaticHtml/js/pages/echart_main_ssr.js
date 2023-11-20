
function RenderSVG(chart,domsvg){
	var svgStr = chart.renderToSVGString();
	domsvg.innerHTML = svgStr;
	
	
	chart.dispose();
	chart = null;
}


window.onload = () => {

	// var a = "{a:1,b:2}";

	// console.log(looseJsonParse("{a:(4-1), b:function(){}, c:new Date()}"));
	// console.log(looseJsonParse(a));

	const default_width = 1024;
	const default_height = 768;
	var data_mode = "file";
	//get query parameter
	var url_string = window.location.href;

	var _mode = getQueryString(url_string, "m");//data mode
	if (_mode != undefined) {
		data_mode = _mode;
	}



	var width = default_width;
	var height = default_height;

	var _width = getQueryString(url_string, "w");
	if (_width != undefined) {
		var _ = parseInt(_width);
		if (!isNaN(_)) {
			width = _;
		}
	}

	var _height = getQueryString(url_string, "h");
	if (_height != undefined) {
		var _ = parseInt(_height);
		if (!isNaN(_)) {
			height = _;
		}
	}







	// 基于准备好的dom，初始化echarts实例

	var myChart = echarts.init(null, null, {
		renderer: 'svg', // 必须使用 SVG 模式
		ssr: true, // 开启 SSR
		width: width, // 需要指明高和宽
		height: height
	});



	var domSvg = document.getElementById("svgg");
	domSvg.innerHTML = "";





	switch (data_mode) {
		case "file":
			myChart.setOption(echart_option);
			break;
		case "blob":
			var _blob = getQueryString(url_string, "d");//binary data
			var blob = "";
			if (_blob != undefined) {
				blob = b64_to_utf8(_blob);
			}

			if (!isNull(blob)) {

				//from url
				var option = looseJsonParse(blob);
				if (option != undefined) {
					myChart.setOption(option);
				}
				console.log("blob");
			} else {


			}
			break;
		default:
			var option = {
				animation: false,
				title: {
					text: 'ECharts 入门示例'
				},
				tooltip: {},
				legend: {
					data: ['销量']
				},
				xAxis: {
					data: ['衬衫', '羊毛衫', '雪纺衫', '裤子', '高跟鞋', '袜子']
				},
				yAxis: {},
				series: [
					{
						name: '销量',
						type: 'bar',
						data: [5, 20, 36, 10, 10, 20]
					}, {
						name: '销量1',
						type: 'bar',
						data: [5, 210, 136, 10, 10, 20]
					}
				]
			};
			//   console.log(JSON.stringify(fff));
			//   // 使用刚指定的配置项和数据显示图表。
			//   myChart.setOption(option);

			myChart.setOption(option);
			break;
	}


	RenderSVG(myChart,domSvg);
	// if(data_mode != 'file'){

		
	// 	// var svgStr = myChart.renderToSVGString();
	// 	// domSvg.innerHTML = svgStr;
		
		
	// 	// myChart.dispose();
	// 	// myChart = null;


	// }
};








