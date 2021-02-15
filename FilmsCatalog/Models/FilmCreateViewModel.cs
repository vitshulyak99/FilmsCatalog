using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace FilmsCatalog.Models
{
    public class FilmCreateViewModel
    {
        [MaxLength(100)]
        [Display(Name = "Название")]
        [Required(ErrorMessage = "Обязательное поле")]
        public string Name { get; set; }
        [MaxLength(1000)]
        [Display(Name = "Описание")]
        [Required(ErrorMessage = "Обязательное поле")]
        public string Description { get; set; }
        [Display(Name = "Год выхода")]
        [Required(ErrorMessage = "Обязательное поле")]
        public int Year { get; set; }

        [Display(Name = "Постер")]
        public IFormFile Poster { get; set; }

        [Display(Name = "Режисер")]
        [Required(ErrorMessage = "Обязательное поле")]
        public string Director { get; set; }
    }
}