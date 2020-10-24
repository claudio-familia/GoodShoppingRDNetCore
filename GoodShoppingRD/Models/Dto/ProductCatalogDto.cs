using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodShoppingRD.Models.Dto
{
    public class ProductCatalogDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid CatalogId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
