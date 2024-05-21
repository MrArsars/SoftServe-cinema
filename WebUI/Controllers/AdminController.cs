using System.Diagnostics;
using System.Dynamic;
using Core.Interfaces;
using Core.Models;
using Core.Models.Movie;
using Core.Models.Session;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class AdminController : Controller
    {
        private readonly ISessionService _sessionService;
        private readonly IMovieService _movieService;
        private readonly IActorService _actorService;
        private readonly IHallService _hallService;
        private readonly IGenreService _genreService;
        private dynamic _eo = new ExpandoObject();

        public AdminController(IMovieService movieService, ISessionService sessionService, IActorService actorService,
            IGenreService genreService, IHallService hallService)
        {
            _sessionService = sessionService;
            _actorService = actorService;
            _movieService = movieService;
            _genreService = genreService;
            _hallService = hallService;
        }

        public IActionResult InsertMovie(Movie movie)
        {
            movie.Genres = new();
            movie.Actors = new();

            movie.Genres.Add(new Genre()
            {
                Id = Guid.Parse("31a66ba2-1f63-4491-9e1a-b09e86e17011"),
                Name = "Без жанрів",
            });
            movie.Actors.Add(new Actor()
            {
                Id = Guid.Parse("af5d1c61-424f-4088-ac3c-5819ec24f971"),
                Name = "Без",
                Surname = "акторів"
            });

            _movieService.Create(movie);

            return RedirectToAction("Movies");
        }

        public IActionResult UpdateMovie(Guid movieId)
        {
            var movie = _movieService.GetById(movieId);
            return View(movie);
        }

        public IActionResult UpdateSession(Guid sessionId)
        {
            var session = _sessionService.GetById(sessionId);
            var moviesList = _movieService.GetAll();
            var hallList = _hallService.GetAll();

            var halls = new SelectList(hallList, nameof(Hall.Id), nameof(Hall.Name));
            var movies = new SelectList(moviesList, nameof(Movie.Id), nameof(Movie.Name));

            ViewBag.Movies = movies;
            ViewBag.Halls = halls;
            return View(new SessionDto(session));
        }

        public IActionResult Update(Movie movie)
        {
            _movieService.Update(movie);
            return RedirectToAction("Movies");
        }

        public IActionResult AddMovie()
        {
            var movie = new Movie();
            var genresList = _genreService.GetAll();
            var genres = new SelectList(genresList, nameof(Genre.Id), nameof(Genre.Name));

            ViewBag.Genres = genres;
            return View(movie);
        }

        public IActionResult AddSession()
        {
            var session = new SessionDto();
            var moviesList = _movieService.GetAll();
            var hallList = _hallService.GetAll();

            var halls = new SelectList(hallList, nameof(Hall.Id), nameof(Hall.Name));
            var movies = new SelectList(moviesList, nameof(Movie.Id), nameof(Movie.Name));

            ViewBag.Movies = movies;
            ViewBag.Halls = halls;
            return View(session);
        }

        public IActionResult InsertSession(SessionDto sessionDto)
        {
            sessionDto.ReservedPlaces = new();
            _sessionService.Create(sessionDto);

            return RedirectToAction("Sessions");
        }

        public IActionResult UpdateS(SessionDto sessionDto)
        {
            if (sessionDto.ReservedPlaces is null) sessionDto.ReservedPlaces = new();
            _sessionService.Update(sessionDto);
            return RedirectToAction("Sessions");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Sessions()
        {
            var sessions = _sessionService.GetAll();
            _eo.Sessions = sessions;
            return View(_eo);
        }

        public IActionResult Movies()
        {
            var actors = _actorService.GetAll();
            var movies = _movieService.GetAll();
            var genres = _genreService.GetAll();
            _eo.Actors = actors;
            _eo.Movies = movies;
            _eo.Genres = genres;
            return View(_eo);
        }

        public IActionResult DeleteSession(Guid id)
        {
            var res = _sessionService.DeleteById(id);
            return RedirectToAction("Sessions");
        }

        public IActionResult DeleteMovies(Guid id)
        {
            var result = _movieService.Delete(id);
            result = result && _sessionService.DeleteByMovieId(id);
            if (result)
            {
                return RedirectToAction("Movies");
            }

            ModelState.AddModelError("", "Failed to delete the movie. Please try again.");

            return RedirectToAction("Movies");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}