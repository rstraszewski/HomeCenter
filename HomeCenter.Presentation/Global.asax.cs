using System.DirectoryServices.ActiveDirectory;
using System.Threading;
using System.Web.Security;
using Alchemy;
using Alchemy.Classes;
using HomeCenter.App_Start;
using HomeCenter.AuthenticationDomain;
using HomeCenter.CommonDomain;
using HomeCenter.Communication;
using HomeCenter.Database;
using HomeCenter.DeviceDomain;
using HomeCenter.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using TypeLite;
using TypeLite.Net4;

namespace HomeCenter
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public partial class MvcApplication : System.Web.HttpApplication
    {

        protected void Application_Start()
        {
            System.Data.Entity.Database.SetInitializer(new HomeCenterDbInitializer());
            PostAuthenticateRequest += Application_PostAuthenticateRequest;
            Console.Write("Creating database: ...");
            
            CreateDatabaseIfNotExisting();
            Console.WriteLine("Done");

            Console.Write("Registring mappings, routes and bundles...");
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperConfig.RegisterMappings();
            Console.WriteLine("Done.");
            Console.Write("Unity initialise...");
            
            var container = Bootstrapper.Initialise();
            Console.WriteLine("Done.");
            //DomainEvents.Initialise(container);
            Console.Write("Web Socket initialise...");
            container.Resolve<IWebSocket>().Initialise();
            Console.WriteLine("Done.");
            Console.Write("Quartz initialise...");
            container.Resolve<IJobScheduler>().Initialise();
            Console.WriteLine("Done.");
            Console.Write("Serial Port initialise...");
            container.Resolve<ISerialPortConnection>().Initialise();
            Console.WriteLine("Done.");
        }

        void CreateDatabaseIfNotExisting()
        {
            using (var db = new HomeCenterDb())
            {
                db.Database.Initialize(false);
            }
        }


    }
}