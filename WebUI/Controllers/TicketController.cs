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
    static dynamic TicketModel = new ExpandoObject();

    public TicketController(ISessionService sessionService, IPlaceService placeService)
    {
        _sessionService = sessionService;
        _placeService = placeService;
    }

    public IActionResult Index(Guid? sessionId = null)
    {
        if (sessionId is null) return View(TicketModel);
        var session = _sessionService.GetById((Guid)sessionId);
        var places = _placeService.GetAll().Where(x => x.HallId.Equals(session.Hall.Id)).ToList();
        TicketModel.Session = session;
        TicketModel.Places = places;
        TicketModel.SelectedPlaces = new List<Guid>();
        return View(TicketModel);
    }

    [HttpPost]
    public IActionResult TogglePlace(Guid placeId)
    {
        List<Guid> list = TicketModel.SelectedPlaces;
        if (list.Exists(x => x.Equals(placeId)))
            TicketModel.SelectedPlaces.Remove(placeId);
        else
            TicketModel.SelectedPlaces.Add(placeId);

        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}