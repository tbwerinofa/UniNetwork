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
    public class TimeTrialDistanceBL : ITimeTrialDistanceBL
    {

        protected readonly SqlServerApplicationDbContext _context;

  


        #region Constructors
        public TimeTrialDistanceBL(SqlServerApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        public async Task<SaveResult> SaveEntityList(TimeTrialViewModel model, TimeTrial parentEntity)
        {
            SaveResult saveResult = new SaveResult();

            var currentEntities = parentEntity.TimeTrialDistances;

            if (model.DistanceIds == null)
            {

                if (currentEntities.Any())
                {
                    var TimeTrialDistanceIds = currentEntities.ToList().ToList().Select(b => b.Id);

                    var toDeleteList = _context.TimeTrialDistance.Where(a => TimeTrialDistanceIds.Contains(a.Id));

                    _context.TimeTrialDistance.RemoveRange(toDeleteList);
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

                var regions = _context.Distance.Where(a => model.DistanceIds.Contains(a.Id));

                if (currentEntities.Any())
                {

                    saveResult.IsSuccess = await AddTimeTrialDistanceWherePreviousExists(model, currentEntities, regions);

                    if (saveResult.IsSuccess)
                    {
                        saveResult = await ManageExistingTimeTrialDistance(model, currentEntities, regions);
                    }
                }
                else
                {

                    saveResult.IsSuccess = await AddEntity(model, currentEntities, regions);
                }
            }


            return saveResult;
        }

        private async Task<SaveResult> ManageExistingTimeTrialDistance(TimeTrialViewModel model,
         ICollection<TimeTrialDistance> currentEntityList,
         IQueryable<Distance> distance)
        {
            SaveResult saveResult = new SaveResult
            {
                IsSuccess = true
            };

            List<TimeTrialDistance> assignedTimeTrialDistances = new List<TimeTrialDistance>();

            foreach (var record in currentEntityList)
            {
                if (saveResult.IsSuccess)
                {
                    if (!model.DistanceIds.Any(a => a == record.DistanceId))
                    {
                        var deleteTimeTrialDistance = await _context.TimeTrialDistance.FindAsync(record.Id);

                        if (saveResult.IsSuccess)
                        {
                            _context.Remove(deleteTimeTrialDistance);
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

        private async Task<bool> AddTimeTrialDistanceWherePreviousExists(TimeTrialViewModel model,
    ICollection<TimeTrialDistance> TimeTrialDistances,
    IQueryable<Distance> distances)
        {
            bool isSaveSuccess = true;
            List<TimeTrialDistance> assignedTimeTrialDistances = new List<TimeTrialDistance>();
            foreach (var recordId in model.DistanceIds)
            {

                var currentReferralEntity = distances.Where(a => a.Id == recordId).FirstOrDefault();
                if (currentReferralEntity != null)
                {

                    if (!TimeTrialDistances.Any(a => a.DistanceId == recordId))
                    {
                        // var currentUserRoleID = currentMenuRoles.First(a => a.Distance.RoleID == currentRolePermission.RoleID).RegionID;
                        var newEntity = new TimeTrialDistance();
                        assignedTimeTrialDistances.Add(newEntity.ToEntity(recordId, model.Id, model.SessionUserId));
                    }
                }


            }


            if (assignedTimeTrialDistances.Count > 0)
            {
                await _context.TimeTrialDistance.AddRangeAsync(assignedTimeTrialDistances);
                await _context.SaveChangesAsync();
                isSaveSuccess = true;
            }

            return isSaveSuccess;
        }


        private async Task<SaveResult> EditEntityAsync(TimeTrialViewModel model, TimeTrialDistance record)
        {
            var saveResult = new SaveResult();
            var editTimeTrialDistance = await _context.TimeTrialDistance.FindAsync(record.Id);
            editTimeTrialDistance.ToEntity(record.DistanceId, model.Id, model.SessionUserId);

            _context.Update(editTimeTrialDistance);
            await _context.SaveChangesAsync();

            saveResult.IsSuccess = true;
            return saveResult;

        }

        private async Task<bool> AddEntity(TimeTrialViewModel model,
               ICollection<TimeTrialDistance> TimeTrialDistances,
               IQueryable<Distance> distances)
        {

            bool isSaveSuccess = true;
            List<TimeTrialDistance> TimeTrialDistanceList = new List<TimeTrialDistance>();
            foreach (var record in model.DistanceIds)
            {
                var currentDistance = distances.Any(a => a.Id == record);
                if (currentDistance)
                {
                    TimeTrialDistance TimeTrialDistance = new TimeTrialDistance();
                    TimeTrialDistanceList.Add(TimeTrialDistance.ToEntity(record, model.Id, model.SessionUserId));
                }
            }

            if (TimeTrialDistanceList.Count > 0)
            {
                await _context.TimeTrialDistance.AddRangeAsync(TimeTrialDistanceList);
                await _context.SaveChangesAsync();
                isSaveSuccess = true;
            }

            return isSaveSuccess;
        }
    }
}
