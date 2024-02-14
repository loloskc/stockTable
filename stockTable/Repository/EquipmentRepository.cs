using Microsoft.EntityFrameworkCore;
using stockTable.Models;
using stockTable.Interfaces;
using stockTable.Data;

namespace stockTable.Repository
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private readonly ApplicationDbContext _context;

        public EquipmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Equipment equipment)
        {
            _context.Add(equipment);
            return Save();
        }

        public bool Delete(Equipment equipment)
        {
            _context.Remove(equipment);
            return Save();
        }

        public async Task<IEnumerable<Equipment>> GetAll()
        {
            return await _context.Equipments.Include(u=>u.Document).Include(u=>u.Status).ToListAsync();
        }

        public async Task<Equipment?> GetById(int id)
        {
            return await _context.Equipments.Include(u=>u.Document).Include(s=>s.Status).FirstOrDefaultAsync(i=>i.Id == id);
        }

        public Task<Equipment?> GetByIdNoTrack(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetCount()
        {
            return await _context.Equipments.CountAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(Equipment equipment)
        {
            _context.Update(equipment);
            return Save();
        }
    }
}
