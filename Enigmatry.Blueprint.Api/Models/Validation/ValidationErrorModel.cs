using System.Collections.Generic;
using System.Linq;
using Enigmatry.Blueprint.Core.Helpers;
using FluentValidation;
using FluentValidation.Results;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Enigmatry.Blueprint.Api.Models.Validation
{
    [PublicAPI]
    // used to model the validation value message REST response
    public class ValidationErrorModel
    {
        public ValidationErrorModel()
        {
        }

        public ValidationErrorModel(ValidationException contextException)
        {
            Errors = MapValidationErrors(contextException.Errors).ToList();
        }

        public ValidationErrorModel(ModelStateDictionary modelState)
        {
            Errors = MapValidationErrors(modelState).ToList();
        }

        public IList<ErrorModel> Errors { get; set; }

        public static IEnumerable<ErrorModel> MapValidationErrors(ModelStateDictionary modelState)
        {
            foreach (KeyValuePair<string, ModelStateEntry> error in modelState)
            foreach (ModelError modelError in error.Value.Errors)
            {
                yield return new ErrorModel
                {
                    Field = error.Key.FirstLetterToLowerCase(),
                    ErrorMessage = modelError.ErrorMessage
                };
            }
        }

        private IEnumerable<ErrorModel> MapValidationErrors(IEnumerable<ValidationFailure> contextExceptionErrors)
        {
            foreach (ValidationFailure error in contextExceptionErrors)
            {
                yield return new ErrorModel
                {
                    Field = error.PropertyName.FirstLetterToLowerCase(),
                    ErrorMessage = error.ErrorMessage,
                };
            }
        }
    }
}