using Microsoft.EntityFrameworkCore;
using stockTable.Data;
using stockTable.Interfaces;
using stockTable.Migrations;
using stockTable.Models;
using System.Diagnostics;

namespace stockTable.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetById(string id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(i => i.Id == id);
            return user;
        }

        public async Task<IEnumerable<string>> GetUserRoleById(string userId)
        {
            var UserRole = await _context.UserRoles.Where(i => i.UserId == userId).ToListAsync();
            var list = new List<string>();
            foreach (var thisRole in UserRole)
            {
                var role = await _context.Roles.FirstOrDefaultAsync(i => i.Id == thisRole.RoleId);
                list.Add(role.Name);
            }
            return list;
        }

        public async Task<IEnumerable<User>> GetUsersByRole(string roleName)
        {
            var listUserId = await GetUserId(roleName);
            List<User> user = new List<User>();
            foreach(var id in listUserId)
            {
                user.Add(await GetById(id));
            }
            return user;
        }

        private async Task<string> GetRoleId(string roleName)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(i => i.NormalizedName == roleName);
            return role.Id;
        }

        private async Task<IEnumerable<string>> GetUserId(string roleName)
        {
            var roleId = await GetRoleId(roleName);
            var list = await _context.UserRoles.Where(i=>i.RoleId == roleId).ToListAsync();

            List<string> userId = new List<string>();
            foreach (var item in list)
            {
                userId.Add(item.UserId);
            }
            return userId;
        }
    }
}
