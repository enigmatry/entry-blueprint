using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using Microsoft.EntityFrameworkCore.Storage;

namespace Enigmatry.Blueprint.Infrastructure.Data.Conventions
{
    [UsedImplicitly]
    public class CustomSqlServerConventionSetBuilder : SqlServerConventionSetBuilder
    {
        public CustomSqlServerConventionSetBuilder(RelationalConventionSetBuilderDependencies dependencies, ISqlGenerationHelper sqlGenerationHelper)
            : base(dependencies, sqlGenerationHelper)
        { }

        public override ConventionSet AddConventions(ConventionSet conventionSet)
        {
            base.AddConventions(conventionSet);

            conventionSet.ModelBuiltConventions.Add(new DefaultStringLengthConvention());

            return conventionSet;
        }
    }
}
