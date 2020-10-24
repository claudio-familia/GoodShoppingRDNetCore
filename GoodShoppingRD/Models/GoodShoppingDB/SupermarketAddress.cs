using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace GoodShoppingRD.Models
{
    public class SupermarketAddress : EntityBase
    {
        public Guid Supermarket_id { get; set; }

        public string Address { get; set; }

        public string Longitud { get; set; }

        public string Latitude { get; set; }

        [ForeignKey("Supermarket_id")]
        public Supermarket Supermarket { get; set; }
    }    
}


