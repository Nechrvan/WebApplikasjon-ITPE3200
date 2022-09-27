$(function () {
    HentAlleStasjoner();
});

function HentAlleStasjoner() {
    $.get("bestilling/hentAlleStasjoner", function (stasjoner) {
        if (stasjoner) {
            listStartStasjoner(stasjoner);
        } else {
            $("#feil").html("Feil i db");
        }
    });
}

function listStartStasjoner(stasjoner) {
    let ut = "<select class='browser-default custom-select' onchange='listEndeStasjoner()' id='startstasjon'>";
    ut += "<option>Velg startstasjon</option>";
    for (let stasjon of stasjoner) {
        ut += "<option>" + stasjon.stasjonsNavn + "</option>";
    }
    ut += "</select>";
    $("#startstasjon").html(ut);
    console.log(JSON.stringify(stasjoner));
}

function listEndeStasjoner() {
    let startstasjon = $('#startstasjon option:selected').val();
    const url = "bestilling/hentEndeStasjoner?startStasjonsNavn=" + startstasjon;
    $.get(url, function (stasjoner) {
        if (stasjoner) {

            const uniq = new Set(stasjoner.map(e => JSON.stringify(e)));

            const unikeStasjoner = Array.from(uniq).map(e => JSON.parse(e));

            let ut = "<label >Jeg reiser til : </label>";
            ut += "<select class='browser-default custom-select' onchange=' listTidspunkt()'>";
            ut += "<option>Velg endestasjon</option>";

            for (let stasjon of unikeStasjoner) {
                ut += "<option>" + stasjon.stasjonsNavn + "</option>";
            }

            ut += "</select>";
            $("#endestasjon").html(ut);
        } else {
            $("#feil").html("Feil i db");
        }
    });
}
function listTidspunkt() {
    let startstasjon = $('#startstasjon option:selected').val();
    let endestasjon = $('#endestasjon option:selected').val();
    const url = "bestilling/hentAlleTurer";
    $.get(url, function (turer) {
        if (turer) {
            let ut = "<label>Tidspunktet</label>";
            ut += "<select class='browser-default custom-select' id='tidspunkt'>";
            for (let tur of turer) {
                if (startstasjon === tur.startStasjon.stasjonsNavn && endestasjon === tur.endeStasjon.stasjonsNavn) {
                    ut += "<option>" + tur.tid + "</option>";
                }
            }
            ut += "</select>";
            $("#tid").html(ut);
            if (document.getElementById('tidspunkt').options.length == 0) {
                $("#ikkeTurDato").html("Ingen tilgjengelige turer på valgt dato");
            }
            else {
                $("#ikkeTurDato").html("");
            }
        }
        else {
            $("#feil").html("Feil i db");
        }
    });
}

function beregnPris() {
    let startstasjon = $('#startstasjon option:selected').val();
    let endestasjon = $('#endestasjon option:selected').val();
    let tid = $('#tid option:selected').val();
    let antallBarn = $("#antallBarn").val();
    let antallVoksne = $("#antallVoksne").val();

    let pris;
    let barnepris = 0;
    let voksenpris = 0;

    const url = "bestilling/hentAlleTurer";
    $.get(url, function (turer) {
        if (turer) {
            for (let tur of turer) {
                if (startstasjon === tur.startStasjon.stasjonsNavn && endestasjon === tur.endeStasjon.stasjonsNavn /*&& dato === tur.dato*/ && tid == tur.tid) {
                    barnepris = tur.barnePris;
                    voksenpris = tur.voksenPris;
                }
            }
            if (antallBarn > 0 && antallVoksne > 0) {
                pris = (barnepris * antallBarn) + (voksenpris * antallVoksne);
            }
            else if (antallBarn <= 0 && antallVoksne > 0) {
                pris = voksenpris * antallVoksne;
            }
            else if (antallVoksne <= 0 && antallBarn > 0) {
                pris = barnepris * antallBarn;
            }
            else {
                pris = 0;
            }
            if (antallBarn > 0 && antallBarn < 10) {
                $("#prisBarn").html("Pris barn: " + barnepris + " kr x " + antallBarn + " = " + barnepris * antallBarn + " kr");
            }
            else {
                $("#prisBarn").html("");
            }
            if (antallVoksne > 0 && antallVoksne < 10) {
                $("#prisVoksen").html("Pris voksen: " + voksenpris + " kr x " + antallVoksne + " = " + voksenpris * antallVoksne + " kr");
            }
            else {
                $("#prisVoksen").html("");
            }
        }
        else {
            $("#feil").html("Feil i db");
        }
    });
}

