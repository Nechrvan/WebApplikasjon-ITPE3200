function validerStartstasjon() {
        const ikkeValgtSS = $('#startstasjon option:selected').val();
        if (ikkeValgtSS === "Velg startstasjon") {
            $("#feilStartstasjon").html("Må velge en startstasjon");
            return false;
        }
        else {
            $("#feilStartstasjon").html("");
            return true;
        }
    }

    function validerEndestasjon() {
        const ikkeValgtES = $('#endestasjon option:selected').val();
        if (ikkeValgtES === "Velg endestasjon") {
            $("#feilEndestasjon").html("Må velge en endestasjon");
            return false;
        }
        else {
            $("#feilEndestasjon").html("");
            return true;
        }
    }
    function validerTid() {
        const tid = $('#tid option:selected').val();
        if (tid === "Velg tidspunkt") {
            $("#feilTid").html("Må velge et tidspunkt");
            return false;
        }
        else {
            $("#feilTid").html("");
            return true;
        }
    }

    function validerFornavn(fornavn) {
        const regexp = /^[a-zA-ZæøåÆØÅ\.\ \-]{2,20}$/;
        const ok = regexp.test(fornavn);
        if (!ok) {
            $("#feilFornavn").html("Fornavnet må bestå av 2 til 20 bokstaver");
            return false;
        }
        else {
            $("#feilFornavn").html("");
            return true;
        }
    }

    function validerEtternavn(etternavn) {
        const regexp = /^[a-zA-ZæøåÆØÅ\.\ \-]{2,20}$/;
        const ok = regexp.test(etternavn);
        if (!ok) {
            $("#feilEtternavn").html("Etternavnet må bestå av 2 til 20 bokstaver");
            return false;
        }
        else {
            $("#feilEtternavn").html("");
            return true;
        }
    }

    function validerTelefonnummer(Telefonnummer) {
        const regexp = /^[0-9]{8}$/;
        const ok = regexp.test(Telefonnummer);
        if (!ok) {
            $("#feilTelefonnummer").html("Telefonnummeret må bestå av 8 tall");
            return false;
        }
        else {
            $("#feilTelefonnummer").html("");
            return true;
        }
    }

    function validerEpost(Epost) {
        const regexp = /^[A-Za-z0-9]{1}[A-Za-z0-9_.-]*@{1}[A-Za-z0-9_.-]{1,}\.[A-Za-z0-9]{1,}$/;
        const ok = regexp.test(Epost);
        if (!ok) {
            $("#feilEpost").html("Epost er skrevet inn feil");
            return false;
        } else {
            $("#feilEpost").html("");
            return true;
        }
    }

    function validerAdresse(adresse) {
        var regexp = /^[0-9a-zA-ZæøåÆØÅ\ \.\-]{2,50}$/;
        var ok = regexp.test(adresse);
        if (!ok) {
            $("#feilAdresse").html("Adressen må bestå av 2 til 50 bokstaver og tall");
            return false;
        }
        else {
            $("#feilAdresse").html("");
            return true;
        }
    }

    function validerPostnummer(postnummer) {
        var regexp = /^[0-9]{4}$/;
        var ok = regexp.test(postnummer);
        if (!ok) {
            $("#feilPostnummer").html("Postnummeret må bestå av 4 tall");
            return false;
        }
        else {
            $("#feilPostnummer").html("");
            return true;
        }
    }

    function validerPoststed(poststed) {
        var regexp = /^[a-zA-ZæøåÆØÅ\ \.\-]{2,20}/;
        var ok = regexp.test(poststed);
        if (!ok) {
            $("#feilPoststed").html("Poststedet må bestå av 2 til 20 bokstaver");
            return false;
        }
        else {
            $("#feilPoststed").html("");
            return true;
        }
    }

    function validerAntallBarn(AntallBarn) {
        const regexp = /^[0-9]{1}$/;
        const ok = regexp.test(AntallBarn);
        if (!ok) {
            $("#feilAntallBarn").html("Antall barn kan kun bestå av 0-9 stk");
            return false;
        }
        else {
            $("#feilAntallBarn").html("");
            return true;
        }
    }

    function validerAntallVoksne(AntallVoksne) {
        const regexp = /^[0-9]{1}$/;
        const ok = regexp.test(AntallVoksne);
        if (!ok) {
            $("#feilAntallVoksne").html("Antall voksne kan kun bestå av 0-9 stk");
            return false;
        }
        else {
            $("#feilAntallVoksne").html("");
            return true;
        }
    }
