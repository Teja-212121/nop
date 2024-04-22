using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nopguru.Nop.Plugins.HomePage.Popup.Models.HomePagePopup
{
    public record HomePagePopupSearchModel : BaseSearchModel
    {
        [NopResourceDisplayName("Plugins.HomePage.Popup.Field.SearchByName")]
        public string SearchByName { get; set; }

        [NopResourceDisplayName("Plugins.HomePage.Popup.Field.SearchByContactNumber")]
        public string SearchByContactNumber { get; set; }

        [NopResourceDisplayName("Plugins.HomePage.Popup.Field.SearchByEmail")]
        public string SearchByEmail { get; set; }

        [NopResourceDisplayName("Plugins.HomePage.Popup.Field.SearchBySubject")]
        public string SearchBySubject { get; set; }

        [NopResourceDisplayName("Plugins.HomePage.Popup.Field.SearchByPageName")]
        public string SearchByPageName { get; set; }
    }
}
