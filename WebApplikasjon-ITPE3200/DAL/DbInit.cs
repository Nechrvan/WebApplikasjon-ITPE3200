using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using WebApplikasjon_ITPE3200.DAL;
using WebApplikasjon_ITPE3200.Models;
using static WebApplikasjon_ITPE3200.Models.KundeContext;

namespace WebApplikasjon_ITPE3200.Models
{
    public static class DbInit
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<KundeContext>();

               // må slette og opprette databasen hver gang når den skalinitieres (seed`es)
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();


                /*7 Stasjoner med forskjellige tider Fra og Til (28 stasjoner alt)*/
                var stasjon1 = new Stasjon { StasjonsNavn = "Hirtshals" };
                var stasjon2 = new Stasjon { StasjonsNavn = "Kiel" };
                var stasjon3 = new Stasjon { StasjonsNavn = "Kristiansand" };
                var stasjon4 = new Stasjon { StasjonsNavn = "Larvik" };
                var stasjon5 = new Stasjon { StasjonsNavn = "Oslo" };
                var stasjon6 = new Stasjon { StasjonsNavn = "Sandefjord" };
                var stasjon7 = new Stasjon { StasjonsNavn = "Strømstad" };


                context.Stasjoner.Add(stasjon1);
                context.Stasjoner.Add(stasjon2);
                context.Stasjoner.Add(stasjon3);
                context.Stasjoner.Add(stasjon4);
                context.Stasjoner.Add(stasjon5);
                context.Stasjoner.Add(stasjon6);
                /*---------OPPRETTER DATOER--------*/
                string dato1 = "01/12/2021";

                /*---------OPPRETTER TIDER--------*/
                string tid1 = "14:00-10:00";
                string tid2 = "14:00-10:00";
                string tid3 = "17:30-21:15";
                string tid4 = "22:15-02:00";
                string tid5 = "08:00-11:15";
                string tid6 = "16:30-19:45";
                string tid7 = "12:15-15:30";
                string tid8 = "20:45-23:59";
                string tid9 = "10:00-12:30";
                string tid10 = "17:00-19:30";
                string tid11 = "13:40-16:10";
                string tid12 = "20:00-22:30";

