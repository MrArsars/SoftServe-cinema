using System.Diagnostics;
using Core.Interfaces;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.Controllers;

public class AdminController : Controller
{
    private readonly ISessionService _sessionService;
    private readonly IMovieService _movieService;

    public AdminController(IMovieService movieService, ISessionService sessionService)
    {
        _sessionService = sessionService;
        _movieService = movieService;
    }


    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Sessions()
    {
        var sessions = _sessionService.GetAll();
        return View(sessions);
    }

    public IActionResult Movies()
    {
        var movies = _movieService.GetAll();
        return View(movies);
    }

    public IActionResult AddMovie()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}