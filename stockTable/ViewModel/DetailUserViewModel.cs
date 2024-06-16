using stockTable.Models;

namespace stockTable.ViewModel
{
    public class DetailUserViewModel
    {
        public User User { get; set; }
        public IEnumerable<string> RoleName { get; set; }
    }
}
