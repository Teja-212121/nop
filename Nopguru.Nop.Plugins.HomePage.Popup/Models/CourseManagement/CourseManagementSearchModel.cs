using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nopguru.Nop.Plugins.HomePage.Popup.Models.CourseManagement
{
    public record CourseManagementSearchModel : BaseSearchModel
    {
        [NopResourceDisplayName("Plugins.HomePage.Popup.Field.SearchByCourceName")]
        public string SearchByCourceName { get; set; }

        [NopResourceDisplayName("Plugins.HomePage.Popup.Field.SearchByDisplayOrder")]
        public int SearchByDisplayOrder { get; set; }

        [NopResourceDisplayName("Plugins.HomePage.Popup.Field.SearchByIsActive")]
        public bool SearchByIsActive { get; set; }
    }
}
