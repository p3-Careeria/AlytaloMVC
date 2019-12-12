/// <reference path="../typings/jquery/jquery.d.ts" />

class Talo {
    public Nimi: string;
    public Osoite: string;
}

function initLuoTalo() {
    $("#BtnLuoTalo").click(function () {
        var Nimi: string = $('#inputNimi').val();
        var Osoite: string = $("#inputOsoite").val();

        var data: Talo = new Talo();
        data.Nimi = Nimi;
        data.Osoite = Osoite;
        alert("data: " + data.Nimi);

        $.ajax({
            type: "POST",
            url: "/home/luouusitalo",
            data: JSON.stringify(data),
            contentType: "application/json",
            success: function (data) {
                if (data.success == true) {
                    alert("Talon luonti onnistui");
                    $(location).attr('href', '/Home/Talo');
                } else {

                }

            },
            dataType: "json"
        });
    });
}   
