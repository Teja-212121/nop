using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Nop.Core;
using Nop.Core.Domain.Logging;
using Nop.Services.Cms;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Plugins;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Infrastructure;
using Nop.Web.Framework.Menu;

namespace Nopguru.Nop.Plugins.HomePage.Popup
{
    public class HomePagePopupPlugin : BasePlugin, IMiscPlugin, IAdminMenuPlugin
    {
        #region Fields

        private readonly ILocalizationService _localizationService;
        private readonly ISettingService _settingService;
        private readonly IWebHelper _webHelper;
        private readonly ILogger _logger;
        private readonly IPermissionService _permissionService;

        #endregion

        #region Ctor

        public HomePagePopupPlugin(ILocalizationService localizationService,
            ISettingService settingService,
            IWebHelper webHelper,
            ILogger logger,
            IPermissionService permissionService)
        {
            _localizationService = localizationService;
            _settingService = settingService;
            _webHelper = webHelper;
            _logger = logger;
            _permissionService = permissionService;
        }

        #endregion

        #region Method

        #region Configuration

        /// <summary>
        /// Gets a configuration page URL
        /// </summary>
        public override string GetConfigurationPageUrl()
        {
            return $"{_webHelper.GetStoreLocation()}Admin/HomePagePopup/Configure";
        }

