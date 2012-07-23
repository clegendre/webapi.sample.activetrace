/// <reference path="jquery-1.6.4.js" />
/// <reference path="jquery.signalR-0.5.2.js" />
/// <reference path="jquery-ui-1.8.20.js" />

$(function () {
    function updateTable(trace) {
        var row = $('<tr></tr>');
        row.append('<td>' + trace.RequestUri + '</td>');
        row.append('<td>' + trace.Level + '</td>');
        row.append('<td>' + trace.Category + '</td>');
        row.append('<td>' + trace.Operator + '</td>');
        row.append('<td>' + trace.Operation + '</td>');
        $('<td>' + trace.Status + '</td>').appendTo(row);

        row.insertAfter('#header');

        row.hover(
            function () {
                $('#message').text(trace.Message);
                $('#exception').text(trace.Exception);
                $(this).css("background-color", "lightgreen");
            },
            function () {
                $('#message').text("");
                $('#exception').text("");
                $(this).css("background-color", "white");
            });
    };

    var connection = $.connection('/trace');

    connection.received(function (trace) {
        $('#debuginfo').text(trace);
        updateTable(trace);
    });

    connection.start();

    $('#btnClear').click(function () {
        $('#debuginfo').text("clear the trace records");
        $('td', '#tracetable').remove();
    });
});