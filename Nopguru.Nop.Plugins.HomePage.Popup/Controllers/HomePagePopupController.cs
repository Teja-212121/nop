using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core;
using Nop.Services.Affiliates;
using Nop.Services.Catalog;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Areas.Admin.Factories;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Models.Extensions;
using Nop.Web.Framework.Mvc.Filters;
using Nopguru.Nop.Plugins.HomePage.Popup.Domains;
using Nopguru.Nop.Plugins.HomePage.Popup.Infrastructure;
using Nopguru.Nop.Plugins.HomePage.Popup.Models.Configure;
using Nopguru.Nop.Plugins.HomePage.Popup.Models.HomePagePopup;
using Nopguru.Nop.Plugins.HomePage.Popup.Models.HomePagePopupFrontEnd;
using Nopguru.Nop.Plugins.HomePage.Popup.Services;

namespace Nopguru.Nop.Plugins.HomePage.Popup.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class HomePagePopupController : BasePluginController
    {
        #region Fields

        private readonly ILocalizationService _localizationService;
        private readonly INotificationService _notificationService;
        private readonly IPermissionService _permissionService;
        private readonly ISettingService _settingService;
        private readonly HomePagePopupSettings _homePagePopupSettings;
        private readonly IHomePagePopupService _homePagePopupService;
        private readonly IProductService _productService;
        private readonly ICourseManagementService _courseManagementService;
        private readonly IAffiliateService _affiliateService;

        #endregion

        #region Ctor

        public HomePagePopupController(ILocalizationService localizationService,
            INotificationService notificationService,
            IPermissionService permissionService,
            ISettingService settingService,
            HomePagePopupSettings homePagePopupSettings,
            IHomePagePopupService homePagePopupService,
            IProductService productService,
            ICourseManagementService courseManagementService,
            IAffiliateService affiliateService)
        {
            _localizationService = localizationService;
            _notificationService = notificationService;
            _permissionService = permissionService;
            _settingService = settingService;
            _homePagePopupSettings = homePagePopupSettings;
            _homePagePopupService = homePagePopupService;
            _productService = productService;
            _courseManagementService = courseManagementService;
            _affiliateService = affiliateService;
        }

        #endregion

        #region Method

        #region Configure

        [AuthorizeAdmin]
        [Area(AreaNames.Admin)]
        public async Task<IActionResult> Configure()
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            var model = new ConfigurationModel
            {
                Enable = _homePagePopupSettings.Enable,
                HomePage = _homePagePopupSettings.HomePage,
                LoginPage = _homePagePopupSettings.LoginPage,
                RegisterPage = _homePagePopupSettings.RegisterPage,
                CategoryPage = _homePagePopupSettings.CategoryPage,
                ProductDetailPage = _homePagePopupSettings.ProductDetailPage,
                AllProductPage = _homePagePopupSettings.AllProductPage,
                SpecificProductPage = _homePagePopupSettings.SpecificProductPage,
                SpecificProductPageIds = _homePagePopupSettings.SpecificProductPageIds,
                SearchPage = _homePagePopupSettings.SearchPage,
                ShoppingCartPage = _homePagePopupSettings.ShoppingCartPage,
                WishListPage = _homePagePopupSettings.WishListPage,
                CheckoutPage = _homePagePopupSettings.CheckoutPage,
                OnePageCheckout = _homePagePopupSettings.OnePageCheckout,
                MultipleCheckout = _homePagePopupSettings.MultipleCheckout,
                PopupLogoPictureId = _homePagePopupSettings.PopupLogoPictureId
            };

            var products = await _productService.SearchProductsAsync();
            foreach (var product in products)
            {
                model.AvailableProducts.Add(new SelectListItem { Text = product.Name, Value = product.Id.ToString() });
            }

            return View("~/Plugins/HomePage.Popup/Views/Configure/Configure.cshtml", model);
        }

        [AuthorizeAdmin]
        [Area(AreaNames.Admin)]
        [HttpPost]
        public async Task<IActionResult> Configure(ConfigurationModel model)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            if (!ModelState.IsValid)
                return await Configure();

            _homePagePopupSettings.Enable = model.Enable;
            _homePagePopupSettings.HomePage = model.HomePage;
            _homePagePopupSettings.LoginPage = model.LoginPage;
            _homePagePopupSettings.RegisterPage = model.RegisterPage;
            _homePagePopupSettings.CategoryPage = model.CategoryPage;
            _homePagePopupSettings.ProductDetailPage = model.ProductDetailPage;
            _homePagePopupSettings.AllProductPage = model.AllProductPage;
            _homePagePopupSettings.SpecificProductPage = model.SpecificProductPage;
            _homePagePopupSettings.SpecificProductPageIds = model.SpecificProductPageIds.ToList();
            _homePagePopupSettings.SearchPage = model.SearchPage;
            _homePagePopupSettings.ShoppingCartPage = model.ShoppingCartPage;
            _homePagePopupSettings.WishListPage = model.WishListPage;
            _homePagePopupSettings.CheckoutPage = model.CheckoutPage;
            _homePagePopupSettings.OnePageCheckout = model.OnePageCheckout;
            _homePagePopupSettings.MultipleCheckout = model.MultipleCheckout;
            _homePagePopupSettings.PopupLogoPictureId = model.PopupLogoPictureId;

            await _settingService.SaveSettingAsync(_homePagePopupSettings);

            _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Plugins.Saved"));

            return await Configure();
        }

        #endregion

        #region HomePagePopup - Admin

        [AuthorizeAdmin]
        [Area(AreaNames.Admin)]
        public async Task<IActionResult> List()
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            var model = new HomePagePopupSearchModel();
            model.SetGridPageSize();
            return View("~/Plugins/HomePage.Popup/Views/HomePagePopup/List.cshtml", model);
        }

        [AuthorizeAdmin]
        [Area(AreaNames.Admin)]
        [HttpPost]
        public async Task<IActionResult> List(HomePagePopupSearchModel searchModel)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            var homePagePopups = await _homePagePopupService.GetAllHomePagePopupList(searchModel.SearchByName, searchModel.SearchByContactNumber,
                searchModel.SearchByEmail, searchModel.SearchBySubject, searchModel.SearchByPageName, pageIndex: searchModel.Page - 1,
                pageSize: searchModel.PageSize);

            //prepare list model
            var model = await new HomePagePopupListModel().PrepareToGridAsync(searchModel, homePagePopups, () =>
            {
                //fill in model values from the entity
                return homePagePopups.SelectAwait(async hp =>
                {
                    var cource = await _courseManagementService.GetCourseManagementByIdAsync(hp.Courses);
                    var affiliate = await _affiliateService.GetAffiliateByIdAsync(hp.AffiliateId);

                    //fill in model values from the entity
                    var homePagePopupModel = new HomePagePopupModel()
                    {
                        Id = hp.Id,
                        Name = hp.Name,
                        ContactNumber = hp.ContactNumber,
                        Email = hp.Email,
                        Subject = hp.Subject,
                        Courses = hp.Courses,
                        CourseName = cource.Name,
                        PageName = hp.PageName,
                        AffiliateId = hp.AffiliateId,
                        AffiliateName = affiliate != null ? await _affiliateService.GetAffiliateFullNameAsync(affiliate) : ""
                    };

                    return homePagePopupModel;
                });
            });
            return Json(model);
        }

        #endregion

        #region HomePagePopup - Front

        [HttpGet]
        public virtual async Task<IActionResult> HomePagePopupFront()
        {
            var model = new HomePagePopupFrontSideModel();

            var courseManagements = await _courseManagementService.GetAllCourseManagementList();
            model.AvailableCourses.Add(new SelectListItem { Text = "--Select--", Value = "0", Selected = true });
            foreach (var course in courseManagements.Where(x => x.IsActive))
            {
                model.AvailableCourses.Add(new SelectListItem { Text = course.Name, Value = course.Id.ToString() });
            }

            return Json(model.AvailableCourses);
        }

        [HttpPost]
        public virtual async Task<IActionResult> HomePagePopupFront(string name, string contactnumber, string email, string subject, string courses, string pageName, int affiliateId)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            if (string.IsNullOrEmpty(contactnumber))
                throw new ArgumentNullException("contactnumber");

            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException("email");

            if (string.IsNullOrEmpty(subject))
                throw new ArgumentNullException("subject");

            if (string.IsNullOrEmpty(courses))
                throw new ArgumentNullException("courses");

            if (string.IsNullOrEmpty(pageName))
                throw new ArgumentNullException("pageName");

            HomePagePopup homePagePopup = new HomePagePopup();

            homePagePopup.Name = name;
            homePagePopup.ContactNumber = contactnumber;
            homePagePopup.Email = email;
            homePagePopup.Subject = subject;
            homePagePopup.Courses = int.Parse(courses);
            homePagePopup.AffiliateId = affiliateId;
            if (pageName == "/")
            {
                homePagePopup.PageName = "Home";
            }
            else
            {
                pageName = pageName.Replace("/", "");
                homePagePopup.PageName = pageName;
            }

            await _homePagePopupService.InsertHomePagePopupAsync(homePagePopup);

            return Json(new { Result = true });
        }

        #endregion

        #endregion
    }
}
