using Microsoft.EntityFrameworkCore;
using stockTable.Data;
using stockTable.Interfaces;
using stockTable.Models;

namespace stockTable.Repository
{
    public class LowEquipmentRepository : ILowEquipment
    {
        private readonly ApplicationDbContext _context;
        public LowEquipmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(LowEquipment equipment)
        {
            _context.Add(equipment);
            return Save();
        }

        public bool Delete(LowEquipment equipment)
        {
            _context.Remove(equipment);
            return Save();
        }

        public async Task<IEnumerable<LowEquipment>> GetAll()
        {
            
            var list = await _context.LowEquipments.Include(e=>e.Document).Include(e=>e.Status).ToListAsync();
            return list;
        }

        public async Task<LowEquipment?> GetById(int id)
        {
            return await _context.LowEquipments.Include(e=>e.Document).Include(e=>e.Status).FirstOrDefaultAsync(e=>e.Id==id);
        }

        public Task<LowEquipment?> GetByIdNoTrack(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<LowEquipment>> GetByStatusId(int statusId)
        {
            return await _context.LowEquipments.Where(c => c.StatusId == statusId).Include(c => c.Document).ToListAsync();
        }

        public async Task<int> GetCount()
        {
            return await _context.LowEquipments.CountAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(LowEquipment equipment)
        {
            _context.Update(equipment);
            return Save();
        }
    }
}
