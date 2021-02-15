using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace FilmsCatalog.Attributes
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        public int MaxFileSize { get; init; } = 4096;
        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!(value is IFormFile file)) return ValidationResult.Success;
            return file.Length > MaxFileSize ? new ValidationResult(GetErrorMessage()) : ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return ErrorMessage + $" {MaxFileSize} bytes";
        }
    }
}