using System.Diagnostics;
using System.Dynamic;
using Core.Interfaces;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.Controllers;

public class FilmController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IMovieService _movieService;
    private readonly ISessionService _sessionService;

    public FilmController(ILogger<HomeController> logger, IMovieService movieService, ISessionService sessionService)
    {
        _logger = logger;
        _movieService = movieService;
        _sessionService = sessionService;
    }
    
    public IActionResult Index(Guid id)
    {
        var movie = _movieService.GetById(id);
        var sessions = _sessionService.GetAll().Where(x => x.Movie.Id.Equals(id)).ToList();
        dynamic model = new ExpandoObject();
        model.Movie = movie;
        model.Sessions = sessions;
        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
}