using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Шабашка.DAL;
using Шабашка.Domain.Entity;
using Шабашка.рф.Models;

/*
[Authorize(Policy = "AdminOnly")]*/
public class AdminController : Controller
{
    private readonly ApplicationContext _context;

    public AdminController(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var users = await _context.Users.Include(u => u.Profile).ToListAsync();
        return View(users);
    }

    public async Task<IActionResult> Edit(long id)
    {
        var user = await _context.Users.Include(u => u.Profile).FirstOrDefaultAsync(u => u.id == id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(User user)
    {
        if (ModelState.IsValid)
        {
            _context.Update(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(user);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(long id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
