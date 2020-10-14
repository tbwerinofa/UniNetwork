using BusinessLogic.Interface;
using BusinessObject.Component;
using BusinessObject.ViewModel;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transform;

namespace BusinessLogic.Implementation
{
    public class ApplicationRoleMessageTemplateBL:IApplicationRoleMessageTemplateBL
    {
        #region Global Fields
        protected readonly SqlServerApplicationDbContext _context;
        
        #endregion

        #region Constructors
        public ApplicationRoleMessageTemplateBL(SqlServerApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Methods
        public async Task<SaveResult> SaveEntityList(MessageTemplateViewModel model, MessageTemplate parentEntity)
        {
            SaveResult saveResult = new SaveResult();

            var currentEntities = parentEntity.ApplicationRoleMessageTemplates;

            if (model.RoleIds == null)
            {

                if (currentEntities.Any())
                {
                    var ApplicationRoleMessageTemplateIds = currentEntities.ToList().ToList().Select(b => b.Id);

                    var toDeleteList = _context.ApplicationRoleMessageTemplate.Where(a => ApplicationRoleMessageTemplateIds.Contains(a.Id));

                    _context.ApplicationRoleMessageTemplate.RemoveRange(toDeleteList);
                    await _context.SaveChangesAsync();
                    saveResult.IsSuccess = true;
                }
                else
                {
                    saveResult.IsSuccess = true;
                }
            }
            else
            {

                var regions = _context.ApplicationRole.Where(a => model.RoleIds.Contains(a.Id));

                if (currentEntities.Any())
                {

                    saveResult.IsSuccess = await AddApplicationRoleMessageTemplateWherePreviousExists(model, currentEntities, regions);

                    if (saveResult.IsSuccess)
                    {
                        saveResult = await ManageExistingApplicationRoleMessageTemplate(model, currentEntities, regions);
                    }
                }
                else
                {

                    saveResult.IsSuccess = await AddEntity(model, currentEntities, regions);
                }
            }


            return saveResult;
        }

        private async Task<SaveResult> ManageExistingApplicationRoleMessageTemplate(MessageTemplateViewModel model,
         ICollection<ApplicationRoleMessageTemplate> currentEntityList,
         IQueryable<ApplicationRole> ApplicationRole)
        {
            SaveResult saveResult = new SaveResult
            {
                IsSuccess = true
            };

            List<ApplicationRoleMessageTemplate> assignedApplicationRoleMessageTemplates = new List<ApplicationRoleMessageTemplate>();

            foreach (var record in currentEntityList)
            {
                if (saveResult.IsSuccess)
                {
                    if (!model.RoleIds.Any(a => a == record.ApplicationRoleId))
                    {
                        var deleteApplicationRoleMessageTemplate = await _context.ApplicationRoleMessageTemplate.FindAsync(record.Id);

                        if (saveResult.IsSuccess)
                        {
                            _context.Remove(deleteApplicationRoleMessageTemplate);
                            await _context.SaveChangesAsync();
                            saveResult.IsSuccess = true;
                        }
                    }
                    else
                    {
                        saveResult = await EditEntityAsync(model, record);
                    }
                }
            }
            return saveResult;
        }

        private async Task<bool> AddApplicationRoleMessageTemplateWherePreviousExists(MessageTemplateViewModel model,
            ICollection<ApplicationRoleMessageTemplate> ApplicationRoleMessageTemplates,
            IQueryable<ApplicationRole> ApplicationRoles)
        {
            bool isSaveSuccess = true;
            List<ApplicationRoleMessageTemplate> assignedApplicationRoleMessageTemplates = new List<ApplicationRoleMessageTemplate>();
            foreach (var recordId in model.RoleIds)
            {

                var currentRolePermission = ApplicationRoles.Where(a => a.Id == recordId).FirstOrDefault();
                if (currentRolePermission != null)
                {

                    if (!ApplicationRoleMessageTemplates.Any(a => a.ApplicationRoleId == recordId))
                    {
                        // var currentUserRoleID = currentMenuRoles.First(a => a.ApplicationRole.RoleID == currentRolePermission.RoleID).RegionID;
                        var ApplicationRoleMessageTemplateRepo = new ApplicationRoleMessageTemplate();
                        assignedApplicationRoleMessageTemplates.Add(ApplicationRoleMessageTemplateRepo.ToEntity(recordId, model.Id, model.SessionUserId));
                    }
                }
            }

            if (assignedApplicationRoleMessageTemplates.Count > 0)
            {
                await _context.ApplicationRoleMessageTemplate.AddRangeAsync(assignedApplicationRoleMessageTemplates);
                await _context.SaveChangesAsync();
                isSaveSuccess = true;
            }

            return isSaveSuccess;
        }


        private async Task<SaveResult> EditEntityAsync(MessageTemplateViewModel model,
            ApplicationRoleMessageTemplate record)
        {
            var saveResult = new SaveResult();
            var editApplicationRoleMessageTemplate = await _context.ApplicationRoleMessageTemplate.FindAsync(record.Id);
           editApplicationRoleMessageTemplate.ToEntity(record.ApplicationRoleId, model.Id, model.SessionUserId);

            _context.Update(editApplicationRoleMessageTemplate);
            await _context.SaveChangesAsync();

            saveResult.IsSuccess = true;
            return saveResult;

        }

        private async Task<bool> AddEntity(MessageTemplateViewModel model,
               ICollection<ApplicationRoleMessageTemplate> ApplicationRoleMessageTemplates,
               IQueryable<ApplicationRole> ApplicationRoles)
        {

            bool isSaveSuccess = true;
            List<ApplicationRoleMessageTemplate> ApplicationRoleMessageTemplateList = new List<ApplicationRoleMessageTemplate>();
            foreach (var record in model.RoleIds)
            {
                var currentApplicationRole = ApplicationRoles.Any(a => a.Id == record);
                if (currentApplicationRole)
                {
                    ApplicationRoleMessageTemplate ApplicationRoleMessageTemplate = new ApplicationRoleMessageTemplate();
                    ApplicationRoleMessageTemplateList.Add(ApplicationRoleMessageTemplate.ToEntity(record, model.Id, model.SessionUserId));
                }
            }

            if (ApplicationRoleMessageTemplateList.Count > 0)
            {
                await _context.ApplicationRoleMessageTemplate.AddRangeAsync(ApplicationRoleMessageTemplateList);
                await _context.SaveChangesAsync();
                isSaveSuccess = true;
            }

            return isSaveSuccess;
        }

        #endregion
    }
}
