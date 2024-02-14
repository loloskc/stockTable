using stockTable.Models;

namespace stockTable.Interfaces
{
    public interface IDocumentRepository
    {
        bool Add(Document document);
        bool Delete(Document document);
        bool Update(Document document);
        bool Save();
        Task<IEnumerable<Document>> GetAll();
        Task<Document?> GetById(int id);
        bool NubmerIsValid(string num);
        Task<Document?> GetByNumber(string num);
        Task<Document?> GetByIdNoTrack(int id);
        Task<int> GetCount();


    }
}
