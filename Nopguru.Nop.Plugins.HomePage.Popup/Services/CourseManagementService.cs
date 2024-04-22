using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Data;
using Nopguru.Nop.Plugins.HomePage.Popup.Domains;

namespace Nopguru.Nop.Plugins.HomePage.Popup.Services
{
    public partial class CourseManagementService : ICourseManagementService
    {
        #region Fields

        private readonly IRepository<CourseManagement> _courseManagementRepository;

        #endregion

        #region Ctor

        public CourseManagementService(IRepository<CourseManagement> courseManagementRepository)
        {
            _courseManagementRepository = courseManagementRepository;
        }

        #endregion

        #region Method

        public virtual async Task InsertCourseManagementAsync(CourseManagement courseManagement)
        {
            await _courseManagementRepository.InsertAsync(courseManagement, false);
        }

        public virtual async Task UpdateCourseManagementAsync(CourseManagement courseManagement)
        {
            await _courseManagementRepository.UpdateAsync(courseManagement, false);
        }

        public virtual async Task DeleteCourseManagementAsync(CourseManagement courseManagement)
        {
            await _courseManagementRepository.DeleteAsync(courseManagement, false);
        }

        public virtual async Task<IPagedList<CourseManagement>> GetAllCourseManagementList(string name = null, int displayOrder = 0, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var courseManagements = await _courseManagementRepository.GetAllPagedAsync(query =>
            {
                if (!string.IsNullOrWhiteSpace(name))
                    query = query.Where(c => c.Name.Contains(name));

                if (displayOrder > 0)
                    query = query.Where(c => c.DisplayOrder == displayOrder);

                return query;
            }, pageIndex, pageSize);

            return courseManagements;
        }

        public virtual async Task<CourseManagement> GetCourseManagementByIdAsync(int id)
        {
            return await _courseManagementRepository.GetByIdAsync(id);
        }

        #endregion
    }
}
