using stockTable.Models;

namespace stockTable.Interfaces
{
    public interface ILowEquipment
    {
        bool Add(LowEquipment equipment);
        bool Delete(LowEquipment equipment);
        bool Update(LowEquipment equipment);
        bool Save();
        Task<IEnumerable<LowEquipment>> GetAll();
        Task<LowEquipment?> GetById(int id);
        Task<LowEquipment?> GetByIdNoTrack(int id);
        Task<int> GetCount();
    }
}
