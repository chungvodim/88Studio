using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Extensions.Localization;
using Tearc.Resources;
using DotNetify;

namespace ViewModels
{
    public abstract class TearcBaseVM : BaseVM
    {
        protected abstract IStringLocalizer localizerImpl { get;}
        /// <summary>
        /// Receives culture code, and forces all localized strings to update and sent to the client.
        /// </summary>
        public virtual string CultureCode
        {
            get { return Get<string>(); }
            set
            {
                Set(value);
                Changed(nameof(LocalizedStrings));
            }
        }

        public Dictionary<string, string> LocalizedStrings => Localizer.GetAllStrings().ToDictionary(i => i.Name, i => i.Value);

        /// <summary>
        /// New ASP.NET abstraction to help manage string localization. It pulls the strings from the .resx file.  
        /// See https://docs.microsoft.com/en-us/aspnet/core/fundamentals/localization.
        /// </summary>
        protected IStringLocalizer Localizer => string.IsNullOrEmpty(CultureCode) || CultureCode == "en-US" ? localizerImpl : localizerImpl.WithCulture(new CultureInfo(CultureCode));

    }
}
