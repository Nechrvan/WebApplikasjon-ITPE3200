using Xunit;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using WebApplikasjon_ITPE3200.Controllers;
using WebApplikasjon_ITPE3200.Models;
using WebApplikasjon_ITPE3200.DAL;

namespace test1
{

    /*
      Metoder som ble testet: 
     
     - HentAlleStasjoner 
     - HentEndeStasjoner 
     - HentAlleTurer 
     - Kreditt 
     - OpprettTur 
     - SlettTur 
     - EndreTur 
     - LoggInn 
     - Lagre 
     */
    public class test1
    {
        private const string _loggetInn = "loggetInn";
        private const string _ikkeLoggetInn = "";

        private readonly Mock<IBaatBestillingRepository> mockRep = new Mock<IBaatBestillingRepository>();
        private readonly Mock<ILogger<BestillingController>> mockLog = new Mock<ILogger<BestillingController>>();

        private readonly Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
        private readonly MockHttpSession mockSession = new MockHttpSession();

        [Fact]
        public async Task HentAlleStasjoner()
        {
            var stasjon1 = new Stasjon
            {
                SId = 1,
                StasjonsNavn = "Hirtshals",
            };
            var stasjon2 = new Stasjon
            {
                SId = 2,
                StasjonsNavn = "Kiel"
            };
            var stasjon3 = new Stasjon
            {
                SId = 3,
                StasjonsNavn = "Kristiansand"
            };
            var stasjon4 = new Stasjon
            {
                SId = 4,
                StasjonsNavn = "Larvik",
            };
            var stasjon5 = new Stasjon
            {
                SId = 5,
                StasjonsNavn = "Oslo"
            };
            var stasjon6 = new Stasjon
            {
                SId = 6,
                StasjonsNavn = "Sandefjord"
            };
            var stasjon7 = new Stasjon
            {
                SId = 7,
                StasjonsNavn = "Strømstad"
            };
            var stasjonsListe = new List<Stasjon>();
            stasjonsListe.Add(stasjon1);
            stasjonsListe.Add(stasjon2);
            stasjonsListe.Add(stasjon3);
            stasjonsListe.Add(stasjon4);
            stasjonsListe.Add(stasjon5);
            stasjonsListe.Add(stasjon6);
            stasjonsListe.Add(stasjon7);

            mockRep.Setup(s => s.HentAlleStasjoner()).ReturnsAsync(stasjonsListe);
            var bestillingController = new BestillingController(mockRep.Object, mockLog.Object);
            var resultat = await bestillingController.HentAlleStasjoner() as OkObjectResult;
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal(stasjonsListe, resultat.Value);
        }

        [Fact]
        public async Task HentAlleStasjonerTom()
        {
            mockRep.Setup(s => s.HentAlleStasjoner()).ReturnsAsync(() => null);
            var bestillingController = new BestillingController(mockRep.Object, mockLog.Object);
            var resultat = await bestillingController.HentAlleStasjoner() as OkObjectResult;
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Null(resultat.Value);
        }

        [Fact]
        public async Task HentEndeStasjonerOK()
        {

            var stasjon1 = new Stasjon
            {
                SId = 1,
                StasjonsNavn = "Hirtshals",
            };
            var stasjon2 = new Stasjon
            {
                SId = 2,
                StasjonsNavn = "Kiel"
            };
            var stasjon3 = new Stasjon
            {
                SId = 3,
                StasjonsNavn = "Kristiansand"
            };
            var stasjon4 = new Stasjon
            {
                SId = 4,
                StasjonsNavn = "Larvik",
            };
            var stasjon5 = new Stasjon
            {
                SId = 5,
                StasjonsNavn = "Oslo"
            };
            var stasjon6 = new Stasjon
            {
                SId = 6,
                StasjonsNavn = "Sandefjord"
            };
            var stasjon7 = new Stasjon
            {
                SId = 7,
                StasjonsNavn = "Strømstad"
            };

            var tur1 = new Turer
            {
                TurId = 1,
                StartStasjon = stasjon1,
                EndeStasjon = stasjon2,
                Dato = "01/12/2021",
                Tid = "12:15-15:30",
                BarnePris = 199,
                VoksenPris = 499
            };

            var endeStasjonListe = new List<Stasjon>();
            endeStasjonListe.Add(stasjon1);
            endeStasjonListe.Add(stasjon2);

            mockRep.Setup(s => s.HentEndeStasjoner(stasjon1.StasjonsNavn)).ReturnsAsync(endeStasjonListe);
            var bestillingController = new BestillingController(mockRep.Object, mockLog.Object);
            var resultat = await bestillingController.HentEndeStasjoner(stasjon1.StasjonsNavn) as OkObjectResult;
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal(endeStasjonListe, resultat.Value);
        }

