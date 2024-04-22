using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core;
using Nopguru.Nop.Plugins.HomePage.Popup.Domains;

namespace Nopguru.Nop.Plugins.HomePage.Popup.Services
{
    public partial interface ICourseManagementService
    {
        Task InsertCourseManagementAsync(CourseManagement courseManagement);

        Task UpdateCourseManagementAsync(CourseManagement courseManagement);

        Task DeleteCourseManagementAsync(CourseManagement courseManagement);

        Task<IPagedList<CourseManagement>> GetAllCourseManagementList(string name = null, int displayOrder = 0, int pageIndex = 0, int pageSize = int.MaxValue);

        Task<CourseManagement> GetCourseManagementByIdAsync(int id);
    }
}
