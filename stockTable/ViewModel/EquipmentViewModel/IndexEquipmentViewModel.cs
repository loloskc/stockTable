using stockTable.Models;

namespace stockTable.ViewModel.EquipmentViewModel
{
    public class IndexEquipmentViewModel
    {
        public IEnumerable<Equipment> Equipments { get; set; }
        public string SearchField { get; set; }
        public int StatusId { get; set; }
    }
}
