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

        // Объединяем конструкторы
        public AccountController(IAccountService accountService, ApplicationContext context)
        {
            _accountService = accountService;
            _context = context;
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(User model)
        {
            if (ModelState.IsValid)
            {
                // Хешируем пароль перед добавлением пользователя в контекст
                model.Password = HashPasswordHelper.HashPassword(model.Password);

                // Добавляем пользователя в контекст
                _context.Users.Add(model);
                // Сохраняем изменения в базе данных
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
                // Хешируем введённый пароль
                var hashedPassword = HashPasswordHelper.HashPassword(model.Password);

                // Пытаемся найти пользователя с введёнными данными
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Name == model.Name && u.Password == hashedPassword);

                if (user != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Name)
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