function beregnOgValiderBarn() {
    let antallBarn = $("#antallBarn").val();
    validerAntallBarn(antallBarn);
    beregnPris();
}

function beregnOgValiderVoksen() {
    let antallVoksne = $("#antallVoksne").val();
    validerAntallVoksne(antallVoksne);
    beregnPris();
}

function validerOgLagBestilling() {
    const StartstasjonOK = validerStartstasjon($("#startstasjon").val());
    const EndestasjonOK = validerEndestasjon($("#endestasjon").val());
    const TidOK = validerTid($("#tid").val());
    const FornavnOK = validerFornavn($("#fornavn").val());
    const EtternavnOK = validerEtternavn($("#etternavn").val());
    const TelefonnummerOK = validerTelefonnummer($("#telefonnummer").val());
    const EpostOK = validerEpost($("#epost").val());
    const AdresseOK = validerAdresse($("#adresse").val());
    const PostnummerOK = validerPostnummer($("#postnummer").val());
    const PoststedOK = validerPoststed($("#poststed").val());
    const AntallBarnOK = validerAntallBarn($("#antallBarn").val());
    const AntallVoksneOK = validerAntallVoksne($("#antallVoksne").val());
    if (StartstasjonOK && EndestasjonOK && TidOK && FornavnOK && EtternavnOK
        && TelefonnummerOK && EpostOK && AdresseOK && PostnummerOK && PoststedOK && AntallBarnOK && AntallVoksneOK) {
        lagMinEgenPopUp();
    }
}

function lagMinEgenPopUp() {
    const options = { show: true };
    $('#myModal').modal('show')
    formaterBestilling();
}

function formaterBestilling() {
    let startstasjon = $('#startstasjon option:selected').val();
    let endestasjon = $('#endestasjon option:selected').val();
    let tid = $('#tid option:selected').val();
    let antallBarn = $("#antallBarn").val();
    let antallVoksne = $("#antallVoksne").val();

    let pris;
    let barnepris = 0;
    let voksenpris = 0;

    const url = "bestilling/hentAlleTurer";
    $.get(url, function (turer) {
        if (turer) {
            for (let tur of turer) {
                if (startstasjon === tur.startStasjon.stasjonsNavn && endestasjon === tur.endeStasjon.stasjonsNavn /*&& dato === tur.dato*/ && tid == tur.tid) {
                    console.log("tur.barnepris: " + tur.barnePris + ", antallBarn: " + antallBarn + ", tur.voksenPris: " + tur.voksenPris + ", antallVoksne: " + antallVoksne);
                    barnepris = tur.barnePris;
                    voksenpris = tur.voksenPris;
                }
            }
            if (antallBarn > 0 && antallVoksne > 0) {
                pris = (barnepris * antallBarn) + (voksenpris * antallVoksne);
            }
            else if (antallBarn <= 0 && antallVoksne > 0) {
                pris = voksenpris * antallVoksne;
            }
            else if (antallVoksne <= 0 && antallBarn > 0) {
                pris = barnepris * antallBarn;
            }
            else {
                pris = 0;
            }
        }

        let ut = "<table class='table table-striped'><tr>" +
            "<tr>Startstasjon : </tr>" + $("#startstasjon option:selected").val() + "<br>" +
            "<tr>Endestasjon : </tr>" + $("#endestasjon option:selected").val() + "<br>" +
            "<tr>Tid : </tr>" + $("#tid option:selected").val() + "<br>"+
            "<tr>TurDato : </tr>" + $("#avgang").val() + "<br>" +
            "<tr>ReturDato : </tr>" + $("#retur").val() + "<br>" +
            "<tr><br>" +
            "<tr>Fornavn : </tr>" + $("#fornavn").val() + "<br>" +
            "<tr>Etternav : </tr>" + $("#etternavn").val() + "<br>" +
            "<tr>Telefonnummer : </tr>" + $("#telefonnummer").val() + "<br>" +
            "<tr>Epost : </tr>" + $("#epost").val() + "<br>" +
            "<tr>Adresse : </tr>" + $("#adresse").val() + "<br>" +
            "<tr>Postnummer : </tr>" + $("#postnummer").val() + "<br>" +
            "<tr>Poststed : </tr>" + $("#poststed").val() + "<br>" +
            "<tr><br>" +
            "<tr>Antall barn : </tr>" + $("#antallBarn").val() + "<br>" +
            "<tr>Antall voksne : </tr>" + $("#antallVoksne").val() + "<br>" +
            "<tr>Totalpris : </tr>" + pris + "<br>" +
            "</tr>";
        ut += "</table>";
        $("#innhold").html(ut);
    });
}

