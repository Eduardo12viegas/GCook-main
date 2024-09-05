using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GCook.Models;

namespace GCook.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(
        ILogger<HomeController> logger,
        AppDbContext
    )
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        HomeVM home = new()
        {
            Categorias = _context.Categorias
                .Where(c => c.ExibirHome)
                .AsNoTracking()
                .ToList(),
            Receitas = _context.Receitas
                .Include(r => r.Categoria)
                .Include(r => r.Ingredientes)
                .AsNoTracking()
                .ToList()
        };
        return View(home);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
