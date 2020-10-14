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
    public class TrainingPlanRaceDefinitionBL : ITrainingPlanRaceDefinitionBL
    {

        protected readonly SqlServerApplicationDbContext _context;




        #region Constructors
        public TrainingPlanRaceDefinitionBL(SqlServerApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        public async Task<SaveResult> SaveEntityList(TrainingPlanViewModel model, TrainingPlan parentEntity)
        {
            SaveResult saveResult = new SaveResult();

            //List<int> RaceDefinitionIds = new List<int>
            //{
            //    model.RaceDefinitionId
            //};
            //model.RaceDefinitionIds = RaceDefinitionIds;

            var currentEntities = parentEntity.TrainingPlanRaceDefinitions;

            if (model.RaceDefinitionIds == null)
            {

                if (currentEntities.Any())
                {
                    var TrainingPlanRaceDefinitionIds = currentEntities.ToList().ToList().Select(b => b.Id);

                    var toDeleteList = _context.TrainingPlanRaceDefinition.Where(a => TrainingPlanRaceDefinitionIds.Contains(a.Id));

                    _context.TrainingPlanRaceDefinition.RemoveRange(toDeleteList);
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

                var regions = _context.RaceDefinition.Where(a => model.RaceDefinitionIds.Contains(a.Id));

                if (currentEntities.Any())
                {

                    saveResult.IsSuccess = await AddTrainingPlanRaceDefinitionWherePreviousExists(model, currentEntities, regions);

                    if (saveResult.IsSuccess)
                    {
                        saveResult = await ManageExistingTrainingPlanRaceDefinition(model, currentEntities, regions);
                    }
                }
                else
                {

                    saveResult.IsSuccess = await AddEntity(model, currentEntities, regions);
                }
            }


            return saveResult;
        }

        private async Task<SaveResult> ManageExistingTrainingPlanRaceDefinition(TrainingPlanViewModel model,
         ICollection<TrainingPlanRaceDefinition> currentEntityList,
         IQueryable<RaceDefinition> roleAppPage)
        {
            SaveResult saveResult = new SaveResult
            {
                IsSuccess = true
            };

            List<TrainingPlanRaceDefinition> assignedTrainingPlanRaceDefinitions = new List<TrainingPlanRaceDefinition>();

            foreach (var record in currentEntityList)
            {
                if (saveResult.IsSuccess)
                {
                    if (!model.RaceDefinitionIds.Any(a => a == record.RaceDefinitionId))
                    {
                        var deleteTrainingPlanRaceDefinition = await _context.TrainingPlanRaceDefinition.FindAsync(record.Id);

                        if (saveResult.IsSuccess)
                        {
                            _context.Remove(deleteTrainingPlanRaceDefinition);
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

        private async Task<bool> AddTrainingPlanRaceDefinitionWherePreviousExists(TrainingPlanViewModel model,
    ICollection<TrainingPlanRaceDefinition> TrainingPlanRaceDefinitions,
    IQueryable<RaceDefinition> regions)
        {
            bool isSaveSuccess = true;
            List<TrainingPlanRaceDefinition> assignedTrainingPlanRaceDefinitions = new List<TrainingPlanRaceDefinition>();
            foreach (var recordId in model.RaceDefinitionIds)
            {

                var currentRolePermission = regions.Where(a => a.Id == recordId).FirstOrDefault();
                if (currentRolePermission != null)
                {

                    if (!TrainingPlanRaceDefinitions.Any(a => a.RaceDefinitionId == recordId))
                    {
                        // var currentUserRoleID = currentMenuRoles.First(a => a.RaceDefinition.RoleID == currentRolePermission.RoleID).RegionID;
                        var TrainingPlanRaceDefinitionRepo = new TrainingPlanRaceDefinition();
                        assignedTrainingPlanRaceDefinitions.Add(TrainingPlanRaceDefinitionRepo.ToEntity(recordId, model.Id, model.SessionUserId));
                    }
                }


            }

            if (assignedTrainingPlanRaceDefinitions.Count > 0)
            {
                await _context.TrainingPlanRaceDefinition.AddRangeAsync(assignedTrainingPlanRaceDefinitions);
                await _context.SaveChangesAsync();
                isSaveSuccess = true;
            }

            return isSaveSuccess;
        }


        private async Task<SaveResult> EditEntityAsync(TrainingPlanViewModel model, TrainingPlanRaceDefinition record)
        {
            var saveResult = new SaveResult();
            var editTrainingPlanRaceDefinition = await _context.TrainingPlanRaceDefinition.FindAsync(record.Id);
            editTrainingPlanRaceDefinition.ToEntity(record.RaceDefinitionId, model.Id, model.SessionUserId);

            _context.Update(editTrainingPlanRaceDefinition);
            await _context.SaveChangesAsync();

            saveResult.IsSuccess = true;
            return saveResult;

        }

        private async Task<bool> AddEntity(TrainingPlanViewModel model,
               ICollection<TrainingPlanRaceDefinition> TrainingPlanRaceDefinitions,
               IQueryable<RaceDefinition> referralEntityList)
        {

            bool isSaveSuccess = true;
            List<TrainingPlanRaceDefinition> TrainingPlanRaceDefinitionList = new List<TrainingPlanRaceDefinition>();
            foreach (var record in model.RaceDefinitionIds)
            {
                var currentRolePermission = referralEntityList.Any(a => a.Id == record);
                if (currentRolePermission)
                {
                    TrainingPlanRaceDefinition TrainingPlanRaceDefinition = new TrainingPlanRaceDefinition();
                    TrainingPlanRaceDefinitionList.Add(TrainingPlanRaceDefinition.ToEntity(record, model.Id, model.SessionUserId));
                }
            }

            if (TrainingPlanRaceDefinitionList.Count > 0)
            {
                await _context.TrainingPlanRaceDefinition.AddRangeAsync(TrainingPlanRaceDefinitionList);
                await _context.SaveChangesAsync();
                isSaveSuccess = true;
            }

            return isSaveSuccess;
        }
    }
}
