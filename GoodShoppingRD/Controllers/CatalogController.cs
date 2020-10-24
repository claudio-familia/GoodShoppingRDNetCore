using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GoodShoppingRD.Controllers.Base;
using GoodShoppingRD.Models;
using GoodShoppingRD.Models.Dto;
using GoodShoppingRD.Repository.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoodShoppingRD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CatalogController : ApiBaseController
    {
        private readonly IBaseRepository<Catalog> _catalogRepository;
        private readonly IMapper _mapper;
        public CatalogController(IBaseRepository<Catalog> productcatalogRepository, IMapper mapper)
        {
            _catalogRepository = productcatalogRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<Catalog>> Get()
        {
            return await _catalogRepository.GetAll();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            if (id == null)
                throw new ArgumentNullException("Invalid Id");

            Catalog productcatalog = await _catalogRepository.Get(new Guid(id));

            if (productcatalog == null)
                return NotFound("Catalog not found");

            return Ok(productcatalog);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Create(CatalogDto productcatalogRequest)
        {
            if (productcatalogRequest == null)
                throw new ArgumentNullException("Invalid catalog");

            if (productcatalogRequest.Name == null)
                throw new ArgumentNullException("Catalog name is required");

            var catalog = _mapper.Map<Catalog>(productcatalogRequest);

            catalog.CreatedAt = DateTime.Now;
            

            var productcatalog = await _catalogRepository.Add(catalog);

            return Ok(productcatalog);
        }

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> Update(Catalog ProductCatalog)
        {
            if (ProductCatalog == null)
                throw new ArgumentNullException("Invalid Id");

            var ProductCatalogUpdated = await _catalogRepository.Update(ProductCatalog);

            return Ok(ProductCatalogUpdated);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
                throw new ArgumentNullException("Invalid Id");

            await _catalogRepository.Remove(new Guid(id));

            return Ok();
        }

    }
}
