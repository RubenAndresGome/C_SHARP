using Backend.DTOs;

namespace Backend.Services
{
    public interface ICommonService<T,TI, TU>
    {

        Task<IEnumerable<T>>   Get();

        Task<T> GetById(long id);
        Task<T> Add(TI beerInsertDto);
        Task<T> Update(long id, TU beerUpdateDto);
        Task<T> Delete(long id);

        bool validate(TI dto);
        bool validate(TU dto);
        public List<string> Errors { get; }

    }
}