function validerKortnummer(Kortnummer) {
    const regexp = /^[0-9]{16}$/;
    const ok = regexp.test(Kortnummer);
    if (!ok) {
        $("#feilKortnummer").html("Kortnummer må bestå av 16 tall");
        return false;
    } else {
        $("#feilKortnummer").html("");
        return true;
    }
}
function validerUtlopsDato(utlopsDato) {
    const regexp = /^\d{2}\/\d{2}$/;
    const ok = regexp.test(utlopsDato);
    if (!ok) {
        $("#feilUtlopsDato").html("UtløpsDatoen må være sånn month/År Fex:(09/21)");
        return false;
    } else {
        $("#feilUtlopsDato").html("");
        return true;
    }
}
function validerCvc(Cvc) {
    const regexp = /^[0-9]{3}$/;
    const ok = regexp.test(Cvc);
    if (!ok) {
        $("#feilCvc").html("Cvc'en må bestå av 3 tall");
        return false;
    } else {
        $("#feilCvc").html("");
        return true;
    }
}


    // AdminValidering
    function validerBarnePrisAdmin(barnePrisAdmin) {
        const regex = /^[0-9]{2,4}$/;
        const ok = regex.test(barnePrisAdmin);
        if (!ok) {
            $("#feilBarnePrisAdmin").html("Barnepris må være mellom 2 og 4 sifre");
            return false;
        } else {
            $("#feilBarnePrisAdmin").html("");
            return true;
        }
    }

    function validerVoksenPrisAdmin(voksenPrisAdmin) {
        const regex = /^[0-9]{2,4}$/;
        const ok = regex.test(voksenPrisAdmin);
        if (!ok) {
            $("#feilVoksenPrisAdmin").html("Voksenpris må være mellom 2 og fire sifre");
            return false;
        } else {
            $("#feilVoksenPrisAdmin").html("");
            return true;
        }
    }

    function validerStartStasjonAdmin(startstasjonAdmin) {
        const regexp = /^[a-zA-ZæøåÆØÅ. \-]{2,20}$/;
        const ok = regexp.test(startstasjonAdmin);
        if (!ok) {
            $("#feilStartStasjonAdmin").html("Startstasjon må bestå av 2 til 20 bokstaver")
            return false;
        } else {
            $("#feilStartStasjonAdmin").html("");
            return true;
        }
    }

    function validerEndeStasjonAdmin(endestasjonAdmin) {
        const regex = /^[a-zA-ZæøåÆØÅ. \-]{2,20}$/;
        const ok = regex.test(endestasjonAdmin);
        if (!ok) {
            $("#feilEndeStasjonAdmin").html("Endestasjon må bestå av 2 til 20 bokstaver")
            return false;
        } else {
            $("#feilEndeStasjonAdmin").html("");
            return true;
        }
    }

    function validerDatoAdmin(dato) {
        const regexp = /^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$/;

        const ok = regexp.test(dato);
        if (!ok) {
            $("#feilDatoAdmin").html("Dato må være i riktig format.");
            return false;
        }
        else {
            $("#feilDatoAdmin").html("");
            return true;
        }
    }

    function validerDatoOgTid(dato, tid) {
        var today = new Date();
        var dateParts = dato.split("/");
        var timeParts = tid.split(":");
        var datoInput = new Date(+dateParts[2], dateParts[1] - 1, +dateParts[0], timeParts[0], timeParts[1]);

        const regex = /^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$/;
        const ok = regex.test(dato);
        if (!ok) {
            $("#feilDatoAdmin").html("Formatet på dato må være DD/MM/YYYY");
            return false;
        } else {
            if (today.getTime() < datoInput.getTime()) {
                $("#feilDatoAdmin").html("");
                return true;
            } else {
                $("#feilDatoAdmin").html("Dato/tid kan ikke være tilbake i tid");
                return false;
            }
        }
    }

    function validerTidAdmin(tidAdmin) {
        const regex = /^([01]?[0-9]|2[0-3]):[0-5][0-9]$/;
        const ok = regex.test(tidAdmin);
        if (!ok) {
            $("#feilTidAdmin").html("Formatet på tid må være TT:SS ")
            return false;
        } else {
            $("#feilTidAdmin").html("");
            return true;
        }
    }

    function validerLikeStasjonerOpprett() {
        const start = $("#startstasjonAdmin").val();
        const slutt = $("#endestasjonAdmin").val();

        if (start === slutt) {
            $("#feil").html("Stasjonsnavnene kan ikke være like")
            return false;
        }
        else {

            $("#feil").html("")
            return true;
        }
    }

    //Endre validering
    function validerBarnePrisEndre(Barnepris, linje) {
        const regex = /^[0-9]{2,4}$/;
        const ok = regex.test(Barnepris);
        if (!ok) {
            $("#feilBarnePrisAdmin" + linje).html("Barnepris må være mellom 2 og 4 tegn");
            return false;
        } else {
            $("#feilBarnePrisAdmin" + linje).html("");
            return true;
        }
    }

    function validerVoksenPrisEndre(voksenPrisAdmin, linje) {
        const regex = /^[0-9]{2,4}$/;
        const ok = regex.test(voksenPrisAdmin);
        if (!ok) {
            $("#feilVoksenPrisAdmin" + linje).html("Voksenpris må være mellom 2 og 4 tegn");
            return false;
        } else {
            $("#feilVoksenPrisAdmin" + linje).html("");
            return true;
        }
    }

    function validerStartStasjonEndre(startstasjonAdmin, linje) {
        const regexp = /^[a-zA-ZæøåÆØÅ. \-]{2,20}$/;
        const ok = regexp.test(startstasjonAdmin);
        if (!ok) {
            $("#feilStartStasjon" + linje).html("Startstasjon må bestå av 2 til 20 bokstaver")
            return false;
        } else {
            $("#feilStartStasjon" + linje).html("");
            return true;
        }
    }

    function validerEndeStasjonEndre(endestasjonAdmin, linje) {
        const regex = /^[a-zA-ZæøåÆØÅ. \-]{2,20}$/;
        const ok = regex.test(endestasjonAdmin);
        if (!ok) {
            $("#feilEndeStasjonAdmin" + linje).html("Endestasjon må bestå av 2 til 20 bokstaver")
            return false;
        } else {
            $("#feilEndeStasjonAdmin" + linje).html("");
            return true;
        }
    }



    function validerDatoEndre(datoAdmin, tidAdmin, linje) {
        var today = new Date();
        var dateParts = datoAdmin.split("/");
        var timeParts = tidAdmin.split(":");
        var datoInput = new Date(+dateParts[2], dateParts[1] - 1, +dateParts[0], timeParts[0], timeParts[1]);

        const regex = /^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$/;
        const ok = regex.test(datoAdmin);
        if (!ok) {
            $("#feilDatoAdmin" + linje).html("Formatet på dato må være DD/MM/YYYY");
            return false;
        } else {
            if (today.getTime() < datoInput.getTime()) {
                $("#feilDatoAdmin" + linje).html("");
                return true;
            }
            else {
                $("#slettFeil" + linje).html("Dato/tid kan ikke være tilbake i tid");
                return false;
            }
        }
    }

    function validerTidEndre(tidAdmin, linje) {
        const regex = /^([01]?[0-9]|2[0-3]):[0-5][0-9]$/;
        const ok = regex.test(tidAdmin);
        if (!ok) {
            $("#feilTidAdmin" + linje).html("Formatet på tid må være TT: SS ")
            return false;
        } else {
            $("#feilTidAdmin" + linje).html("");
            return true;
        }
    }

    function validerBrukernavn(brukernavn) {

        const regex = /^[a-zA-ZæøåÆØÅ\.\\-]{2,20}$/;
        const ok = regex.test(brukernavn);
        if (!ok) {

            $("#feilBrukernavn").html("Brukernavnet må være mellom 2-20 bokstaver");
            return false;
        }
        else {
            $("#feilBrukernavn").html("");
            return true;
        }
    }

    function validerPassord(passord) {

        const regex = /^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$/;
        const ok = regex.test(passord);
        if (!ok) {

            $("#feilPassord").html("Feil i passord. Minimum 6 tegn. ");
            return false;
        }
        else {
            $("#feilPassord").html("");
            return true;
        }
    }

    function validerLikeStasjoner(start, slutt, linje) {
        if (start === slutt) {
            $("#slettFeil" + linje).html("Stasjonsnavnene kan ikke være like")
            return false;
        }
        else {
            $("#slettFeil" + linje).html("")
            return true;
        }
    }

    function validerLikeStasjonerOpprett(start, slutt) {
        if (start === slutt) {
            $("#feil").html("Stasjonsnavnene kan ikke være like");
            return false;
        }
        else {
            $("#feil").html("");
            return true;
        }
    }