function lagreBestilling() {
    const bestilling = {
        startStasjon: $("#startstasjon option:selected").val(),
        endeStasjon: $("#endestasjon option:selected").val(),
        tid: $("#tid option:selected").val(),
        fornavn: $("#fornavn").val(),
        etternavn: $("#etternavn").val(),
        telefonnummer: $("#telefonnummer").val(),
        epost: $("#epost").val(),
        adresse: $("#adresse").val(),
        postnummer: $("#postnummer").val(),
        poststed: $("#poststed").val(),
        antallBarn: $("#antallBarn").val(),
        ticketType: getTicketType(),
        departureDato: $("#avgang").val(),
        returnDato: $("#retur").val(),
        ticketClass: getKlassetType(),
        antallVoksne: $("#antallVoksne").val()
    }

    const url = "bestilling/lagre";
    $.post(url, bestilling, function () {
        window.location.href = 'bekreft.html';
    })
        .fail(function () {
            $("#feil").html("Feil på server - prøv igjen senere");
        });
}
function getKlassetType() {
    const econonyInput = $("#economy");
    const fristClassInput = $("#first_Class");
    const businessInput =$("#business");

    const klassetArray = [econonyInput, fristClassInput, businessInput];

    let type;
    for (let klassetType of klassetArray) {
        if (klassetType.checked) {
            type = klassetType.value
        }
    }
    return type;
}
function showKredittForm() {
    const ticketType = getTicketType();
    if (ticketType === 'En vei') {
        returDate = $("#retur").val(" ");
    } else {
        returDate = $("#retur").val();
    }
}
function getTicketType() {
    const singleInput = document.getElementById("single");
    const turReturInput = document.getElementById("turRetur");

    const ticketArray = [singleInput, turReturInput];
    let type;

    for (let ticketType of ticketArray) {
        if (ticketType.checked) {
            type = ticketType.value
        }
    }
    return type;
}
$(function () {

    hentAlleStasjoner();

    if (getTicketType() === 'En vei') {
        hideReturDatoInput()
    }
    const avgangInput = document.getElementById("avgang");
    const returInput = document.getElementById("retur");


    setDefaultDato(avgangInput);
    setDefaultDato(returInput);

    const currentDate = getCurrentDateString();
    deaktivereTidligereDatoer(avgangInput, currentDate);
    deaktivereTidligereDatoer(returInput, currentDate);
})

function onAvgangChange() {
    if (getTicketType() === 'Retur') {
        const avgangInput = document.getElementById("avgang")
        const returInput = document.getElementById("retur")
        returInput.value = avgangInput.value
        deaktivereTidligereDatoer(returInput, avgangInput.value)
    }
}

function onTicketTypeChange() {
    const ticketType = getTicketType();
    if (ticketType === 'En vei') {
        hideReturDatoInput()
    } else {
        showReturDatoInput();
        const avgangInput = document.getElementById("avgang")
        const returInput = document.getElementById("retur")
        returInput.value = avgangInput.value
        deaktivereTidligereDatoer(returInput, avgangInput.value)
    }
}

function setDefaultDato(datoInput) {
    const currentDate = getCurrentDateString();
    datoInput.value = currentDate;
}

function getCurrentDateString() {
    const currentDate = new Date();
    const month = parseInt(currentDate.getMonth()) + 1;
    const date = currentDate.getFullYear() + "-" + month.toString().padStart(2, '0') + "-" + currentDate.getDate().toString().padStart(2, '0');
    return date;
}

function deaktivereTidligereDatoer(datoInput, date) {
    datoInput.setAttribute('min', date)
}

function hideReturDatoInput() {
    const returInput = document.getElementById("retur");
    const returLabel = document.getElementById("returLabel")
    returInput.style.display = "none";
    returLabel.style.display = "none";
}

function showReturDatoInput() {
    const returInput = document.getElementById("retur");
    const returLabel = document.getElementById("returLabel")
    returInput.style.display = "initial";
    returLabel.style.display = "initial";
}

function kreditt() {
    const kortnummerOk = valideringKortnummer($("#kortnummer").val());
    const utlopsDatoOK = valideringUtlopsDato($("#utlopsDato").val());
    const cvcOK = validerCvc($("#cvc").val());


    if (kortnummerOk && utlopsDatoOK && cvcOK) {
        const kreditt = {
            kortnummerO: $("#kortnummer").val(),
            utlopsDato: $("#utlopsDato").val(),
             cvc: $("#cvc").val()
        }
        $.post("Kreditt/lagre", kreditt, function (OK) {
            if (OK) {
                window.location.href = 'bekreft.html';
            }
            else {
                $("#feil").html("Feil i input.");
            }
        })
            .fail(function (feil) {
                $("#feil").html("Feil på server.");
            });
    }
}