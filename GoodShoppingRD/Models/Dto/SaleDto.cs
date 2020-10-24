using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodShoppingRD.Models.Dto
{
    public class SaleDto
    {
        public Guid SupermarketId { get; set; }

        public Guid ProductId { get; set; }

        public int Price { get; set; }

        public int SaleType { get; set; }

        public DateTime ExpireDate { get; set; }
    }
}
