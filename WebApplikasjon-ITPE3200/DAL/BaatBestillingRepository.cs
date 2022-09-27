using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApplikasjon_ITPE3200.Models;
using WebApplikasjon_ITPE3200.DAL;

using static WebApplikasjon_ITPE3200.Models.KundeContext;
using System.Diagnostics.CodeAnalysis;

namespace WebApplikasjon_ITPE3200.DAL
{
    [ExcludeFromCodeCoverage]
    public class BaatBestillingRepository : IBaatBestillingRepository
    {
        private readonly KundeContext _db;
        private ILogger<BaatBestillingRepository> _log;

        public BaatBestillingRepository(KundeContext db, ILogger<BaatBestillingRepository> log)
        {
            _db = db;
            _log = log;
        }

        public async Task<List<Stasjon>> HentAlleStasjoner()
        {
            List<Stasjon> alleStasjoner = await _db.Stasjoner.ToListAsync();
            return alleStasjoner;
        }

        public async Task<List<Turer>> HentAlleTurer()
        {
            List<Turer> alleTurer = await _db.Turer.ToListAsync();
            return alleTurer;
        }

        public async Task<List<Stasjon>> HentEndeStasjoner(string startStasjonsNavn)
        {
            List<Turer> alleTurer = await _db.Turer.ToListAsync();
            var endeStasjon = new List<Stasjon>();

            foreach (var turen in alleTurer)
            {
                if (startStasjonsNavn.Equals(turen.StartStasjon.StasjonsNavn))
                {
                    endeStasjon.Add(turen.EndeStasjon);
                }
            }
            return endeStasjon;
        }
        public async Task<bool> Kreditt(Kreditt kredittInfo)
        {
            try
            {
                var nyKreditt = new Kreditt();
                nyKreditt.Kortnummer = kredittInfo.Kortnummer;
                nyKreditt.KId = kredittInfo.KId;
                nyKreditt.UtlopsDato = kredittInfo.UtlopsDato;
                nyKreditt.Cvc = kredittInfo.Cvc;

                _db.Kreditt.Add(nyKreditt);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return false;
            }
        }

        public async Task<bool> Lagre(BaatBestilling innBaatBestilling)
        {
            int turID = 0;
            List<Turer> alleTurer = await _db.Turer.ToListAsync();
            foreach (var turen in alleTurer)
            {
                if (innBaatBestilling.StartStasjon.Equals(turen.StartStasjon.StasjonsNavn) &&
                    innBaatBestilling.EndeStasjon.Equals(turen.EndeStasjon.StasjonsNavn) &&
                    innBaatBestilling.Tid.Equals(turen.Tid)  )
                {
                    turID = turen.TurId;
                }
            }
            Turer funnetTur = _db.Turer.Find(turID);

            double totalpris = (innBaatBestilling.AntallBarn * funnetTur.BarnePris) + (innBaatBestilling.AntallVoksne * funnetTur.VoksenPris);


            int kundeID = 0;
            List<Kunde> alleKunder = await _db.Kunder.ToListAsync();

            foreach (var kunde in alleKunder)
            {
                if (innBaatBestilling.Fornavn.Equals(kunde.Fornavn) &&
                    innBaatBestilling.Etternavn.Equals(kunde.Etternavn) &&
                    innBaatBestilling.Epost.Equals(kunde.Epost) &&
                    innBaatBestilling.Telefonnummer.Equals(kunde.Telefonnummer) &&
                    innBaatBestilling.Adresse.Equals(kunde.Adresse) &&
                    innBaatBestilling.Postnummer.Equals(kunde.Postnummer) &&
                     innBaatBestilling.Poststed.Equals(kunde.Poststed)
                 )
                {
                    kundeID = kunde.KId;
                }
            }
            try
            {
                var nyBestillingRad = new Bestilling();
                nyBestillingRad.AntallBarn = innBaatBestilling.AntallBarn;
                nyBestillingRad.AntallVoksne = innBaatBestilling.AntallVoksne;
                nyBestillingRad.TicketType = innBaatBestilling.TicketType;
                nyBestillingRad.TicketClass = innBaatBestilling.TicketClass;
                nyBestillingRad.DepartureDato = innBaatBestilling.DepartureDato;
                nyBestillingRad.ReturnDato = innBaatBestilling.ReturnDato;
                nyBestillingRad.TotalPris = totalpris;
                nyBestillingRad.Tur = funnetTur;


                Kunde funnetKunde = await _db.Kunder.FindAsync(kundeID);

                if (funnetKunde == null)
                {
                    var kundeRad = new Kunde();
                    kundeRad.Fornavn = innBaatBestilling.Fornavn;
                    kundeRad.Etternavn = innBaatBestilling.Etternavn;
                    kundeRad.Telefonnummer = innBaatBestilling.Telefonnummer;
                    kundeRad.Epost = innBaatBestilling.Epost;
                    kundeRad.Adresse = innBaatBestilling.Adresse;
                    kundeRad.Postnummer = innBaatBestilling.Postnummer;
                    kundeRad.Poststed = innBaatBestilling.Poststed;
                    _db.Kunder.Add(kundeRad);
                    await _db.SaveChangesAsync();
                    nyBestillingRad.kunde = kundeRad;

                }
                else
                {
                    nyBestillingRad.kunde = funnetKunde;
                }
                _db.Bestillinger.Add(nyBestillingRad);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return false;
            }
        }

