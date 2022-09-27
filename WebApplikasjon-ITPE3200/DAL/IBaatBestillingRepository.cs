using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using WebApplikasjon_ITPE3200.Models;

namespace WebApplikasjon_ITPE3200.DAL
{
    public interface IBaatBestillingRepository
    {
        Task<List<Stasjon>> HentAlleStasjoner();

        Task<List<Turer>> HentAlleTurer();
        Task<List<Stasjon>> HentEndeStasjoner(string startStasjonsNavn);
        Task<bool> Lagre(BaatBestilling innBussBestilling);
        Task<bool> Kreditt(Kreditt innKreditt);
        Task<bool> OpprettTur(Tur innTur);
        Task<bool> EndreTur(Tur endreTur);
        Task<bool> SlettTur(int TurId);
        Task<bool> LoggInn(Bruker bruker);
      
    }
}
