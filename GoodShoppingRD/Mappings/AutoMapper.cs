using AutoMapper;
using GoodShoppingRD.Models;
using GoodShoppingRD.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodShoppingRD.Mappings
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<SupermarketDto, Supermarket>();
            CreateMap<ProductDto, Product>();
            CreateMap<SaleDto, Sale>();
            CreateMap<CatalogDto, Catalog>();
            CreateMap<ShoppingCartDto, ShoppingCart>();
        }
    }
}
