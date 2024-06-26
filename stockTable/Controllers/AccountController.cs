﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using stockTable.Constants;
using stockTable.Data;
using stockTable.Interfaces;
using stockTable.Models;
using stockTable.ViewModel;
using System.Linq;
using System.Security.Claims;

namespace stockTable.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<AccountController> _logger;

        public AccountController(UserManager<User> userManager,SignInManager<User> signInManager,ApplicationDbContext context,IUserRepository userRepository,ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _userRepository = userRepository;
            _logger = logger;
        }

        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            
            if(user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, model.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);
                    var userPrincipal = await _signInManager.CreateUserPrincipalAsync(user);
                    var identity = userPrincipal.Identity;
                    if (result.Succeeded)
                    {
                        _logger.LogInformation($"{DateTime.Now.ToLongDateString()}  Пользователь: {user.Id} Действия: Зашел в аккауни");
                        return RedirectToAction("Index", "Home");
                    }
                }
                TempData["Error"] = "Wrong credentials. Please, try again";
                return View(model);
            }
            TempData["Error"] = "Wrong credentials. Please, try again";
            return View(model);
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation($"{DateTime.Now.ToLongDateString()}  Пользователь: {User.Identity.Name} Действия: Вышел из аккаунта");

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult AdminPanel()
        {
            if(User.IsInRole("admin"))
                return View();
            else 
                return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> ListAccount()
        {
            string currentUserId = User.FindFirst(ClaimsConstants.UserId).Value;
            var user = await _userRepository.GetById(currentUserId);

            await _userManager.AddToRoleAsync(user, "reader");

            if (User.IsInRole("admin"))
            {
                var list = await _userRepository.GetAll();
                return View(list);
            }
            else
                return RedirectToAction("Index", "Home");
            
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            if (User.IsInRole("admin"))
            {
                var response = new RegisterViewModel();
                return View(response);
            }
            else
                return RedirectToAction("Index", "Home");

        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(model);
            }

            var newUser = new User()
            {
                Email = model.Email,
                UserName = model.UserName,
                PhoneNumber = model.PhoneNumber
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, model.Password);
            if (newUserResponse.Succeeded)
            {
                switch (model.Role)
                {
                    case "reader": await _userManager.AddToRoleAsync(newUser, UserRole.Reader);
                        break;
                    case "editor": await _userManager.AddToRoleAsync(newUser, UserRole.Editor);
                        break;
                    case "admin": await _userManager.AddToRoleAsync(newUser, UserRole.Admin);
                        break;
                }
                

            }
            _logger.LogInformation($"{DateTime.Now.ToLongDateString()}  Пользователь: {User.Identity.Name} Действия: Создал аккаунт id:{newUser.Id} Name:{newUser.UserName}");

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Detail(string id)
        {
            string currentUserId = User.FindFirst(ClaimsConstants.UserId).Value;

            if(currentUserId == id || User.IsInRole("admin"))
            {
                DetailUserViewModel model = new DetailUserViewModel();
                model.User = await _userRepository.GetById(id);
                model.RoleName = await _userRepository.GetUserRoleById(id);

                
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
