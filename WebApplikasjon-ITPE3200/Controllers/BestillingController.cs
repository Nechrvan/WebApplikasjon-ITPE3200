using System;
using System.Collections.Generic;
using System.Linq;
using WebApplikasjon_ITPE3200.Models;
using Microsoft.AspNetCore.Mvc;
using WebApplikasjon_ITPE3200.DAL;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Stasjon = WebApplikasjon_ITPE3200.DAL.Stasjon;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace WebApplikasjon_ITPE3200.Controllers
{
    [Route("[controller]/[action]")]
    public class BestillingController : ControllerBase
    {
        private readonly ILogger<BestillingController> _log;
        private readonly IBaatBestillingRepository _db;
        private const string _loggetInn = "loggetInn";
        private const string _ikkeLoggetInn = "";


        public BestillingController(IBaatBestillingRepository db, ILogger<BestillingController> log)
        {
            _db = db;
            _log = log;
        }


        public async Task<ActionResult> HentAlleStasjoner()
        {
            List<Stasjon> alleStasjoner = await _db.HentAlleStasjoner();
            return Ok(alleStasjoner);
        }

        public async Task<ActionResult> HentAlleTurer()
        {
            List<Turer> alleTurer = await _db.HentAlleTurer();
            return Ok(alleTurer);
        }

        public async Task<ActionResult> HentEndeStasjoner(string startStasjonsNavn)
        {
            List<Stasjon> endeStasjon = await _db.HentEndeStasjoner(startStasjonsNavn);
            return Ok(endeStasjon);
        }

        public async Task<ActionResult> Lagre(BaatBestilling innBaatBestilling)
        {
            if (ModelState.IsValid)
            {
               
                bool returOk = await _db.Lagre(innBaatBestilling);
                if (!returOk)
                {
                    _log.LogInformation("Bestilling ble ikke registrert");
                    return BadRequest("Bestilling ble ikke registrert");
                }
                return Ok("Bestilling registrert");
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }
        public async Task<ActionResult> Kreditt(Kreditt kredittInfo)
        {
            if (ModelState.IsValid)
            {
                bool returnOk = await _db.Kreditt(kredittInfo);
                if (!returnOk)
                {
                    _log.LogInformation("Kunne ikke lagre kredittinfo");
                    return BadRequest("Kunne ikke lagre kredittinfo");
                }
                return Ok("Kredittinfo ble lagret");
            }
            _log.LogInformation("Feil i inputValidering");
            return BadRequest("Feil i inputValidering på server");
        }

        public async Task<ActionResult> OpprettTur(Tur innTur)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }

            if (ModelState.IsValid)
            {

                bool returOk = await _db.OpprettTur(innTur);
                if (!returOk)

                {

                    _log.LogInformation("Tur ble ikke registrert");
                    return BadRequest("Turen ble ikke registrert");
                }
                return Ok("Tur registrert");
            }

            var errors = ModelState
            .Where(x => x.Value.Errors.Count > 0)
            .Select(x => new { x.Key, x.Value.Errors })
            .ToArray();

            foreach (var error in errors)
            {
                System.Diagnostics.Debug.WriteLine(error);
            }

            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }

        public async Task<ActionResult> EndreTur(Tur endreTur)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }

            if (ModelState.IsValid)
            {

                bool returOk = await _db.EndreTur(endreTur);
                if (!returOk)
                {
                    _log.LogInformation("Tur ble ikke registrert");
                    return BadRequest("Tur ble ikke registrert");
                }
                return Ok("Tur registrert");
            }
            var errors = ModelState
            .Where(x => x.Value.Errors.Count > 0)
            .Select(x => new { x.Key, x.Value.Errors })
            .ToArray();

            foreach (var error in errors)
            {
                System.Diagnostics.Debug.WriteLine(error);
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }

        public async Task<ActionResult> SlettTur(int TurId)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }

            bool returOk = await _db.SlettTur(TurId);
            if (!returOk)
            {
                _log.LogInformation("Tur ble ikke slettet");
                return BadRequest("Tur ble ikke slettet");
            }
            return Ok("Tur slettet");

        }
    

        //Tor sin kode
        public async Task<ActionResult> LoggInn(Bruker bruker)
        {
            if (ModelState.IsValid)
            {
                bool returnOK = await _db.LoggInn(bruker);
                if (!returnOK)
                {
                    _log.LogInformation("Innloggingen feilet for bruker");
                    HttpContext.Session.SetString(_loggetInn, _ikkeLoggetInn);
                    return Ok(false);
                }
                HttpContext.Session.SetString(_loggetInn, _loggetInn);
                return Ok(true);
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }

    }
}

