using Microsoft.AspNetCore.Mvc;
using stockTable.Interfaces;
using stockTable.Models;
using stockTable.Service;
using stockTable.ViewModel;
using stockTable.ViewModel.EquipmentViewModel;

namespace stockTable.Controllers
{
    public class EquipmentController : Controller
    {
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly IDocumentRepository _documentRepository;
        private readonly IBarCodeService _barCodeService;
        private readonly ILogger<EquipmentController> _logger;
        private readonly SearchService searchService;

        public EquipmentController(IEquipmentRepository equipmentRepository, IStatusRepository statusRepository,
            IDocumentRepository documentRepository, IBarCodeService barCodeService, ILogger<EquipmentController> logger)
        {
            _equipmentRepository = equipmentRepository;
            _statusRepository = statusRepository;
            _documentRepository = documentRepository;
            _barCodeService = barCodeService;
            _logger = logger;
            searchService = new();
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Equipment> equipments = await _equipmentRepository.GetAll();
            IEnumerable<Status> statuses = await _statusRepository.GetAll();

            IndexEquipmentViewModel vModel = new IndexEquipmentViewModel()
            {
                Equipments = equipments,
                Statuses = statuses
            };
            return View(vModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string searchField, int statusId)
        {
            IEnumerable<Equipment> equipments = await _equipmentRepository.GetAll();
            IEnumerable<Status> statuses = await _statusRepository.GetAll();


            if (statusId == 0)
            {
                var result = searchService.GetEquipment(equipments, searchField);
                IndexEquipmentViewModel vModel = new IndexEquipmentViewModel()
                {
                    Equipments = result,
                    Statuses = statuses,
                };
                return View(vModel);
            }
            else
            {
                equipments = await _equipmentRepository.GetByStatusId(statusId);
                var result = searchService.GetEquipment(equipments, searchField);
                IndexEquipmentViewModel vModel = new IndexEquipmentViewModel()
                {
                    Equipments = result,
                    Statuses = statuses,
                };
                return View(vModel);
            }

        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            if (User.IsInRole("admin") || User.IsInRole("editor"))
            {
                IEnumerable<Status> status = await _statusRepository.GetAll();
                var equipmentVM = new CreateEqViewModel();
                equipmentVM.Statuses = status;
                return View(equipmentVM);
            }
            return RedirectToAction("Index", "Home");

        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEqViewModel equipmnetVM)
        {
            if (User.IsInRole("admin") || User.IsInRole("editor"))
            {
                equipmnetVM.Statuses = await _statusRepository.GetAll();
                if (ModelState.IsValid)
                {
                    var equipment = equipmnetVM.Equipment;
                    var document = equipmnetVM.Document;
                    if (_equipmentRepository.NubmerIsValid(equipment!.InventoryNum))
                    {
                        _documentRepository.Add(document!);
                        equipment.Document = document;
                        _equipmentRepository.Add(equipment);
                        _logger.LogInformation($"{DateTime.Now.ToLongDateString()}  Пользователь: {User.Identity.Name} Действия: Создал запись оборудования id:{equipment.Id}");
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View(equipmnetVM);
                    }
                }
                else return View(equipmnetVM);
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var equipment = await _equipmentRepository.GetById(id);
            var vModel = new DetailEquipmentViewModel()
            {
                Equipment = equipment!,
                ImageArray = _barCodeService.GetImage(equipment!.InventoryNum)
            };
            return View(vModel);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var equipment = await _equipmentRepository.GetById(id);
            var statuses = await _statusRepository.GetAll();
            if (equipment == null) return View("Error");
            var vModel = new EditEqViewModel()
            {
                Equipment = equipment,
                Statuses = statuses,
                Document = equipment.Document
            };

            return View(vModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditEqViewModel model)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit Equipment");
                model.Statuses = await _statusRepository.GetAll();
                return View(model);
            }
            
            var document = model.Document;
            var equipment = model.Equipment;
            equipment.Document = document;
            _documentRepository.Update(document);
            _equipmentRepository.Update(equipment);
            return RedirectToAction("Index","Equipment");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var equipment = await _equipmentRepository.GetById(id);
            var document = await _documentRepository.GetById(equipment.IdDocument);
            if ( equipment != null)
            {
                
                _equipmentRepository.Delete(equipment);
                _documentRepository.Delete(document!);
                return RedirectToAction("Index", "Equipment");
            }
            else
                return View("Error");

        }
    }
}
