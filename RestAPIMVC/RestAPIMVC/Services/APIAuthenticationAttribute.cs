using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using RestAPIMVC.Repositories;

namespace RestAPIMVC.Services;

public class APIAuthenticationAttribute: AuthenticationHandler<AuthenticationSchemeOptions>
{
    private IUserRepository _userRepository;
    public APIAuthenticationAttribute(
        IOptionsMonitor<AuthenticationSchemeOptions> options, 
        ILoggerFactory logger, 
        UrlEncoder encoder, 
        ISystemClock clock, 
        IUserRepository userRepository) : base(options, logger, encoder, clock)
    {
        this._userRepository = userRepository;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        Endpoint endpoint = Context.GetEndpoint();
        if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
            return AuthenticateResult.NoResult();
        
        //checking for the authorization in the header
        if (!Request.Headers.ContainsKey("Authorization"))
            return AuthenticateResult.Fail("Authorization Header required!");
        
        //Authorization: Scheme <credential>
        AuthenticationHeaderValue authorizationHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
        
        //substring Ignore API-Key
        
        //basic format
        //Authorization: Basic base64(username:password)
        byte[] credentialByte = Convert.FromBase64String(authorizationHeader.Parameter);
        string[] credentials = Encoding.UTF8.GetString(credentialByte).Split(":");

        string username = credentials[0];
        string password = credentials[1];
        
        //is the username and password valid?
        if(!this._userRepository.Authenticate(username, password))
            return AuthenticateResult.Fail("Incorrect username or password");
        
        //Setup claims
        Claim[] claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, username),
            new Claim(ClaimTypes.Name, username)
        };

        ClaimsIdentity identity = new ClaimsIdentity(claims, Scheme.Name);
        ClaimsPrincipal principal = new ClaimsPrincipal(identity);
        AuthenticationTicket ticket = new AuthenticationTicket(principal, Scheme.Name);

        return AuthenticateResult.Success(ticket);

    }

    protected override Task HandleChallengeAsync(AuthenticationProperties properties)
    {
        Response.Headers["WWW-Authenticate"] = "Basic realm=\"\", charset=\"UTF-8\"";
        return base.HandleChallengeAsync(properties);
    }
}