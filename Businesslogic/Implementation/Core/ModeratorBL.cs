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
    public class ModeratorBL:IModeratorBL
    {

        protected readonly SqlServerApplicationDbContext _context;

  


        #region Constructors
        public ModeratorBL(SqlServerApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        public async Task<SaveResult> SaveEntityList(CalendarViewModel model, Calendar parentEntity)
        {
            SaveResult saveResult = new SaveResult();

            var currentEntities = parentEntity.Moderators;

            if (model.ModeratorIds == null)
            {

                if (currentEntities.Any())
                {
                    var ModeratorIds = currentEntities.ToList().ToList().Select(b => b.Id);

                    var toDeleteList = _context.Moderator.Where(a => ModeratorIds.Contains(a.Id));

                    _context.Moderator.RemoveRange(toDeleteList);
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

                var existingEntityList = _context.Member.Where(a => model.ModeratorIds.Contains(a.Id));

                if (currentEntities.Any())
                {

                    saveResult.IsSuccess = await AddModeratorWherePreviousExists(model, currentEntities, existingEntityList);

                    if (saveResult.IsSuccess)
                    {
                        saveResult = await ManageExistingModerator(model, currentEntities, existingEntityList);
                    }
                }
                else
                {

                    saveResult.IsSuccess = await AddEntity(model, currentEntities, existingEntityList);
                }
            }


            return saveResult;
        }

        private async Task<SaveResult> ManageExistingModerator(CalendarViewModel model,
         ICollection<Moderator> currentEntityList,
         IQueryable<Member> existingEntityList)
        {
            SaveResult saveResult = new SaveResult
            {
                IsSuccess = true
            };

            List<Moderator> assignedModerators = new List<Moderator>();

            foreach (var record in currentEntityList)
            {
                if (saveResult.IsSuccess)
                {
                    if (!model.ModeratorIds.Any(a => a == record.MemberId))
                    {
                        var deleteModerator = await _context.Moderator.FindAsync(record.Id);

                        if (saveResult.IsSuccess)
                        {
                            _context.Remove(deleteModerator);
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

        private async Task<bool> AddModeratorWherePreviousExists(CalendarViewModel model,
    ICollection<Moderator> Moderators,
    IQueryable<Member> existingEntityList)
        {
            bool isSaveSuccess = true;
            List<Moderator> assignedModerators = new List<Moderator>();
            foreach (var recordId in model.ModeratorIds)
            {

                var currentRolePermission = existingEntityList.Where(a => a.Id == recordId).FirstOrDefault();
                if (currentRolePermission != null)
                {

                    if (!Moderators.Any(a => a.MemberId == recordId))
                    {
                        // var currentUserRoleID = currentMenuRoles.First(a => a.Distance.RoleID == currentRolePermission.RoleID).RegionID;
                        var ModeratorRepo = new Moderator();
                        assignedModerators.Add(ModeratorRepo.ToEntity(recordId, model.Id, model.SessionUserId));
                    }
                }


            }


            if (assignedModerators.Count > 0)
            {
                await _context.Moderator.AddRangeAsync(assignedModerators);
                await _context.SaveChangesAsync();
                isSaveSuccess = true;
            }

            return isSaveSuccess;
        }


        private async Task<SaveResult> EditEntityAsync(CalendarViewModel model, Moderator record)
        {
            var saveResult = new SaveResult();
            var editModerator = await _context.Moderator.FindAsync(record.Id);
            editModerator.ToEntity(record.MemberId, model.Id, model.SessionUserId);

            _context.Update(editModerator);
            await _context.SaveChangesAsync();

            saveResult.IsSuccess = true;
            return saveResult;

        }

        private async Task<bool> AddEntity(CalendarViewModel model,
               ICollection<Moderator> Moderators,
               IQueryable<Member> referralEntityList)
        {

            bool isSaveSuccess = true;
            List<Moderator> ModeratorList = new List<Moderator>();
            foreach (var record in model.ModeratorIds)
            {
                var currentReferralEntity = referralEntityList.Any(a => a.Id == record);
                if (currentReferralEntity)
                {
                    Moderator Moderator = new Moderator();
                    ModeratorList.Add(Moderator.ToEntity(record, model.Id, model.SessionUserId));
                }
            }

            if (ModeratorList.Count > 0)
            {
                await _context.Moderator.AddRangeAsync(ModeratorList);
                await _context.SaveChangesAsync();
                isSaveSuccess = true;
            }

            return isSaveSuccess;
        }
    }
}
