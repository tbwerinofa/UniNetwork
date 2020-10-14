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
    public class TrainingPlanDistanceBL : ITrainingPlanDistanceBL
    {

        protected readonly SqlServerApplicationDbContext _context;




        #region Constructors
        public TrainingPlanDistanceBL(SqlServerApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        public async Task<SaveResult> SaveEntityList(TrainingPlanViewModel model, TrainingPlan parentEntity)
        {
            SaveResult saveResult = new SaveResult();

            //List<int> DistanceIds = new List<int>
            //{
            //    model.DistanceId
            //};
            //model.DistanceIds = DistanceIds;

            var currentEntities = parentEntity.TrainingPlanDistances;

            if (model.DistanceIds == null)
            {

                if (currentEntities.Any())
                {
                    var TrainingPlanDistanceIds = currentEntities.ToList().ToList().Select(b => b.Id);

                    var toDeleteList = _context.TrainingPlanDistance.Where(a => TrainingPlanDistanceIds.Contains(a.Id));

                    _context.TrainingPlanDistance.RemoveRange(toDeleteList);
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

                    saveResult.IsSuccess = await AddTrainingPlanDistanceWherePreviousExists(model, currentEntities, regions);

                    if (saveResult.IsSuccess)
                    {
                        saveResult = await ManageExistingTrainingPlanDistance(model, currentEntities, regions);
                    }
                }
                else
                {

                    saveResult.IsSuccess = await AddEntity(model, currentEntities, regions);
                }
            }


            return saveResult;
        }

        private async Task<SaveResult> ManageExistingTrainingPlanDistance(TrainingPlanViewModel model,
         ICollection<TrainingPlanDistance> currentEntityList,
         IQueryable<Distance> roleAppPage)
        {
            SaveResult saveResult = new SaveResult
            {
                IsSuccess = true
            };

            List<TrainingPlanDistance> assignedTrainingPlanDistances = new List<TrainingPlanDistance>();

            foreach (var record in currentEntityList)
            {
                if (saveResult.IsSuccess)
                {
                    if (!model.DistanceIds.Any(a => a == record.DistanceId))
                    {
                        var deleteTrainingPlanDistance = await _context.TrainingPlanDistance.FindAsync(record.Id);

                        if (saveResult.IsSuccess)
                        {
                            _context.Remove(deleteTrainingPlanDistance);
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

        private async Task<bool> AddTrainingPlanDistanceWherePreviousExists(TrainingPlanViewModel model,
    ICollection<TrainingPlanDistance> TrainingPlanDistances,
    IQueryable<Distance> regions)
        {
            bool isSaveSuccess = true;
            List<TrainingPlanDistance> assignedTrainingPlanDistances = new List<TrainingPlanDistance>();
            foreach (var recordId in model.DistanceIds)
            {

                var currentRolePermission = regions.Where(a => a.Id == recordId).FirstOrDefault();
                if (currentRolePermission != null)
                {

                    if (!TrainingPlanDistances.Any(a => a.DistanceId == recordId))
                    {
                        // var currentUserRoleID = currentMenuRoles.First(a => a.Distance.RoleID == currentRolePermission.RoleID).RegionID;
                        var TrainingPlanDistanceRepo = new TrainingPlanDistance();
                        assignedTrainingPlanDistances.Add(TrainingPlanDistanceRepo.ToEntity(recordId, model.Id, model.SessionUserId));
                    }
                }


            }

            if (assignedTrainingPlanDistances.Count > 0)
            {
                await _context.TrainingPlanDistance.AddRangeAsync(assignedTrainingPlanDistances);
                await _context.SaveChangesAsync();
                isSaveSuccess = true;
            }

            return isSaveSuccess;
        }


        private async Task<SaveResult> EditEntityAsync(TrainingPlanViewModel model, TrainingPlanDistance record)
        {
            var saveResult = new SaveResult();
            var editTrainingPlanDistance = await _context.TrainingPlanDistance.FindAsync(record.Id);
            editTrainingPlanDistance.ToEntity(record.DistanceId, model.Id, model.SessionUserId);

            _context.Update(editTrainingPlanDistance);
            await _context.SaveChangesAsync();

            saveResult.IsSuccess = true;
            return saveResult;

        }

        private async Task<bool> AddEntity(TrainingPlanViewModel model,
               ICollection<TrainingPlanDistance> TrainingPlanDistances,
               IQueryable<Distance> regions)
        {

            bool isSaveSuccess = true;
            List<TrainingPlanDistance> TrainingPlanDistanceList = new List<TrainingPlanDistance>();
            foreach (var record in model.DistanceIds)
            {
                var currentRolePermission = regions.Any(a => a.Id == record);
                if (currentRolePermission)
                {
                    TrainingPlanDistance TrainingPlanDistance = new TrainingPlanDistance();
                    TrainingPlanDistanceList.Add(TrainingPlanDistance.ToEntity(record, model.Id, model.SessionUserId));
                }
            }

            if (TrainingPlanDistanceList.Count > 0)
            {
                await _context.TrainingPlanDistance.AddRangeAsync(TrainingPlanDistanceList);
                await _context.SaveChangesAsync();
                isSaveSuccess = true;
            }

            return isSaveSuccess;
        }
    }
}
