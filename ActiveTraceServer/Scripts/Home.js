/// <reference path="jquery-1.6.4.js" />
/// <reference path="jquery-ui-1.8.20.js" />
/// <reference path="jquery.base64.js" />

$(function () {
    function updateTraceTable(trace) {
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

    function setupTraceConnection(token) {
        var connection = $.connection('/trace');

        connection.received(function (trace) {
            $('#debuginfo').text(trace);
            updateTraceTable(trace);
        });

        connection.start().done(
            function () {
                connection.send(token);
            });

        $('#btnClear').click(function () {
            $('#debuginfo').text("clear the trace records");
            $('td', '#tracetable').remove();
        });
    };

    $('#btnLogout').hide();
    $('div#trace').hide();

    $('#btnLogin').click(function (event) {
        var username = $('input#txtUsername').val();
        var password = $('input#txtPassword').val();

        var options = {
            url: 'api/TraceAuthentication/GetToken',
            type: 'GET',
            data: { 'username': username },
            beforeSend: function (xhr) {
                var raw = username + ":" + password;
                var encoded = jQuery.base64.encode(raw);
                xhr.setRequestHeader("Authorization", "Base " + encoded);
            },
            timeout: '60000',
            success: function (token) {
                $('div#trace').show();
                $('#logTable').hide();
                $('#btnLogin').hide();
                $('#btnLogout').show();
                setupTraceConnection(token);
            },
            error: function (data, status, err) {
                alert(err);
            }
        }
        $.ajax(options);
    });

});