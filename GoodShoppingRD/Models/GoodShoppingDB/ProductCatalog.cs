using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoodShoppingRD.Models
{
    public class ProductCatalog
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid CatalogId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
