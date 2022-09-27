$(function () {
    var url = "bestilling/HentAlleTurer";
    $.get(url, function (turene) {
        if (turene === "Feil innlogging") {
            $(location).attr('href', 'loggInn.html');
        }
        var ut = formaterTurer(turene);
        $("#endreTurene").html(ut);
    });
});

function formaterTurer(turer) {
    let ut = "<table class='table table-striped'>" +
        "<th>TurId</th>" +
        "<th>Startstasjon</th>" +
        "<th>Endestasjon</th>" +
        "<th>Dato</th>" +
        "<th>Tid</th>" +
        "<th>Barnepris</th>" +
        "<th>Voksenpris</th>" +
        "<th>Endre</th>" +
        "<th>Slett</th>" +
        "</tr>";
    var linje = 1;
    $.each(turer, function (key, tur) {
        ut += "<tr>" +
            "<td><input type='text' readonly id='TurId" + linje + "' size='3' value='" + tur.turId + "'/></td>" +
            "<td><input type='text' id='startstasjon" + linje + "' value='" + tur.startStasjon.stasjonsNavn + "'/></td>" +
            "<td><input type='text' id='endestasjon" + linje + "' value='" + tur.endeStasjon.stasjonsNavn + "'/></td>" +
            "<td> <input type='text' id='dato" + linje + "' value='" + tur.dato + "'/></td>" +
            "<td><input type='text' id='tid" + linje + "' size='5' value='" + tur.tid + "'/></td>" +
            "<td><input type='text' id='barnePris" + linje + "' size='7' value='" + tur.barnePris + "'/></td>" +
            "<td><input type='text' id='voksenPris" + linje + "' size='7' value='" + tur.voksenPris + "'/></td>" +
            "<td> <a class='btn btn-info' onclick='validerOgEndreTur(" + linje + ")'>Endre</button></td>" +
            "<td> <a class='btn btn-danger' onclick='SlettTur(" + linje + ")'>Slett</button></td>" +
            "</tr>" +
            "<tr>" +
            "<td><span style='color: red; font-size: 80%' id='TurIdFeil" + linje + "' size='3'/></td>" +
            "<td><span style='color: red; font-size: 80%' id='feilStartStasjon" + linje + "'/></td>" +
            "<td><span style='color: red; font-size: 80%' id='feilEndeStasjonAdmin" + linje + "'/></td>" +
            "<td><span style='color: red; font-size: 80%' id='feilDatoAdmin" + linje + "'/></td>" +
            "<td><span style='color: red; font-size: 80%' id='feilTidAdmin" + linje + "' size='5'/></td>" +
            "<td><span style='color: red; font-size: 80%' id='feilBarnePrisAdmin" + linje + "' size='7'/></td>" +
            "<td><span style='color: red; font-size: 80%' id='feilVoksenPrisAdmin" + linje + "' size='7'/></td>" +
            "<td><span style='color: red; font-size: 80%' id='slettFeil" + linje + "' size='7'/>" +
            "<td><span style='color: red; font-size: 80%' id='endreFeil" + linje + "' size='7'/></td ></tr> ";
        linje++;
    });
    ut += "</table>";
    return ut;
}

function validerOgEndreTur(linje) {
    const StartstasjonOK = validerStartStasjonEndre($("#startstasjon"+linje).val(), linje);
    const EndestasjonOK = validerEndeStasjonEndre($("#endestasjon"+linje).val(), linje);
    const ikkeLikeStasjoner = validerLikeStasjoner($("#startstasjon"+linje).val(), $("#endestasjon"+linje).val(), linje);
    const DatoOK = validerDatoEndre($("#dato"+linje).val(), $("#tid"+linje).val(), linje);
    const TidOK = validerTidEndre($("#tid"+linje).val(), linje);
    const PrisBarnOK = validerBarnePrisEndre($("#barnePris"+linje).val(), linje);
    const PrisVoksenOK = validerVoksenPrisEndre($("#voksenPris"+linje).val(), linje);
    if (StartstasjonOK && EndestasjonOK && ikkeLikeStasjoner && TidOK && DatoOK && PrisBarnOK && PrisVoksenOK) {
           EndreTur(linje);
           return true;
    }
    else {
        $("#feil").html("Feil i inputvalidering - kan ikke endre på turen! ");
    }
}

function EndreTur(linje) {
    var tur = {
        TurId: $("#TurId" + linje).val(),
        startstasjon: $("#startstasjon" + linje).val(),
        endestasjon: $("#endestasjon" + linje).val(),
        dato: $("#dato" + linje).val(),
        tid: $("#tid" + linje).val(),
        barnePris: $("#barnePris" + linje).val(),
        voksenPris: $("#voksenPris" + linje).val()
    }
    var endreOK = confirm("Ønsker du å endre på tur med TurId: " + tur.TurId + " ?");
    if (endreOK) {
        var url = "bestilling/EndreTur";
        $.post(url, tur, function () {
            $(location).attr('href', 'admin.html');
        });
    }
}

function SlettTur(linje) {
    var TurId = $("#TurId" + linje).val();
    var slettOK = confirm("Ønsker du å slette tur med TurId: " + TurId + " ?");
    if (slettOK) {
        var url = "bestilling/SlettTur?TurId=" + TurId;
        $.get(url, function () {
            window.location.href = "admin.html";
        });
    }
}
