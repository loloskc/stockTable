using Microsoft.AspNetCore.Mvc;
using stockTable.Interfaces;
using stockTable.Models;
using stockTable.ViewModel;

namespace stockTable.Controllers
{
    public class EquipmentController : Controller
    {
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly IDocumentRepository _documentRepository;
        private readonly IBarCodeService _barCodeService;

        public EquipmentController(IEquipmentRepository equipmentRepository, IStatusRepository statusRepository, IDocumentRepository documentRepository, IBarCodeService barCodeService)
        {
            _equipmentRepository = equipmentRepository;
            _statusRepository = statusRepository;
            _documentRepository = documentRepository;
            _barCodeService = barCodeService;   
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
            var equipmentVM = new CreateEqViewModel();
            equipmentVM.Statuses = status;
            return View(equipmentVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEqViewModel equipmnetVM)
        {

            equipmnetVM.Statuses = await _statusRepository.GetAll();
            if (ModelState.IsValid)
            {
                var equipment = equipmnetVM.Equipment;
                var document = equipmnetVM.Document;
                if (_equipmentRepository.NubmerIsValid(equipment.InventoryNum))
                {
                    _documentRepository.Add(document);
                    equipment.Document = document;
                    _equipmentRepository.Add(equipment);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(equipmnetVM);
                }
            }
            else return View(equipmnetVM);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var equipment = await _equipmentRepository.GetById(id);
            var vModel = new DetailEquipmentViewModel()
            {
                Equipment = equipment,
                ImageArray = _barCodeService.GetImage(equipment.InventoryNum)
            };
            return View(vModel);
        }
    }
}
