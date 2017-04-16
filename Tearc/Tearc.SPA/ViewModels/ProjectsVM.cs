using DotNetify;
using DotNetify.Routing;
using Microsoft.Extensions.Localization;
using Tearc.SPA.Resources;
using System.Collections.Generic;
using System.Linq;

namespace ViewModels
{
    /// <summary>
    /// This view model demonstrates the two-way data binding between this server-side view model and the browser view.
    /// </summary>
    public class ProjectsVM : TearcBaseVM, IRoutable
    {
        private readonly IStringLocalizer _localizer;
        protected override IStringLocalizer localizerImpl { get { return _localizer; } }
        public RoutingState RoutingState { get; set; }

        public ProjectsVM(IStringLocalizer<GlobalResource> localizer)
        {
            _localizer = localizer;
            // Register the route templates with RegisterRoutes method extension of the IRoutable.
            this.RegisterRoutes("Projects", new List<RouteTemplate>
            {
                new RouteTemplate("ProjectDefault") { UrlPattern = "" },
                new RouteTemplate("Project") { UrlPattern = "project(/:title)" }
            });
        }
    }

    public class ProjectDetailsVM : BaseVM
    {
        // Normally services should come from dependency injection.
        private readonly WebStoreService _webStoreService = new WebStoreService();

        public WebStoreRecord Book { get; set; }

        public ProjectDetailsVM()
        {
        }
    }
}
