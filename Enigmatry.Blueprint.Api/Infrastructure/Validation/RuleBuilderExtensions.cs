using Enigmatry.Blueprint.Api.Resources;
using FluentValidation;

namespace Enigmatry.Blueprint.Api.Infrastructure.Validation
{
    public static class RuleBuilderExtensions{
        public static IRuleBuilderOptions<T, TProperty> WithUniquePropertyViolationMessage<T, TProperty>(
            this IRuleBuilderOptions<T, TProperty> rule)
        {
            return rule.WithMessage(x => Localization_SharedResource.PropertyNameMustBeUnique);
        }
    }
}
