/// <reference path="../typings/jquery/jquery.d.ts" />


function LuoOminaisuus(valinta: string) {

    $.ajax({
        type: "POST",
        url: "/home/lisaaominaisuus",
        data: JSON.stringify(valinta),
        contentType: "application/json",
        success: function (valinta) {
            if (valinta.success == true) {
                alert(valinta.ominaisuus.substring(1, valinta.ominaisuus.length - 1) + " lisätty");
                $(".result").html(valinta);
            } else {
                alert("Tallennus epäonnistui " + valinta.error);
            }
        },
        dataType: "json"
    });

}

