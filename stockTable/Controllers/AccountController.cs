using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using stockTable.Data;
using stockTable.Interfaces;
using stockTable.Models;
using stockTable.ViewModel;

namespace stockTable.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly IUserRepository _userRepository;

        public AccountController(UserManager<User> userManager,SignInManager<User> signInManager,ApplicationDbContext context,IUserRepository userRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _userRepository = userRepository;
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
            return RedirectToAction("Index", "Home");
        }
    }
}
