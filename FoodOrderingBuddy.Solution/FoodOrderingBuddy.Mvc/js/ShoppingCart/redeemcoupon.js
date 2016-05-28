/// <reference path="_references.js" />
$(document).ready(function () {
    $(".redeemCoupon").click(function () {
        var menuItemId = $(this).attr("data-id");
        console.log(menuItemId);

        $("#dialogGetcoupon").dialog({
            resizable: false,
            width: 640,
            height: 200,
            modal: true,
            hide: "clip",
            show: "slide",
            buttons: {
                "Validate": function () {
                    var code = $("#couponCode").val();
                    console.log(code);
                    $.ajax({
                        type: "POST",
                        url: "/Coupon/GetCoupon",
                        dataType: "json",
                        data: {
                            "code": code
                        },
                        success: function (data) {
                            var isRedeemed = data.IsRedeemed;

                            if (isRedeemed == true) {
                                alert("This code has already been redeemed, Sorry mate :(");
                                return;
                            }

                            console.log(data.Code);
                            console.log(data.Amount);
                            console.log(data.DateIssued);
                            var date = new Date(parseInt(data.DateIssued.substr(6)));
                            console.log(date);

                            $("#lblCode").text(data.Code);
                            $("#lblAmount").text(data.Amount);
                            $("#lblDateIssued").text(date);

                            $("#dialogGetcoupon").dialog("close");
                            $("#dialgoRedeemCoupon").dialog({
                                resizable: false,
                                width: 640,
                                height: 275,
                                modal: true,
                                show: "slide",
                                buttons: {
                                    "OK": function () {

                                    },
                                    Cancel: function () {
                                        $("#dialgoRedeemCoupon").dialog("close");
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
    })
})