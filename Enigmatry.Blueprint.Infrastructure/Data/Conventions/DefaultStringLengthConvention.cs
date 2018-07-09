using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Enigmatry.Blueprint.Infrastructure.Data.Conventions
{
    public sealed class DefaultStringLengthConvention : IModelBuiltConvention
    {
        internal const int DefaultStringLength = 255;
        private const string MaxLengthAnnotation = "MaxLength";

        private readonly int _defaultStringLength;

        public DefaultStringLengthConvention(int defaultStringLength = DefaultStringLength)
        {
            _defaultStringLength = defaultStringLength;
        }

        public InternalModelBuilder Apply(InternalModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Metadata.GetEntityTypes())
            {
                foreach (var property in entity.GetProperties())
                {
                    if (property.ClrType == typeof(string))
                    {
                        if (property.FindAnnotation(MaxLengthAnnotation) == null)
                        {
                            property.AddAnnotation(MaxLengthAnnotation, _defaultStringLength);
                        }
                    }
                }
            }

            return modelBuilder;
        }
    }
}