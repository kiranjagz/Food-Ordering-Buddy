$(document).ready(function () {

    //$(document).tooltip({
    //    track: true
    //});

    $("#catergoryname").tooltip({
        show: {
            effect: "slideDown",
            delay: 250
        },
        track: true
    });

    $("#datecreated").tooltip({
        show: {
            effect: "slideDown",
            delay: 250
        },
        track: true
    });

    $("#Image").tooltip({
        show: {
            effect: "slideDown",
            delay: 250
        },
        track: true
    });

    $("#datecreated").datepicker({
        dateFormat: "dd/M/yy",
        changeMonth: true,
        changeYear: true,
        yearRange: "-60:+0"
    });
})