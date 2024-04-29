using stockTable.Models;

namespace stockTable.ViewModel.EquipmentViewModel
{
    public class EditEqViewModel
    {
        public Document? Document { get; set; }
        public Equipment? Equipment { get; set; }
        public IEnumerable<Status>? Statuses { get; set; }
    }
}
