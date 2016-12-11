google.charts.load("current", { packages: ["corechart"] });
google.charts.setOnLoadCallback(drawChart);

function drawChart() {
    $.ajax({
        type: "POST",
        url: "/Products/GetHistogram",
        dataType: "json",
        contentType: "application/json; charset=utf-8"
    }).done(function (result) {
        result.unshift(['Product', 'Percentage of dishes']);
        var data = google.visualization.arrayToDataTable(result);
        var options = {
            title: 'Products percentage in dishes',
            legend: { position: 'none' },
            histogram: {
                lastBucketPercentile: 1,
                minValue: 0,
                maxValue: 100
            }
        };

        var chart = new google.visualization.Histogram(document.getElementById('chart_div'));
        $('.loader').remove();
        chart.draw(data, options);
    });
}