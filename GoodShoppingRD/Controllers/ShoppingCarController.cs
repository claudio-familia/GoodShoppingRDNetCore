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
    public class ShoppingCartController : ApiBaseController
    {
        private readonly IBaseRepository<ShoppingCart> _shoppingcartRepository;
        private readonly IMapper _mapper;
        public ShoppingCartController(IBaseRepository<ShoppingCart> shoppingcartRepository, IMapper mapper)
        {
            _shoppingcartRepository = shoppingcartRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<ShoppingCart>> Get()
        {
            return await _shoppingcartRepository.GetAll();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            if (id == null)
                throw new ArgumentNullException("Invalid Shopping Car");

            ShoppingCart shoppingcart = await _shoppingcartRepository.Get(new Guid(id));

            if (shoppingcart == null)
                return NotFound("ShoppingCart not found");

            return Ok(shoppingcart);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Create(ShoppingCartDto shoppingcartRequest)
        {
            if (shoppingcartRequest == null)
                throw new ArgumentNullException("Invalid supermarket");

            if (shoppingcartRequest.Name == null)
                throw new ArgumentNullException("Supermarket's name is required");

            var newShoppingCart = _mapper.Map<ShoppingCart>(shoppingcartRequest);

            newShoppingCart.CreatedAt = DateTime.Now;
            newShoppingCart.CreatedBy = CurrentUserId;

            var shoppingcart = await _shoppingcartRepository.Add(newShoppingCart);

            return Ok(shoppingcart);
        }

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> Update(ShoppingCart ShoppingCart)
        {
            if (ShoppingCart == null)
                throw new ArgumentNullException("Invalid supermarket");

            ShoppingCart.UpdatedAt = DateTime.Now;
            ShoppingCart.UpdatedBy = CurrentUserId;

            var shoppingCartUpdated = await _shoppingcartRepository.Update(ShoppingCart);

            return Ok(shoppingCartUpdated);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
                throw new ArgumentNullException("Invalid Id");

            await _shoppingcartRepository.Remove(new Guid(id));

            return Ok();
        }

    }
}
