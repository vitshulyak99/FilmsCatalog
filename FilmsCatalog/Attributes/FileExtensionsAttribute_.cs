using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace FilmsCatalog.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class 
        FileExtensionsAttribute_ : ValidationAttribute
    {
        private List<string> AllowedExtensions { get;  } = new();

        public string Extensions
        {
            get => string.Join(',', AllowedExtensions);
            set
            { 
                AllowedExtensions.AddRange(  value.Split(new [] { ',' }, StringSplitOptions.RemoveEmptyEntries));
            }
        }

        public override bool IsValid(object value)
        {
            if (value is not IFormFile file) return true;
            var fileName = file.FileName;

            return AllowedExtensions.Any(y => fileName.EndsWith(y));
        }
    }
}