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
    public class RaceResultMappingBL: IRaceResultMappingBL
    {

        protected readonly SqlServerApplicationDbContext _context;

  


        #region Constructors
        public RaceResultMappingBL(SqlServerApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        public async Task<SaveResult> SaveEntityList(RaceResultViewModel model, RaceDistance parentEntity)
        {
            SaveResult saveResult = new SaveResult();


            var currentEntities = parentEntity.RaceResults;

            if (model.MemberIds == null)
            {

                if (currentEntities.Any())
                {
                    var RaceResultIds = currentEntities.ToList().ToList().Select(b => b.Id);

                    var toDeleteList = _context.RaceResult.Where(a => RaceResultIds.Contains(a.Id));

                    _context.RaceResult.RemoveRange(toDeleteList);
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

                var memberList = _context.Member.Include(a=> a.Person)
                    .Where(a => model.MemberIds.Contains(a.Id));


                if (currentEntities.Any())
                {

                    saveResult.IsSuccess = await AddRaceResultWherePreviousExists(model, currentEntities, memberList);

                    if (saveResult.IsSuccess)
                    {
                        saveResult = await ManageExistingRaceResult(model, currentEntities, memberList);
                    }
                }
                else
                {

                    saveResult.IsSuccess = await AddEntity(model, currentEntities, memberList);
                }
            }


            return saveResult;
        }

        private async Task<SaveResult> ManageExistingRaceResult(RaceResultViewModel model,
         ICollection<RaceResult> currentEntityList,
         IQueryable<Member> member)
        {
            SaveResult saveResult = new SaveResult
            {
                IsSuccess = true
            };

            List<RaceResult> assignedRaceResults = new List<RaceResult>();

            foreach (var record in currentEntityList)
            {
                if (saveResult.IsSuccess)
                {
                    if (!model.MemberIds.Any(a => a == record.MemberId))
                    {
                        var deleteRaceResult = await _context.RaceResult.FindAsync(record.Id);

                        if (saveResult.IsSuccess)
                        {
                             _context.Remove(deleteRaceResult);
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

        private async Task<bool> AddRaceResultWherePreviousExists(RaceResultViewModel model,
    ICollection<RaceResult> RaceResults,
    IQueryable<Member> members)
        {
            bool isSaveSuccess = true;
            List<RaceResult> assignedRaceResults = new List<RaceResult>();
            foreach (var recordId in model.MemberIds)
            {

                var currentMember = members.Where(a => a.Id == recordId).FirstOrDefault();
                if (currentMember != null)
                {

                    if (!RaceResults.Any(a => a.MemberId == recordId))
                    {
                        int ageGroupId = await CalculateAge(currentMember, model.RaceDistanceId);
                        var raceResultRepo = new RaceResult();
                        assignedRaceResults.Add(raceResultRepo.ToEntity(recordId, model.RaceDistanceId, model.TimeTaken, model.Position,ageGroupId, model.SessionUserId));
                    }
                }


            }

            if (assignedRaceResults.Count > 0)
            {
                await _context.RaceResult.AddRangeAsync(assignedRaceResults);
                await _context.SaveChangesAsync();
                isSaveSuccess = true;
            }

            return isSaveSuccess;
        }


        private async Task<SaveResult> EditEntityAsync(RaceResultViewModel model, RaceResult record, IQueryable<Member> referralEntityList)
        {

            int ageGroupId = await CalculateAge(referralEntityList.FirstOrDefault(), model.RaceDistanceId);

            var saveResult = new SaveResult();
            var editRaceResult = await _context.RaceResult.FindAsync(record.Id);
            editRaceResult.ToEntity(record.MemberId, model.RaceDistanceId, model.TimeTaken, model.Position, ageGroupId, model.SessionUserId);

            _context.Update(editRaceResult);
            await _context.SaveChangesAsync();

            saveResult.IsSuccess = true;
            return saveResult;

        }

        private async Task<bool> AddEntity(RaceResultViewModel model,
               ICollection<RaceResult> containerEntityList,
               IQueryable<Member> referralEntityList)
        {

            bool isSaveSuccess = true;
            List<RaceResult> RaceResultList = new List<RaceResult>();
            foreach (var record in model.MemberIds)
            {
                var currentReferralEntity = referralEntityList.Any(a => a.Id == record);
                if (currentReferralEntity)
                {
                    int ageGroupId = await CalculateAge(referralEntityList.FirstOrDefault(), model.RaceDistanceId);

                    RaceResult RaceResult = new RaceResult();
                    RaceResultList.Add(RaceResult.ToEntity(record, model.RaceDistanceId,model.TimeTaken,model.Position, ageGroupId,model.SessionUserId));
                }
            }

            if (RaceResultList.Count > 0)
            {
                await _context.RaceResult.AddRangeAsync(RaceResultList);
                await _context.SaveChangesAsync();
                isSaveSuccess = true;
            }

            return isSaveSuccess;
        }

        private async Task<int> CalculateAge(Member memberEntity,int raceDistanceId)
        {
            RaceDistance raceDistanceEntity = await _context.RaceDistance.FindAsync(raceDistanceId);
            if(raceDistanceEntity== null)
            {
                throw new ArgumentNullException("Race Distance not found");
            }

            int age = raceDistanceEntity.EventDate.Year - memberEntity.Person.BirthDate.Year;
            AgeGroup ageGroupEntity = await _context.AgeGroup.FirstOrDefaultAsync(a => a.MinValue < age &&  age <= a.MaxValue);
                 if (ageGroupEntity == null)
            {
                throw new ArgumentNullException("Agre Group not found");
            }
            return ageGroupEntity.Id;
        }
            }
}
