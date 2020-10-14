using BusinessLogic.Interface;
using BusinessObject.Component;
using BusinessObject.ViewModel;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transform;

namespace BusinessLogic.Implementation
{
    public class TimeTrialResultMappingBL: ITimeTrialResultMappingBL
    {

        protected readonly SqlServerApplicationDbContext _context;

  


        #region Constructors
        public TimeTrialResultMappingBL(SqlServerApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        public async Task<SaveResult> SaveEntityList(TimeTrialResultViewModel model, TimeTrialDistance parentEntity)
        {
            SaveResult saveResult = new SaveResult();


            var currentEntities = parentEntity.TimeTrialResults;

            if (model.MemberIds == null)
            {

                if (currentEntities.Any())
                {
                    var TimeTrialResultIds = currentEntities.ToList().ToList().Select(b => b.Id);

                    var toDeleteList = _context.TimeTrialResult.Where(a => TimeTrialResultIds.Contains(a.Id));

                    _context.TimeTrialResult.RemoveRange(toDeleteList);
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

                var memberList = _context.Member.Include(a=> a.Person.AgeGroup)
                    .Where(a => model.MemberIds.Contains(a.Id));


                if (currentEntities.Any())
                {

                    saveResult.IsSuccess = await AddTimeTrialResultWherePreviousExists(model, currentEntities, memberList);

                    if (saveResult.IsSuccess)
                    {
                        saveResult = await ManageExistingTimeTrialResult(model, currentEntities, memberList);
                    }
                }
                else
                {

                    saveResult.IsSuccess = await AddEntity(model, currentEntities, memberList);
                }
            }


            return saveResult;
        }

        private async Task<SaveResult> ManageExistingTimeTrialResult(TimeTrialResultViewModel model,
         ICollection<TimeTrialResult> currentEntityList,
         IQueryable<Member> member)
        {
            SaveResult saveResult = new SaveResult
            {
                IsSuccess = true
            };

            List<TimeTrialResult> assignedTimeTrialResults = new List<TimeTrialResult>();

            foreach (var record in currentEntityList)
            {
                if (saveResult.IsSuccess)
                {
                    if (!model.MemberIds.Any(a => a == record.MemberId))
                    {
                        var deleteTimeTrialResult = await _context.TimeTrialResult.FindAsync(record.Id);

                        if (saveResult.IsSuccess)
                        {
                             _context.Remove(deleteTimeTrialResult);
                            await _context.SaveChangesAsync();
                            saveResult.IsSuccess = true;
                        }
                    }
                    else
                    {
                        saveResult = await EditEntityAsync(model, record,member);
                    }
                }
            }
            return saveResult;
        }

        private async Task<bool> AddTimeTrialResultWherePreviousExists(TimeTrialResultViewModel model,
    ICollection<TimeTrialResult> TimeTrialResults,
    IQueryable<Member> members)
        {
            bool isSaveSuccess = true;
            List<TimeTrialResult> assignedTimeTrialResults = new List<TimeTrialResult>();
            foreach (var recordId in model.MemberIds)
            {

                var currentMember = members.Where(a => a.Id == recordId).FirstOrDefault();
                if (currentMember != null)
                {

                    if (!TimeTrialResults.Any(a => a.MemberId == recordId))
                    {
                        int ageGroupId = await CalculateAge(currentMember, model.TimeTrialDistanceId);
                        var TimeTrialResultRepo = new TimeTrialResult();
                        assignedTimeTrialResults.Add(TimeTrialResultRepo.ToEntity(recordId, model.TimeTrialDistanceId, model.TimeTaken, model.Position,ageGroupId, model.SessionUserId));
                    }
                }


            }

            if (assignedTimeTrialResults.Count > 0)
            {
                await _context.TimeTrialResult.AddRangeAsync(assignedTimeTrialResults);
                await _context.SaveChangesAsync();
                isSaveSuccess = true;
            }

            return isSaveSuccess;
        }


        private async Task<SaveResult> EditEntityAsync(TimeTrialResultViewModel model, TimeTrialResult record, IQueryable<Member> referralEntityList)
        {

            int ageGroupId = await CalculateAge(referralEntityList.FirstOrDefault(), model.TimeTrialDistanceId);

            var saveResult = new SaveResult();
            var editTimeTrialResult = await _context.TimeTrialResult.FindAsync(record.Id);
            editTimeTrialResult.ToEntity(record.MemberId, model.TimeTrialDistanceId, model.TimeTaken, model.Position, ageGroupId, model.SessionUserId);

            _context.Update(editTimeTrialResult);
            await _context.SaveChangesAsync();

            saveResult.IsSuccess = true;
            return saveResult;

        }

        private async Task<bool> AddEntity(TimeTrialResultViewModel model,
               ICollection<TimeTrialResult> containerEntityList,
               IQueryable<Member> referralEntityList)
        {

            bool isSaveSuccess = true;
            List<TimeTrialResult> TimeTrialResultList = new List<TimeTrialResult>();
            foreach (var record in model.MemberIds)
            {
                var currentReferralEntity = referralEntityList.Any(a => a.Id == record);
                if (currentReferralEntity)
                {
                    int ageGroupId = await CalculateAge(referralEntityList.FirstOrDefault(), model.TimeTrialDistanceId);

                    TimeTrialResult TimeTrialResult = new TimeTrialResult();
                    TimeTrialResultList.Add(TimeTrialResult.ToEntity(record, model.TimeTrialDistanceId,model.TimeTaken,model.Position, ageGroupId,model.SessionUserId));
                }
            }

            if (TimeTrialResultList.Count > 0)
            {
                await _context.TimeTrialResult.AddRangeAsync(TimeTrialResultList);
                await _context.SaveChangesAsync();
                isSaveSuccess = true;
            }

            return isSaveSuccess;
        }

        private async Task<int> CalculateAge(Member memberEntity,int TimeTrialDistanceId)
        {
            TimeTrialDistance TimeTrialDistanceEntity = await _context.TimeTrialDistance.FindAsync(TimeTrialDistanceId);
            if(TimeTrialDistanceEntity== null)
            {
                throw new ArgumentNullException("Race Distance not found");
            }

            int age =  memberEntity.Person.AgeGroup.Id;
            AgeGroup ageGroupEntity = await _context.AgeGroup.FirstOrDefaultAsync(a => a.MinValue < age &&  age <= a.MaxValue);
                 if (ageGroupEntity == null)
            {
                throw new ArgumentNullException("Agre Group not found");
            }
            return ageGroupEntity.Id;
        }
            }
}
