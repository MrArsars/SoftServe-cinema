using System.Diagnostics;
using System.Dynamic;
using Core.Interfaces;
using Core.Models.Ticket;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;
using Core.Models;
using Core.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers;

public class TicketController : Controller
{
    private readonly ISessionService _sessionService;
    private readonly IPlaceService _placeService;
    private readonly IEmailService _emailService;
    private readonly ITicketService _ticketService;
    private readonly UserManager<User> _userManager;
    static dynamic _ticketModel = new ExpandoObject();

    public TicketController(ISessionService sessionService, IPlaceService placeService, IEmailService emailService,
        ITicketService ticketService, UserManager<User> userManager)
    {
        _sessionService = sessionService;
        _placeService = placeService;
        _emailService = emailService;
        _ticketService = ticketService;
        _userManager = userManager;
    }

    public IActionResult Index(Guid? sessionId = null)
    {
        if (sessionId is null) return View(_ticketModel);
        var session = _sessionService.GetById((Guid)sessionId);
        var places = _placeService.GetAll().Where(x => x.HallId.Equals(session.Hall.Id)).ToList();
        _ticketModel.Session = session;
        _ticketModel.Places = places;
        _ticketModel.SelectedPlaces = new List<Guid>();
        return View(_ticketModel);
    }

    [HttpPost]
    public IActionResult TogglePlace(Guid placeId)
    {
        List<Guid> list = _ticketModel.SelectedPlaces;
        if (list.Exists(x => x.Equals(placeId)))
            _ticketModel.SelectedPlaces.Remove(placeId);
        else
            _ticketModel.SelectedPlaces.Add(placeId);

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult>? SendTickets()
    {
        if (_ticketModel.SelectedPlaces.Count == 0)
            return null;

        List<TicketDto> TicketDtos = new();
        foreach (var place in _ticketModel.SelectedPlaces)
        {
            TicketDtos.Add(new TicketDto()
            {
                Id = new Guid(),
                SessionId = _ticketModel.Session.Id,
                PlaceId = place
            });
        }

        var res = _ticketService.Create(TicketDtos);
        if (!res) return null;

        if (User.Identity.IsAuthenticated)
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);
            string email = user?.Email;

            var sessions = _sessionService.GetAll();
            var places = _placeService.GetAll();
            
            var tickets = TicketDtos.Select(ticket => new Ticket(ticket, sessions.FirstOrDefault(x => x.Id.Equals(ticket.SessionId)), places.FirstOrDefault(x => x.Id.Equals(ticket.PlaceId)))).ToList();

            _emailService.SendEmailAsync(email, tickets);
        }

        return RedirectToAction("Index", "Home");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}