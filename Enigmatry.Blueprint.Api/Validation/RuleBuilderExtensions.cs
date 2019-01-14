using Enigmatry.Blueprint.Api.Resources;
using FluentValidation;

namespace Enigmatry.Blueprint.Api.Validation
{
    public static class RuleBuilderExtensions{
        public static IRuleBuilderOptions<T, TProperty> WithUniquePropertyViolationMessage<T, TProperty>(
            this IRuleBuilderOptions<T, TProperty> rule)
        {
            return rule.WithLocalizedMessage(typeof(Localization_SharedResource),
                nameof(Localization_SharedResource.PropertyNameMustBeUnique));
        }
    }
}