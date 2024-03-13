using Microsoft.AspNetCore.Mvc;
using stockTable.Interfaces;
using stockTable.Models;

namespace stockTable.Controllers
{
    public class LowEquipmentController : Controller
    {
        private readonly ILowEquipment _lowEquipmentRepository;
        public LowEquipmentController(ILowEquipment lowEquipment)
        {
            _lowEquipmentRepository = lowEquipment;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<LowEquipment> equipments = await _lowEquipmentRepository.GetAll();
            return View(equipments);
        }
    }
}