        /// <summary>
        /// Install the plugin
        /// </summary>
        /// <returns>A task that represents the asynchronous operation</returns>
        public override async Task InstallAsync()
        {
            await _settingService.SaveSettingAsync(new HomePagePopupSettings());

            await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
            {
                ["Plugins.HomePage.Popup.Configuration.Fields.Enable"] = "Enable",
                ["Plugins.HomePage.Popup.Configuration.Fields.Enable.Hint"] = "Enable your plugin for more configure.",
                ["Plugins.HomePage.Popup.Configuration.Fields.HomePage"] = "Home Page",
                ["Plugins.HomePage.Popup.Configuration.Fields.HomePage.Hint"] = "Enable home page for display popup in home page.",
                ["Plugins.HomePage.Popup.Configuration.Fields.LoginPage"] = "Login Page",
                ["Plugins.HomePage.Popup.Configuration.Fields.LoginPage.Hint"] = "Enable login page for display popup in login page.",
                ["Plugins.HomePage.Popup.Configuration.Fields.RegisterPage"] = "Register Page",
                ["Plugins.HomePage.Popup.Configuration.Fields.RegisterPage.Hint"] = "Enable register page for display popup in register page.",
                ["Plugins.HomePage.Popup.Configuration.Fields.CategoryPage"] = "Category Page",
                ["Plugins.HomePage.Popup.Configuration.Fields.CategoryPage.Hint"] = "Enable category page for display popup in category page.",
                ["Plugins.HomePage.Popup.Configuration.Fields.ProductDetailPage"] = "Product Detail Page",
                ["Plugins.HomePage.Popup.Configuration.Fields.ProductDetailPage.Hint"] = "Enable product detail page for display popup in product detail page.",
                ["Plugins.HomePage.Popup.Configuration.Fields.AllProductPage"] = "All Product Page",
                ["Plugins.HomePage.Popup.Configuration.Fields.AllProductPage.Hint"] = "Enable all product page for display popup in all product page.",
                ["Plugins.HomePage.Popup.Configuration.Fields.SpecificProductPage"] = "Specific Product Page",
                ["Plugins.HomePage.Popup.Configuration.Fields.SpecificProductPage.Hint"] = "Enable specific product page for display popup in specific product page.",
                ["Plugins.HomePage.Popup.Configuration.Fields.SpecificProductPageIds"] = "Specific Product",
                ["Plugins.HomePage.Popup.Configuration.Fields.SpecificProductPageIds.Hint"] = "Enable specific product for display popup in specific product.",
                ["Plugins.HomePage.Popup.Configuration.Fields.SearchPage"] = "Search Page",
                ["Plugins.HomePage.Popup.Configuration.Fields.SearchPage.Hint"] = "Enable search page for display popup in search page.",
                ["Plugins.HomePage.Popup.Configuration.Fields.ShoppingCartPage"] = "Shopping Cart Page",
                ["Plugins.HomePage.Popup.Configuration.Fields.ShoppingCartPage.Hint"] = "Enable shopping cart page for display popup in shopping cart page.",
                ["Plugins.HomePage.Popup.Configuration.Fields.WishListPage"] = "Wishlist Page",
                ["Plugins.HomePage.Popup.Configuration.Fields.WishListPage.Hint"] = "Enable wishlist page for display popup in wishlist page.",
                ["Plugins.HomePage.Popup.Configuration.Fields.CheckoutPage"] = "Checkout Page",
                ["Plugins.HomePage.Popup.Configuration.Fields.CheckoutPage.Hint"] = "Enable checkout page for display popup in checkout page.",
                ["Plugins.HomePage.Popup.Configuration.Fields.OnePageCheckout"] = "Onepage Checkout",
                ["Plugins.HomePage.Popup.Configuration.Fields.OnePageCheckout.Hint"] = "Enable one page checkout for display popup in one page checkout.",
                ["Plugins.HomePage.Popup.Configuration.Fields.MultipleCheckout"] = "Multiple Checkout",
                ["Plugins.HomePage.Popup.Configuration.Fields.MultipleCheckout.Hint"] = "Enable multiple page checkout for display popup in checkout page.",
                ["Plugins.HomePage.Popup.Field.Name"] = "Name",
                ["Plugins.HomePage.Popup.Field.Name.Hint"] = "Enter the name",
                ["Plugins.HomePage.Popup.Field.ContactNumber"] = "Contact Number",
                ["Plugins.HomePage.Popup.Field.ContactNumber.Hint"] = "Enter the Contact Number",
                ["Plugins.HomePage.Popup.Field.Email"] = "Email Address",
                ["Plugins.HomePage.Popup.Field.Email.Hint"] = "Enter the email address",
                ["Plugins.HomePage.Popup.Field.Subject"] = "Subject",
                ["Plugins.HomePage.Popup.Field.Subject.Hint"] = "Enter the Subject",
                ["Plugins.HomePage.Popup.Field.Courses"] = "Course",
                ["Plugins.HomePage.Popup.Field.Courses.Hint"] = "Enter the Cources name",
                ["Plugins.HomePage.Popup.Field.SearchByName"] = "Name",
                ["Plugins.HomePage.Popup.Field.SearchByName.Hint"] = "Search value by name",
                ["Plugins.HomePage.Popup.Field.SearchByContactNumber"] = "Contact Number",
                ["Plugins.HomePage.Popup.Field.SearchByContactNumber.Hint"] = "Search value by contact number",
                ["Plugins.HomePage.Popup.Field.SearchByEmail"] = "Email",
                ["Plugins.HomePage.Popup.Field.SearchByEmail.Hint"] = "Search value by email",
                ["Plugins.HomePage.Popup.Field.SearchBySubject"] = "Subject",
                ["Plugins.HomePage.Popup.Field.SearchBySubject.Hint"] = "Search value by subject",
                ["Plugins.HomePage.Popup.Field.SearchByPageName"] = "Page name",
                ["Plugins.HomePage.Popup.Field.SearchByPageName.hint"] = "Search value by page name",
                ["Plugins.HomePage.Popup.Field.SearchByCourceName"] = "Cource name",
                ["Plugins.HomePage.Popup.Field.SearchByCourceName.hint"] = "Search by cource name.",
                ["Plugins.HomePage.Popup.Field.SearchByDisplayOrder"] = "Display Order",
                ["Plugins.HomePage.Popup.Field.SearchByDisplayOrder.hint"] = "Search by display order.",
                ["Plugins.HomePage.Popup.Field.SearchByIsActive"] = "Is Active",
                ["Plugins.HomePage.Popup.Field.SearchByIsActive.hint"] = "Search by is active",
                ["Plugins.HomePage.Popup.Field.Title"] = "Homepage Popup",
                ["Plugins.HomePage.Popup.MainMenu.Title"] = "Homepage Popup",
                ["Plugins.HomePage.Popup.MainMenu.Configuration"] = "Configuration",
                ["Plugins.HomePage.Popup.MainMenu.ManageList"] = "Manage Homepage Popup",
                ["Plugins.HomePage.Popup.Field.PageName"] = "Page Name",
                ["Plugins.HomePage.Popup.Field.CourseName"] = "Course name",
                ["Plugins.HomePage.Popup.Field.CourseName.hint"] = "Enter the cource name.",
                ["Plugins.HomePage.Popup.Field.DisplayOrder"] = "Display order",
                ["Plugins.HomePage.Popup.Field.DisplayOrder.hint"] = "Enter the display order.",
                ["Plugins.HomePage.Popup.Field.IsActive"] = "Is active",
                ["Plugins.HomePage.Popup.Field.IsActive.hint"] = "Enable is active for cource name activation.",
                ["Plugins.HomePage.Popup.MainMenu.CourseManagementList"] = "Course Management",
                ["Admin.HomePage.CourseManagement.Added"] = "The new course management has been added successfully.",
                ["Admin.HomePage.CourseManagement.Updated"] = "The course management has been updated successfully.",
                ["Admin.HomePage.CourseManagement.Deleted"] = "The course management has been deleted successfully.",
                ["Plugins.CourseManagement.Popup.Field.Title"] = "Course Management",
                ["Plugins.CourseManagement.Popup.Field.AddNew"] = "Create course management",
                ["Plugins.CourseManagement.Popup.BackToList"] = "Back to list",
                ["Plugins.CourseManagement.Popup.Field.EditCourseManagement"] = "Edit course management",
                ["Plugins.CourseManagement.Popup.Field.Info"] = "Course Management - Info",
                ["Nopguru.Plugin.Widgets.NopGuru.Title"] = "NopGuru",
                ["Plugins.HomePage.Popup.Configuration.Fields.PopupLogoPictureId"] = "Popup Image",
                ["Plugins.HomePage.Popup.Configuration.Fields.PopupLogoPictureId.hint"] = "Please upload popup image.",
                ["plugins.homepage.popup.field.affiliateid"]= "Affiliate Name"
            });