                /*---------OPPRETTER TURER--------*/
                var tur1 = new Turer { StartStasjon = stasjon1, EndeStasjon = stasjon2, Tid = tid7,Dato= dato1, BarnePris = 199, VoksenPris = 499 };
                var tur2 = new Turer { StartStasjon = stasjon1, EndeStasjon = stasjon3, Tid = tid8, Dato = dato1, BarnePris = 225, VoksenPris = 450 };
                var tur3 = new Turer { StartStasjon = stasjon1, EndeStasjon = stasjon4, Tid = tid4, Dato = dato1, BarnePris = 350, VoksenPris = 799 };
                var tur4 = new Turer { StartStasjon = stasjon1, EndeStasjon = stasjon5, Tid = tid5, Dato = dato1, BarnePris = 450, VoksenPris = 899 };
                var tur5 = new Turer { StartStasjon = stasjon1, EndeStasjon = stasjon6, Tid = tid3, Dato = dato1, BarnePris = 150, VoksenPris = 299};
                var tur6 = new Turer { StartStasjon = stasjon2, EndeStasjon = stasjon7, Tid = tid2, Dato = dato1, BarnePris = 100, VoksenPris = 199 };
                var tur7 = new Turer { StartStasjon = stasjon2, EndeStasjon = stasjon1, Tid = tid9, Dato = dato1, BarnePris = 525, VoksenPris = 999 };
                var tur8 = new Turer { StartStasjon = stasjon2, EndeStasjon = stasjon3, Tid = tid1,Dato = dato1,BarnePris = 375, VoksenPris = 849 };
                var tur9 = new Turer { StartStasjon = stasjon2, EndeStasjon = stasjon4, Tid = tid11, Dato = dato1, BarnePris = 245, VoksenPris = 599 };
                var tur10 = new Turer { StartStasjon = stasjon2, EndeStasjon = stasjon5, Tid = tid12, Dato = dato1, BarnePris = 599, VoksenPris = 1199 };
                var tur11 = new Turer { StartStasjon = stasjon2, EndeStasjon = stasjon6, Tid = tid5, Dato = dato1, BarnePris = 199, VoksenPris = 499 };
                var tur12= new Turer { StartStasjon = stasjon3, EndeStasjon = stasjon1, Tid = tid9, Dato = dato1, BarnePris = 225, VoksenPris = 450 };
                var tur13= new Turer { StartStasjon = stasjon3, EndeStasjon = stasjon2, Tid = tid5,Dato = dato1, BarnePris = 350, VoksenPris = 799 };
                var tur14= new Turer { StartStasjon = stasjon3, EndeStasjon = stasjon7, Tid = tid2,Dato = dato1, BarnePris = 180, VoksenPris = 425 };
                var tur15= new Turer { StartStasjon = stasjon4, EndeStasjon = stasjon1, Tid = tid8,Dato = dato1, BarnePris = 100, VoksenPris = 199 };
                var tur16= new Turer { StartStasjon = stasjon4, EndeStasjon = stasjon2, Tid = tid9, Dato = dato1, BarnePris = 525, VoksenPris = 999 };
                var tur17= new Turer { StartStasjon = stasjon4, EndeStasjon = stasjon7, Tid = tid11, Dato = dato1, BarnePris = 225, VoksenPris = 450 };
                var tur18= new Turer { StartStasjon = stasjon5, EndeStasjon = stasjon1, Tid = tid1, Dato = dato1, BarnePris = 350, VoksenPris = 799 };
                var tur19= new Turer { StartStasjon = stasjon5, EndeStasjon = stasjon2, Tid = tid2, Dato = dato1, BarnePris = 450, VoksenPris = 899 };
                var tur20= new Turer { StartStasjon = stasjon5, EndeStasjon = stasjon7, Tid = tid6, Dato = dato1, BarnePris = 100, VoksenPris = 199 };
                var tur21= new Turer { StartStasjon = stasjon6, EndeStasjon = stasjon1, Tid = tid10, Dato = dato1, BarnePris = 525, VoksenPris = 999 };
                var tur22 = new Turer { StartStasjon = stasjon6, EndeStasjon = stasjon2, Tid = tid11, Dato = dato1, BarnePris = 375, VoksenPris = 849 };
                var tur23= new Turer { StartStasjon = stasjon6, EndeStasjon = stasjon7, Tid = tid3, Dato = dato1, BarnePris = 180, VoksenPris = 425 };
                var tur24= new Turer { StartStasjon = stasjon7, EndeStasjon = stasjon2, Tid = tid2, Dato = dato1, BarnePris = 525, VoksenPris = 999 };
                var tur25 = new Turer { StartStasjon = stasjon7, EndeStasjon = stasjon3, Tid = tid5, Dato = dato1, BarnePris = 375, VoksenPris = 849 };
                var tur26 = new Turer { StartStasjon = stasjon7, EndeStasjon = stasjon4, Tid = tid12, Dato = dato1,BarnePris = 245, VoksenPris = 599 };
                var tur27 = new Turer { StartStasjon = stasjon7, EndeStasjon = stasjon5, Tid = tid10, Dato = dato1, BarnePris = 599, VoksenPris = 1199 };
                var tur28 = new Turer { StartStasjon = stasjon7, EndeStasjon = stasjon6, Tid = tid1, Dato = dato1, BarnePris = 599, VoksenPris = 1199 };

                context.Turer.Add(tur1);
                context.Turer.Add(tur2);
                context.Turer.Add(tur3);
                context.Turer.Add(tur4);
                context.Turer.Add(tur5);
                context.Turer.Add(tur6);
                context.Turer.Add(tur7);
                context.Turer.Add(tur8);
                context.Turer.Add(tur9);
                context.Turer.Add(tur10);
                context.Turer.Add(tur11);
                context.Turer.Add(tur12);
                context.Turer.Add(tur13);
                context.Turer.Add(tur14);
                context.Turer.Add(tur15);
                context.Turer.Add(tur16);
                context.Turer.Add(tur17);
                context.Turer.Add(tur18);
                context.Turer.Add(tur19);
                context.Turer.Add(tur20);
                context.Turer.Add(tur21);
                context.Turer.Add(tur22);
                context.Turer.Add(tur23);
                context.Turer.Add(tur24);
                context.Turer.Add(tur25);
                context.Turer.Add(tur26);
                context.Turer.Add(tur27);
                context.Turer.Add(tur28);
             
               

                /*---------OPPRETTER ADMIN-BRUKER--------*/

                var bruker = new Brukere();
                bruker.Brukernavn = "Admin";
                string passord = "Admin1";
                byte[] salt = BaatBestillingRepository.LagSalt();
                byte[] hash = BaatBestillingRepository.LagHash(passord, salt);
                bruker.Passord = hash;
                bruker.Salt = salt;
                context.Brukere.Add(bruker);

                context.SaveChanges();

            }
        }
    }
}

