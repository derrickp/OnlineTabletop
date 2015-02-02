using Autofac;
using Autofac.Integration.Owin;
using Autofac.Integration.WebApi;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using MongoDB.Driver;
using OnlineTabletop.Accounts;
using OnlineTabletop.Models;
using OnlineTabletop.Persistence;
using OnlineTabletop.SelfHost.Providers;
using Owin;
using System;
using System.Reflection;
using System.Web.Http; 

namespace OnlineTabletop.SelfHost
{
    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            config.MapHttpAttributeRoutes();

            var connectionString = "mongodb://localhost";
            var client = new MongoClient(connectionString);

            appBuilder.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions()
                {
                    // for demo purposes
                    AllowInsecureHttp = true,

                    TokenEndpointPath = new PathString("/token"),
                    AccessTokenExpireTimeSpan = TimeSpan.FromHours(8),

                    Provider = new SimpleAuthorizationServerProvider(new AccountManager(client))
                });

            appBuilder.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            var builder = new ContainerBuilder();

            builder.Register(c => new PlayerRepository(client)).As<IPlayerRepository<Player>>().InstancePerRequest();
            builder.Register(c => new CharacterRepository(client)).As<ICharacterRepository<Character>>().InstancePerRequest();
            builder.Register(c => new AccountManager(client)).As<IAccountManager<Account>>().InstancePerRequest();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).InstancePerRequest();

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            appBuilder.UseAutofacMiddleware(container);
            appBuilder.UseAutofacWebApi(config);

            appBuilder.UseWebApi(config);
        }
    }
}
