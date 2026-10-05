using System.Security.Claims;
using Enigmatry.Entry.Blueprint.Core.Logging;
using Enigmatry.Entry.Blueprint.Domain.Identity;
using Enigmatry.Entry.Blueprint.Infrastructure.Authorization;
using FakeItEasy;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Enigmatry.Entry.Blueprint.Api.Tests.CoreFeatures;

[Category("unit")]
public class SecurityLoggingAuthorizationMiddlewareResultHandlerFixture
{
    private static readonly Guid UserId = new("5B1C0E2D-0C0E-4E7B-9B4F-2C7A6A9D1F10");

    private ISecurityLogger<SecurityLoggingAuthorizationMiddlewareResultHandler> _logger = null!;
    private List<(string MessageTemplate, object?[] PropertyValues)> _warnings = null!;
    private SecurityLoggingAuthorizationMiddlewareResultHandler _handler = null!;
    private AuthorizationPolicy _policy = null!;
    private HttpContext _context = null!;
    private bool _nextInvoked;

    [SetUp]
    public void SetUp()
    {
        _warnings = [];
        _logger = CreateLogger(_warnings);
        _handler = new SecurityLoggingAuthorizationMiddlewareResultHandler(_logger);
        _policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
        _context = CreateHttpContext();
        _nextInvoked = false;
    }

    [Test]
    public async Task SuccessfulAuthorizationIsNotLoggedAndContinuesPipeline()
    {
        await _handler.HandleAsync(Next, _context, _policy, PolicyAuthorizationResult.Success());

        _nextInvoked.ShouldBeTrue();
        A.CallTo(_logger).MustNotHaveHappened();
    }

    [Test]
    public async Task ChallengedAuthorizationIsLoggedAsSecurityWarning()
    {
        await _handler.HandleAsync(Next, _context, _policy, PolicyAuthorizationResult.Challenge());

        _nextInvoked.ShouldBeFalse();
        var (messageTemplate, propertyValues) = _warnings.ShouldHaveSingleItem();
        messageTemplate.ShouldContain("challenged");
        propertyValues.ShouldContain("POST");
        propertyValues.ShouldContain("/api/users");
    }

    [Test]
    public async Task ForbiddenAuthorizationIsLoggedAsSecurityWarningWithUserAndRequirements()
    {
        var failure = AuthorizationFailure.Failed(_policy.Requirements);

        await _handler.HandleAsync(Next, _context, _policy, PolicyAuthorizationResult.Forbid(failure));

        _nextInvoked.ShouldBeFalse();
        var (messageTemplate, propertyValues) = _warnings.ShouldHaveSingleItem();
        messageTemplate.ShouldContain("denied access");
        propertyValues.ShouldContain(UserId);
        propertyValues.ShouldContain("john.doe");
        propertyValues.ShouldContain("/api/users");
        propertyValues.OfType<string>().ShouldContain(s => s.Contains("DenyAnonymousAuthorizationRequirement", StringComparison.Ordinal));
    }

    private Task Next(HttpContext context)
    {
        _nextInvoked = true;
        return Task.CompletedTask;
    }

    private static ISecurityLogger<SecurityLoggingAuthorizationMiddlewareResultHandler> CreateLogger(
        List<(string MessageTemplate, object?[] PropertyValues)> warnings)
    {
        var logger = A.Fake<ISecurityLogger<SecurityLoggingAuthorizationMiddlewareResultHandler>>();
        A.CallTo(() => logger.LogSecurityWarning(A<string>._, A<object?[]>._))
            .Invokes(call => warnings.Add((call.GetArgument<string>(0)!, call.GetArgument<object?[]>(1)!)));
        return logger;
    }

    private static HttpContext CreateHttpContext()
    {
        var context = new DefaultHttpContext
        {
            RequestServices = CreateRequestServices(),
            User = new ClaimsPrincipal(new ClaimsIdentity([new Claim(ClaimTypes.Name, "john.doe")], "Test"))
        };
        context.Request.Method = "POST";
        context.Request.Path = "/api/users";
        return context;
    }

    private static IServiceProvider CreateRequestServices()
    {
        var currentUserProvider = A.Fake<ICurrentUserProvider>();
        A.CallTo(() => currentUserProvider.UserId).Returns(UserId);

        return new ServiceCollection()
            .AddSingleton(currentUserProvider)
            .AddSingleton(A.Fake<IAuthenticationService>())
            .BuildServiceProvider();
    }
}
