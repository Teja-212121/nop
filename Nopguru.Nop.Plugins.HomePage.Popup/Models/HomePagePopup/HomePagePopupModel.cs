using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nopguru.Nop.Plugins.HomePage.Popup.Models.HomePagePopup
{
    public record HomePagePopupModel : BaseNopEntityModel
    {
        [NopResourceDisplayName("Plugins.HomePage.Popup.Field.Name")]
        public string Name { get; set; }

        [NopResourceDisplayName("Plugins.HomePage.Popup.Field.ContactNumber")]
        public string ContactNumber { get; set; }

        [NopResourceDisplayName("Plugins.HomePage.Popup.Field.Email")]
        public string Email { get; set; }

        [NopResourceDisplayName("Plugins.HomePage.Popup.Field.Subject")]
        public string Subject { get; set; }

        public int Courses { get; set; }

        [NopResourceDisplayName("Plugins.HomePage.Popup.Field.Courses")]
        public string CourseName { get; set; }

        [NopResourceDisplayName("Plugins.HomePage.Popup.Field.PageName")]
        public string PageName { get; set; }

        [NopResourceDisplayName("Plugins.HomePage.Popup.Field.AffiliateId")]
        public int AffiliateId { get; set; }

        public string AffiliateName { get;set; }
    }
}
