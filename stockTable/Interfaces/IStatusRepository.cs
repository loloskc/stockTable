using stockTable.Models;

namespace stockTable.Interfaces
{
    public interface IStatusRepository
    {
        bool Add(Status status);
        bool Update(Status status);
        bool Delete(Status status);
        bool Save();
        Task<IEnumerable<Status>> GetAll();
        Task<Status?> GetById(int id);
        Task<int> GetCount();
    }
}
