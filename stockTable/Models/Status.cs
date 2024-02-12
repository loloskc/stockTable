using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace stockTable.Models
{
    public class Status
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
    }
}
