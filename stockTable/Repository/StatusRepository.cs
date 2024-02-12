using Microsoft.EntityFrameworkCore;
using stockTable.Models;
using stockTable.Interfaces;
using stockTable.Data;

namespace stockTable.Repository
{
    public class StatusRepository : IStatusRepository
    {
        private readonly ApplicationDbContext _context;

        public StatusRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Status status)
        {
            _context.Add(status);
            return Save();
        }

        public bool Delete(Status status)
        {
            _context.Remove(status);
            return Save();
        }

        public async Task<IEnumerable<Status>> GetAll()
        {
            return await _context.Statuses.ToListAsync();
        }

        public async Task<Status?> GetById(int id)
        {
           return await _context.Statuses.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> GetCount()
        {
            return await _context.Statuses.CountAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(Status status)
        {
            _context.Update(status);
            return Save();
        }
    }
}
