using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Models.Extensions;
using Nop.Web.Framework.Mvc.Filters;
using Nopguru.Nop.Plugins.HomePage.Popup.Domains;
using Nopguru.Nop.Plugins.HomePage.Popup.Models.CourseManagement;
using Nopguru.Nop.Plugins.HomePage.Popup.Services;

namespace Nopguru.Nop.Plugins.HomePage.Popup.Controllers
{
    [AuthorizeAdmin]
    [Area(AreaNames.Admin)]
    [AutoValidateAntiforgeryToken]
    public class CourseManagementController : BasePluginController
    {
        #region Fields

        private readonly ILocalizationService _localizationService;
        private readonly ICourseManagementService _courseManagementService;
        private readonly IPermissionService _permissionService;
        private readonly INotificationService _notificationService;

        #endregion

        #region Ctor

        public CourseManagementController(ILocalizationService localizationService,
            IPermissionService permissionService,
            ICourseManagementService courseManagementService,
            INotificationService notificationService)
        {
            _localizationService = localizationService;
            _permissionService = permissionService;
            _courseManagementService = courseManagementService;
            _notificationService = notificationService;
        }

        #endregion

        #region Method

        #region List / Create / Edit / Delete

        public virtual IActionResult Index()
        {
            return RedirectToAction("List");
        }

        public virtual async Task<IActionResult> List()
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            var searchModel = new CourseManagementSearchModel();

            searchModel.SetGridPageSize();

            return View("~/Plugins/HomePage.Popup/Views/CourseManagement/List.cshtml", searchModel);
        }


        [HttpPost]
        public async Task<IActionResult> List(CourseManagementSearchModel searchModel)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            var courseManagements = await _courseManagementService.GetAllCourseManagementList(searchModel.SearchByCourceName, searchModel.SearchByDisplayOrder, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            var model = await new CourseManagementListModel().PrepareToGridAsync(searchModel, courseManagements, () =>
            {
                return courseManagements.SelectAwait(async cm =>
                {
                    var courseManagementModel = new CourseManagementModel();

                    courseManagementModel.Id = cm.Id;
                    courseManagementModel.CourseName = cm.Name;
                    courseManagementModel.DisplayOrder = cm.DisplayOrder;
                    courseManagementModel.IsActive = cm.IsActive;

                    return courseManagementModel;
                });
            });

            return Json(model);
        }

        public virtual async Task<IActionResult> Create()
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            var model = new CourseManagementModel();

            return View("~/Plugins/HomePage.Popup/Views/CourseManagement/Create.cshtml", model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual async Task<IActionResult> Create(CourseManagementModel model, bool continueEditing)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var courseManagement = new CourseManagement();
                courseManagement.Name = model.CourseName;
                courseManagement.DisplayOrder = model.DisplayOrder;
                courseManagement.IsActive = model.IsActive;

                await _courseManagementService.InsertCourseManagementAsync(courseManagement);

                _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.HomePage.CourseManagement.Added"));

                if (!continueEditing)
                    return RedirectToAction("List");

                return RedirectToAction("Edit", new { id = courseManagement.Id });
            }

            return View(model);
        }

        public virtual async Task<IActionResult> Edit(int id)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            var courseManagement = await _courseManagementService.GetCourseManagementByIdAsync(id);
            if (courseManagement == null)
                return RedirectToAction("List");

            var model = new CourseManagementModel();

            model.CourseName = courseManagement.Name;
            model.DisplayOrder = courseManagement.DisplayOrder;
            model.IsActive = courseManagement.IsActive;

            return View("~/Plugins/HomePage.Popup/Views/CourseManagement/Edit.cshtml", model);
        }


        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual async Task<IActionResult> Edit(CourseManagementModel model, bool continueEditing)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            var courseManagement = await _courseManagementService.GetCourseManagementByIdAsync(model.Id);
            if (courseManagement == null)
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {
                courseManagement.Name = model.CourseName;
                courseManagement.DisplayOrder = model.DisplayOrder;
                courseManagement.IsActive = model.IsActive;

                await _courseManagementService.UpdateCourseManagementAsync(courseManagement);

                _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.HomePage.CourseManagement.Updated"));

                if (!continueEditing)
                    return RedirectToAction("List");

                return RedirectToAction("Edit", new { id = courseManagement.Id });
            }

            return View(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Delete(int id)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            var courseManagement = await _courseManagementService.GetCourseManagementByIdAsync(id);
            if (courseManagement == null)
                return RedirectToAction("List");

            await _courseManagementService.DeleteCourseManagementAsync(courseManagement);

            _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.HomePage.CourseManagement.Deleted"));

            return RedirectToAction("List");
        }

        #endregion

        #endregion

    }
}
