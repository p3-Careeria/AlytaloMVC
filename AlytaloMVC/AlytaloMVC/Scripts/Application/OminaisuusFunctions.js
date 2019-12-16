
//setTimeout (function () {
//    var x = document.getElementById("saunanTilsu").textContent;
//    alert("Kuluuu " + x); 

//    for (var x in document.getElementById("saunanTilsu").textContent) {
//        alert(x);
//    }


//}, 500); 

function saunanTilanne() {
   // var x = document.getElementById("saunaTilsu").textContent;
    alert("Entäs tämä "); 

}

// vai iteroida noi elementit .onload:n jälkeen 
/*
 setTimeout(function () {            // viivästetty, koska window.onload() ei joka kerralla toimi
                                    // Arvaan, että tuoteObj jää välillä null:ksi, koska JSON ei ehdi latautua
    var sisalto = "<ul>";
    for (var x in tuoteObj.tuotteet) {
        var onkoListalla = sisalto.search(tuoteObj.tuotteet[x].Linkki);
        if (onkoListalla == -1) {
                                                            // osoitehärpäkkeiden poisto ja eka kirjain isoksi
            var osRemoved = tuoteObj.tuotteet[x].Linkki;
            osRemoved = osRemoved.replace("https://", "");
            osRemoved = osRemoved.replace("http://", "");
            osRemoved = osRemoved.replace(".com/", "");
            osRemoved = osRemoved.replace(".fi/", "");
            osRemoved = osRemoved.replace("www.", "");
            osRemoved = osRemoved.charAt(0).toUpperCase() + osRemoved.slice(1);

            sisalto += "<li><a class = 'toimittajat' href =";
            sisalto += tuoteObj.tuotteet[x].Linkki + ">" + osRemoved + "</a></li>";
        }
    }
    sisalto += "</ul>"
    document.getElementById("linkit").innerHTML = sisalto;
}, 100);
*/