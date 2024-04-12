using Microsoft.AspNetCore.Mvc;
using stockTable.Interfaces;
using stockTable.Models;
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

        public EquipmentController(IEquipmentRepository equipmentRepository, IStatusRepository statusRepository,
            IDocumentRepository documentRepository, IBarCodeService barCodeService, ILogger<EquipmentController> logger)
        {
            _equipmentRepository = equipmentRepository;
            _statusRepository = statusRepository;
            _documentRepository = documentRepository;
            _barCodeService = barCodeService;   
            _logger = logger;
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
        public async Task<IActionResult> Index(string searchField)
        {
            IEnumerable<Equipment> equipments = await _equipmentRepository.GetAll();

            if (!String.IsNullOrEmpty(searchField))
            {
                var resultByInventoryNum = equipments.Where(c => c.InventoryNum!.Contains(searchField)).ToList();
                var resultByModel = equipments.Where(c => c.Model!.Contains(searchField)).ToList();
                var resultByTypeEq = equipments.Where(c => c.TypeEq!.Contains(searchField)).ToList();
                var resultByIp = equipments.Where(c => c.IPAddress!.Contains(searchField)).ToList();
                var resultBySerialNum = equipments.Where(c => c.SerialNum!.Contains(searchField)).ToList();

                var res = resultByInventoryNum.Union(resultByModel).Union(resultByIp).Union(resultByTypeEq).Union(resultBySerialNum);
                IndexEquipmentViewModel vModel = new IndexEquipmentViewModel()
                {
                    Equipments = res
                };
                return View(vModel);
            }
            else
            {
                return View(new IndexEquipmentViewModel() { Equipments = equipments});
            }

        }

        [HttpPost]
        public async Task<IActionResult> Index(string searchField, int statusId)
        {
            IEnumerable<Equipment> equipments = await _equipmentRepository.GetByStatusId(statusId);

            if (!String.IsNullOrEmpty(searchField))
            {
                var resultByInventoryNum = equipments.Where(c => c.InventoryNum!.Contains(searchField)).ToList();
                var resultByModel = equipments.Where(c => c.Model!.Contains(searchField)).ToList();
                var resultByTypeEq = equipments.Where(c => c.TypeEq!.Contains(searchField)).ToList();
                var resultByIp = equipments.Where(c => c.IPAddress!.Contains(searchField)).ToList();
                var resultBySerialNum = equipments.Where(c => c.SerialNum!.Contains(searchField)).ToList();

                var res = resultByInventoryNum.Union(resultByModel).Union(resultByIp).Union(resultByTypeEq).Union(resultBySerialNum);
                IndexEquipmentViewModel vModel = new IndexEquipmentViewModel()
                {
                    Equipments = res
                };
                return View(vModel);
            }
            else
            {
                return View(new IndexEquipmentViewModel() { Equipments = equipments });
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
