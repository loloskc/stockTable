using stockTable.Data;
using stockTable.Interfaces;
using stockTable.Models;

namespace stockTable.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<IEnumerable<User>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetUserByRole(string role)
        {
            throw new NotImplementedException();
        }
    }
}
