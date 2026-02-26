using Backend.DTOs;
using Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.EntityFrameworkCore;


namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        private object beerUpdateDto;

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
        public async Task<ActionResult> GetById(long id)
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

        [HttpPost]
        public async Task<ActionResult <BeerDto >> Add (BeerInsertDto beerInsertDto)
        {
            var beer = new Beer
            {
                Name = beerInsertDto.Name,
                Alcohol = beerInsertDto.Alcohol,
                BrandID = (int)beerInsertDto.BrandID
            };
            await _context.Beers.AddAsync(beer);



            await _context.SaveChangesAsync();

            var beerDto = new BeerDto
            {
                Id =        beer.BeerID,
                Name =      beer.Name,
                Alcohol =   beer.Alcohol,
                BrandID =   beer.BrandID
            };


            return CreatedAtAction(nameof(GetById), new { id = beer.BeerID }, beerDto);
        }

        [HttpPut("{id}") ]
        public async Task<ActionResult <BeerDto>> Update(long  id , BeerUpdateDto beerUpdateDto)
            { 
             var beer = await _context.Beers.FindAsync(id);
            if(beer==null)
            {
                return NotFound();

            }

            beer.Name = beerUpdateDto.Name;
            beer.Alcohol = beerUpdateDto.Alcohol;
            beer.BrandID = beerUpdateDto.BrandID;
            await _context.SaveChangesAsync();

            var beerDto = new BeerDto
            {
                Id = beer.BeerID,
                Name = beer.Name,
                Alcohol = beer.Alcohol,
                BrandID = beer.BrandID
            };

            return Ok(beerDto);

        }


        /*
         **
         **
         **
         */


        



    }

    }

