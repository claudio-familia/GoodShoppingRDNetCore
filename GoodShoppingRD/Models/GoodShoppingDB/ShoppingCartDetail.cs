using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodShoppingRD.Models
{
    public class ShoppingCartDetail: EntityBase
    {
        public Guid ShoppingCartId { get; set; }
        public Guid SaleId { get; set; }
        public int Quantity { get; set; }
    }
}