        [Fact]
        public async Task HentEndeStasjonerTom()
        {
            var stasjon1 = new Stasjon();
            var endeStasjonListe = new List<Stasjon>();
            endeStasjonListe.Add(stasjon1);
            mockRep.Setup(s => s.HentEndeStasjoner(stasjon1.StasjonsNavn)).ReturnsAsync(() => null);
            var bestillingController = new BestillingController(mockRep.Object, mockLog.Object);
            var resultat = await bestillingController.HentEndeStasjoner(stasjon1.StasjonsNavn) as OkObjectResult;
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Null(resultat.Value);
        }

        [Fact]
        public async Task HentAlleTurer()
        {
            var stasjon1 = new Stasjon
            {
                SId = 1,
                StasjonsNavn = "Hirtshals",
            };
            var stasjon2 = new Stasjon
            {
                SId = 2,
                StasjonsNavn = "Kiel"
            };
            var stasjon3 = new Stasjon
            {
                SId = 3,
                StasjonsNavn = "Kristiansand"
            };
            var stasjon4 = new Stasjon
            {
                SId = 4,
                StasjonsNavn = "Larvik",
            };
            var stasjon5 = new Stasjon
            {
                SId = 5,
                StasjonsNavn = "Oslo"
            };
            var stasjon6 = new Stasjon
            {
                SId = 6,
                StasjonsNavn = "Sandefjord"
            };
            var stasjon7 = new Stasjon
            {
                SId = 7,
                StasjonsNavn = "Strømstad"
            };
            // Arrange
            var tur1 = new Turer
            {
                TurId = 1,
                StartStasjon = stasjon1,
                EndeStasjon = stasjon2,
               Dato = "01/12/2021",
                Tid = "12:15-15:30",
                BarnePris = 199,
                VoksenPris = 499
            };

            var tur2 = new Turer
            {
                TurId = 2,
                StartStasjon = stasjon1,
                EndeStasjon = stasjon3,
                Dato = "01/12/2021",
                Tid = "20:45-23:59",
                BarnePris = 225,
                VoksenPris = 450
            };

            var tur3 = new Turer
            {
                TurId = 3,
                StartStasjon = stasjon1,
                EndeStasjon = stasjon4,
                Dato = "01/12/2021",
                Tid = "22:15-02:00",
                BarnePris = 350,
                VoksenPris = 799
            };
            var turListe = new List<Turer>();
            turListe.Add(tur1);
            turListe.Add(tur2);
            turListe.Add(tur3);

            mockRep.Setup(k => k.HentAlleTurer()).ReturnsAsync(turListe);
            var bestillingController = new BestillingController(mockRep.Object, mockLog.Object);
            var resultat = await bestillingController.HentAlleTurer() as OkObjectResult;
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal(turListe, resultat.Value);
        }

        [Fact]
        public async Task HentAlleTurerTomListe()
        {
            mockRep.Setup(k => k.HentAlleTurer()).ReturnsAsync(() => null);
            var bestillingController = new BestillingController(mockRep.Object, mockLog.Object);
            var resultat = await bestillingController.HentAlleTurer() as OkObjectResult;
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Null(resultat.Value);
        }

        [Fact]
        public async Task OpprettTurLoggetInnOK()
        {
            // Arrange
            mockRep.Setup(k => k.OpprettTur(It.IsAny<Tur>())).ReturnsAsync(true);

            var bestillingController = new BestillingController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await bestillingController.OpprettTur(It.IsAny<Tur>()) as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Tur registrert", resultat.Value);
        }

        [Fact]
        public async Task OpprettTurLoggetInnIkkeOK()
        {
            // Arrange

            mockRep.Setup(k => k.OpprettTur(It.IsAny<Tur>())).ReturnsAsync(false);

            var bestillingController = new BestillingController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await bestillingController.OpprettTur(It.IsAny<Tur>()) as BadRequestObjectResult;

            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Turen ble ikke registrert", resultat.Value);
        }

        [Fact]
        public async Task OpprettTurLoggetInnFeilModel()
        {
            var tur1 = new Tur
            {
                TurId = 1,
                StartStasjon = "",
                EndeStasjon = "Oslo",
                Dato = "10/12/2021",
                Tid = "07:00",
                BarnePris = 75,
                VoksenPris = 190
            };

            mockRep.Setup(k => k.OpprettTur(tur1)).ReturnsAsync(true);
            var bestillingController = new BestillingController(mockRep.Object, mockLog.Object);
            bestillingController.ModelState.AddModelError("Startstasjon", "Feil i inputvalidering på server");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await bestillingController.OpprettTur(tur1) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering på server", resultat.Value);
        }

