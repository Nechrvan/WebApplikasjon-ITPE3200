using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebApplikasjon_ITPE3200.Models
{
    [ExcludeFromCodeCoverage]
    public class BaatBestilling
    {
        public int Id { get; set; }

        //Kunde
        [RegularExpression(@"^[a-zA-ZæøåÆØÅ. \-]{2,20}$")]
        public string Fornavn { get; set; }

        [RegularExpression(@"^[a-zA-ZæøåÆØÅ. \-]{2,20}$")]
        public string Etternavn { get; set; }

        [RegularExpression(@"^[0-9]{8}$")]
        public string Telefonnummer { get; set; }

        [RegularExpression(@"^\S+@\S+$")]
        public string Epost { get; set; }

        [RegularExpression(@"[a-zA-ZæøåÆØÅ0-9_]*[a-zA-Z_]?[a-zA-Z\ \.0-9_]{2,50}")]
        public string Adresse { get; set; }

        [RegularExpression(@"[0-9]{4}")]
        public string Postnummer { get; set; }

        [RegularExpression(@"[0-9a-zA-ZæøåÆØÅ. \-]{2,50}")]
        public string Poststed { get; set; }

        //Bestilling
        [RegularExpression(@"^[0-9]{1}$")]
        public int AntallBarn { get; set; }

        [RegularExpression(@"^[0-9]{1}$")]
        public int AntallVoksne { get; set; }
        public string TicketType { get; set; }

        public string TicketClass { get; set; }

        public string DepartureDato { get; set; }

        public string ReturnDato { get; set; }
        [RegularExpression(@"^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$")]
        public string Dato { get; set; }

        public string Tid { get; set; }

        public double BarnePris { get; set; }

        public double VoksenPris { get; set; }

        public string StartStasjon { get; set; }

        public string EndeStasjon { get; set; }

    }

}
