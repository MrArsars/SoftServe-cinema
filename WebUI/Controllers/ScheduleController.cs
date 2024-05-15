using System.Diagnostics;
using Core.Models.Session;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.Controllers;

public class ScheduleController : Controller
{
    private readonly ISessionService _sessionService;

    public ScheduleController(ISessionService sessionService)
    {
        _sessionService = sessionService;
    }

    public IActionResult Index()
    {
        var sessions = _sessionService.GetAll();
        return View(sessions);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}