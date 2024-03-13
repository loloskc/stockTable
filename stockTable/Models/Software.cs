namespace stockTable.Models
{
    public class Software
    {
        public int Id { get; set; }
        public string InventoryNum { get; set; }
        public string CabinetNum { get; set; }
        public int Price { get; set; }
        public string Responsible { get; set; }
        public string NameForBookkeeping { get; set; }
    }
}
