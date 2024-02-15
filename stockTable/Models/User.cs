using Microsoft.AspNetCore.Identity;


namespace stockTable.Models
{
    public class User: IdentityUser
    {
        public bool Checked { get; set; }
    }
}
