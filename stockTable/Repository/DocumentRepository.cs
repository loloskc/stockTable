using Microsoft.EntityFrameworkCore;
using stockTable.Models;
using stockTable.Interfaces;
using stockTable.Data;

namespace stockTable.Repository
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly ApplicationDbContext _context;

        public DocumentRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public bool Add(Document document)
        {
            _context.Add(document);
            return Save();
        }

        public bool Delete(Document document)
        {
            _context.Remove(document);
            return Save();
        }

        public async Task<IEnumerable<Document>> GetAll()
        {
            return await _context.Documents.ToListAsync();
        }

        public async Task<Document?> GetById(int id)
        {
            return await _context.Documents.FirstOrDefaultAsync(i=>i.Id == id);
        }

        public Task<Document?> GetByIdNoTrack(int id)
        {
            throw new NotImplementedException();
        }


        public async Task<int> GetCount()
        {
            return await _context.Documents.CountAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(Document document)
        {
            _context.Update(document);
            return Save();
        }

    }
}