        public static byte[] LagHash(string passord, byte[] salt)
        {
            return KeyDerivation.Pbkdf2(
                    password: passord,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA512,
                    iterationCount: 1000,
                    numBytesRequested: 32);
        }

        public static byte[] LagSalt()
        {
            var csp = new RNGCryptoServiceProvider();
            var salt = new byte[24];
            csp.GetBytes(salt);
            return salt;
        }

        public async Task<bool> LoggInn(Bruker bruker)
        {
            try
            {
                Brukere funnetBruker = await _db.Brukere.FirstOrDefaultAsync(b => b.Brukernavn == bruker.Brukernavn);
                byte[] hash = LagHash(bruker.Passord, funnetBruker.Salt);
                bool ok = hash.SequenceEqual(funnetBruker.Passord);
                if (ok)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return false;
            }

        }
        public async Task<bool> OpprettTur(Tur innTur)
        {
            try
            {
                var nyTurRad = new Turer();
                nyTurRad.Dato = innTur.Dato;
                nyTurRad.Tid = innTur.Tid;
                nyTurRad.BarnePris = innTur.BarnePris;
                nyTurRad.VoksenPris = innTur.VoksenPris;

                bool startStasjonFunnet = false;
                List<Stasjon> alleStasjoner = await _db.Stasjoner.ToListAsync();
                foreach (var stasjon in alleStasjoner)
                {
                    if (innTur.StartStasjon.Equals(stasjon.StasjonsNavn))
                    {
                        nyTurRad.StartStasjon = stasjon;
                        startStasjonFunnet = true;
                    }
                }

                if (!startStasjonFunnet)
                {
                    var startStasjonRad = new Stasjon();
                    startStasjonRad.StasjonsNavn = innTur.StartStasjon;
                    nyTurRad.StartStasjon = startStasjonRad;
                }

                bool endeStasjonFunnet = false;
                foreach (var stasjon in alleStasjoner)
                {
                    if (innTur.EndeStasjon.Equals(stasjon.StasjonsNavn))
                    {
                        nyTurRad.EndeStasjon = stasjon;
                        endeStasjonFunnet = true;
                    }
                }

                if (!endeStasjonFunnet)
                {
                    var endeStasjonRad = new Stasjon();
                    endeStasjonRad.StasjonsNavn = innTur.EndeStasjon;
                    nyTurRad.EndeStasjon = endeStasjonRad;
                }

                _db.Turer.Add(nyTurRad);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return false;
            }
        }

        public async Task<bool> EndreTur(Tur endreTur)
        {
            try
            {
                var tur = await _db.Turer.FindAsync(endreTur.TurId); 
                tur.Tid = endreTur.Tid;
                tur.Dato = endreTur.Dato;
                tur.BarnePris = endreTur.BarnePris;
                tur.VoksenPris = endreTur.VoksenPris;

                bool startStasjonFunnet = false;
                List<Stasjon> alleStasjoner = await _db.Stasjoner.ToListAsync(); 
                foreach (var stasjon in alleStasjoner) 
                {
                    if (endreTur.StartStasjon.Equals(stasjon.StasjonsNavn)) 
                    {
                        tur.StartStasjon = stasjon; 
                        startStasjonFunnet = true;

                        if(endreTur.StartStasjon == endreTur.EndeStasjon)
                        {
                            return false;
                        }
                    }
                }

                if (!startStasjonFunnet)
                {
                    var startStasjonRad = new Stasjon(); 
                    startStasjonRad.StasjonsNavn = endreTur.StartStasjon; 
                    tur.StartStasjon = startStasjonRad; 
                }

                bool endeStasjonFunnet = false;
                foreach (var stasjon in alleStasjoner)
                {
                    if (endreTur.EndeStasjon.Equals(stasjon.StasjonsNavn))
                    {
                        tur.EndeStasjon = stasjon;
                        endeStasjonFunnet = true;

                        if(endreTur.EndeStasjon == endreTur.StartStasjon)
                        {
                            return false;
                        }
                    }
                }

                if (!endeStasjonFunnet)
                {
                    var endeStasjonRad = new Stasjon();
                    endeStasjonRad.StasjonsNavn = endreTur.EndeStasjon;
                    tur.EndeStasjon = endeStasjonRad;
                }

                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return false;
            }
        }

        public async Task<bool> SlettTur(int TurId)
        {
            try
            {
                Turer enTur = await _db.Turer.FindAsync(TurId);
                _db.Turer.Remove(enTur);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
