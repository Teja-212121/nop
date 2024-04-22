using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Configuration;

namespace Nopguru.Nop.Plugins.HomePage.Popup
{
    public class HomePagePopupSettings : ISettings
    {
        public bool Enable { get; set; }

        public bool HomePage { get; set; }

        public bool LoginPage { get; set; }

        public bool RegisterPage { get; set; }

        public bool CategoryPage { get; set; }

        public bool ProductDetailPage { get; set; }

        public bool AllProductPage { get; set; }

        public bool SpecificProductPage { get; set; }

        public List<int> SpecificProductPageIds { get; set; } = new();

        public bool SearchPage { get; set; }

        public bool ShoppingCartPage { get; set; }

        public bool WishListPage { get; set; }

        public bool CheckoutPage { get; set; }

        public bool OnePageCheckout { get; set; }

        public bool MultipleCheckout { get; set; }

        public int PopupLogoPictureId { get; set; }
    }
}
