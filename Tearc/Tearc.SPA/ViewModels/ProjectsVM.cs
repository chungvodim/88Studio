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

        private readonly ProjectService _projectsService = new ProjectService();
        public RoutingState RoutingState { get; set; }
        public IEnumerable<object> Projects => _projectsService.GetAllProjects().Select(i => new { Info = i, Route = this.GetRoute("Project", "project/" + i.UrlSafeTitle) });

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

    public class ProjectDetailsVM : BaseVM, IRoutable
    {
        // Normally services should come from dependency injection.
        private readonly ProjectService _projectsService = new ProjectService();

        public RoutingState RoutingState { get; set; }
        public ProjectRecord Project { get; set; }

        public ProjectDetailsVM()
        {
            this.OnRouted((sender, e) =>
            {
                if (!string.IsNullOrEmpty(e.From))
                {
                    // Extract the book title from the route path.
                    var bookTitle = e.From.Replace("project/", "");

                    Project = _projectsService.GetProjectByTitle(bookTitle);
                    Changed(nameof(Project));
                }
            });
        }
    }
}
