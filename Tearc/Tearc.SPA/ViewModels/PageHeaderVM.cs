using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Extensions.Localization;
using Tearc.SPA.Resources;
using DotNetify;

namespace ViewModels
{
   /// <summary>
   /// This view model demonstrates the two-way data binding between this server-side view model and the browser view.
   /// </summary>
   public class PageHeaderVM : TearcBaseVM
   {
        private readonly IStringLocalizer _localizer;
        protected override IStringLocalizer localizerImpl { get { return _localizer; } }

        /// <summary>
        /// Constructor.
        /// </summary>
        public PageHeaderVM(IStringLocalizer<GlobalResource> localizer)
        {
            _localizer = localizer;
        }
    }
}
