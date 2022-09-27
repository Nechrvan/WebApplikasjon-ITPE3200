using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplikasjon_ITPE3200.Models
{
    public class Kreditt
    {
        public int Id { get; set; }
        public int KId { get; set; }

        [RegularExpression(@"[0-9]{16}")]
        public string Kortnummer { get; set; }
        [RegularExpression(@"[0-9]{2}[/][0-9]{2}")]
        public string UtlopsDato { get; set; }
        [RegularExpression(@"[0-9]{3}")]
        public string Cvc { get; set; }
    }
}