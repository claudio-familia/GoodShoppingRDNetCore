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
    public class SupermarketController : ApiBaseController
    {
        private readonly IBaseRepository<Supermarket> _supermarketRepository;
        private readonly IMapper _mapper;
        public SupermarketController(IBaseRepository<Supermarket> supermarketRepository, IMapper mapper)
        {
            _supermarketRepository = supermarketRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<Supermarket>> Get()
        {
            return await _supermarketRepository.GetAll();
        }

        [HttpGet]
        [Route("/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            if(id == null) 
                throw new ArgumentNullException("Invalid Id");

            Supermarket supermarket = await _supermarketRepository.Get(new Guid(id));

            if (supermarket == null) 
                return NotFound("Supermarket not found");

            return Ok(supermarket);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Create(SupermarketDto supermarketRequest)
        {
            if (supermarketRequest == null)
                throw new ArgumentNullException("Invalid supermarket");

            if(supermarketRequest.Name == null)
                throw new ArgumentNullException("Supermarket's name is required");

            var newSupermarket = _mapper.Map<Supermarket>(supermarketRequest);

            newSupermarket.CreatedAt = DateTime.Now;
            newSupermarket.CreatedBy = CurrentUserId;

            var supermarket = await _supermarketRepository.Add(newSupermarket);

            return Ok(supermarket);
        }

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> Update(Supermarket Supermarket)
        {
            if (Supermarket == null)
                throw new ArgumentNullException("Invalid supermarket");

            Supermarket.UpdatedAt = DateTime.Now;
            Supermarket.UpdatedBy = CurrentUserId;

            var supermarketUpdated = await _supermarketRepository.Update(Supermarket);

            return Ok(supermarketUpdated);
        }

        [HttpDelete]
        [Route("/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
                throw new ArgumentNullException("Invalid Id");

            await _supermarketRepository.Remove(new Guid(id));            

            return Ok();
        }

    }
}
