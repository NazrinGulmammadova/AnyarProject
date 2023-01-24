using Core.Entities;
using DataAccess.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebApplication1.Areas.AnyarAdmin.ViewModels.Team;
using WebApplication1.Utilities;

namespace WebApplication1.Areas.AnyarAdmin.Controllers;
[Area("AnyarAdmin")]
public class TeamMemberController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;

    public TeamMemberController(AppDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    public IActionResult Index()
    {
        return View(_context.TeamMembers);
    }
    public async Task<IActionResult> Detail(int id)
    {
        var model = await _context.TeamMembers.FindAsync(id);
        if (model == null) return NotFound();
        return View(model);
    }
    public IActionResult Create(int id)
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TeamMemberVM teamVM)
    {
        if (!ModelState.IsValid) return View(teamVM);
        if (teamVM.Image == null)
        {
            ModelState.AddModelError("Image", "Please select the image");
            return View(teamVM);
        }
        if (!teamVM.Image.CheckFileSize(100))
        {
            ModelState.AddModelError("Image", "Please choose correct file size");
            return View(teamVM);
        }
        if (!teamVM.Image.CheckFileFormat("image/"))
        {
            ModelState.AddModelError("Image", "Plase choose correct file format");
            return View(teamVM);
        }
        var filename = string.Empty;
        try
        {
            filename = await teamVM.Image.CopyFileAsync(_env.WebRootPath, "assets", "img", "team");
        }
        catch (Exception)
        {
            return View(teamVM);
        }
        TeamMember member = new()
        {
            Image = filename,
            Name = teamVM.Name,
            Position = teamVM.Position,
            Description = teamVM.Description
        };
        await _context.AddAsync(member);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Delete(int id)
    {
        var model = await _context.TeamMembers.FindAsync(id);
        if (model == null) return NotFound();
        return View(model);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    [ActionName("Delete")]
    public async Task<IActionResult> DeletePost(int id)
    {
        var model=await _context.TeamMembers.FindAsync(id);
        if (model == null) return NotFound();
        Helper.DeleteFile(_env.WebRootPath, "assets", "img", "team", model.Image);
        _context.TeamMembers.Remove(model);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
