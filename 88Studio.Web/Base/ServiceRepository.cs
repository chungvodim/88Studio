using _88Studio.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Principal;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using _88Studio.Dto;

namespace _88Studio.Web.Base
{
    public class ServiceRepository
    {
        private string _CurrentLocale;
        public string CurrentLocale
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_CurrentLocale))
                {
                    _CurrentLocale = Entity.Locale.DEFAULT_LOCALE;
                }
                return this._CurrentLocale;
            }

            set
            {
                this._CurrentLocale = value;
            }
        }
        public IPrincipal CurrentUser { get; private set; }
        public _88Studio.Enum.UserRole[] CurrentRoles { get; private set; }

        public ServiceRepository(string locale, System.Security.Principal.IPrincipal user, _88Studio.Enum.UserRole[] Roles)
        {
            this.CurrentLocale = locale;
            this.CurrentUser = user;
            this.CurrentRoles = Roles;
        }
        public IAdminstrationService AdministrationService
        {
            get
            {
                var service = DependencyResolver.Current.GetService<IAdminstrationService>();
                service.UserID = CurrentUser.Identity.GetUserId<int>();
                service.BranchID = CurrentUser.Identity.GetBranchID();
                service.CompanyID = CurrentUser.Identity.GetCompanyID();
                service.Roles = CurrentRoles;
                return service;
            }
        }

        public IMasterDataService MasterDataService
        {
            get { return DependencyResolver.Current.GetService<IMasterDataService>(); }
        }
        public IStatisticService StatisticService
        {
            get { return DependencyResolver.Current.GetService<IStatisticService>(); }
        }
        public ILogService LogService
        {
            get { return DependencyResolver.Current.GetService<ILogService>(); }
        }
        public IListingService ListingService
        {
            get
            {
                var listingService = DependencyResolver.Current.GetService<IListingService>();
                listingService.CurrentLocale = this.CurrentLocale;
                listingService.UserID = CurrentUser.Identity.GetUserId<int>();
                listingService.BranchID = CurrentUser.Identity.GetBranchID();
                listingService.CompanyID = CurrentUser.Identity.GetCompanyID();

                return listingService;
            }
        }
        public IImageService ImageService
        {
            get { return DependencyResolver.Current.GetService<IImageService>(); }
        }

        public ILeadService LeadService
        {
            get { return DependencyResolver.Current.GetService<ILeadService>(); }
        }
    }
}