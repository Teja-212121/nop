using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core;

namespace Nopguru.Nop.Plugins.HomePage.Popup.Domains
{
    public class HomePagePopup : BaseEntity
    {
        public string Name { get; set; }

        public string ContactNumber { get; set; }

        public string Email { get; set; }

        public string Subject { get; set; }

        public int Courses { get; set; }

        public string PageName { get; set; }

        public int AffiliateId { get; set; }
    }
}
