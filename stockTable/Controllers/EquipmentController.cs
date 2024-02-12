using Microsoft.AspNetCore.Mvc;
using stockTable.Interfaces;
using stockTable.Models;

namespace stockTable.Controllers
{
    public class EquipmentController : Controller
    {
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IStatusRepository _statusRepository;

        public EquipmentController(IEquipmentRepository equipmentRepository, IStatusRepository statusRepository)
        {
            _equipmentRepository = equipmentRepository;
            _statusRepository = statusRepository;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Equipment> equipments = await _equipmentRepository.GetAll();
            return View(equipments);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            IEnumerable<Status> status = await _statusRepository.GetAll();
            return View(status);
        }

        [HttpPost]
        public IActionResult Create(int i)
        {
            return RedirectToAction("Index");
        }
    }
}
