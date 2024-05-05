using JobPosting.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobPosting.Models;
using System.Threading.Tasks;

public class HomeController : Controller
{
    private readonly JobPostingContext _context;

    public HomeController(JobPostingContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var employers = await _context.Employers.ToListAsync();
        return View(employers);
    }

    public IActionResult Privacy()
    {
        return View();
    }
}