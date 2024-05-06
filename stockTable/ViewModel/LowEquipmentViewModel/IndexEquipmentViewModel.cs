using stockTable.Models;

namespace stockTable.ViewModel.LowEquipmentViewModel
{
    public class IndexEquipmentViewModel
    {
        public IEnumerable<LowEquipment> Equipments { get; set; }
        public string SearchField { get; set; }
        public IEnumerable<Status> Statuses { get; set; }
        public int StatusId { get; set; }
    }
}
