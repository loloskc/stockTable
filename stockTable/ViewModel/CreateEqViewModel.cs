using stockTable.Models;
using System.ComponentModel.DataAnnotations.Schema;


namespace stockTable.ViewModel
{
    public class CreateEqViewModel
    {
        public Document? Document { get; set; }
        public Equipment? Equipment { get; set; }
        public IEnumerable<Status>? Statuses { get; set; }
        //public string TypeEq { get; set; }
        //public string Model { get; set; }
        //public string? NamePC { get; set; }
        //public string? OCName { get; set; }
        //public string IPAddress { get; set; }
        //public string? SerialNum { get; set; }
        //public string dataE { get; set; }
        //public int Price { get; set; }
        //public int StatusId { get; set; }
        //public string InventoryNum { get; set; }
        //public string CabinetNum { get; set; }
        //public string? NumContract { get; set; }
        //public string? ContractNum { get; set; }
        //public string DataContract { get; set; }
        //public string Responsible { get; set; }
        //public string NameForBookkeeping { get; set; }

    }
}
