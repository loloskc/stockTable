using stockTable.Models;

namespace stockTable.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll();
        Task<IEnumerable<User>> GetUsersByRole(string role);
        Task<User> GetById(string id);
        Task<IEnumerable<string>> GetUserRoleById(string userId);

    }
}
