using CyberSecurityProject.CustomValidations;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CyberSecurityProject.Models
{
    public class MealViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string ImageUrl { get; set; }
        [ExtensionVal(ErrorMessage = "Select only Image", AllowedExtensions = "jpg,png,jpeg,gif")]
        [SizeVal(MaxSize = 2, ErrorMessage = "File is too big!")]
        [Required(ErrorMessage ="Required**")]    
        public IFormFile Image { get; set; }
    }
}
