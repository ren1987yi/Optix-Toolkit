(function (root, factory) {
    if (typeof define === 'function' && define.amd) {
        // AMD. Register as an anonymous module.
        define(['exports', 'echarts'], factory);
    } else if (typeof exports === 'object' && typeof exports.nodeName !== 'string') {
        // CommonJS
        factory(exports, require('echarts'));
    } else {
        // Browser globals
        factory({}, root.echarts);
    }
}(this, function (exports, echarts) {
    var log = function (msg) {
        if (typeof console !== 'undefined') {
            console && console.error && console.error(msg);
        }
    };
    if (!echarts) {
        log('ECharts is not Loaded');
        return;
    }
    echarts.registerTheme('chalk', {
        "color": [
            "#43dcff",
            "#38db9a",
            "#a1c2d1",
            "#ce1010",
            "#26865f",
            "#ffffff",
            "#d2f5a6",
            "#76f2f2"
        ],
        "backgroundColor": "#0c141f",
        "textStyle": {},
        "title": {
            "textStyle": {
                "color": "#a1c2d1"
            },
            "subtextStyle": {
                "color": "#9a9b9d"
            }
        },
        "line": {
            "itemStyle": {
                "borderWidth": "4"
            },
            "lineStyle": {
                "width": "3"
            },
            "symbolSize": "0",
            "symbol": "circle",
            "smooth": true
        },
        "radar": {
            "itemStyle": {
                "borderWidth": "4"
            },
            "lineStyle": {
                "width": "3"
            },
            "symbolSize": "0",
            "symbol": "circle",
            "smooth": true
        },
        "bar": {
            "itemStyle": {
                "barBorderWidth": 0,
                "barBorderColor": "#cccccc"
            }
        },
        "pie": {
            "itemStyle": {
                "borderWidth": 0,
                "borderColor": "#cccccc"
            }
        },
        "scatter": {
            "itemStyle": {
                "borderWidth": 0,
                "borderColor": "#cccccc"
            }
        },
        "boxplot": {
            "itemStyle": {
                "borderWidth": 0,
                "borderColor": "#cccccc"
            }
        },
        "parallel": {
            "itemStyle": {
                "borderWidth": 0,
                "borderColor": "#cccccc"
            }
        },
        "sankey": {
            "itemStyle": {
                "borderWidth": 0,
                "borderColor": "#cccccc"
            }
        },
        "funnel": {
            "itemStyle": {
                "borderWidth": 0,
                "borderColor": "#cccccc"
            }
        },
        "gauge": {
            "itemStyle": {
                "borderWidth": 0,
                "borderColor": "#cccccc"
            }
        },
        "candlestick": {
            "itemStyle": {
                "color": "#fc97af",
                "color0": "transparent",
                "borderColor": "#fc97af",
                "borderColor0": "#87f7cf",
                "borderWidth": "2"
            }
        },
        "graph": {
            "itemStyle": {
                "borderWidth": 0,
                "borderColor": "#cccccc"
            },
            "lineStyle": {
                "width": "1",
                "color": "#ffffff"
            },
            "symbolSize": "0",
            "symbol": "circle",
            "smooth": true,
            "color": [
                "#43dcff",
                "#38db9a",
                "#a1c2d1",
                "#ce1010",
                "#26865f",
                "#ffffff",
                "#d2f5a6",
                "#76f2f2"
            ],
            "label": {
                "color": "#293441"
            }
        },
        "map": {
            "itemStyle": {
                "areaColor": "#f3f3f3",
                "borderColor": "#999999",
                "borderWidth": 0.5
            },
            "label": {
                "color": "#893448"
            },
            "emphasis": {
                "itemStyle": {
                    "areaColor": "rgba(255,178,72,1)",
                    "borderColor": "#eb8146",
                    "borderWidth": 1
                },
                "label": {
                    "color": "rgb(137,52,72)"
                }
            }
        },
        "geo": {
            "itemStyle": {
                "areaColor": "#f3f3f3",
                "borderColor": "#999999",
                "borderWidth": 0.5
            },
            "label": {
                "color": "#893448"
            },
            "emphasis": {
                "itemStyle": {
                    "areaColor": "rgba(255,178,72,1)",
                    "borderColor": "#eb8146",
                    "borderWidth": 1
                },
                "label": {
                    "color": "rgb(137,52,72)"
                }
            }
        },
        "categoryAxis": {
            "axisLine": {
                "show": true,
                "lineStyle": {
                    "color": "#004051"
                }
            },
            "axisTick": {
                "show": true,
                "lineStyle": {
                    "color": "#004051"
                }
            },
            "axisLabel": {
                "show": true,
                "color": "#9a9b9d"
            },
            "splitLine": {
                "show": true,
                "lineStyle": {
                    "color": [
                        "#004051"
                    ]
                }
            },
            "splitArea": {
                "show": true,
                "areaStyle": {
                    "color": [
                        "rgba(200,200,200,0.05)",
                        "rgba(200,200,200,0.02)"
                    ]
                }
            }
        },
        "valueAxis": {
            "axisLine": {
                "show": true,
                "lineStyle": {
                    "color": "#004051"
                }
            },
            "axisTick": {
                "show": true,
                "lineStyle": {
                    "color": "#004051"
                }
            },
            "axisLabel": {
                "show": true,
                "color": "#9a9b9d"
            },
            "splitLine": {
                "show": true,
                "lineStyle": {
                    "color": [
                        "#004051"
                    ]
                }
            },
            "splitArea": {
                "show": true,
                "areaStyle": {
                    "color": [
                        "rgba(200,200,200,0.05)",
                        "rgba(200,200,200,0.02)"
                    ]
                }
            }
        },
        "logAxis": {
            "axisLine": {
                "show": true,
                "lineStyle": {
                    "color": "#004051"
                }
            },
            "axisTick": {
                "show": true,
                "lineStyle": {
                    "color": "#004051"
                }
            },
            "axisLabel": {
                "show": true,
                "color": "#9a9b9d"
            },
            "splitLine": {
                "show": true,
                "lineStyle": {
                    "color": [
                        "#004051"
                    ]
                }
            },
            "splitArea": {
                "show": true,
                "areaStyle": {
                    "color": [
                        "rgba(200,200,200,0.05)",
                        "rgba(200,200,200,0.02)"
                    ]
                }
            }
        },
        "timeAxis": {
            "axisLine": {
                "show": true,
                "lineStyle": {
                    "color": "#004051"
                }
            },
            "axisTick": {
                "show": true,
                "lineStyle": {
                    "color": "#004051"
                }
            },
            "axisLabel": {
                "show": true,
                "color": "#9a9b9d"
            },
            "splitLine": {
                "show": true,
                "lineStyle": {
                    "color": [
                        "#004051"
                    ]
                }
            },
            "splitArea": {
                "show": true,
                "areaStyle": {
                    "color": [
                        "rgba(200,200,200,0.05)",
                        "rgba(200,200,200,0.02)"
                    ]
                }
            }
        },
        "toolbox": {
            "iconStyle": {
                "borderColor": "#574f4f"
            },
            "emphasis": {
                "iconStyle": {
                    "borderColor": "#999999"
                }
            }
        },
        "legend": {
            "textStyle": {
                "color": "#9a9b9d"
            }
        },
        "tooltip": {
            "axisPointer": {
                "lineStyle": {
                    "color": "#cccccc",
                    "width": 1
                },
                "crossStyle": {
                    "color": "#cccccc",
                    "width": 1
                }
            }
        },
        "timeline": {
            "lineStyle": {
                "color": "#87f7cf",
                "width": 1
            },
            "itemStyle": {
                "color": "#87f7cf",
                "borderWidth": 1
            },
            "controlStyle": {
                "color": "#87f7cf",
                "borderColor": "#87f7cf",
                "borderWidth": 0.5
            },
            "checkpointStyle": {
                "color": "#fc97af",
                "borderColor": "#fc97af"
            },
            "label": {
                "color": "#87f7cf"
            },
            "emphasis": {
                "itemStyle": {
                    "color": "#f7f494"
                },
                "controlStyle": {
                    "color": "#87f7cf",
                    "borderColor": "#87f7cf",
                    "borderWidth": 0.5
                },
                "label": {
                    "color": "#87f7cf"
                }
            }
        },
        "visualMap": {
            "color": [
                "#fc97af",
                "#87f7cf"
            ]
        },
        "dataZoom": {
            "backgroundColor": "rgba(255,255,255,0)",
            "dataBackgroundColor": "rgba(114,204,255,1)",
            "fillerColor": "rgba(114,204,255,0.2)",
            "handleColor": "#72ccff",
            "handleSize": "100%",
            "textStyle": {
                "color": "#333333"
            }
        },
        "markPoint": {
            "label": {
                "color": "#293441"
            },
            "emphasis": {
                "label": {
                    "color": "#293441"
                }
            }
        }
    });
}));
