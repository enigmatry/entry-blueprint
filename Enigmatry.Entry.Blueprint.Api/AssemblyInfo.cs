using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;

[assembly: InternalsVisibleTo("Enigmatry.Entry.Blueprint.Api.Tests")]
[assembly: ApiConventionType(typeof(DefaultApiConventions))]
[assembly: ApiController]
