using Backend.DTOs;
using Backend.Models;
using Backend.Models.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class BeerService : ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto>
    {
        //private StoreContext _context;
        private IRepository<Beer> _beersRepository;

        public BeerService(
            //StoreContext context,
            IRepository<Beer> beersRepository
            )
        {
            //_context = context;
            _beersRepository = beersRepository;
        }

        public async Task<IEnumerable<BeerDto>> Get()
        {
           
            var beers = await _beersRepository.Get();
            var result = beers.Select(b => new BeerDto
            {
                BeerID = (long)(int)b.BeerID,
                Name = b.Name,
                Alcohol = b.Alcohol,
                BrandID = (long)(int)b.BrandID
            }).ToList();

            return result;

        }
        public async Task<BeerDto> GetById(long id)
        {
            var beer = await _beersRepository.GetById(id);

            if (beer != null)
            {
                var beerDto = new BeerDto
                {
                    BeerID = (long)(int)beer.BeerID,
                    Name = beer.Name,
                    Alcohol = beer.Alcohol,
                    BrandID = (long)(int)beer.BrandID
                };
                return beerDto;
            }

           return null;
        }


        public async Task<BeerDto> Add(BeerInsertDto beerInsertDto)
        {

            //var _context= this._context;

            var beer = new Beer
            {
                Name = beerInsertDto.Name,
                Alcohol = beerInsertDto.Alcohol,
                BrandID = (int)beerInsertDto.BrandID
            };
            await _beersRepository.Add(beer);

            await _beersRepository.Save();

            var beerDto = new BeerDto
            {
                BeerID = beer.BeerID,
                Name = beer.Name,
                Alcohol = beer.Alcohol,
                BrandID = beer.BrandID
            };
            return beerDto;
        }

        public async    Task<BeerDto> Delete(long id)
        {
            var beer = await this._beersRepository.GetById(id);
            if (beer != null)
            {
                //antes de eliminar crear el DTO con los datos del beer para devolverlo
                //// después de eliminarlo, ya que una vez eliminado no podrás acceder a sus propiedades.
                var beerDto = new BeerDto
                {
                    BeerID = beer.BeerID,
                    Name = beer.Name,
                    Alcohol = beer.Alcohol,
                    BrandID = beer.BrandID
                };

                this._beersRepository.Delete(beer);
                await this._beersRepository.Save();
            
                return beerDto;

            }
            return null;

        }

        public async Task<BeerDto> Update(long id, BeerUpdateDto beerUpdateDto)
        {
            var beer = await _beersRepository.GetById(id);
            if(beer != null)
            {
                beer.Name = beerUpdateDto.Name;
                beer.Alcohol = beerUpdateDto.Alcohol;
                beer.BrandID = (int)beerUpdateDto.BrandID;
                _beersRepository.Update(beer);
                await _beersRepository.Save();
                
                // Si tu DTO usa long y tu Modelo usa int, haz el cast manual aquí:

                var beerDto = new BeerDto
                {
                    BeerID = beer.BeerID,
                    Name = beer.Name,
                    Alcohol = beer.Alcohol,
                    BrandID = beer.BrandID
                };
                return beerDto;

            }
            return null;
        }
    }
}
