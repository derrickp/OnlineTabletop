using Autofac;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using OnlineTabletop.Persistence;
using OnlineTabletop.Models;
using OnlineTabletop.Accounts;
using MongoDB.Driver;

namespace OnlineTabletop.Server
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();

            // Get your HttpConfiguration.
            var config = GlobalConfiguration.Configuration;

            var connectionString = "mongodb://localhost";
            var client = new MongoClient(connectionString);
            
            //builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerRequest().WithParameter("client", new MongoClient(connectionString));
            //builder.Register(c => new Repository<Player>(new MongoClient(connectionString))).As<IRepository<Player>>().InstancePerRequest();
            builder.Register(c => new PlayerRepository(client)).As<IPlayerRepository<Player>>().InstancePerRequest();
            builder.Register(c => new CharacterRepository(client)).As<ICharacterRepository<Character>>().InstancePerRequest();
            builder.Register(c => new AccountManager(client)).As<IAccountManager<Account>>().InstancePerRequest();

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
