using System.ComponentModel.DataAnnotations;

namespace stockTable.Models
{
    public class Document
    {
        [Key]
        public int Id { get; set; }
        public string CabinetNum { get; set; }
        public string? NumContract { get; set; }
        public string DataContract { get; set; }
        public string Responsible { get; set; }
        public string NameForBookkeeping { get; set; }
    }
}
