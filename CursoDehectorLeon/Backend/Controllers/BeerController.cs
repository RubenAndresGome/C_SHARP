using Backend.DTOs;
using Backend.Models;
using Backend.Services;
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

        //private StoreContext _context { get; set; }
        private IValidator<BeerInsertDto> _beerInsertValidator { get; set; }
        private IValidator<BeerUpdateDto> _beerUpdateValidator { get; set; }

        private ICommonService<BeerDto,BeerInsertDto,BeerUpdateDto> _beerService { get; set; } 
        public BeerController
  
            (
            //StoreContext context,
            IValidator<BeerInsertDto> beerinsertValidator,
            IValidator<BeerUpdateDto> beerUpdateValidator,
            [FromKeyedServices("beerService")]ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto> beerService
            )
        {
            //_context = context;
            _beerInsertValidator=beerinsertValidator;
            _beerUpdateValidator= beerUpdateValidator;
            _beerService = beerService;
        }


        [HttpGet]
        public async Task<IEnumerable<BeerDto>> Get()
        {

            return await _beerService.Get();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(long id)
        {
            var beerDto = await _beerService.GetById(id);
            return  beerDto == null ? NotFound() : Ok(beerDto);

        }

        [HttpPost]
        public async Task<ActionResult <BeerDto >> Add (BeerInsertDto beerInsertDto)
        {
            var validationResult = await _beerInsertValidator.ValidateAsync(beerInsertDto);
            if (!validationResult.IsValid) 
            { 
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }
            var beerDto = await _beerService.Add(beerInsertDto);
            return CreatedAtAction(nameof(GetById), new { id = beerDto.BeerID }, beerDto);
        }

        [HttpPut("{id}") ]
        public async Task<ActionResult <BeerDto>> Update(long  id , BeerUpdateDto beerUpdateDto)
            { 
            var validationResult = await _beerUpdateValidator.ValidateAsync(beerUpdateDto);
            if(!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }
            /*
             
             */
            var beerDto = await _beerService.Update(id, beerUpdateDto);

            return  beerDto ==  null ? NotFound() :    Ok(beerDto);

        }


        /*
         **
         **
         **
         */

        [HttpDelete("{id}")]
        public async Task<ActionResult<BeerDto>> Delete( long id)
        {
           var beerDto=await _beerService.Delete(id);
            return beerDto == null ? NotFound() : Ok(beerDto);
        }


    }

    }

