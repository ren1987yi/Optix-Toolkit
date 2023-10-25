


window.onload = () => {

	// var a = "{a:1,b:2}";

	// console.log(looseJsonParse("{a:(4-1), b:function(){}, c:new Date()}"));
	// console.log(looseJsonParse(a));



	// 基于准备好的dom，初始化echarts实例

	var myChart = echarts.init(document.getElementById('main'));

	var domError = document.getElementById("error");
	domError.innerHTML = "";
	// 指定图表的配置项和数据
	//   var fff = {
	// 	title: {
	// 	  text: 'ECharts 入门示例'
	// 	},
	// 	tooltip: {},
	// 	legend: {
	// 	  data: ['销量']
	// 	},
	// 	xAxis: {
	// 	  data: ['衬衫', '羊毛衫', '雪纺衫', '裤子', '高跟鞋', '袜子']
	// 	},
	// 	yAxis: {},
	// 	series: [
	// 	  {
	// 		name: '销量',
	// 		type: 'bar',
	// 		data: [5, 20, 36, 10, 10, 20]
	// 	  }
	// 	]
	//   };
	//   console.log(JSON.stringify(fff));
	//   // 使用刚指定的配置项和数据显示图表。
	//   myChart.setOption(option);

	window.addEventListener('resize', function () {
		myChart.resize()
	})



	const httpRequest = new XMLHttpRequest();




	httpRequest.onreadystatechange = () => {

		if (httpRequest.readyState === XMLHttpRequest.DONE) {

			if (httpRequest.status == 200) {


				var json = httpRequest.responseText;
				var option = looseJsonParse(json);
				if (option != undefined) {
					domError.innerHTML = "";
					myChart.setOption(option);
				}
			} else {
				domError.innerHTML = "data not found";
			}
		} else {
			// 还没准备好。
			//console.log("nok");
		}

	};




	var url_string = window.location.href;
	var option_data = getQueryString(url_string, "d");//data file path
	var _interval = getQueryString(url_string, "i");//interval number
	var data_from_url = false; //data from url
	var _blob = getQueryString(url_string, "blob");//binary data
	var blob = "";
	if (_blob != undefined) {
		domError.innerHTML = "data is error";
		blob = b64_to_utf8(_blob);
		domError.innerHTML = blob;
		data_from_url = true;
	}


	///set background color
	var bg = '#ffffff';
	var _bg = getQueryString(url_string, "bg");//bg
	if (_bg != undefined) {
		bg = _bg;
		if (bg.charCodeAt(0) != '#') {
			bg = '#' + bg;
		}
	}
	document.body.style.backgroundColor = bg;



	//set data source
	if (data_from_url) {
		//from url
		var option = looseJsonParse(blob);
		if (option != undefined) {
			domError.innerHTML = "";
			myChart.setOption(option);
		}

	} else {
		//from file
		var is_cycle = undefined;
		var interval = 0;
		if (_interval != undefined) {
			interval = parseInt(_interval);
			if (interval <= 0) {
				interval = 60000;
			}
			is_cycle = true;
		}
		var url_root = "/chartdata/options/";
		var reqUrl = url_root + option_data;

		if (is_cycle == undefined) {

			httpRequest.open("GET", reqUrl, true);
			httpRequest.send();
		} else {
			httpRequest.open("GET", reqUrl, true);
			httpRequest.send();
			setInterval(() => {

				httpRequest.open("GET", reqUrl, true);
				httpRequest.send();
			}, interval);
		}
	}
};








