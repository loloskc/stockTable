using stockTable.Models;

namespace stockTable.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll();
        Task<IEnumerable<User>> GetUserByRole(string role);
        Task<User> GetById(string id);

    }
}
