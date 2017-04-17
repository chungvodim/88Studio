using DotNetify;
using DotNetify.Routing;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tearc.SPA.Resources;

namespace ViewModels
{
    public class IndexVM : TearcBaseVM, IRoutable
    {
        private readonly IStringLocalizer _localizer;
        protected override IStringLocalizer localizerImpl { get { return _localizer; } }

        public class Link
        {
            public string Id => Route.TemplateId;
            public Route Route { get; set; }
            public string Caption { get; set; }
            public string Description { get; set; }
        }

        public RoutingState RoutingState { get; set; }

        public List<Link> BasicExampleLinks => new List<Link>
      {
         new Link { Route = this.GetRoute("Projects"), Caption = "Projectss", Description = "Projectsss"  },
         new Link { Route = this.GetRoute("HelloWorld"), Caption = "Hello World", Description = "Getting familiar with the basics"  },
         new Link { Route = this.GetRoute("ControlTypes"), Caption = "Control Types", Description = "Using various HTML control types"  },
         new Link { Route = this.GetRoute("SimpleList"), Caption = "Simple List", Description = "Simple CRUD list"  }
      };

        public List<Link> FurtherExampleLinks => new List<Link>
      {
         new Link { Route = this.GetRoute("GridView"), Caption = "Grid View", Description = "Master-detail view with search, pagination, wizard and localization" },
         new Link { Route = this.GetRoute("CompositeView"), Caption = "Composite View", Description = "Building modular app through composition" },
         new Link { Route = this.GetRoute("LiveChart"), Caption = "Live Chart", Description = "Real-time made easy"  },
         new Link { Route = this.GetRoute("BookStore"), Caption = "Book Store", Description = "SPA routing with deep-linking"  }
      };

        public IndexVM(IStringLocalizer<GlobalResource> localizer)
        {
            _localizer = localizer;
            this.RegisterRoutes("index", new List<RouteTemplate>
         {
            new RouteTemplate("Home",           "/module/get/Projects/ProjectsVM") { UrlPattern = "", ViewUrl = "Projects" },
            new RouteTemplate("Projects",        "/module/get/Projects/ProjectsVM"),
            new RouteTemplate("HelloWorld",     "/module/get/HelloWorld/HelloWorldVM"),
            new RouteTemplate("ControlTypes",   "/module/get/ControlTypes/ControlTypesVM"),
            new RouteTemplate("SimpleList",     "/module/get/SimpleList/SimpleListVM" ),
            new RouteTemplate("GridView",       "/module/get/GridView/GridViewVM" ),
            new RouteTemplate("CompositeView",  "/module/get/CompositeView" ),
            new RouteTemplate("LiveChart",      "/module/get/LiveChart/LiveChartVM"),
            new RouteTemplate("BookStore",       "/module/get/BookStore/BookStoreVM"),
         });
        }
    }
}
