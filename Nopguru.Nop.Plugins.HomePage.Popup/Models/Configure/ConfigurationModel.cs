using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nopguru.Nop.Plugins.HomePage.Popup.Models.Configure
{
    public record ConfigurationModel : BaseNopModel
    {
        public ConfigurationModel()
        {
            SpecificProductPageIds = new List<int>();
            AvailableProducts = new List<SelectListItem>();
        }

        [NopResourceDisplayName("Plugins.HomePage.Popup.Configuration.Fields.Enable")]
        public bool Enable { get; set; }

        [NopResourceDisplayName("Plugins.HomePage.Popup.Configuration.Fields.HomePage")]
        public bool HomePage { get; set; }

        [NopResourceDisplayName("Plugins.HomePage.Popup.Configuration.Fields.LoginPage")]
        public bool LoginPage { get; set; }

        [NopResourceDisplayName("Plugins.HomePage.Popup.Configuration.Fields.RegisterPage")]
        public bool RegisterPage { get; set; }

        [NopResourceDisplayName("Plugins.HomePage.Popup.Configuration.Fields.CategoryPage")]
        public bool CategoryPage { get; set; }

        [NopResourceDisplayName("Plugins.HomePage.Popup.Configuration.Fields.ProductDetailPage")]
        public bool ProductDetailPage { get; set; }

        [NopResourceDisplayName("Plugins.HomePage.Popup.Configuration.Fields.AllProductPage")]
        public bool AllProductPage { get; set; }

        [NopResourceDisplayName("Plugins.HomePage.Popup.Configuration.Fields.SpecificProductPage")]
        public bool SpecificProductPage { get; set; }

        [NopResourceDisplayName("Plugins.HomePage.Popup.Configuration.Fields.SpecificProductPageIds")]
        public IList<int> SpecificProductPageIds { get; set; }
        public IList<SelectListItem> AvailableProducts { get; set; }

        [NopResourceDisplayName("Plugins.HomePage.Popup.Configuration.Fields.SearchPage")]
        public bool SearchPage { get; set; }

        [NopResourceDisplayName("Plugins.HomePage.Popup.Configuration.Fields.ShoppingCartPage")]
        public bool ShoppingCartPage { get; set; }

        [NopResourceDisplayName("Plugins.HomePage.Popup.Configuration.Fields.WishListPage")]
        public bool WishListPage { get; set; }

        [NopResourceDisplayName("Plugins.HomePage.Popup.Configuration.Fields.CheckoutPage")]
        public bool CheckoutPage { get; set; }

        [NopResourceDisplayName("Plugins.HomePage.Popup.Configuration.Fields.OnePageCheckout")]
        public bool OnePageCheckout { get; set; }

        [NopResourceDisplayName("Plugins.HomePage.Popup.Configuration.Fields.MultipleCheckout")]
        public bool MultipleCheckout { get; set; }

        [UIHint("Picture")]
        [NopResourceDisplayName("Plugins.HomePage.Popup.Configuration.Fields.PopupLogoPictureId")]
        public int PopupLogoPictureId { get; set; }
    }
}
