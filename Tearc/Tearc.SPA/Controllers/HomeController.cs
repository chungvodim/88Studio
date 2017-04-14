﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using DotNetify;
using DotNetify.Routing;
using Microsoft.Extensions.Logging;

namespace Tearc.SPA.Controllers
{
    public class HomeController : Controller
    {
        private IHostingEnvironment _hostingEnv;
        private readonly ILogger _logger;

        public HomeController(IHostingEnvironment hostingEnv, ILogger<HomeController> logger)
        {
            _hostingEnv = hostingEnv;
            _logger = logger;
            _logger.LogInformation("test logging");
        }

        // The single-page app's entry point - always starts from index.html.
        [Route("{*id}")]
        public IActionResult Index(string id) => this.FileResult(_hostingEnv, "index.html");

        // Returns JS files for Composite View example, including initial view model states for faster client-side rendering.
        [Route("module/get/CompositeView")]
        public IActionResult AFITop100(string view, string vm)
        {
            var js = this.File(_hostingEnv, "/js/PaginatedTable.js") + this.File(_hostingEnv, "/js/CompositeView.js");
            string vmState = BuildStateString("AFITop100VM")
               + BuildStateString("AFITop100VM.FilterableMovieTableVM.MovieTableVM")
               + BuildStateString("AFITop100VM.MovieDetailsVM");
            return Content(vmState + js, "text/js");
        }

        // Returns JS file of a view, including initial view model states for faster client-side rendering.
        [Route("module/get/{view}/{vm?}")]
        public IActionResult Module(string view, string vm)
        {
            var js = this.File(_hostingEnv, $"/js/{view}.js");
            string vmState = vm != null ? BuildStateString(vm) : null;
            return Content(vmState + js, "text/js");
        }

        private string BuildStateString(string vm) => $"window.vmStates = window.vmStates || {{}}; window.vmStates['{vm}'] = {VMController.GetInitialState(vm) ?? "{}"};";
    }
}
