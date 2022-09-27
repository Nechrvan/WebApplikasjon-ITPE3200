using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace WebApplikasjon_ITPE3200.Models
{
    [ExcludeFromCodeCoverage]
    public class Tur
    {
        public int TurId { get; set; }

        [RegularExpression(@"^[a-zA-ZæøåÆØÅ. \-]{2,20}$")]
        public string StartStasjon { get; set; }

        [RegularExpression(@"^[a-zA-ZæøåÆØÅ. \-]{2,20}$")]
        public string EndeStasjon { get; set; }
        
        public string TicketType { get; set; }

        public string TicketClass { get; set; }

        public string DepartureDato { get; set; }

        public string ReturnDato { get; set; }
        [RegularExpression(@"^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$")]
        public string Dato { get; set; }

        [RegularExpression(@"^([01]?[0-9]|2[0-3]):[0-5][0-9]$")]
        public string Tid { get; set; }

        [RegularExpression(@"^[0-9]{2,4}$")]
        public int BarnePris { get; set; }

        [RegularExpression(@"^[0-9]{2,4}$")]
        public int VoksenPris { get; set; }
    }
}
