using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using BusinessLogic;
using BusinessLogic.Interface;
using BusinessObject;
using BusinessObject.Component;
using BusinessObject.ViewModel;
using DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UniSport.Areas.Admin.Controllers;
using UniSport.Models;

namespace UniNetworkWeb.Controllers
{
    public class JoinController : BaseController
    {
        #region global field
        private IEntityViewLogic<MemberStagingViewModel> _entityBL;
        private readonly IMessageTemplateBL _messageTemplateBL;
        #endregion
        #region Constructor
        public JoinController(UserManager<ApplicationUser> userManager,
                    IEntityViewLogic<MemberStagingViewModel> entityBL,
                    IMessageTemplateBL messageTemplateBL)
                    : base(userManager)
        {
            _entityBL = entityBL;
            _messageTemplateBL = messageTemplateBL;
        }
        #endregion

        #region methods
        public async Task<IActionResult> Index(int? Id)
        {
            MemberStagingViewModel model = await _entityBL.GetEntityById(Id);
            return View(model);
        }


        public IActionResult Join(MemberStagingViewModel viewModel)
        {

            viewModel = new MemberStagingViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<JsonResult> SaveModel([FromForm]MemberStagingViewModel viewModel)
        {

            SaveResult resultSet = new SaveResult();

            if (viewModel.IDTypeId == 1)
            {
                ModelState.Remove("AlternateIDNumber");
            }
            else
            {
                ModelState.Remove("IDNumber");
            }

            if (ModelState.IsValid)
            {

                Type t = viewModel.GetType();
                ApplicationUser usr = await GetCurrentUserAsync();
                viewModel.SessionUserId = "ca0cdade-73a4-4829-b082-2a3a1a04cce0";// usr.Id;


                viewModel.NotificationRoles = await GetMessageTemplateNotificationUser();

                viewModel.RequestUrl = HtmlEncoder.Default.Encode(string.Format("{0}/Admin/MemberApplication", Request.Host));



                resultSet = await _entityBL.SaveEntity(viewModel);
            }
            else
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        resultSet.Message = error.ErrorMessage;
                    }
                }
            }
            return Json(resultSet);
        }


        public async Task<IEnumerable<UserViewModel>> GetMessageTemplateNotificationUser()
        {

            var messageTemplate = await _messageTemplateBL.GetEntityByName(MessageTemplateConst.RegistrationNotification);

            List<ApplicationUser> notificationRoles = new List<ApplicationUser>();
            if (messageTemplate.ApplicationRoleMessageTemplates.Any())
            {
                foreach (var item in messageTemplate.ApplicationRoleMessageTemplates)
                {
                    notificationRoles.AddRange(await GetUserFromRoleAsync(item.ApplicationRole.Name));
                }

            }

            return notificationRoles.Select(a => new UserViewModel
            {
                Id = a.Id,
                Email = a.Email,
                FirstName = a.FirstName,
                Surname = a.Surname,
                FullName = a.FullName
            }).GroupBy(a => new { a.Id, a.Email, a.FirstName, a.Surname, a.FullName })
                     .Select(a => new UserViewModel
                     {
                         Id = a.Key.Id,
                         Email = a.Key.Email,
                         FirstName = a.Key.FirstName,
                         Surname = a.Key.Surname,
                         FullName = a.Key.FullName
                     });


        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #endregion
    }
}
