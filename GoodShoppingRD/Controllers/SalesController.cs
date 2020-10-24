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
    public class SalesController : ApiBaseController
    {
        private readonly IBaseRepository<Sale> _saleRepository;
        private readonly IMapper _mapper;
        public SalesController(IBaseRepository<Sale> saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<Sale>> Get()
        {
            return await _saleRepository.GetAll();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            if (id == null)
                throw new ArgumentNullException("Invalid Id");

            Sale sale = await _saleRepository.Get(new Guid(id));

            if (sale == null)
                return NotFound("Sale not found");

            return Ok(sale);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Create(/*string supermarketId, string productId, */SaleDto saleRequest)
        {
            if (saleRequest == null)
                throw new ArgumentNullException("Invalid sale");

            if (saleRequest.SupermarketId == null)
                throw new ArgumentNullException("Supermarket is required");

            if (saleRequest.ProductId == null)
                throw new ArgumentNullException("Product is required");

            if (saleRequest.Price <= 0)
                throw new ArgumentNullException("Product's price is required");

            //saleRequest.SupermarketId = new Guid(supermarketId);
            //saleRequest.ProductId = new Guid(productId);

            var newSale = _mapper.Map<Sale>(saleRequest);

            newSale.CreatedAt = DateTime.Now;
            newSale.CreatedBy = CurrentUserId;

            var sale = await _saleRepository.Add(newSale);

            return Ok(sale);
        }

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> Update(Sale sale)
        {
            if (sale == null)
                throw new ArgumentNullException("Invalid sale");

            sale.UpdatedAt = DateTime.Now;
            sale.UpdatedBy = CurrentUserId;

            var saleUpdated = await _saleRepository.Update(sale);

            return Ok(saleUpdated);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
                throw new ArgumentNullException("Invalid Id");

            await _saleRepository.Remove(new Guid(id));

            return Ok();
        }

    }
}
