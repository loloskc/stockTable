using stockTable.Models;

namespace stockTable.Interfaces
{
    public interface IEquipmentRepository
    {
        bool Add(Equipment equipment);
        bool Delete(Equipment equipment);
        bool Update(Equipment equipment);
        bool Save();
        Task<IEnumerable<Equipment>> GetAll();
        Task<Equipment?> GetById(int id);
        Task<Equipment?> GetByIdNoTrack(int id);
        Task<int> GetCount();

    }
}
