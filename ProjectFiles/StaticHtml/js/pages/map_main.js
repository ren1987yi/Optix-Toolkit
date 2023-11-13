


$('#main').vectorMap({
	map: 'cn_mill',
	backgroundColor: 'transparent',
	borderColor: '#818181',
	regionStyle: {
		initial: {
			fill: '#7474b126',
		}
	},
	zoomMax: 10,
	zoomMin: 0.1,
	series: {
		regions: [{
			values: {
				'CN-46': '#868ff363',
				'CN-51': '#868ff363',
				'CN-14': '#868ff363',
				'CN-12': '#868ff363',
			},
			attribute: 'fill',
		}
		]
	},
}
);