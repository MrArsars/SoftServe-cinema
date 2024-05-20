using System.Diagnostics;
using System.Dynamic;
using Core.Models.Session;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.Controllers;

public class TicketController : Controller
{
    private readonly ISessionService _sessionService;
    private readonly IPlaceService _placeService;
    public TicketController(ISessionService sessionService, IPlaceService placeService)
    {
        _sessionService = sessionService;
        _placeService = placeService;
    }

    public IActionResult Index(Guid sessionId)
    {
        var session = _sessionService.GetById(sessionId);
        var places = _placeService.GetAll().Where(x => x.HallId.Equals(session.Hall.Id)).ToList();
        dynamic TicketModel = new ExpandoObject();
        TicketModel.Session = session;
        TicketModel.Places = places;
        return View(TicketModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}