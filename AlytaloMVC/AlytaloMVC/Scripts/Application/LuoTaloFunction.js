/// <reference path="../typings/jquery/jquery.d.ts" />
var Talo = /** @class */ (function () {
    function Talo() {
    }
    return Talo;
}());
function initLuoTalo() {
    $("#BtnLuoTalo").click(function () {
        var Nimi = $('#inputNimi').val();
        var Osoite = $("#inputOsoite").val();
        var data = new Talo();
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
                    // TODO: Reaktiot 
                    // $(location).attr('href', '/Home/talo (TMS)');
                }
                else {
                }
            },
            dataType: "json"
        });
    });
}
//# sourceMappingURL=LuoTaloFunction.js.map