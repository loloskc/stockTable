using Microsoft.AspNetCore.Mvc;
using stockTable.Interfaces;
using stockTable.Service;
using stockTable.ViewModel.LowEquipmentViewModel;

namespace stockTable.Controllers
{
    public class LowEquipmentController : Controller
    {
        private readonly ILowEquipment _lowEquipmentRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly IDocumentRepository _documentRepository;
        private readonly IBarCodeService _barCodeService;
        private readonly ILogger<EquipmentController> _logger;
        private readonly SearchService searchService;
        public LowEquipmentController(ILowEquipment lowEquipment, IStatusRepository statusRepository,
            IDocumentRepository documentRepository, IBarCodeService barCodeService, ILogger<EquipmentController> logger)
        {
            _lowEquipmentRepository = lowEquipment;
            _statusRepository = statusRepository;
            _documentRepository = documentRepository;
            _barCodeService = barCodeService;
            _logger = logger;
            searchService = new();
        }

        public async Task<IActionResult> Index()
        {
            var equipments = await _lowEquipmentRepository.GetAll();
            var statuses = await _statusRepository.GetAll();

            var vModel = new IndexEquipmentViewModel()
            {
                Equipments = equipments,
                Statuses = statuses
            };

            return View(vModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string searchField, int statusId)
        {
            var equipments = await _lowEquipmentRepository.GetAll();
            var statuses = await _statusRepository.GetAll();

            if (statusId == 0)
            {
                var result = searchService.GetEquipment(equipments, searchField);
                var vModel = new IndexEquipmentViewModel()
                {
                    Equipments = result,
                    Statuses = statuses
                };
                return View(vModel);
            }
            else
            {
                var equipment = await _lowEquipmentRepository.GetByStatusId(statusId);
                var result = searchService.GetEquipment(equipment, searchField);
                var vModel = new IndexEquipmentViewModel()
                {
                    Equipments = result,
                    Statuses = statuses
                };
                return View(vModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            if (User.IsInRole("admin") || User.IsInRole("editor"))
            {
                var statuses = await _statusRepository.GetAll();
                var vModel = new CreateLowEqViewModel()
                {
                    Statuses = statuses
                };
                return View(vModel);
            }
            return RedirectToAction("Index", "LowEquipment");
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateLowEqViewModel model)
        {
            if (User.IsInRole("admin") || User.IsInRole("editor"))
            {
                model.Equipment.Status = await _statusRepository.GetById(model.Equipment.StatusId);
                model.Statuses = await _statusRepository.GetAll();
                if (ModelState.IsValid)
                {
                    var equipment = model.Equipment;
                    equipment.Document = model.Document;

                    _documentRepository.Add(model.Document!);
                    _lowEquipmentRepository.Add(equipment);

                    _logger.LogInformation($"{DateTime.Now.ToLongDateString()}  Пользователь: {User.Identity.Name} Действия: Создал запись малоценки id:{equipment.Id}");
                    return RedirectToAction("Index", "LowEquipment");
                }
                else
                    return View(model);
            }
            else
                return RedirectToAction("Index", "LowEquipment");
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var equipment = await _lowEquipmentRepository.GetById(id);
            var vModel = new DetailLowEqViewModel()
            {
                Equipment = equipment,
                ImageArray = _barCodeService.GetImage(equipment!.SerialNum)
            };
            return View(vModel);
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (User.IsInRole("admin") || User.IsInRole("editor"))
            {
                var equipment = await _lowEquipmentRepository.GetById(id);
                var document = await _documentRepository.GetById(equipment.IdDocument);
                if (equipment != null)
                {

                    _lowEquipmentRepository.Delete(equipment);
                    _documentRepository.Delete(document!);
                    return RedirectToAction("Index", "LowEquipment");
                }
                else
                    return View("Error");
            }
            return RedirectToAction("Index", "LowEquipment");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (User.IsInRole("admin") || User.IsInRole("editor"))
            {
                var equipment = await _lowEquipmentRepository.GetById(id);
                var statuses = await _statusRepository.GetAll();
                if(equipment==null)
                    return View("Error");
                var vModel = new EditLowEquipmentViewModel()
                {
                    Equipment = equipment,
                    Statuses = statuses,
                    Document = equipment.Document
                };
                return View(vModel);
            }
            return RedirectToAction("Index", "LowEquipment");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,EditLowEquipmentViewModel model)
        {
            if (User.IsInRole("admin") || User.IsInRole("editor"))
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Failed to edit LowEquipment");
                    model.Statuses = await _statusRepository.GetAll();
                    return View(model);
                }

                var document = model.Document;
                var equipment = model.Equipment;
                equipment.Document = document;
                _documentRepository.Update(document);
                _lowEquipmentRepository.Update(equipment);
            }
            return RedirectToAction("Index", "LowEquipment");
        }
    }
}

