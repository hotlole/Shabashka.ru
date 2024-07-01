using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using Шабашка.DAL;
using Шабашка.Domain.Entity;
using Шабашка.Service;
using Шабашка.рф.Models;
using Шабашка.Domain.Helpers;

namespace Шабашка.рф.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly ApplicationContext _context;

        public AccountController(IAccountService accountService, ApplicationContext context)
        {
            _accountService = accountService;
            _context = context;
        }

        [HttpGet]
        public IActionResult Register() => View(new RegisterViewModel());

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Name = model.Name,
                    Password = HashPasswordHelper.HashPassword(model.Password)
                };

                _context.Users.Add(user);
                _context.SaveChanges();

                // Создание пустого профиля для нового пользователя
                var profile = new Profile
                {
                    UserId = user.id,
                    Email = user.Name, // Пример, вы можете установить значения по умолчанию
                    Age = 0 // Пример, вы можете установить значения по умолчанию
                };

                _context.Profiles.Add(profile);
                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var hashedPassword = HashPasswordHelper.HashPassword(model.Password);
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Name == model.Name && u.Password == hashedPassword);

                if (user != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Name),
                        new Claim("UserID", user.id.ToString())
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Неправильное имя пользователя или пароль.");
                }
            }

            return View(model);
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
