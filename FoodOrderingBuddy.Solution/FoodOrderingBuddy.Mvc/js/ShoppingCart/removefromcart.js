$(document).ready(function () {
    $(".removeItem").click(function () {

        var recordToDeleteId = $(this).attr("data-id");
        console.log(recordToDeleteId);

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
                        url: "/ShoppingCart/RemoveFromCart",
                        dataType: "json",
                        data: {
                            "id": recordToDeleteId
                        },
                        success: function (data) {
                            console.log(data.ItemCount);
                            if (data.ItemCount == 0) {
                                $("#row-" + data.DeleteId).fadeOut("slow");
                            } else {
                                $("#item-count-" + data.DeleteId).text(data.ItemCount);
                            }

                            $("#cart-total").text(data.Total);
                            var cartTotal = parseInt($("#cart-total").val());
                            console.log(cartTotal);

                            $("#message").text(data.Message);
                            $("#cart-status").text(data.Count);
                            $("#dialog").dialog("close");
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