        [Fact]
        public async Task OpprettTurIkkeLoggetInn()
        {
            mockRep.Setup(k => k.OpprettTur(It.IsAny<Tur>())).ReturnsAsync(true);

            var bestillingController = new BestillingController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await bestillingController.OpprettTur(It.IsAny<Tur>()) as UnauthorizedObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        [Fact]
        public async Task SlettTurLoggetInnOK()
        {
            // Arrange

            mockRep.Setup(k => k.SlettTur(It.IsAny<int>())).ReturnsAsync(true);

            var bestillingController = new BestillingController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await bestillingController.SlettTur(It.IsAny<int>()) as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Tur slettet", resultat.Value);
        }

        [Fact]
        public async Task SlettTurLoggetInnIkkeOK()
        {
            // Arrange

            mockRep.Setup(k => k.SlettTur(It.IsAny<int>())).ReturnsAsync(false);

            var bestillingController = new BestillingController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await bestillingController.SlettTur(It.IsAny<int>()) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Tur ble ikke slettet", resultat.Value);
        }

        [Fact]
        public async Task SletteTurIkkeLoggetInn()
        {
            mockRep.Setup(k => k.SlettTur(It.IsAny<int>())).ReturnsAsync(true);

            var bestillingController = new BestillingController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await bestillingController.SlettTur(It.IsAny<int>()) as UnauthorizedObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }


        [Fact]
        public async Task EndreTurLoggetInnOK()
        {
            // Arrange

            mockRep.Setup(k => k.EndreTur(It.IsAny<Tur>())).ReturnsAsync(true);

            var bestillingController = new BestillingController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await bestillingController.EndreTur(It.IsAny<Tur>()) as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Tur registrert", resultat.Value);
        }

        [Fact]
        public async Task EndreLoggetInnIkkeOK()
        {
            // Arrange

            mockRep.Setup(k => k.EndreTur(It.IsAny<Tur>())).ReturnsAsync(false);

            var bestillingController = new BestillingController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await bestillingController.EndreTur(It.IsAny<Tur>()) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Tur ble ikke registrert", resultat.Value);
        }

        [Fact]
        public async Task EndreLoggetInnFeilModel()
        {

            var tur1 = new Tur
            {
                TurId = 1,
                StartStasjon = "",
                EndeStasjon = "Oslo",
                Dato = "10/12/2021",
                Tid = "07:00",
                BarnePris = 75,
                VoksenPris = 190
            };

            mockRep.Setup(k => k.EndreTur(tur1)).ReturnsAsync(true);

            var bestillingController = new BestillingController(mockRep.Object, mockLog.Object);

            bestillingController.ModelState.AddModelError("Startstasjon", "Feil i inputvalidering på server");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await bestillingController.EndreTur(tur1) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering på server", resultat.Value);
        }

        [Fact]
        public async Task EndreIkkeLoggetInn()
        {
            mockRep.Setup(k => k.EndreTur(It.IsAny<Tur>())).ReturnsAsync(true);

            var bestillingController = new BestillingController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await bestillingController.EndreTur(It.IsAny<Tur>()) as UnauthorizedObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        [Fact]
        public async Task LoggInnOK()
        {
            mockRep.Setup(k => k.LoggInn(It.IsAny<Bruker>())).ReturnsAsync(true);

            var bestillingController = new BestillingController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await bestillingController.LoggInn(It.IsAny<Bruker>()) as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.True((bool)resultat.Value);
        }

        [Fact]
        public async Task LoggInnFeilPassordEllerBruker()
        {
            mockRep.Setup(k => k.LoggInn(It.IsAny<Bruker>())).ReturnsAsync(false);

            var bestillingController = new BestillingController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await bestillingController.LoggInn(It.IsAny<Bruker>()) as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.False((bool)resultat.Value);
        }

        [Fact]
        public async Task LoggInnInputFeil()
        {
            mockRep.Setup(k => k.LoggInn(It.IsAny<Bruker>())).ReturnsAsync(true);

            var bestillingController = new BestillingController(mockRep.Object, mockLog.Object);

            bestillingController.ModelState.AddModelError("Brukernavn", "Feil i inputvalidering på server");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await bestillingController.LoggInn(It.IsAny<Bruker>()) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering på server", resultat.Value);
        }


        [Fact]
        public async Task LagreOK()
        {
            var innBaatbestilling = new BaatBestilling
            {
                Id = 1,
                Fornavn = "Per",
                Etternavn = "Hansen",
                Telefonnummer = "12345678",
                Epost = "PeHansen@oslomet.no",
                Adresse = "Drammensveien 85",
                Postnummer = "1234",
                Poststed = "Oslo",
                AntallBarn = 1,
                AntallVoksne = 2,
                Dato = "01/12/2021",
                Tid = "09:00",
                BarnePris = 75,
                VoksenPris = 190,
                StartStasjon = "Oslo",
                EndeStasjon = "Kiel"
            };

            mockRep.Setup(k => k.Lagre(innBaatbestilling)).ReturnsAsync(true);
            var bestillingController = new BestillingController(mockRep.Object, mockLog.Object);
            var resultat = await bestillingController.Lagre(innBaatbestilling) as OkObjectResult;
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Bestilling registrert", resultat.Value);
        }

        [Fact]
        public async Task LagreIkkeOK()
        {
            var innBaatbestilling = new BaatBestilling
            {
                Id = 1,
                Fornavn = "Per",
                Etternavn = "Hansen",
                Telefonnummer = "12345678",
                Epost = "PeHansen@oslomet.no",
                Adresse = "Drammensveien 85",
                Postnummer = "1234",
                Poststed = "Oslo",
                AntallBarn = 1,
                AntallVoksne = 2,
                Dato = "01/12/2021",
                Tid = "09:00",
                BarnePris = 75,
                VoksenPris = 190,
                StartStasjon = "Oslo",
            };

            mockRep.Setup(k => k.Lagre(innBaatbestilling)).ReturnsAsync(false);
            var bestillingController = new BestillingController(mockRep.Object, mockLog.Object);
            var resultat = await bestillingController.Lagre(innBaatbestilling) as BadRequestObjectResult;
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Bestilling ble ikke registrert", resultat.Value);
        }

        [Fact]
        public async Task LagreFeilInput()
        {
            var innBaatbestilling = new BaatBestilling
            {
                Id = 1,
                Fornavn = "Per",
                Etternavn = "Hansen",
                Telefonnummer = "12",
                Epost = "PeHansen@oslomet.no",
                Adresse = "Drammensveien 85",
                Postnummer = "1234",
                Poststed = "Oslo",
                AntallBarn = 1,
                AntallVoksne = 2,
                Dato = "01/12/2021",
                Tid = "09:00",
                BarnePris = 75,
                VoksenPris = 190,
                StartStasjon = "Oslo",
                EndeStasjon = "Kiel"
            };

            mockRep.Setup(b => b.Lagre(innBaatbestilling)).ReturnsAsync(true);
            var bestillingController = new BestillingController(mockRep.Object, mockLog.Object);
            bestillingController.ModelState.AddModelError("Telefonnummer", "Feil i inputvalidering på server");

            var resultat = await bestillingController.Lagre(innBaatbestilling) as BadRequestObjectResult;
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering på server", resultat.Value);
        }
        [Fact]
        public async Task KredittOK()
        {
            var innKreditt = new Kreditt
            {
                Id = 1,
                KId = 1,
                Kortnummer = "1234123412341234",
                UtlopsDato = "11/22",
                Cvc = "123",

            };

            mockRep.Setup(k => k.Kreditt(innKreditt)).ReturnsAsync(true);
            var bestillingController = new BestillingController(mockRep.Object, mockLog.Object);
            var resultat = await bestillingController.Kreditt(innKreditt) as OkObjectResult;
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Kredittinfo ble lagret", resultat.Value);
        }

        [Fact]
        public async Task KredittFeilInfo()
        {
            var innKreditt = new Kreditt
            {
                Id = 1,
                KId=1,
                Kortnummer="1234123412341234",
                UtlopsDato="11/22",
                
            };

            mockRep.Setup(k => k.Kreditt(innKreditt)).ReturnsAsync(true);
            var bestillingController = new BestillingController(mockRep.Object, mockLog.Object);
            var resultat = await bestillingController.Kreditt(innKreditt) as OkObjectResult;
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Kredittinfo ble lagret", resultat.Value);
        }

        [Fact]
        public async Task KredittInnInputFeil()
        {
            var innKreditt = new Kreditt
            {
                Id = 1,
                KId = 1,
                Kortnummer = "123412341234123",
                UtlopsDato = "11/22",
                Cvc="123",

            };

            mockRep.Setup(k => k.Kreditt(innKreditt)).ReturnsAsync(true);
            var bestillingController = new BestillingController(mockRep.Object, mockLog.Object);
            var resultat = await bestillingController.Kreditt(innKreditt) as OkObjectResult;
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Kredittinfo ble lagret", resultat.Value);
        }
    }

    }


