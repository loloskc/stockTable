using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace stockTable.Models
{
    public class Equipment
    {
        [Key]
        public int Id { get; set; }
        public string TypeEq { get; set; }
        public string InventoryNum { get; set; }
        public string Model { get; set; }
        public string? NamePC { get; set; }
        public string? OCName { get; set; }
        public string IPAddress { get; set; }
        public string? SerialNum { get; set; }
        public string dataE { get; set; }
        public int Price { get; set; }
        [ForeignKey("Document")]
        public int IdDocument { get; set; }
        
        public Document? Document { get; set; }
        [ForeignKey("Status")]
        public int StatusId { get; set; }
        
        public Status? Status { get; set; }



    }
}

