using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;
using OnlineTabletop.Accounts;

namespace OnlineTabletop.SelfHost.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private AccountManager _accountManager;

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // OAuth2 supports the notion of client authentication
            // this is not used here
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            // validate user credentials (demo!)
            // user credentials should be stored securely (salted, iterated, hashed yada)
            if (!_accountManager.VerifyLogin(context.UserName, context.Password))
            {
                context.Rejected();
                return;
            }
            
            // create identity
            
            var id = new ClaimsIdentity(context.Options.AuthenticationType, context.UserName, "player");
            id.AddClaim(new Claim("userName", context.UserName));
            id.AddClaim(new Claim("role", "player"));
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            context.Validated(id);
        }

        public SimpleAuthorizationServerProvider(AccountManager accountManager)
        {
            this._accountManager = accountManager;
        }
    }
}
