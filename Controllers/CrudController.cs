using AutoMapper;
using Gestamp.API.Data;
using Gestamp.API.Dtos;
using Gestamp.API.Helpers;
using Gestamp.API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Gestamp.API.Controllers
{
    
    [Route("api/[Controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CrudController: ControllerBase
    {
        private readonly ISellingRepository _repo;
        private readonly IMapper _mapper;

        public CrudController(ISellingRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet("{id}", Name = "GetSale")]
        public async Task<ActionResult<Sale>> Get(int id)
        {
            var sale = await _repo.GetSaleById(id);

            if (sale == null)
            {
                return NotFound("Sales not found");
            }

            return sale;
        }

        [HttpGet]
        public async Task<IActionResult> GetSales([FromQuery]SalesParams salesParams)
        {

            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var userFromRepo = await _repo.GetUser(currentUserId);

            if (currentUserId != userFromRepo.Id)
                return Unauthorized();


            var sales = await _repo.GetSales(salesParams);

            var salesToReturn = _mapper.Map<IEnumerable<SaleForListDto>>(sales);

            Response.AddPagination(sales.CurrentPage, sales.PageSize,
                sales.TotalCount, sales.TotalPages);

            return Ok(salesToReturn);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<Sale>> Post(int id, [FromBody] Sale sale)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();


            _repo.Add(sale);

            var result = await _repo.SaveAll();
            if (!result)
            {
                return StatusCode(500);
            }

            return new CreatedAtRouteResult("GetSale", new { id = sale.Id }, sale);
        }

        [HttpPut("{id}/{orderId}")]
        // Este endpoint podría aceptar un dto
        public async Task<IActionResult> Put(int id, int orderId, [FromBody] SaleForUpdateDto saleDto)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var saleForRepo = await _repo.GetSaleByIdForUpdateOrDelete(orderId);

            _mapper.Map(saleDto, saleForRepo);
            _repo.Update(saleForRepo);
            
            if (await _repo.SaveAll())
                return NoContent();

            // si ha ido mal
            throw new Exception($"Updating sale {id} failed on save.");
            
        }

        [HttpDelete("{id}/{orderId}")]
        public async Task<ActionResult<Sale>> Delete(int id, int orderId)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var sale = await _repo.GetSaleByIdForUpdateOrDelete(orderId);

            if (sale == null)
            {
                return NotFound();
            }

            _repo.Delete(sale);
            await _repo.SaveAll();

            return NoContent();
        }

    }
}
