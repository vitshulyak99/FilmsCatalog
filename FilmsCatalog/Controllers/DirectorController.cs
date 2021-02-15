using System.Linq;
using FilmsCatalog.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FilmsCatalog.Controllers
{
    public class DirectorController : Controller
    {
        private readonly IDirectorService _directorService;

        public DirectorController(IDirectorService directorService)
        {
            _directorService = directorService;
        }

        [HttpGet("[controller]/[action]/{text}")]
        public PartialViewResult Suggestions(string text)
        {
            var directors = _directorService.GetMatch(text).Select(x => x.Name).ToList();
            return PartialView("_DirectorSuggestions", directors);
        }
    }
}