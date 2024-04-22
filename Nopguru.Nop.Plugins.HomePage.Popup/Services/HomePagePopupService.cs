using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Data;
using Nopguru.Nop.Plugins.HomePage.Popup.Domains;

namespace Nopguru.Nop.Plugins.HomePage.Popup.Services
{
    public partial class HomePagePopupService : IHomePagePopupService
    {
        #region Fields

        private readonly IRepository<HomePagePopup> _homePagePopupRepository;

        #endregion

        #region Ctor

        public HomePagePopupService(IRepository<HomePagePopup> homePagePopupRepository)
        {
            _homePagePopupRepository = homePagePopupRepository;
        }

        #endregion

        #region Method

        public virtual async Task InsertHomePagePopupAsync(HomePagePopup homePagePopup)
        {
            await _homePagePopupRepository.InsertAsync(homePagePopup, false);
        }

        public virtual async Task UpdateHomePagePopupAsync(HomePagePopup homePagePopup)
        {
            await _homePagePopupRepository.UpdateAsync(homePagePopup, false);
        }

        public virtual async Task DeleteHomePagePopupAsync(HomePagePopup homePagePopup)
        {
            await _homePagePopupRepository.DeleteAsync(homePagePopup, false);
        }

        public virtual async Task<IPagedList<HomePagePopup>> GetAllHomePagePopupList(string name = null, string contactNumber = null, string email = null, string subject = null, string pageName = null, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var homePagePopups = await _homePagePopupRepository.GetAllPagedAsync(query =>
            {
                if (!string.IsNullOrWhiteSpace(name))
                    query = query.Where(c => c.Name.Contains(name));

                if (!string.IsNullOrWhiteSpace(contactNumber))
                    query = query.Where(c => c.ContactNumber.Contains(contactNumber));

                if (!string.IsNullOrWhiteSpace(email))
                    query = query.Where(c => c.Email.Contains(email));

                if (!string.IsNullOrWhiteSpace(subject))
                    query = query.Where(c => c.Subject.Contains(subject));

                if (!string.IsNullOrWhiteSpace(pageName))
                    query = query.Where(c => c.PageName.Contains(pageName));

                return query;
            }, pageIndex, pageSize);

            return homePagePopups;
        }

        public virtual async Task<HomePagePopup> GetHomePagePopupByIdAsync(int id)
        {
            return await _homePagePopupRepository.GetByIdAsync(id);
        }

        #endregion
    }
}
