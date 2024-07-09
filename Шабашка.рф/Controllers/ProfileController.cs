using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Шабашка.DAL;
using Шабашка.Domain.Entity;
using Шабашка.рф.Models;
using System.IO;
using Microsoft.AspNetCore.Http;

[Authorize]
public class ProfileController : Controller
{
    private readonly ApplicationContext _context;

    public ProfileController(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Profile()
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == "UserID")?.Value;
        if (userId == null)
        {
            return NotFound();
        }

        var user = await _context.Users
                                 .Include(u => u.Profile)
                                 .FirstOrDefaultAsync(u => u.id == long.Parse(userId));

        if (user == null)
        {
            return NotFound();
        }

        var model = new ProfileViewModel
        {
            id = user.Profile.id,
            Age = user.Profile.Age,
            Email = user.Profile.Email,
            AvatarPath = user.Profile.AvatarPath
        };

        return View(model);
    }

    public async Task<IActionResult> EditProfile()
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == "UserID")?.Value;
        if (userId == null)
        {
            return NotFound();
        }

        var user = await _context.Users
                                 .Include(u => u.Profile)
                                 .FirstOrDefaultAsync(u => u.id == long.Parse(userId));

        if (user == null)
        {
            return NotFound();
        }

        var model = new ProfileViewModel
        {
            id = user.Profile.id,
            Age = user.Profile.Age,
            Email = user.Profile.Email,
            AvatarPath = user.Profile.AvatarPath
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> EditProfile(ProfileViewModel model, IFormFile Avatar)
    {
        if (ModelState.IsValid)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserID")?.Value;
            if (userId == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                                     .Include(u => u.Profile)
                                     .FirstOrDefaultAsync(u => u.id == long.Parse(userId));

            if (user == null)
            {
                return NotFound();
            }

            user.Profile.Age = model.Age;
            user.Profile.Email = model.Email;

            if (Avatar != null && Avatar.Length > 0)
            {
                var fileName = Path.GetFileName(Avatar.FileName);
                var filePath = Path.Combine("wwwroot/uploads", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await Avatar.CopyToAsync(stream);
                }

                user.Profile.AvatarPath = $"/uploads/{fileName}";
            }

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Profile));
        }

        return View(model);
    }
}
