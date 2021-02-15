using System.Diagnostics;
using System.Linq;
using FilmsCatalog.Extensions;
using FilmsCatalog.Helpers;
using FilmsCatalog.Models;
using FilmsCatalog.Models.Db;
using FilmsCatalog.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReflectionIT.Mvc.Paging;

namespace FilmsCatalog.Controllers
{
    public class FilmController : Controller
    {
        private readonly IFilmService _filmService;
        private readonly FileHelper _fileHelper;
        private const int DefaultPageSize = 5;

        public FilmController(IFilmService filmService, FileHelper fileHelper)
        {
            _filmService = filmService;
            _fileHelper = fileHelper;
        }

        [HttpGet]
        public IActionResult Index() => Index(1);


        [HttpGet("page/{page:int}", Name = "FilmPage")]
        public IActionResult Index(int page)
        {
            if (page < 1)
            {
                return BadRequest("Invalid page or size argument");
            }

            var count = _filmService.Count;


            var pagingList = PagingList.Create(_filmService.Query(), DefaultPageSize, page);

            return View(pagingList);
        }


        [HttpGet]
        [Authorize]
        public IActionResult Create() => View();

        [HttpPost]
        [Authorize]
        public IActionResult Create(FilmCreateViewModel model)
        {
            var userId = User.UserId();

            var film = new Film
            {
                AddedAt = !string.IsNullOrEmpty(userId) ? new User { Id = userId } : null,
                Name = model.Name,
                Description = model.Description,
                Year = model.Year,
                Director = new Director { Name = model.Director }
            };

            if (model.Poster is not null)
            {
                film.Poster = _fileHelper.SaveUploadedFile(model.Poster);
            }

            film = _filmService.Create(film);

            return RedirectToAction("Details", new { film.Id });
        }

        [HttpGet]
        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = User.UserId();
            if (!_filmService.CheckOwner(id, userId)) return BadRequest();

            var film = _filmService.GetById(id);
            var viewModel = new FilmEditViewModel
            {
                Id = id,
                Name = film.Name,
                Description = film.Description,
                Director = film.Director.Name,
                PosterImg = _fileHelper.MakeUrlToFile(film.Poster),
                Year = film.Year
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(FilmEditViewModel model)
        {
            var userId = User.UserId();
            if (!_filmService.CheckOwner(model.Id, userId)) return BadRequest();
            var film = _filmService.GetById(model.Id);
            film.Description = model.Description;
            film.Director = film.Director.Name.Equals(model.Director)
                ? film.Director
                : new Director { Name = model.Director };
            film.Year = model.Year;
            if (model.Poster is not null)
            {
                _fileHelper.DeleteFile(film.Poster);
                film.Poster = _fileHelper.SaveUploadedFile(model.Poster);
            }

            _filmService.Update(film);
            return RedirectToAction("Details", new { model.Id });
        }

        [HttpGet("[action]/{id:int}")]
        public IActionResult Details(int id)
        {
            var film = _filmService.GetById(id);
            if (film is null) return NotFound($"Not found film by id: {id}");

            var details = new FilmDetailsViewModel
            {
                Id = id,
                Name = film.Name,
                Description = film.Description,
                Director = film.Director.Name,
                PosterImg = _fileHelper.MakeUrlToFile(film.Poster),
                Year = film.Year,
                User = film.AddedAt
            };

            return View(details);
        }

        [HttpGet("[action]/{id:int}")]
        public IActionResult Delete(int id)
        {
            var fileName = _filmService.Query().Where(x => x.Id.Equals(id)).Select(x => x.Poster).FirstOrDefault();
            _fileHelper.DeleteFile(fileName);
            _filmService.Delete(id);

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}