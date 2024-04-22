using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core;

namespace Nopguru.Nop.Plugins.HomePage.Popup.Domains
{
    public class CourseManagement : BaseEntity
    {
        public string Name { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; }
    }
}
