using Microsoft.EntityFrameworkCore;

namespace Backend.Models.Repositories
{
    public class BeerRepository : IRepository<Beer>
    {
        private StoreContext _context;

        public BeerRepository(StoreContext context)
        {


            _context = context;
        }



        public async Task<IEnumerable<Beer>> Get()
        {
           return await this._context.Beers.ToListAsync();
        }

        public async Task<Beer> GetById(long id)
        {
            return await _context.Beers.FindAsync(id);

        }

        public async Task Add(Beer beer)
        {
             await _context.Beers.AddAsync(beer);
        }

        public void Update(Beer beer)
        {
            this._context.Beers.Attach(beer);
            this._context.Entry(beer).State = EntityState.Modified; //modificado

        }


        public void Delete(Beer beer)
        {
            this._context.Beers.Remove(beer);
        }


        public async Task Save()
        {
            await this._context.SaveChangesAsync();
        }

        public IEnumerable<Beer> Search(Func<Beer,bool> filter)
        {
            var r= _context.Beers.Where(filter).ToList();
            return r;
        }


    }
}
