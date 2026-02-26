using Backend.DTOs;
using Backend.Models;
using Backend.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Globalization;


namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        private object beerUpdateDto;

        private StoreContext _context { get; set; }
        private IValidator<BeerInsertDto> _beerinsertValidator { get; set; }

        public BeerController(StoreContext context, IValidator<BeerInsertDto> beerinsertValidator)
        {
            _context = context;
            _beerinsertValidator=beerinsertValidator;
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
            var validationResult = await _beerinsertValidator.ValidateAsync(beerInsertDto);

            if (!validationResult.IsValid) 
            { 
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }


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
            beer.BrandID = (int)beerUpdateDto.BrandID;
            await _context.SaveChangesAsync();
            // Si tu DTO usa long y tu Modelo usa int, haz el cast manual aquí:
            
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

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete( long id)
        {
           var beer = await _context.Beers.FindAsync(id);
            if(beer == null)
            {
                return NotFound();
            }
            _context.Beers.Remove(beer);
            //realizar cambio 
            await _context.SaveChangesAsync();
            return Ok();
        }



    }

    }

