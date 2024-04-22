using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;

namespace Nopguru.Nop.Plugins.HomePage.Popup.Models.HomePagePopupFrontEnd
{
    public record HomePagePopupFrontSideModel : BaseNopEntityModel
    {
        public HomePagePopupFrontSideModel()
        {
            AvailableCourses = new List<SelectListItem>();
        }

        public IList<SelectListItem> AvailableCourses { get; set; }
    }
}
