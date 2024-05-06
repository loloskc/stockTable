using stockTable.Models;

namespace stockTable.ViewModel.LowEquipmentViewModel
{
    public class EditLowEquipmentViewModel
    {
        public Document? Document { get; set; }
        public LowEquipment? Equipment { get; set; }
        public IEnumerable<Status>? Statuses { get; set; }
    }
}
