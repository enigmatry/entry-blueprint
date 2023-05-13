using Enigmatry.Blueprint.Api.Features.CkeditorDemo;
using Enigmatry.Entry.CodeGeneration.Configuration.Form;
using Enigmatry.Entry.CodeGeneration.Configuration.Form.Controls;
using JetBrains.Annotations;

namespace Enigmatry.Blueprint.CodeGeneration.Setup.Features.Ckeditor
{
    [UsedImplicitly]
    public class CkeditorDemoConfiguration : IFormComponentConfiguration<GetDescription.Response>
    {
        public void Configure(FormComponentBuilder<GetDescription.Response> builder)
        {
            builder.Component()
                .HasName("CkeditorDemo")
                .BelongsToFeature("CkeditorDemo");

            builder.RichTextInputFormControl(x => x.Description)
                .WithAppearance(FormControlAppearance.Outline)
                .WithFloatLabel(FormControlFloatLabel.Always);
        }
    }
}
