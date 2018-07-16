using System.Collections.Generic;
using System.Linq;
using Enigmatry.Blueprint.Core.Helpers;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Enigmatry.Blueprint.Api.Models
{
    [PublicAPI]
    // used to model the validation value message REST response
    public class ValidationErrorModel
    {
        public ValidationErrorModel()
        {
        }

        public ValidationErrorModel(ModelStateDictionary modelState)
        {
            Errors = FlattenModelStateErrors(modelState).ToList();
        }

        public IList<ErrorModel> Errors { get; } = new List<ErrorModel>();

        public static IEnumerable<ErrorModel> FlattenModelStateErrors(ModelStateDictionary modelState)
        {
            foreach (KeyValuePair<string, ModelStateEntry> error in modelState)
            foreach (ModelError modelError in error.Value.Errors)
            {
                yield return new ErrorModel
                {
                    Key = error.Key.FirstLetterToLowerCase(),
                    ErrorMessage = modelError.ErrorMessage
                };
            }
        }
    }
}