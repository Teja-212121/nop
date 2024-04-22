using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Nop.Core;
using Nop.Core.Domain.Affiliates;
using Nop.Services.Affiliates;
using Nop.Services.Configuration;
using Nop.Services.Customers;
using Nop.Services.Media;
using Nop.Web.Framework.Models;

namespace Nopguru.Nop.Plugins.HomePage.Popup.Filter
{
    public class HomePagePopupFilterAttribute : ActionFilterAttribute
    {
        #region Fields

        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ISettingService _settingService;
        private readonly IStoreContext _storeContext;
        private readonly IPictureService _pictureService;
        private readonly IWorkContext _workContext;
        private readonly IAffiliateService _affiliateService;

        #endregion

        #region Ctor

        public HomePagePopupFilterAttribute(IWebHostEnvironment hostingEnvironment,
            ISettingService settingService,
            IStoreContext storeContext,
            IPictureService pictureService,
            IWorkContext workContext,
            IAffiliateService affiliateService)
        {
            _hostingEnvironment = hostingEnvironment;
            _settingService = settingService;
            _storeContext = storeContext;
            _pictureService = pictureService;
            _workContext = workContext;
            _affiliateService = affiliateService;
        }

        #endregion

        #region Utilities



        #endregion

        #region Method

        public override async void OnResultExecuted(ResultExecutedContext context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            try
            {
                var storeId = _storeContext.GetActiveStoreScopeConfigurationAsync().Result;
                var homePagePopupSettings = _settingService.LoadSettingAsync<HomePagePopupSettings>(storeId).Result;

                if (homePagePopupSettings.Enable)
                {
                    var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
                    var actionName = actionDescriptor?.ActionName;
                    var controllerName = actionDescriptor?.ControllerName;

                    var logo = string.Empty;
                    var logoPictureId = homePagePopupSettings.PopupLogoPictureId;

                    if (logoPictureId > 0)
                        logo = _pictureService.GetPictureUrlAsync(logoPictureId, showDefaultPicture: false).Result;

                    var customer = await _workContext.GetCurrentCustomerAsync();
                    var affiliate = await _affiliateService.GetAffiliateByIdAsync(customer.AffiliateId);

                    if (homePagePopupSettings.HomePage && actionName == "Index" && controllerName == "Home")
                    {
                        string homePagePopUp = string.Empty;
                        string homePageJqueryUI = string.Empty;
                        string homepagePopupScript = string.Empty;

                        // get auto ask popup view page
                        string path = _hostingEnvironment.ContentRootPath + "\\Plugins\\HomePage.Popup\\Views\\HomePagePopupFrontEnd\\HomePagePopup.cshtml";
                        if (File.Exists(path))
                        {
                            homePagePopUp = File.ReadAllText(path);
                        }

                        //added external js
                        string jqueryUIFile = _hostingEnvironment.ContentRootPath + "\\Plugins\\HomePage.Popup\\Script\\jquery-ui.js";
                        if (jqueryUIFile != null)
                        {
                            using (StreamReader reader = new StreamReader(jqueryUIFile))
                            {
                                homePageJqueryUI = reader.ReadToEnd();
                            }
                        }
                        else
                        {
                            homePageJqueryUI = string.Empty;
                        }


                        // get auto ask popup script file
                        string homepagePopUpScriptFile = _hostingEnvironment.ContentRootPath + "\\Plugins\\HomePage.Popup\\Script\\HomePagePopup.js";
                        if (homepagePopUpScriptFile != null)
                        {
                            using (StreamReader reader = new StreamReader(homepagePopUpScriptFile))
                            {
                                homepagePopupScript = reader.ReadToEnd();
                            }
                        }
                        else
                        {
                            homepagePopupScript = string.Empty;
                        }

                        homePagePopUp = homePagePopUp.Replace(System.Environment.NewLine, "").Replace("\"", "'");

                        StringBuilder sb = new StringBuilder();

                        sb.Append("<script type=\"text/javascript\">\n\t");
                        sb.Append(" $(\"" + homePagePopUp + "\").insertAfter(\".store-search-box\");");
                        sb.Append("$('.popup_logo').html(\"" + "<img src='" + logo + "' class='site-logo'/>" + "\");");
                        if (affiliate != null)
                            sb.Append("$('.affiliateId').html(\"" + "<input type='text' id='affiliateId' name='affiliateId' value='" + affiliate.Id + "' style='display:none;'/>" + "\");");
                        else
                            sb.Append("$('.affiliateId').html(\"" + "<input type='text' id='affiliateId' name='affiliateId' value='0' style='display:none;'/>" + "\");");
                        sb.Append(homePageJqueryUI);
                        sb.Append(homepagePopupScript);
                        sb.Append("</script>\n");

                        var bytes = Encoding.UTF8.GetBytes(sb.ToString());

                        context.HttpContext.Response.Body.Write(bytes, 0, bytes.Length);
                    }

                    if (homePagePopupSettings.LoginPage && actionName == "Login" && controllerName == "Customer")
                    {
                        string homePagePopUp = string.Empty;
                        string homePageJqueryUI = string.Empty;
                        string homepagePopupScript = string.Empty;

                        // get auto ask popup view page
                        string path = _hostingEnvironment.ContentRootPath + "\\Plugins\\HomePage.Popup\\Views\\HomePagePopupFrontEnd\\HomePagePopup.cshtml";
                        if (File.Exists(path))
                        {
                            homePagePopUp = File.ReadAllText(path);
                        }

                        //added external js
                        string jqueryUIFile = _hostingEnvironment.ContentRootPath + "\\Plugins\\HomePage.Popup\\Script\\jquery-ui.js";
                        if (jqueryUIFile != null)
                        {
                            using (StreamReader reader = new StreamReader(jqueryUIFile))
                            {
                                homePageJqueryUI = reader.ReadToEnd();
                            }
                        }
                        else
                        {
                            homePageJqueryUI = string.Empty;
                        }


                        // get auto ask popup script file
                        string homepagePopUpScriptFile = _hostingEnvironment.ContentRootPath + "\\Plugins\\HomePage.Popup\\Script\\HomePagePopup.js";
                        if (homepagePopUpScriptFile != null)
                        {
                            using (StreamReader reader = new StreamReader(homepagePopUpScriptFile))
                            {
                                homepagePopupScript = reader.ReadToEnd();
                            }
                        }
                        else
                        {
                            homepagePopupScript = string.Empty;
                        }

                        homePagePopUp = homePagePopUp.Replace(System.Environment.NewLine, "").Replace("\"", "'");

                        StringBuilder sb = new StringBuilder();

                        sb.Append("<script type=\"text/javascript\">\n\t");
                        sb.Append(" $(\"" + homePagePopUp + "\").insertAfter(\".store-search-box\");");
                        sb.Append("$('.popup_logo').html(\"" + "<img src='" + logo + "' class='site-logo'/>" + "\");");
                        if (affiliate != null)
                            sb.Append("$('.affiliateId').html(\"" + "<input type='text' id='affiliateId' name='affiliateId' value='" + affiliate.Id + "' style='display:none;'/>" + "\");");
                        else
                            sb.Append("$('.affiliateId').html(\"" + "<input type='text' id='affiliateId' name='affiliateId' value='0' style='display:none;'/>" + "\");");
                        sb.Append(homePageJqueryUI);
                        sb.Append(homepagePopupScript);
                        sb.Append("</script>\n");

                        var bytes = Encoding.UTF8.GetBytes(sb.ToString());

                        context.HttpContext.Response.Body.Write(bytes, 0, bytes.Length);
                    }

                    if (homePagePopupSettings.RegisterPage && actionName == "Register" && controllerName == "Customer")
                    {
                        string homePagePopUp = string.Empty;
                        string homePageJqueryUI = string.Empty;
                        string homepagePopupScript = string.Empty;

                        // get auto ask popup view page
                        string path = _hostingEnvironment.ContentRootPath + "\\Plugins\\HomePage.Popup\\Views\\HomePagePopupFrontEnd\\HomePagePopup.cshtml";
                        if (File.Exists(path))
                        {
                            homePagePopUp = File.ReadAllText(path);
                        }

                        //added external js
                        string jqueryUIFile = _hostingEnvironment.ContentRootPath + "\\Plugins\\HomePage.Popup\\Script\\jquery-ui.js";
                        if (jqueryUIFile != null)
                        {
                            using (StreamReader reader = new StreamReader(jqueryUIFile))
                            {
                                homePageJqueryUI = reader.ReadToEnd();
                            }
                        }
                        else
                        {
                            homePageJqueryUI = string.Empty;
                        }


                        // get auto ask popup script file
                        string homepagePopUpScriptFile = _hostingEnvironment.ContentRootPath + "\\Plugins\\HomePage.Popup\\Script\\HomePagePopup.js";
                        if (homepagePopUpScriptFile != null)
                        {
                            using (StreamReader reader = new StreamReader(homepagePopUpScriptFile))
                            {
                                homepagePopupScript = reader.ReadToEnd();
                            }
                        }
                        else
                        {
                            homepagePopupScript = string.Empty;
                        }

                        homePagePopUp = homePagePopUp.Replace(System.Environment.NewLine, "").Replace("\"", "'");

                        StringBuilder sb = new StringBuilder();

                        sb.Append("<script type=\"text/javascript\">\n\t");
                        sb.Append(" $(\"" + homePagePopUp + "\").insertAfter(\".store-search-box\");");
                        sb.Append("$('.popup_logo').html(\"" + "<img src='" + logo + "' class='site-logo'/>" + "\");");
                        if (affiliate != null)
                            sb.Append("$('.affiliateId').html(\"" + "<input type='text' id='affiliateId' name='affiliateId' value='" + affiliate.Id + "' style='display:none;'/>" + "\");");
                        else
                            sb.Append("$('.affiliateId').html(\"" + "<input type='text' id='affiliateId' name='affiliateId' value='0' style='display:none;'/>" + "\");");
                        sb.Append(homePageJqueryUI);
                        sb.Append(homepagePopupScript);
                        sb.Append("</script>\n");

                        var bytes = Encoding.UTF8.GetBytes(sb.ToString());

                        context.HttpContext.Response.Body.Write(bytes, 0, bytes.Length);
                    }

                    if (homePagePopupSettings.CategoryPage && actionName == "Category" && controllerName == "Catalog")
                    {
                        string homePagePopUp = string.Empty;
                        string homePageJqueryUI = string.Empty;
                        string homepagePopupScript = string.Empty;

                        // get auto ask popup view page
                        string path = _hostingEnvironment.ContentRootPath + "\\Plugins\\HomePage.Popup\\Views\\HomePagePopupFrontEnd\\HomePagePopup.cshtml";
                        if (File.Exists(path))
                        {
                            homePagePopUp = File.ReadAllText(path);
                        }

                        //added external js
                        string jqueryUIFile = _hostingEnvironment.ContentRootPath + "\\Plugins\\HomePage.Popup\\Script\\jquery-ui.js";
                        if (jqueryUIFile != null)
                        {
                            using (StreamReader reader = new StreamReader(jqueryUIFile))
                            {
                                homePageJqueryUI = reader.ReadToEnd();
                            }
                        }
                        else
                        {
                            homePageJqueryUI = string.Empty;
                        }


                        // get auto ask popup script file
                        string homepagePopUpScriptFile = _hostingEnvironment.ContentRootPath + "\\Plugins\\HomePage.Popup\\Script\\HomePagePopup.js";
                        if (homepagePopUpScriptFile != null)
                        {
                            using (StreamReader reader = new StreamReader(homepagePopUpScriptFile))
                            {
                                homepagePopupScript = reader.ReadToEnd();
                            }
                        }
                        else
                        {
                            homepagePopupScript = string.Empty;
                        }

                        homePagePopUp = homePagePopUp.Replace(System.Environment.NewLine, "").Replace("\"", "'");

                        StringBuilder sb = new StringBuilder();

                        sb.Append("<script type=\"text/javascript\">\n\t");
                        sb.Append(" $(\"" + homePagePopUp + "\").insertAfter(\".store-search-box\");");
                        sb.Append("$('.popup_logo').html(\"" + "<img src='" + logo + "' class='site-logo'/>" + "\");");
                        if (affiliate != null)
                            sb.Append("$('.affiliateId').html(\"" + "<input type='text' id='affiliateId' name='affiliateId' value='" + affiliate.Id + "' style='display:none;'/>" + "\");");
                        else
                            sb.Append("$('.affiliateId').html(\"" + "<input type='text' id='affiliateId' name='affiliateId' value='0' style='display:none;'/>" + "\");");
                        sb.Append(homePageJqueryUI);
                        sb.Append(homepagePopupScript);
                        sb.Append("</script>\n");

                        var bytes = Encoding.UTF8.GetBytes(sb.ToString());

                        context.HttpContext.Response.Body.Write(bytes, 0, bytes.Length);
                    }

                    if (homePagePopupSettings.ProductDetailPage && actionName == "ProductDetails" && controllerName == "Product")
                    {
                        var productId = ((BaseNopEntityModel)((ViewResult)context.Result).Model).Id;

                        if (homePagePopupSettings.SpecificProductPage && homePagePopupSettings.SpecificProductPageIds.Contains(productId))
                        {
                            string homePagePopUp = string.Empty;
                            string homePageJqueryUI = string.Empty;
                            string homepagePopupScript = string.Empty;

                            // get auto ask popup view page
                            string path = _hostingEnvironment.ContentRootPath + "\\Plugins\\HomePage.Popup\\Views\\HomePagePopupFrontEnd\\HomePagePopup.cshtml";
                            if (File.Exists(path))
                            {
                                homePagePopUp = File.ReadAllText(path);
                            }

                            //added external js
                            string jqueryUIFile = _hostingEnvironment.ContentRootPath + "\\Plugins\\HomePage.Popup\\Script\\jquery-ui.js";
                            if (jqueryUIFile != null)
                            {
                                using (StreamReader reader = new StreamReader(jqueryUIFile))
                                {
                                    homePageJqueryUI = reader.ReadToEnd();
                                }
                            }
                            else
                            {
                                homePageJqueryUI = string.Empty;
                            }


                            // get auto ask popup script file
                            string homepagePopUpScriptFile = _hostingEnvironment.ContentRootPath + "\\Plugins\\HomePage.Popup\\Script\\HomePagePopup.js";
                            if (homepagePopUpScriptFile != null)
                            {
                                using (StreamReader reader = new StreamReader(homepagePopUpScriptFile))
                                {
                                    homepagePopupScript = reader.ReadToEnd();
                                }
                            }
                            else
                            {
                                homepagePopupScript = string.Empty;
                            }

                            homePagePopUp = homePagePopUp.Replace(System.Environment.NewLine, "").Replace("\"", "'");

                            StringBuilder sb = new StringBuilder();

                            sb.Append("<script type=\"text/javascript\">\n\t");
                            sb.Append(" $(\"" + homePagePopUp + "\").insertAfter(\".store-search-box\");");
                            sb.Append("$('.popup_logo').html(\"" + "<img src='" + logo + "' class='site-logo'/>" + "\");");
                            if (affiliate != null)
                                sb.Append("$('.affiliateId').html(\"" + "<input type='text' id='affiliateId' name='affiliateId' value='" + affiliate.Id + "' style='display:none;'/>" + "\");");
                            else
                                sb.Append("$('.affiliateId').html(\"" + "<input type='text' id='affiliateId' name='affiliateId' value='0' style='display:none;'/>" + "\");");
                            sb.Append(homePageJqueryUI);
                            sb.Append(homepagePopupScript);
                            sb.Append("</script>\n");

                            var bytes = Encoding.UTF8.GetBytes(sb.ToString());

                            context.HttpContext.Response.Body.Write(bytes, 0, bytes.Length);
                        }
                        else if (homePagePopupSettings.AllProductPage)
                        {
                            var homePagePopUp = string.Empty;
                            var homePageJqueryUI = string.Empty;
                            var homepagePopupScript = string.Empty;

                            // get auto ask popup view page
                            string path = _hostingEnvironment.ContentRootPath + "\\Plugins\\HomePage.Popup\\Views\\HomePagePopupFrontEnd\\HomePagePopup.cshtml";
                            if (File.Exists(path))
                            {
                                homePagePopUp = File.ReadAllText(path);
                            }

                            //added external js
                            string jqueryUIFile = _hostingEnvironment.ContentRootPath + "\\Plugins\\HomePage.Popup\\Script\\jquery-ui.js";
                            if (jqueryUIFile != null)
                            {
                                using (StreamReader reader = new StreamReader(jqueryUIFile))
                                {
                                    homePageJqueryUI = reader.ReadToEnd();
                                }
                            }
                            else
                            {
                                homePageJqueryUI = string.Empty;
                            }


                            // get auto ask popup script file
                            string homepagePopUpScriptFile = _hostingEnvironment.ContentRootPath + "\\Plugins\\HomePage.Popup\\Script\\HomePagePopup.js";
                            if (homepagePopUpScriptFile != null)
                            {
                                using (StreamReader reader = new StreamReader(homepagePopUpScriptFile))
                                {
                                    homepagePopupScript = reader.ReadToEnd();
                                }
                            }
                            else
                            {
                                homepagePopupScript = string.Empty;
                            }

                            homePagePopUp = homePagePopUp.Replace(System.Environment.NewLine, "").Replace("\"", "'");

                            StringBuilder sb = new StringBuilder();

                            sb.Append("<script type=\"text/javascript\">\n\t");
                            sb.Append(" $(\"" + homePagePopUp + "\").insertAfter(\".store-search-box\");");
                            sb.Append("$('.popup_logo').html(\"" + "<img src='" + logo + "' class='site-logo'/>" + "\");");
                            if (affiliate != null)
                                sb.Append("$('.affiliateId').html(\"" + "<input type='text' id='affiliateId' name='affiliateId' value='" + affiliate.Id + "' style='display:none;'/>" + "\");");
                            else
                                sb.Append("$('.affiliateId').html(\"" + "<input type='text' id='affiliateId' name='affiliateId' value='0' style='display:none;'/>" + "\");");
                            sb.Append(homePageJqueryUI);
                            sb.Append(homepagePopupScript);
                            sb.Append("</script>\n");

                            var bytes = Encoding.UTF8.GetBytes(sb.ToString());

                            context.HttpContext.Response.Body.Write(bytes, 0, bytes.Length);
                        }
                    }

                    if (homePagePopupSettings.SearchPage && actionName == "Search" && controllerName == "Catalog")
                    {
                        string homePagePopUp = string.Empty;
                        string homePageJqueryUI = string.Empty;
                        string homepagePopupScript = string.Empty;

                        // get auto ask popup view page
                        string path = _hostingEnvironment.ContentRootPath + "\\Plugins\\HomePage.Popup\\Views\\HomePagePopupFrontEnd\\HomePagePopup.cshtml";
                        if (File.Exists(path))
                        {
                            homePagePopUp = File.ReadAllText(path);
                        }

                        //added external js
                        string jqueryUIFile = _hostingEnvironment.ContentRootPath + "\\Plugins\\HomePage.Popup\\Script\\jquery-ui.js";
                        if (jqueryUIFile != null)
                        {
                            using (StreamReader reader = new StreamReader(jqueryUIFile))
                            {
                                homePageJqueryUI = reader.ReadToEnd();
                            }
                        }
                        else
                        {
                            homePageJqueryUI = string.Empty;
                        }


                        // get auto ask popup script file
                        string homepagePopUpScriptFile = _hostingEnvironment.ContentRootPath + "\\Plugins\\HomePage.Popup\\Script\\HomePagePopup.js";
                        if (homepagePopUpScriptFile != null)
                        {
                            using (StreamReader reader = new StreamReader(homepagePopUpScriptFile))
                            {
                                homepagePopupScript = reader.ReadToEnd();
                            }
                        }
                        else
                        {
                            homepagePopupScript = string.Empty;
                        }

                        homePagePopUp = homePagePopUp.Replace(System.Environment.NewLine, "").Replace("\"", "'");

                        StringBuilder sb = new StringBuilder();

                        sb.Append("<script type=\"text/javascript\">\n\t");
                        sb.Append(" $(\"" + homePagePopUp + "\").insertAfter(\".store-search-box\");");
                        sb.Append("$('.popup_logo').html(\"" + "<img src='" + logo + "' class='site-logo'/>" + "\");");
                        if (affiliate != null)
                            sb.Append("$('.affiliateId').html(\"" + "<input type='text' id='affiliateId' name='affiliateId' value='" + affiliate.Id + "' style='display:none;'/>" + "\");");
                        else
                            sb.Append("$('.affiliateId').html(\"" + "<input type='text' id='affiliateId' name='affiliateId' value='0' style='display:none;'/>" + "\");");
                        sb.Append(homePageJqueryUI);
                        sb.Append(homepagePopupScript);
                        sb.Append("</script>\n");

                        var bytes = Encoding.UTF8.GetBytes(sb.ToString());

                        context.HttpContext.Response.Body.Write(bytes, 0, bytes.Length);
                    }

                    if (homePagePopupSettings.ShoppingCartPage && actionName == "Cart" && controllerName == "ShoppingCart")
                    {
                        string homePagePopUp = string.Empty;
                        string homePageJqueryUI = string.Empty;
                        string homepagePopupScript = string.Empty;

                        // get auto ask popup view page
                        string path = _hostingEnvironment.ContentRootPath + "\\Plugins\\HomePage.Popup\\Views\\HomePagePopupFrontEnd\\HomePagePopup.cshtml";
                        if (File.Exists(path))
                        {
                            homePagePopUp = File.ReadAllText(path);
                        }

                        //added external js
                        string jqueryUIFile = _hostingEnvironment.ContentRootPath + "\\Plugins\\HomePage.Popup\\Script\\jquery-ui.js";
                        if (jqueryUIFile != null)
                        {
                            using (StreamReader reader = new StreamReader(jqueryUIFile))
                            {
                                homePageJqueryUI = reader.ReadToEnd();
                            }
                        }
                        else
                        {
                            homePageJqueryUI = string.Empty;
                        }


                        // get auto ask popup script file
                        string homepagePopUpScriptFile = _hostingEnvironment.ContentRootPath + "\\Plugins\\HomePage.Popup\\Script\\HomePagePopup.js";
                        if (homepagePopUpScriptFile != null)
                        {
                            using (StreamReader reader = new StreamReader(homepagePopUpScriptFile))
                            {
                                homepagePopupScript = reader.ReadToEnd();
                            }
                        }
                        else
                        {
                            homepagePopupScript = string.Empty;
                        }

                        homePagePopUp = homePagePopUp.Replace(System.Environment.NewLine, "").Replace("\"", "'");

                        StringBuilder sb = new StringBuilder();

                        sb.Append("<script type=\"text/javascript\">\n\t");
                        sb.Append(" $(\"" + homePagePopUp + "\").insertAfter(\".store-search-box\");");
                        sb.Append("$('.popup_logo').html(\"" + "<img src='" + logo + "' class='site-logo'/>" + "\");");
                        if (affiliate != null)
                            sb.Append("$('.affiliateId').html(\"" + "<input type='text' id='affiliateId' name='affiliateId' value='" + affiliate.Id + "' style='display:none;'/>" + "\");");
                        else
                            sb.Append("$('.affiliateId').html(\"" + "<input type='text' id='affiliateId' name='affiliateId' value='0' style='display:none;'/>" + "\");");
                        sb.Append(homePageJqueryUI);
                        sb.Append(homepagePopupScript);
                        sb.Append("</script>\n");

                        var bytes = Encoding.UTF8.GetBytes(sb.ToString());

                        context.HttpContext.Response.Body.Write(bytes, 0, bytes.Length);
                    }

                    if (homePagePopupSettings.WishListPage && actionName == "Wishlist" && controllerName == "ShoppingCart")
                    {
                        string homePagePopUp = string.Empty;
                        string homePageJqueryUI = string.Empty;
                        string homepagePopupScript = string.Empty;

                        // get auto ask popup view page
                        string path = _hostingEnvironment.ContentRootPath + "\\Plugins\\HomePage.Popup\\Views\\HomePagePopupFrontEnd\\HomePagePopup.cshtml";
                        if (File.Exists(path))
                        {
                            homePagePopUp = File.ReadAllText(path);
                        }

                        //added external js
                        string jqueryUIFile = _hostingEnvironment.ContentRootPath + "\\Plugins\\HomePage.Popup\\Script\\jquery-ui.js";
                        if (jqueryUIFile != null)
                        {
                            using (StreamReader reader = new StreamReader(jqueryUIFile))
                            {
                                homePageJqueryUI = reader.ReadToEnd();
                            }
                        }
                        else
                        {
                            homePageJqueryUI = string.Empty;
                        }


                        // get auto ask popup script file
                        string homepagePopUpScriptFile = _hostingEnvironment.ContentRootPath + "\\Plugins\\HomePage.Popup\\Script\\HomePagePopup.js";
                        if (homepagePopUpScriptFile != null)
                        {
                            using (StreamReader reader = new StreamReader(homepagePopUpScriptFile))
                            {
                                homepagePopupScript = reader.ReadToEnd();
                            }
                        }
                        else
                        {
                            homepagePopupScript = string.Empty;
                        }

                        homePagePopUp = homePagePopUp.Replace(System.Environment.NewLine, "").Replace("\"", "'");

                        StringBuilder sb = new StringBuilder();

                        sb.Append("<script type=\"text/javascript\">\n\t");
                        sb.Append(" $(\"" + homePagePopUp + "\").insertAfter(\".store-search-box\");");
                        sb.Append("$('.popup_logo').html(\"" + "<img src='" + logo + "' class='site-logo'/>" + "\");");
                        if (affiliate != null)
                            sb.Append("$('.affiliateId').html(\"" + "<input type='text' id='affiliateId' name='affiliateId' value='" + affiliate.Id + "' style='display:none;'/>" + "\");");
                        else
                            sb.Append("$('.affiliateId').html(\"" + "<input type='text' id='affiliateId' name='affiliateId' value='0' style='display:none;'/>" + "\");");
                        sb.Append(homePageJqueryUI);
                        sb.Append(homepagePopupScript);
                        sb.Append("</script>\n");

                        var bytes = Encoding.UTF8.GetBytes(sb.ToString());

                        context.HttpContext.Response.Body.Write(bytes, 0, bytes.Length);
                    }

                    if (homePagePopupSettings.CheckoutPage)
                    {
                        if (homePagePopupSettings.OnePageCheckout && actionName == "OnePageCheckout" && controllerName == "Checkout")
                        {
                            string homePagePopUp = string.Empty;
                            string homePageJqueryUI = string.Empty;
                            string homepagePopupScript = string.Empty;

                            // get auto ask popup view page
                            string path = _hostingEnvironment.ContentRootPath + "\\Plugins\\HomePage.Popup\\Views\\HomePagePopupFrontEnd\\HomePagePopup.cshtml";
                            if (File.Exists(path))
                            {
                                homePagePopUp = File.ReadAllText(path);
                            }

                            //added external js
                            string jqueryUIFile = _hostingEnvironment.ContentRootPath + "\\Plugins\\HomePage.Popup\\Script\\jquery-ui.js";
                            if (jqueryUIFile != null)
                            {
                                using (StreamReader reader = new StreamReader(jqueryUIFile))
                                {
                                    homePageJqueryUI = reader.ReadToEnd();
                                }
                            }
                            else
                            {
                                homePageJqueryUI = string.Empty;
                            }


                            // get auto ask popup script file
                            string homepagePopUpScriptFile = _hostingEnvironment.ContentRootPath + "\\Plugins\\HomePage.Popup\\Script\\HomePagePopup.js";
                            if (homepagePopUpScriptFile != null)
                            {
                                using (StreamReader reader = new StreamReader(homepagePopUpScriptFile))
                                {
                                    homepagePopupScript = reader.ReadToEnd();
                                }
                            }
                            else
                            {
                                homepagePopupScript = string.Empty;
                            }

                            homePagePopUp = homePagePopUp.Replace(System.Environment.NewLine, "").Replace("\"", "'");

                            StringBuilder sb = new StringBuilder();

                            sb.Append("<script type=\"text/javascript\">\n\t");
                            sb.Append(" $(\"" + homePagePopUp + "\").insertAfter(\".store-search-box\");");
                            sb.Append("$('.popup_logo').html(\"" + "<img src='" + logo + "' class='site-logo'/>" + "\");");
                            if (affiliate != null)
                                sb.Append("$('.affiliateId').html(\"" + "<input type='text' id='affiliateId' name='affiliateId' value='" + affiliate.Id + "' style='display:none;'/>" + "\");");
                            else
                                sb.Append("$('.affiliateId').html(\"" + "<input type='text' id='affiliateId' name='affiliateId' value='0' style='display:none;'/>" + "\");");
                            sb.Append(homePageJqueryUI);
                            sb.Append(homepagePopupScript);
                            sb.Append("</script>\n");

                            var bytes = Encoding.UTF8.GetBytes(sb.ToString());

                            context.HttpContext.Response.Body.Write(bytes, 0, bytes.Length);
                        }
                        else if (homePagePopupSettings.MultipleCheckout && actionName == "BillingAddress" && controllerName == "Checkout")
                        {
                            var homePagePopUp = string.Empty;
                            string homePageJqueryUI = string.Empty;
                            string homepagePopupScript = string.Empty;

                            // get auto ask popup view page
                            string path = _hostingEnvironment.ContentRootPath + "\\Plugins\\HomePage.Popup\\Views\\HomePagePopupFrontEnd\\HomePagePopup.cshtml";
                            if (File.Exists(path))
                            {
                                homePagePopUp = File.ReadAllText(path);
                            }

                            //added external js
                            string jqueryUIFile = _hostingEnvironment.ContentRootPath + "\\Plugins\\HomePage.Popup\\Script\\jquery-ui.js";
                            if (jqueryUIFile != null)
                            {
                                using (StreamReader reader = new StreamReader(jqueryUIFile))
                                {
                                    homePageJqueryUI = reader.ReadToEnd();
                                }
                            }
                            else
                            {
                                homePageJqueryUI = string.Empty;
                            }


                            // get auto ask popup script file
                            string homepagePopUpScriptFile = _hostingEnvironment.ContentRootPath + "\\Plugins\\HomePage.Popup\\Script\\HomePagePopup.js";
                            if (homepagePopUpScriptFile != null)
                            {
                                using (StreamReader reader = new StreamReader(homepagePopUpScriptFile))
                                {
                                    homepagePopupScript = reader.ReadToEnd();
                                }
                            }
                            else
                            {
                                homepagePopupScript = string.Empty;
                            }

                            homePagePopUp = homePagePopUp.Replace(System.Environment.NewLine, "").Replace("\"", "'");

                            StringBuilder sb = new StringBuilder();

                            sb.Append("<script type=\"text/javascript\">\n\t");
                            sb.Append(" $(\"" + homePagePopUp + "\").insertAfter(\".store-search-box\");");
                            sb.Append("$('.popup_logo').html(\"" + "<img src='" + logo + "' class='site-logo'/>" + "\");");
                            if (affiliate != null)
                                sb.Append("$('.affiliateId').html(\"" + "<input type='text' id='affiliateId' name='affiliateId' value='" + affiliate.Id + "' style='display:none;'/>" + "\");");
                            else
                                sb.Append("$('.affiliateId').html(\"" + "<input type='text' id='affiliateId' name='affiliateId' value='0' style='display:none;'/>" + "\");");
                            sb.Append(homePageJqueryUI);
                            sb.Append(homepagePopupScript);
                            sb.Append("</script>\n");

                            var bytes = Encoding.UTF8.GetBytes(sb.ToString());

                            context.HttpContext.Response.Body.Write(bytes, 0, bytes.Length);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion
    }
}
