using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace GoodShoppingRD.Models
{
    public class Sale: EntityBase
    {
        public Guid SupermarketId { get; set; }

        public Guid ProductId { get;  set; }

        public int Price { get; set; }

        public int SaleType { get; set; }

        public DateTime ExpireDate { get; set; }

        [ForeignKey("SupermaketId")]
        public Supermarket Supermarket { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

    }
}
