using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace CyberSecurityProject.CustomValidations
{
    public class ExtensionVal : ValidationAttribute
    {
        public string AllowedExtensions { get; set; }
        public override bool IsValid(object value)
        {
            IFormFile myfile = value as IFormFile;
            if(myfile == null)
                return false;
            string Extenstion = Path.GetExtension(myfile.FileName);// carrying the extension with "."
            Extenstion = Extenstion.TrimStart('.'); // removing the dot
            return AllowedExtensions.Contains(Extenstion); // if extension is allowed it will return true, otherwise return false

        }
    }
}