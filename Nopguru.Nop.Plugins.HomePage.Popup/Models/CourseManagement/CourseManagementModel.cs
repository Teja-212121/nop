using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nopguru.Nop.Plugins.HomePage.Popup.Models.CourseManagement
{
    public record CourseManagementModel : BaseNopEntityModel
    {
        [NopResourceDisplayName("Plugins.HomePage.Popup.Field.CourseName")]
        public string CourseName { get; set; }

        [NopResourceDisplayName("Plugins.HomePage.Popup.Field.DisplayOrder")]
        public int DisplayOrder { get; set; }

        [NopResourceDisplayName("Plugins.HomePage.Popup.Field.IsActive")]
        public bool IsActive { get; set; }
    }
}
