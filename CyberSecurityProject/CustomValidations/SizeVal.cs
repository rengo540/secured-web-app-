using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CyberSecurityProject.CustomValidations
{
    public class SizeVal : ValidationAttribute, IClientValidatable
    {
        public int MaxSize { get; set; }

 

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, System.Web.Mvc.ControllerContext context)
        {
            ModelClientValidationRule rule = new ModelClientValidationRule();
            rule.ErrorMessage = base.ErrorMessage;
            rule.ValidationType = "filesize";
            rule.ValidationParameters.Add("MaxSize", MaxSize);
            return new ModelClientValidationRule[] { rule };
        }

        public override bool IsValid(object value)
        {
            if (value == null)
                return true;
            else
            {
                IFormFile file = value as IFormFile;
                return file.Length <= MaxSize * 1048576;
            }
        }
    }


}
