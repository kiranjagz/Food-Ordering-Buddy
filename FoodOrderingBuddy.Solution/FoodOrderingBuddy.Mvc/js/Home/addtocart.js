/// <reference path="_references.js" />
$(document).ready(function () {

    $body = $("body");

    $(document).on({
        ajaxStart: function () { $body.addClass("loading"); },
        ajaxStop: function () { $body.removeClass("loading"); }
    });

    $("#accordion").accordion();

    $("#fooditems tbody tr").click(function () {

        var itemId = $(this).attr("data-id");
        console.log(itemId);

        $("#dialog").dialog({
            resizable: false,
            width: 640,
            height: 200,
            modal: true,
            hide: "clip",
            show: "slide",
            buttons: {
                "Confirm": function () {
                    $.ajax({
                        type: "POST",
                        url: "/ShoppingCart/AddtoCart",
                        dataType: "json",
                        data: {
                            "id": itemId
                        },
                        success: function (data) {
                            $("#cart-status").text(data.Count);
                            $("#message").text(data.Message);
                            $("#dialog").dialog("close");
                            $("#dialogSuccess").dialog({
                                resizable: false,
                                width: 640,
                                height: 200,
                                modal: true,
                                show: "slide",
                                buttons: {
                                    "OK": function () {
                                        $("#dialogSuccess").dialog("close");
                                    }
                                }
                            });
                        },
                        error: function (xhr) {
                            alert(xhr.responseText);
                        }
                    });
                },
                Cancel: function () {
                    $(this).dialog("close");
                }
            }
        });
    });
});
