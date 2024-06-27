using System.Security.Claims;
using System.Threading.Tasks;
using Шабашка.Domain.Response;
using Шабашка.DAL;
using Шабашка.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Шабашка.Domain.Enum;
using Шабашка.рф.Models;

namespace Шабашка.Service
{
    public class AccountService : IAccountService
    {
        public async Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model)
        {
            // Реализация метода Login
            // Пример реализации:
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == model.Name && u.Password == model.Password);
            if (user == null)
            {
                return new BaseResponse<ClaimsIdentity>
                {
                    Description = "Неверный логин или пароль"
                };
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, "User")
            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            return new BaseResponse<ClaimsIdentity>
            {
                Data = claimsIdentity,
                Description = "Вход выполнен успешно",
                StatusCode = StatusCode.OK
            };
        }

        public async Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model)
        {
            // Реализация метода Register
            // Пример реализации:
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == model.Name);
            if (user != null)
            {
                return new BaseResponse<ClaimsIdentity>
                {
                    Description = "Пользователь с таким именем уже существует"
                };
            }

            user = new User
            {
                Name = model.Name,
                Password = model.Password
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, "User")
            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            return new BaseResponse<ClaimsIdentity>
            {
                Data = claimsIdentity,
                Description = "Регистрация прошла успешно",
                StatusCode = StatusCode.OK
            };
        }

        private readonly ApplicationContext _context;

        public AccountService(ApplicationContext context)
        {
            _context = context;
        }
    }
}
