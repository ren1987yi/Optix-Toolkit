window.onload = () => {



	var domError = document.getElementById("error");
	domError.innerHTML = "";


	var colors = ['#1ecab8', '#fd3c97', '#6d81f5', '#ffb822', '#0dc8de'];


	// var options = {
	// 	chart: {
	// 		height: window.innerHeight,
	// 		type: 'bar',
	// 		events: {
	// 			click: function (chart, w, e) {
	// 				console.log(chart, w, e)
	// 			}



	// 		},
	// 		toolbar: {
	// 			show: true
	// 		},
	// 		dropShadow: {
	// 			enabled: true,
	// 			top: 0,
	// 			left: 5,
	// 			bottom: 5,
	// 			right: 0,
	// 			blur: 5,
	// 			color: '#45404a2e',
	// 			opacity: 0.35
	// 		}

	// 	},
	// 	title: {
	// 		text: 'Test Data',
	// 		align: 'left'
	// 	},
	// 	colors: colors,
	// 	plotOptions: {
	// 		bar: {
	// 			dataLabels: {
	// 				position: 'top', // top, center, bottom              
	// 			},
	// 			columnWidth: '30',
	// 			distributed: true,
	// 		},

	// 	},
	// 	dataLabels: {
	// 		enabled: true,
	// 		formatter: function (val) {
	// 			return val + "%";
	// 		},
	// 		offsetY: -20,
	// 		style: {
	// 			fontSize: '12px',
	// 			colors: ["#8997bd"]
	// 		}
	// 	},
	// 	series: [{
	// 		name: 'Inflation',
	// 		data: [4.0, 10.1, 6.0, 8.0, 9.1]
	// 	}],
	// 	xaxis: {
	// 		categories: ["Email", "Referral", "Organic", "Direct", "Campaign",],
	// 		position: 'top',
	// 		labels: {
	// 			offsetY: -18,
	// 			style: {
	// 				cssClass: 'apexcharts-xaxis-label',
	// 			},
	// 		},
	// 		axisBorder: {
	// 			show: false
	// 		},
	// 		axisTicks: {
	// 			show: false
	// 		},
	// 		crosshairs: {
	// 			fill: {
	// 				type: 'gradient',
	// 				gradient: {
	// 					colorFrom: '#D8E3F0',
	// 					colorTo: '#BED1E6',
	// 					stops: [0, 100],
	// 					opacityFrom: 0.4,
	// 					opacityTo: 0.5,
	// 				}
	// 			}
	// 		},
	// 		tooltip: {
	// 			enabled: true,
	// 			offsetY: -37,
	// 		}
	// 	},
	// 	fill: {
	// 		gradient: {
	// 			type: "gradient",
	// 			gradientToColors: ['#FEB019', '#775DD0', '#FEB019', '#FF4560', '#775DD0'],
	// 		},
	// 	},
	// 	yaxis: {
	// 		axisBorder: {
	// 			show: false
	// 		},
	// 		axisTicks: {
	// 			show: false,
	// 		},
	// 		labels: {
	// 			show: false,
	// 			formatter: function (val) {
	// 				return val + "%";
	// 			}
	// 		}

	// 	},
	// }

	// var options = {
	// 	series: [{
	// 		name: 'series1',
	// 		data: [31, 40, 28, 51, 42, 109, 100]
	// 	}, {
	// 		name: 'series2',
	// 		data: [11, 32, 45, 32, 34, 52, 41]
	// 	}],
	// 	chart: {
	// 		height: 350,
	// 		type: 'area'
	// 	},
	// 	dataLabels: {
	// 		enabled: false
	// 	},
	// 	stroke: {
	// 		curve: 'smooth'
	// 	},
	// 	xaxis: {
	// 		type: 'datetime',
	// 		categories: ["2018-09-19T00:00:00.000Z", "2018-09-19T01:30:00.000Z", "2018-09-19T02:30:00.000Z", "2018-09-19T03:30:00.000Z", "2018-09-19T04:30:00.000Z", "2018-09-19T05:30:00.000Z", "2018-09-19T06:30:00.000Z"]
	// 	},
	// 	tooltip: {
	// 		x: {
	// 			format: 'dd/MM/yy HH:mm'
	// 		},
	// 	},
	// };




	var options = {
		chart: {
			height: 300,
			type: 'bar',
			toolbar: {
				show: false
			},
			dropShadow: {
				enabled: true,
				top: 0,
				left: 5,
				bottom: 5,
				right: 0,
				blur: 5,
				//   color: '#45404a2e',
				color: '#ffffff2e',
				opacity: 0.35
			},
		},
		plotOptions: {
			bar: {
				horizontal: false,
				endingShape: 'rounded',
				columnWidth: '25%',
			},
		},
		dataLabels: {
			enabled: false,
		},
		stroke: {
			show: false,
			width: 2,
			colors: ['transparent']
		},
		colors: ['#12fe55', '#007bff'],
		series: [{
			name: '碳强度',
			data: [68, 44, 55, 57, 56],

		}],
		xaxis: {
			categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May'],
			axisBorder: {
				show: false,
			},
			axisTicks: {
				show: false,
			},
			show: false
		},
		legend: {
			offsetY: 0,
		},
		yaxis: {
			title: {
				text: '',
				show: false,
			},
			show: false
		},
		fill: {
			opacity: 1,
		},
		// legend: {
		//     floating: true
		// },
		grid: {
			row: {
				colors: ['transparent', 'transparent'], // takes an array which will be repeated on columns
				opacity: 0.2
			},
			borderColor: '#f1f3fa'
		},
		tooltip: {
			y: {
				formatter: function (val) {
					return "" + val + ""
				}
			}
		}
	};


	var options = {

		series: [{
			name: 'Product',
			type: 'column',
			data: [440, 505, 414, 671, 227, 413, 201, 352, 752, 320, 257, 160],
			color: '#12fe55'

		}, {
			name: 'Carbon',
			type: 'line',
			data: [23, 42, 35, 27, 43, 22, 17, 31, 22, 22, 12, 16],
			color: '#007bff'
		}],
		chart: {
			height: 350,
			type: 'line',
			toolbar: {
				show: false
			}
			
		},
		grid: {
			borderColor: "#45404afe",
			padding: {
				left: 0,
				right: 0
			}
		},
		stroke: {
			width: [0, 4]
		},
		title: {
			text: '',
			show: false
		},
		dataLabels: {
			enabled: false,
			enabledOnSeries: [1]
		},
		
		labels: ['2001', '2001', '2001', '2001', '2001', '06 Jan 2001', '07 Jan 2001', '08 Jan 2001', '09 Jan 2001', '10 Jan 2001', '11 Jan 2001', '12 Jan 2001'],
		xaxis: {
			type: 'string',
			axisBorder: {
				show: false,
			},
			axisTicks: {
				show: false,
			},
			lable: {
				show: false,
				color: '#f00'
			},
			show: false
		},
		yaxis: [{
			show: false,
			title: {
				text: '',
				show: false
			},
			axisTicks: {
				show: false
			},
			axisBorder: {
				show: false,
				color: "#FF1654"
			},

		}, {
			axisTicks: {
				show: false
			},
			axisBorder: {
				show: false,
				color: "#FF1654"
			},
			show: false,
			opposite: true,
			title: {
				text: '',
				show: false
			}
		}]
	};



	const httpRequest = new XMLHttpRequest();




	httpRequest.onreadystatechange = () => {

		if (httpRequest.readyState === XMLHttpRequest.DONE) {

			if (httpRequest.status == 200) {


				var json = httpRequest.responseText;
				var option = looseJsonParse(json);
				if (option != undefined) {
					domError.innerHTML = "";
					// myChart.setOption(option);
					var chart = new ApexCharts(
						document.querySelector("#main"),
						options
					);

					chart.render();
				}
			} else {
				domError.innerHTML = "data not found";
				var chart = new ApexCharts(
					document.querySelector("#main"),
					options
				);

				chart.render();
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
		if (!isNull(blob)) {

			//from url
			var option = looseJsonParse(blob);
			if (option != undefined) {
				domError.innerHTML = "";
				option.chart.height = window.innerHeight;
				option.chart.width = window.innerWidth;
				var chart = new ApexCharts(
					document.querySelector("#main"),
					option
				);
				chart.render();
				// myChart.setOption(option);
			}
		} else {
			domError.innerHTML = "data not found";
			var chart = new ApexCharts(
				document.querySelector("#main"),
				options
			);

			chart.render();
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

		if (option_data != undefined) {

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
		} else {
			domError.innerHTML = "data not found";
			var chart = new ApexCharts(
				document.querySelector("#main"),
				options
			);

			chart.render();
		}
	}

	window.addEventListener('resize', function () {
		// options.chart.height = window.innerHeight;
		// // chart = new ApexCharts(
		// // 	document.querySelector("#main"),
		// // 	options
		// //   );
		//   chart.render(options);
		//   console.log(window.innerHeight);


		chart.updateOptions({
			chart: {
				height: window.innerHeight
			}
		});
	})




};
