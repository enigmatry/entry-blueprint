using Enigmatry.Blueprint.Api.Models;
using Enigmatry.Blueprint.Core.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Enigmatry.Blueprint.Api.Filters
{
    // inspired by https://blog.cloudhub360.com/returning-400-bad-request-from-invalid-model-states-in-asp-net-94275fdfd2a0
    internal class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            CancelSavingIfModelInvalid(context);
            if (!context.ModelState.IsValid) context.Result = CreateErrorResponse(context.ModelState);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            CancelSavingIfModelInvalid(context);
            if (!context.ModelState.IsValid) context.Result = CreateErrorResponse(context.ModelState);
        }

        private static void CancelSavingIfModelInvalid(FilterContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var unitOfWork = context.HttpContext.Resolve<IUnitOfWork>();
                unitOfWork.CancelSaving();
            }
        }

        private static BadRequestObjectResult CreateErrorResponse(ModelStateDictionary modelState) =>
            new BadRequestObjectResult(new ValidationErrorModel(modelState));
    }
}
