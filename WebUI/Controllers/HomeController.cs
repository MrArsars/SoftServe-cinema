using System.Diagnostics;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IActorService _actorService;

    public HomeController(ILogger<HomeController> logger, IActorService actorService)
    {
        _logger = logger;
        _actorService = actorService;
    }

    public IActionResult Index()
    {
        var actors = _actorService.GetAll();
        return View(actors);
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