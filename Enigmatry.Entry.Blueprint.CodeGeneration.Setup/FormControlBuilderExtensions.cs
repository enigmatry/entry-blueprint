using Enigmatry.Entry.CodeGeneration.Configuration.Form.Controls;

namespace Enigmatry.Entry.Blueprint.CodeGeneration.Setup;
internal static class FormControlBuilderExtensions
{
    internal static FormControlGroupBuilder<T> WithTranslationId<T>(this FormControlGroupBuilder<T> builder, string translationId) =>
        builder
            .WithLabelTranslationId(translationId)
            .WithPlaceholderTranslationId(translationId);

    internal static InferredFormControlBuilder WithTranslationId(this InferredFormControlBuilder builder, string translationId) =>
        builder
            .WithLabelTranslationId(translationId)
            .WithPlaceholderTranslationId(translationId);

    internal static SelectFormControlBuilder WithTranslationId(this SelectFormControlBuilder builder, string translationId) =>
        builder
            .WithLabelTranslationId(translationId)
            .WithPlaceholderTranslationId(translationId);

    internal static AutocompleteFormControlBuilder WithTranslationId(this AutocompleteFormControlBuilder builder, string translationId) =>
        builder
            .WithLabelTranslationId(translationId)
            .WithPlaceholderTranslationId(translationId);

    internal static TextareaFormControlBuilder WithTranslationId(this TextareaFormControlBuilder builder, string translationId) =>
        builder
            .WithLabelTranslationId(translationId)
            .WithPlaceholderTranslationId(translationId);
}
