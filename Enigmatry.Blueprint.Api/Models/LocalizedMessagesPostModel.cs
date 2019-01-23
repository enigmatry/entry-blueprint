using System.ComponentModel.DataAnnotations;
using Enigmatry.Blueprint.Api.Resources;
using Enigmatry.Blueprint.Api.Validation;
using FluentValidation;
using JetBrains.Annotations;

namespace Enigmatry.Blueprint.Api.Models
{
    public class LocalizedMessagesPostModel
    {
        [Display(Name ="Id")] public int Id { get; set; }

        [Display(Name="Name", ResourceType = typeof(Localization_SharedResource))] 
        public string Name { get; set; }

        [Display(Name ="Email")] public string Email { get; set; }

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