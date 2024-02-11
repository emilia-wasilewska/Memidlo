using Memidlo.Web.Models.Domain;

namespace Memidlo.Web.Repositories
{
    public interface IMemRepository
    {
        Task<Mem> AddAsync(Mem mem);

        Task<IEnumerable<Mem>> GetAllAsync();

        Task<IEnumerable<Mem>> GetAllAsync(string tagName);

        Task<Mem> MoveToMainAsync(int memId);

        Task<Mem> GetAsync(int id);

        Task<Mem?> GetRandom();

        Task<Mem> UpdateAsync(Mem mem);

        Task<bool> DeleteAsync(int id);

        Task<int> CountMainAsync();

        Task<int> CountWaitingRoomAsync();

        Task<int> CountTaggedAsync(string tagName);

        Task<IEnumerable<Mem>> GetPaginated(int pageSize, int pageNumber, bool? isMain = null);
    }
}
