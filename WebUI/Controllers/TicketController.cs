using System.Diagnostics;
using Core.Models.Session;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.Controllers;

public class TicketController : Controller
{
    private readonly ISessionService _sessionService;
    public TicketController(ISessionService sessionService)
    {
        _sessionService = sessionService;
    }

    public IActionResult Index(Guid sessionId)
    {
        var session = _sessionService.GetById(sessionId);
        return View(session);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}