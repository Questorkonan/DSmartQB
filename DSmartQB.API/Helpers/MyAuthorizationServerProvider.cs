using DSmartQB.CORE.DTOs;
using DSmartQB.CORE.Services;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http.Cors;

namespace DSmartQB.API.Helpers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class MyAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {

        UserTokenDTO user = new UserTokenDTO();
        AccountService _account = new AccountService();


        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }



        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);

            user = _account.CheckUser(context.UserName, context.Password);
            if (user.Id != null)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, user.Role));
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
                identity.AddClaim(new Claim(ClaimTypes.Name, user.Username));

                AuthenticationTicket ticket = new AuthenticationTicket(identity, null);

                context.Validated(identity);
            }
            else
            {
                context.SetError("invalid_grant", "Provided username and password is incorrect");
                //return;
            }

            return Task.FromResult<object>(null);

        }

        public override Task TokenEndpointResponse(OAuthTokenEndpointResponseContext context)
        {
            context.AdditionalResponseParameters.Add("user_name", user.Username);
            context.AdditionalResponseParameters.Add("user_id", user.Id);
            context.AdditionalResponseParameters.Add("Role_id", user.Role);
            context.AdditionalResponseParameters.Add("full_name", user.Fullname);
            context.AdditionalResponseParameters.Add("user_prefix", user.Prefix);
            return base.TokenEndpointResponse(context);
        }

    }
}