            await base.InstallAsync();
        }

        /// <summary>
        /// Uninstall the plugin
        /// </summary>
        /// <returns>A task that represents the asynchronous operation</returns>
        public override async Task UninstallAsync()
        {
            //settings
            await _settingService.DeleteSettingAsync<HomePagePopupSettings>();

            await _localizationService.DeleteLocaleResourcesAsync("Plugins.HomePage.Popup");

            await base.UninstallAsync();
        }

        #endregion

        #region SiteMenu

        public virtual async Task ManageSiteMapAsync(SiteMapNode rootNode)
        {
            var homePagePopupSettings = await _settingService.LoadSettingAsync<HomePagePopupSettings>();

            var mainMenuItem = new SiteMapNode()
            {
                Title = await _localizationService.GetResourceAsync("Nopguru.Plugin.Widgets.NopGuru.Title"),
                Visible = await Authenticate(),
                IconClass = "fas fa-th-list"
            };
            mainMenuItem.Visible = await Authenticate();

            var rootItem = new SiteMapNode()
            {
                SystemName = "Homepage Popup",
                Title = await _localizationService.GetResourceAsync("Plugins.HomePage.Popup.MainMenu.Title"),
                IconClass = "far fa-dot-circle",
                Visible = true
            };
            mainMenuItem.ChildNodes.Add(rootItem);

            rootItem.ChildNodes.Add(new SiteMapNode()
            {
                SystemName = "Configure",
                Title = await _localizationService.GetResourceAsync("Plugins.HomePage.Popup.MainMenu.Configuration"),
                ControllerName = "HomePagePopup",
                ActionName = "Configure",
                Visible = true,
                IconClass = "fa fa-genderless",
                RouteValues = new RouteValueDictionary() { { "area", "Admin" } },
            });

            rootItem.ChildNodes.Add(new SiteMapNode()
            {
                SystemName = "Homepage Popup",
                Title = await _localizationService.GetResourceAsync("Plugins.HomePage.Popup.MainMenu.ManageList"),
                ControllerName = "HomePagePopup",
                ActionName = "List",
                Visible = true,
                IconClass = "fa fa-genderless",
                RouteValues = new RouteValueDictionary() { { "area", "Admin" } },
            });

            rootItem.ChildNodes.Add(new SiteMapNode()
            {
                SystemName = "Course Management",
                Title = await _localizationService.GetResourceAsync("Plugins.HomePage.Popup.MainMenu.CourseManagementList"),
                ControllerName = "CourseManagement",
                ActionName = "List",
                Visible = true,
                IconClass = "fa fa-genderless",
                RouteValues = new RouteValueDictionary() { { "area", "Admin" } },
            });


            try
            {
                if (homePagePopupSettings.Enable)
                {
                    var title = await _localizationService.GetResourceAsync("Nopguru.Plugin.Widgets.NopGuru.Title");
                    var pluginNode = rootNode.ChildNodes.FirstOrDefault(x => x.Title == title);
                    if (pluginNode != null)
                        pluginNode.ChildNodes.Add(rootItem);
                    else
                        rootNode.ChildNodes.Add(mainMenuItem);
                }
            }
            catch (Exception ex)
            {
                await _logger.InsertLogAsync(LogLevel.Error, ex.Message, ex.InnerException.Message);
            }
        }

        /// <summary>
        /// Athorization
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Authenticate()
        {
            bool flag = false;
            if (await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManagePlugins))
            {
                flag = true;
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        #endregion

        #endregion

        #region Properties

        public bool HideInWidgetList => false;

        #endregion
    }
}
