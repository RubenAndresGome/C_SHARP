using Backend.DTOs;
using Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        private StoreContext _context { get; set; }

        public BeerController(StoreContext context)
        {
            _context = context;

        }


        [HttpGet]
        public async Task<IEnumerable<BeerDto>> Get()
        {

            return await _context.Beers.Select(
                b => new BeerDto
                {
                    Id = (long)(int)b.BeerID,
                    Name = b.Name,
                    Alcohol = b.Alcohol,
                    BrandID = (long)(int)b.BrandID
                }).ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var beer = await _context.Beers.FindAsync(id);

            if (beer == null)
            {
                return NotFound();
            }

            var beerDto = new BeerDto
            {
                Id = (long)(int)beer.BeerID,
                Name = beer.Name,
                Alcohol = beer.Alcohol,
                BrandID = (long)(int)beer.BrandID
            };
            return Ok(beerDto);

        }









    }

    }

