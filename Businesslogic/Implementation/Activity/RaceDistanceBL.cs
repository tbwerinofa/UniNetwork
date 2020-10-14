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
    public class RaceDistanceBL:IRaceDistanceBL
    {

        protected readonly SqlServerApplicationDbContext _context;

  


        #region Constructors
        public RaceDistanceBL(SqlServerApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        public async Task<SaveResult> SaveEntityList(RaceViewModel model, Race parentEntity)
        {
            SaveResult saveResult = new SaveResult();

            var currentEntities = parentEntity.RaceDistances;

            if (model.DistanceIds == null)
            {

                if (currentEntities.Any())
                {
                    var RaceDistanceIds = currentEntities.ToList().ToList().Select(b => b.Id);

                    var toDeleteList = _context.RaceDistance.Where(a => RaceDistanceIds.Contains(a.Id));

                    _context.RaceDistance.RemoveRange(toDeleteList);
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

                    saveResult.IsSuccess = await AddRaceDistanceWherePreviousExists(model, currentEntities, regions);

                    if (saveResult.IsSuccess)
                    {
                        saveResult = await ManageExistingRaceDistance(model, currentEntities, regions);
                    }
                }
                else
                {

                    saveResult.IsSuccess = await AddEntity(model, currentEntities, regions);
                }
            }


            return saveResult;
        }

        private async Task<SaveResult> ManageExistingRaceDistance(RaceViewModel model,
         ICollection<RaceDistance> currentEntityList,
         IQueryable<Distance> distance)
        {
            SaveResult saveResult = new SaveResult
            {
                IsSuccess = true
            };

            List<RaceDistance> assignedRaceDistances = new List<RaceDistance>();

            foreach (var record in currentEntityList)
            {
                if (saveResult.IsSuccess)
                {
                    //if (!model.DistanceIds.Any(a => a == record.DistanceId))
                    //{
                    //    var deleteRaceDistance = await _context.RaceDistance.FindAsync(record.Id);

                    //    if (saveResult.IsSuccess)
                    //    {
                    //         _context.Remove(deleteRaceDistance);
                    //        await _context.SaveChangesAsync();
                    //        saveResult.IsSuccess = true;
                    //    }
                    //}
                    //else
                    //{
                    //    saveResult = await EditEntityAsync(model, record);
                    //}

                    for (int row = 0; row < model.DistanceIds.Count(); row++)
                    {

                        if (model.DistanceIds[row] == record.DistanceId)
                        {

                            if (model.EventDateTimes[row] == null)
                            {
                                var deleteRaceDistance = await _context.RaceDistance.FindAsync(record.Id);

                                if (saveResult.IsSuccess)
                                {
                                    _context.Remove(deleteRaceDistance);
                                    await _context.SaveChangesAsync();
                                    saveResult.IsSuccess = true;
                                }

                            }
                            else
                            {
                                record.EventDate = (DateTime)model.EventDateTimes[row];
                                saveResult = await EditEntityAsync(model, record);
                            }
                        }
                    }

                }
            }
            return saveResult;
        }

        private async Task<bool> AddRaceDistanceWherePreviousExists(RaceViewModel model,
    ICollection<RaceDistance> RaceDistances,
    IQueryable<Distance> distances)
        {
            bool isSaveSuccess = true;
            List<RaceDistance> assignedRaceDistances = new List<RaceDistance>();
            //foreach (var recordId in model.DistanceIds)
            //{

            //    var currentRolePermission = distances.Where(a => a.Id == recordId).FirstOrDefault();
            //    if (currentRolePermission != null)
            //    {

            //        if (!RaceDistances.Any(a => a.DistanceId == recordId))
            //        {
            //            // var currentUserRoleID = currentMenuRoles.First(a => a.Distance.RoleID == currentRolePermission.RoleID).RegionID;
            //            var RaceDistanceRepo = new RaceDistance();
            //            //assignedRaceDistances.Add(RaceDistanceRepo.ToEntity(recordId, model.Id, model.SessionUserId));
            //        }
            //    }


            //}

            for (int row = 0; row < model.DistanceIds.Count(); row++)
            {
                if (model.EventDateTimes[row] != null)
                {
                    var currentDistance = distances.Where(a => a.Id == model.DistanceIds[row]).FirstOrDefault();
                    if (currentDistance !=null)
                    {

                        if (!RaceDistances.Any(a => a.DistanceId == model.DistanceIds[row]))
                        {
                            var RaceDistanceRepo = new RaceDistance();
                            assignedRaceDistances.Add(RaceDistanceRepo.ToEntity((int)model.DistanceIds[row], model.Id, (DateTime)model.EventDateTimes[row], model.SessionUserId));
                        }

                    }
                }
            }


            if (assignedRaceDistances.Count > 0)
            {
                await _context.RaceDistance.AddRangeAsync(assignedRaceDistances);
                await _context.SaveChangesAsync();
                isSaveSuccess = true;
            }

            return isSaveSuccess;
        }


        private async Task<SaveResult> EditEntityAsync(RaceViewModel model, RaceDistance record)
        {
            var saveResult = new SaveResult();
            var editRaceDistance = await _context.RaceDistance.FindAsync(record.Id);
           // editRaceDistance.ToEntity(record.DistanceId, model.Id, model.SessionUserId);

            _context.Update(editRaceDistance);
            await _context.SaveChangesAsync();

            saveResult.IsSuccess = true;
            return saveResult;

        }

        private async Task<bool> AddEntity(RaceViewModel model,
               ICollection<RaceDistance> raceDistances,
               IQueryable<Distance> distances)
        {

            bool isSaveSuccess = true;
            List<RaceDistance> RaceDistanceList = new List<RaceDistance>();
            //foreach (var record in model.DistanceIds)
            //{
            //    var currentDistance = distances.Any(a => a.Id == record);
            //    if (currentDistance)
            //    {
            //        RaceDistance raceDistance = new RaceDistance();
            //        RaceDistanceList.Add(raceDistance.ToEntity(record, model.Id, model.SessionUserId));
            //    }
            //}


            for(int row = 0; row < model.DistanceIds.Count(); row++)
            {
                if (model.EventDateTimes[row] != null)
                {
                    var currentDistance = distances.Any(a => a.Id == model.DistanceIds[row]);
                    if (currentDistance)
                    {
                        RaceDistance raceDistance = new RaceDistance();
                        RaceDistanceList.Add(raceDistance.ToEntity((int)model.DistanceIds[row], model.Id,(DateTime)model.EventDateTimes[row], model.SessionUserId));
                    }
                }
            }


            if (RaceDistanceList.Count > 0)
            {
                await _context.RaceDistance.AddRangeAsync(RaceDistanceList);
                await _context.SaveChangesAsync();
                isSaveSuccess = true;
            }

            return isSaveSuccess;
        }
    }
}
