using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodShoppingRD.Models
{
    public class Product : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        ICollection<Catalog> Catalogs { get; set; }
    }   
}
