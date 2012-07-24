/// <reference path="jquery-1.6.4.js" />
/// <reference path="jquery-ui-1.8.20.js" />
/// <reference path="jquery.base64.js" />

$(function () {
    $('#btnLogout').attr("disabled", "true");

    $('#btnLogin').click(function (event) {
        var options = {
            url: 'api/TraceAuthentication/Authenticate',
            type: 'POST',
            beforeSend: function (xhr) {
                var raw = "admin:password";
                var encoded = jQuery.base64.encode(raw);
                xhr.setRequestHeader("Authorization", "Base " + encoded);
            },
            timeout: '3000',
            success: function (data) {
                alert(data);
            },
            error: function (data, status, err) {
                alert(err);
            }
        }
        $.ajax(options);
    });
});