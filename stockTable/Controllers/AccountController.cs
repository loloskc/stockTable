using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using stockTable.Data;
using stockTable.Models;
using stockTable.ViewModel;

namespace stockTable.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationDbContext _context;

        public AccountController(UserManager<User> userManager,SignInManager<User> signInManager,ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
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

        
        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(!ModelState.IsValid)
                return View(model);
            
            var user = await _userManager.FindByEmailAsync(model.Email);
            
            if(user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(model);
            }

            var newUser = new User()
            {
                Email = model.Email,
                UserName = model.Email
            };
            var newUserResponse = await _userManager.CreateAsync(newUser,model.Password);
            if(newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRole.Admin);
                await _signInManager.SignInAsync(newUser, isPersistent: false);
            }
            return RedirectToAction("Index", "Home");
            
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AdminPanel()
        {
            return View();
        }
    }
}
