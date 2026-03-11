using AutoMapper;
using Backend.DTOs;
using Backend.Models;
using Backend.Models.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class BeerService : ICommonService <BeerDto, BeerInsertDto, BeerUpdateDto>
    {
        //private StoreContext _context;
        private IRepository<Beer> _beersRepository;

        public List<string> Errors { get; private set; }
        private IMapper _mapper;

      

       

        public BeerService(
            //StoreContext context,
            IRepository<Beer> beersRepository
            , IMapper mapper
            )
        {
            //_context = context;
            _beersRepository = beersRepository;
            _mapper = mapper;
            this.Errors = new List<string>();
        }

        public async Task<IEnumerable<BeerDto>> Get()
        {
           
            var beers = await _beersRepository.Get();
            /*
            var result = beers.Select(b => new BeerDto
            {
                BeerID = (long)(int)b.BeerID,
                Name = b.Name,
                Alcohol = b.Alcohol,
                BrandID = (long)(int)b.BrandID
            }).ToList();*/

            var result= beers.Select(b=> _mapper.Map<BeerDto>(b));
            return result;

        }
        public async Task<BeerDto> GetById(long id)
        {
            var beer = await _beersRepository.GetById(id);

            if (beer != null)
            {

                /*
                var beerDto = new BeerDto
                {
                    BeerID = (long)(int)beer.BeerID,
                    Name = beer.Name,
                    Alcohol = beer.Alcohol,
                    BrandID = (long)(int)beer.BrandID
                };

                */
                
                var beerDto = _mapper.Map<BeerDto>(beer);
                return beerDto;
            }

           return null;
        }


        public async Task<BeerDto> Add(BeerInsertDto beerInsertDto)
        {

            //var _context= this._context;
            /*
            var beer = new Beer
            {
                Name = beerInsertDto.Name,
                Alcohol = beerInsertDto.Alcohol,
                BrandID = (int)beerInsertDto.BrandID
            };

            */

            var beer = _mapper.Map<Beer>(beerInsertDto);

            await _beersRepository.Add(beer);

            await _beersRepository.Save();

                var beerDto = _mapper.Map<BeerDto>(beer);
            /*
            var beerDto = new BeerDto
            {
                BeerID = beer.BeerID,
                Name = beer.Name,
                Alcohol = beer.Alcohol,
                BrandID = beer.BrandID
            };
            */
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
            // 1. Obtener la entidad de la base de datos
            var beer = await _beersRepository.GetById(id);

            if (beer != null)
            {
                // 2. Mapear los cambios del DTO a la entidad existente
                // Esto actualiza las propiedades de 'beer' con los valores de 'beerUpdateDto'
                _mapper.Map(beerUpdateDto, beer);

                // 3. Persistir los cambios
                _beersRepository.Update(beer);
                await _beersRepository.Save();

                // 4. Retornar el DTO de respuesta mapeado desde la entidad actualizada
                return _mapper.Map<BeerDto>(beer);
            }

            return null;
        }

        public bool validate(BeerInsertDto beerInsertDto)
        {
            if (_beersRepository.Search(b=> b.Name ==beerInsertDto.Name).Count()>0)
            {
                Errors.Add("Ya existe una cerveza con ese nombre.");
                return false;

            }
            return true;
        }

        public bool validate(BeerUpdateDto beerUpdateDto)
        {
            if (    _beersRepository.Search(
             
             b => b.Name == beerUpdateDto.Name &&
            (beerUpdateDto.BeerID != b.BeerID)
             ).Count() > 0)
            {
                Errors.Add("Ya existe una cerveza con ese nombre.");
                return false;

            }


            return true;
        }

        /**/




    }

}
