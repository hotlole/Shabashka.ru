using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Шабашка.DAL;
using Шабашка.рф.Models;

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
            Email = user.Profile.Email
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
            Email = user.Profile.Email
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> EditProfile(ProfileViewModel model)
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

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Profile));
        }

        return View(model);
    }
}
