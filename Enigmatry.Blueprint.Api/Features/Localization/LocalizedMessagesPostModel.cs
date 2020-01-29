using System;
using System.ComponentModel.DataAnnotations;
using Enigmatry.Blueprint.Api.Infrastructure.Validation;
using FluentValidation;
using JetBrains.Annotations;

namespace Enigmatry.Blueprint.Api.Features.Localization
{
    [PublicAPI]
    public class LocalizedMessagesPostModel
    {
        [Display(Name ="Id")] 
        public int Id { get; set; }

        [Display(Name = "Name")] 
        public string Name { get; set; } = String.Empty;

        [Display(Name ="Email")] 
        public string Email { get; set; } = String.Empty;

        [UsedImplicitly]
        public class LocalizedMessagesPostModelValidator : AbstractValidator<LocalizedMessagesPostModel>
        {
            public LocalizedMessagesPostModelValidator()
            {
                RuleFor(m => m.Id).GreaterThan(0);
                RuleFor(m => m.Name).NotEmpty();
                RuleFor(m => m.Name).Must(BeUnique).WithUniquePropertyViolationMessage();
                RuleFor(m => m.Email).EmailAddress().MaximumLength(2);
            }

            private static bool BeUnique(string name)
            {
                return false;
            }
        }
    }
}
