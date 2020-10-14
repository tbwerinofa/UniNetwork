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
    public class TrainingPlanMemberBL : ITrainingPlanMemberBL
    {

        protected readonly SqlServerApplicationDbContext _context;




        #region Constructors
        public TrainingPlanMemberBL(SqlServerApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        public async Task<SaveResult> SaveEntityList(TrainingPlanViewModel model, TrainingPlan parentEntity)
        {
            SaveResult saveResult = new SaveResult();

            //List<int> MemberIds = new List<int>
            //{
            //    model.MemberId
            //};
            //model.MemberIds = MemberIds;

            var currentEntities = parentEntity.TrainingPlanMembers;

            if (model.MemberIds == null)
            {

                if (currentEntities.Any())
                {
                    var TrainingPlanMemberIds = currentEntities.ToList().ToList().Select(b => b.Id);

                    var toDeleteList = _context.TrainingPlanMember.Where(a => TrainingPlanMemberIds.Contains(a.Id));

                    _context.TrainingPlanMember.RemoveRange(toDeleteList);
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

                var regions = _context.Member.Where(a => model.MemberIds.Contains(a.Id));

                if (currentEntities.Any())
                {

                    saveResult.IsSuccess = await AddTrainingPlanMemberWherePreviousExists(model, currentEntities, regions);

                    if (saveResult.IsSuccess)
                    {
                        saveResult = await ManageExistingTrainingPlanMember(model, currentEntities, regions);
                    }
                }
                else
                {

                    saveResult.IsSuccess = await AddEntity(model, currentEntities, regions);
                }
            }


            return saveResult;
        }

        private async Task<SaveResult> ManageExistingTrainingPlanMember(TrainingPlanViewModel model,
         ICollection<TrainingPlanMember> currentEntityList,
         IQueryable<Member> roleAppPage)
        {
            SaveResult saveResult = new SaveResult
            {
                IsSuccess = true
            };

            List<TrainingPlanMember> assignedTrainingPlanMembers = new List<TrainingPlanMember>();

            foreach (var record in currentEntityList)
            {
                if (saveResult.IsSuccess)
                {
                    if (!model.MemberIds.Any(a => a == record.MemberId))
                    {
                        var deleteTrainingPlanMember = await _context.TrainingPlanMember.FindAsync(record.Id);

                        if (saveResult.IsSuccess)
                        {
                            _context.Remove(deleteTrainingPlanMember);
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

        private async Task<bool> AddTrainingPlanMemberWherePreviousExists(TrainingPlanViewModel model,
    ICollection<TrainingPlanMember> TrainingPlanMembers,
    IQueryable<Member> regions)
        {
            bool isSaveSuccess = true;
            List<TrainingPlanMember> assignedTrainingPlanMembers = new List<TrainingPlanMember>();
            foreach (var recordId in model.MemberIds)
            {

                var currentRolePermission = regions.Where(a => a.Id == recordId).FirstOrDefault();
                if (currentRolePermission != null)
                {

                    if (!TrainingPlanMembers.Any(a => a.MemberId == recordId))
                    {
                        // var currentUserRoleID = currentMenuRoles.First(a => a.Member.RoleID == currentRolePermission.RoleID).RegionID;
                        var TrainingPlanMemberRepo = new TrainingPlanMember();
                        assignedTrainingPlanMembers.Add(TrainingPlanMemberRepo.ToEntity(recordId, model.Id, model.SessionUserId));
                    }
                }


            }

            if (assignedTrainingPlanMembers.Count > 0)
            {
                await _context.TrainingPlanMember.AddRangeAsync(assignedTrainingPlanMembers);
                await _context.SaveChangesAsync();
                isSaveSuccess = true;
            }

            return isSaveSuccess;
        }


        private async Task<SaveResult> EditEntityAsync(TrainingPlanViewModel model, TrainingPlanMember record)
        {
            var saveResult = new SaveResult();
            var editTrainingPlanMember = await _context.TrainingPlanMember.FindAsync(record.Id);
            editTrainingPlanMember.ToEntity(record.MemberId, model.Id, model.SessionUserId);

            _context.Update(editTrainingPlanMember);
            await _context.SaveChangesAsync();

            saveResult.IsSuccess = true;
            return saveResult;

        }

        private async Task<bool> AddEntity(TrainingPlanViewModel model,
               ICollection<TrainingPlanMember> TrainingPlanMembers,
               IQueryable<Member> regions)
        {

            bool isSaveSuccess = true;
            List<TrainingPlanMember> TrainingPlanMemberList = new List<TrainingPlanMember>();
            foreach (var record in model.MemberIds)
            {
                var currentRolePermission = regions.Any(a => a.Id == record);
                if (currentRolePermission)
                {
                    TrainingPlanMember TrainingPlanMember = new TrainingPlanMember();
                    TrainingPlanMemberList.Add(TrainingPlanMember.ToEntity(record, model.Id, model.SessionUserId));
                }
            }

            if (TrainingPlanMemberList.Count > 0)
            {
                await _context.TrainingPlanMember.AddRangeAsync(TrainingPlanMemberList);
                await _context.SaveChangesAsync();
                isSaveSuccess = true;
            }

            return isSaveSuccess;
        }
    }
}
