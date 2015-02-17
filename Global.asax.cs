using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using System.ServiceModel.Syndication;
using System.Xml;
using OpenGovApiClient.Models;
using System.Runtime.Caching;

using System.Configuration;

namespace OpenGovApiClient
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var MemCache = MemoryCache.Default;

            var Tasks = (IEnumerable<ServiceTask>)MemCache.Get("TaskList");

            if (Tasks == null)
            {
                var formatter = new Atom10FeedFormatter();
                using (XmlReader reader = XmlReader.Create(ConfigurationManager.AppSettings["ServiceApiUrl"]))
                {
                    formatter.ReadFrom(reader);
                }

                MemCache.Add("Organisation", formatter.Feed.Authors.First().Name, new CacheItemPolicy {});

                var Items = formatter.Feed.Items.Select(i => new ServiceTask
                {
                    CategoryId = i.Categories.First().Name,
                    CategoryName = i.Categories.First().Label,
                    Content = ((TextSyndicationContent)i.Content).Text,
                    Details = i.Links.Where(l => l.RelationshipType == "self" && l.MediaType == "application/atom+xml").SingleOrDefault().Uri,
                    Id = i.Id,
                    Summary = i.Summary.Text,
                    Title = i.Title.Text,
                    Updated = i.LastUpdatedTime.DateTime
                });

                MemCache.Add("TaskList", Items, new CacheItemPolicy { });
            }
        }
    }
}
