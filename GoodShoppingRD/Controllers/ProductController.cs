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
    public class ProductController : ApiBaseController
    {
        private readonly IBaseRepository<Product> _productRepository;
        private readonly IBaseRepository<ProductCatalog> _productCatalogRepository;
        private readonly IMapper _mapper;
        public ProductController(IBaseRepository<Product> productRepository, IMapper mapper, IBaseRepository<ProductCatalog> productCatalogRepository)
        {
            _productRepository = productRepository;
            _productCatalogRepository = productCatalogRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<Product>> Get()
        {
            return await _productRepository.GetAll();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("Invalid Id");

            Product product = await _productRepository.Get(new Guid(id));

            if (product == null)
                return NotFound("Product not found");

            return Ok(product);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Create(ProductDto productRequest)
        {
            if (productRequest == null)
                return BadRequest("Product data is required");

            if (string.IsNullOrEmpty(productRequest.Name))
                return BadRequest("Product name is required");

            var newProduct = _mapper.Map<Product>(productRequest);

            newProduct.CreatedBy = CurrentUserId;
            newProduct.CreatedAt = DateTime.Now;

            var product = await _productRepository.Add(newProduct);

            return Ok(product);
        }

        [HttpPost]
        [Route("/AssignCatalogToProduct")]
        public async Task<IActionResult> AssignCatalogToProduct(string id, string catalogId)
        {
            if (id == null || catalogId == null)
                return BadRequest("Product data is required");

            var productId = new Guid(id);
            var catalog = new Guid(catalogId);

            var newProductCatalog = new ProductCatalog() { 
                CatalogId = catalog,
                ProductId = productId,
                CreatedAt = DateTime.Now
            };

            var productCatalog = await _productCatalogRepository.Add(newProductCatalog);

            return Ok(productCatalog);
        }


        [HttpPut]
        [Route("")]
        public async Task<IActionResult> Update(Product product)
        {
            if (product == null)
                throw new ArgumentNullException("Invalid product");

            product.UpdatedAt = DateTime.Now;
            product.UpdatedBy = CurrentUserId;

            var productUpdated = await _productRepository.Update(product);

            return Ok(productUpdated);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("Invalid Id");

            await _productRepository.Remove(new Guid(id));

            return Ok();
        }
    }
}
