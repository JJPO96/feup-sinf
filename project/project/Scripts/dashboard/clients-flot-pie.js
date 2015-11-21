﻿$(function () {
    $.ajax({
        dataType: "json",
        url: "http://localhost:49328/api/clients/top",
        success: function (clients) {
            clients = JSON.parse(clients);

            var data = [];
            $.each(clients, function (i) {
                data.push({ label: clients[i].name, data: clients[i].percentage });
            });

            $.plot("#placeholderA", data, {
                series: {
                    pie: {
                        show: true
                    }
                },
                grid: {
                    hoverable: true
                },
                tooltip: true,
                tooltipOpts: {
                    content: "%p.0%, %s", // show percentages, rounding to 2 decimal places
                    shifts: {
                        x: 20,
                        y: 0
                    }
                }
            });
        }
    })
});

// custom label formatter used by several of the plots
function labelFormatter(label, series) {
    return "<div style='font-size:8pt; text-align:center; padding:2px; color:white;'>" + label + "<br />" + Math.round(series.percent) + "%</div>";
}

function setCode(lines) {
    $("#code").text(lines.join("\n"));
}