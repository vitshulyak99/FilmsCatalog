using System;
using System.ComponentModel.DataAnnotations;
using FilmsCatalog.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        [FileExtensionsAttribute_(Extensions = ".jpeg,.jpg,.png,.gif", ErrorMessage = "Неверный тип файла" )]
        [MaxFileSize(ErrorMessage = "Неверный размер файла",MaxFileSize = 2048)]
        public IFormFile Poster { get; set; }

        [Display(Name = "Режисер")]
        [Required(ErrorMessage = "Обязательное поле")]
        public string Director { get; set; }
    }
}