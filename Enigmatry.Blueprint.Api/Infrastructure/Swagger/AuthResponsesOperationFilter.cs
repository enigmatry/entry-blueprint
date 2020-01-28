namespace Enigmatry.Blueprint.Api.Infrastructure.Swagger
{
    /*[UsedImplicitly]
    public class AuthResponsesOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            IEnumerable<AuthorizeAttribute> authAttributes = context.MethodInfo.DeclaringType.GetCustomAttributes(true)
                .Union(context.MethodInfo.GetCustomAttributes(true))
                .OfType<AuthorizeAttribute>();

            if (authAttributes.Any())
            {
                operation.Responses.Add("401", new OpenApiResponse {Description = "Unauthorized"});
            }

            // TODO: scopes?
            // operation.Security.Add(new Dictionary<string, IEnumerable<string>>());
        }
    }*/
}
