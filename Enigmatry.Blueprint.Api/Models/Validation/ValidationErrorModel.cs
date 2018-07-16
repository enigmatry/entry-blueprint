using System.Collections.Generic;
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
            FlattenModelStateErrors(modelState);
        }

        public IList<ErrorModel> Errors { get; } = new List<ErrorModel>();

        private void FlattenModelStateErrors(ModelStateDictionary modelState)
        {
            foreach (KeyValuePair<string, ModelStateEntry> error in modelState)
            foreach (ModelError modelError in error.Value.Errors)
            {
                Errors.Add(new ErrorModel
                {
                    Key = error.Key.FirstLetterToLowerCase(),
                    ErrorMessage = modelError.ErrorMessage
                });
            }
        }
    }
}