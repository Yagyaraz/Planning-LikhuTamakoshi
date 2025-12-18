function PieCharts() {
    $.getJSON(GetPieChartDataUrl(),
        function (result) {
            debugger
            var datas = $.map(result, function (e) {
                return { name: e.Name, y: parseFloat(e.Amount), bhuktani: parseFloat(e.bhuktani), nonsamjhauta: parseFloat(e.nonsamjhauta) }
            });

            Highcharts.chart('pieChart', {
                chart: {
                    plotBackgroundColor: null,
                    plotBorderWidth: null,
                    plotShadow: false,
                    type: 'pie'
                },
                title: {
                    text: 'योजना सम्झौता'
                },
                tooltip: {
                    pointFormat: '{Name.Name}: <b>{point.percentage:.1f}%</b>'
                },
                accessibility: {
                    point: {
                        valueSuffix: '%'
                    }
                },
                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        dataLabels: {
                            enabled: true,
                            //format: '<b>{point.name}</b>: {point.percentage:.1f} %'
                        }
                    }
                },
                series: [{
                    name: 'जम्मा',
                    colorByPoint: true,
                    data: datas
                }]
            });
        });
}
//ThekkaPieChart

function ThekkaPieCharts() {
    $.getJSON(GetThekkaPieChart(),
        function (result) {
            debugger
            var datas = $.map(result, function (e) {
                return { name: e.Name, y: parseFloat(e.Amount), bhuktani: parseFloat(e.bhuktani) }
            });

            Highcharts.chart('ThekkapieChart', {
                chart: {
                    plotBackgroundColor: null,
                    plotBorderWidth: null,
                    plotShadow: false,
                    type: 'pie'
                },
                title: {
                    text: ' ठेक्का सम्झौता'
                },
                tooltip: {
                    pointFormat: '{Name.Name}: <b>{point.percentage:.1f}%</b>'
                },
                accessibility: {
                    point: {
                        valueSuffix: '%'
                    }
                },
                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        dataLabels: {
                            enabled: true,
                            //format: '<b>{point.name}</b>: {point.percentage:.1f} %'
                        }
                    }
                },
                series: [{
                    name: 'जम्मा',
                    colorByPoint: true,
                    data: datas
                }]
            });
        });
}


//LineChart
function ColumnCharts() {
    /*var selectElement = document.querySelector('#dropdown');*/
    /*var wardId = (selectElement) ? selectElement.value : '';*/
    $.post(GetUrlForGraph(), { datalist: GetNepaliCurrentFiscalYearMonths() },
        function (result) {
            var categories = $.map(result.monthNames, function (e) { return e; });
            var data = $.map(result.taxList, function (e) {
                return {
                    name: e.name,
                    data: $.map(e.amounts, function (a) { return a; })
                };
            });

            Highcharts.chart('container1', {
                chart: {
                    type: 'column'
                },
                title: {
                    text: 'Monthly Report'
                }, 
                xAxis: {
                    categories: categories,
                    crosshair: true
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Cash (Nrs.)'
                    }
                },
                tooltip: {
                    headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                    pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                        '<td style="padding:0"><b>{point.y:.1f} Nrs.</b></td></tr>',
                    footerFormat: '</table>',
                    shared: true,
                    useHTML: true
                },
                plotOptions: {
                    column: {
                        pointPadding: 0.2,
                        borderWidth: 0
                    }
                },
                series: data
            });
        });
}
