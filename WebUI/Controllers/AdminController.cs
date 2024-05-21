using System.Diagnostics;
using System.Dynamic;
using Core.Interfaces;
using Core.Models;
using Core.Models.Movie;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class AdminController : Controller
    {
        private readonly ISessionService _sessionService;
        private readonly IMovieService _movieService;
        private readonly IActorService _actorService;
        private readonly IGenreService _genreService;
        private dynamic _eo = new ExpandoObject();

        public AdminController(IMovieService movieService, ISessionService sessionService, IActorService actorService, IGenreService genreService)
        {
            _sessionService = sessionService;
            _actorService = actorService;
            _movieService = movieService;
            _genreService = genreService;
        }

        public IActionResult CreateMovie(Movie movie)
        {
            if (ModelState.IsValid)
            {
                var createdMovie = _movieService.Create(movie);
                if (createdMovie != null)
                {
                    return RedirectToAction("Movies"); // Redirect to the Movies action
                }
                else
                {
                    ModelState.AddModelError("", "Failed to create the movie. Please try again.");
                }
            }
            return View("AddMovie", movie);
        }

        public IActionResult CreateActor(dynamic dynamic)
        {
            Actor actor = dynamic.Actor;
            if (ModelState.IsValid)
            {
                var createdActor = _actorService.Create(actor);
                if (createdActor != null)
                {
                    return RedirectToAction("Movies"); // Redirect to the Movies action
                }
                else
                {
                    ModelState.AddModelError("", "Failed to create the actor. Please try again.");
                }
            }
            return View("AddActor", actor);
        }

        public IActionResult CreateGenre(dynamic dynamic)
        {
            if (ModelState.IsValid)
            {
                var createdMovie = _movieService.Create(dynamic.Genre);
                if (createdMovie != null)
                {
                    return RedirectToAction("Movies"); // Redirect to the Movies action
                }
                else
                {
                    ModelState.AddModelError("", "Failed to create the genre. Please try again.");
                }
            }
            return View("AddGenre", dynamic.Genre);
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

        public IActionResult AddGenre()
        {
            return View();
        }

        public IActionResult DeleteMovies(Guid id)
        {
            /*var movie = _movieService.GetById(id);
            if (movie != null)
            {*/
                var result = _movieService.Delete(id);
                if (result)
                {
                    return RedirectToAction("Movies"); // Redirect to the Movies action
                }
                else
                {
                    ModelState.AddModelError("", "Failed to delete the movie. Please try again.");
                }
                /*}
                else
                {
                    ModelState.AddModelError("", "Movie not found.");
                }*/
            return RedirectToAction("Movies");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
