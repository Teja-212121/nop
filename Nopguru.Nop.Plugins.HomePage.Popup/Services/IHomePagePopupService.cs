using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core;
using Nopguru.Nop.Plugins.HomePage.Popup.Domains;

namespace Nopguru.Nop.Plugins.HomePage.Popup.Services
{
    public partial interface IHomePagePopupService
    {
        Task InsertHomePagePopupAsync(HomePagePopup homePagePopup);

        Task UpdateHomePagePopupAsync(HomePagePopup homePagePopup);

        Task DeleteHomePagePopupAsync(HomePagePopup homePagePopup);

        Task<IPagedList<HomePagePopup>> GetAllHomePagePopupList(string name = null, string contactNumber = null, string email = null, string subject = null, string pageName = null, int pageIndex = 0, int pageSize = int.MaxValue);

        Task<HomePagePopup> GetHomePagePopupByIdAsync(int id);

    }
}
