using BusinessLogic.Interface;
using BusinessObject;
using BusinessObject.Component;
using BusinessObject.ViewModel;
using DataAccess;
using DomainObjects;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transform;

namespace BusinessLogic.Implementation
{
    public class TrainingPlanBL : IEntityViewLogic<TrainingPlanViewModel>
    {
        #region Global Fields

        protected readonly SqlServerApplicationDbContext _context;
        protected readonly IFinYearBL _finYearBL;
        protected readonly ITrainingPlanMemberBL _trainingPlanMemberBL;
        protected readonly ITrainingPlanDistanceBL _trainingPlanDistanceBL;
        protected readonly ITrainingPlanRaceDefinitionBL _trainingPlanRaceDefinitionBL;

        #endregion

        #region Constructors
        public TrainingPlanBL(SqlServerApplicationDbContext context,
            IFinYearBL finYearBL,
            ITrainingPlanMemberBL trainingPlanMemberBL,
            ITrainingPlanDistanceBL trainingPlanDistanceBL,
            ITrainingPlanRaceDefinitionBL trainingPlanRaceDefinitionBL)
        {
            _context = context;
            _finYearBL = finYearBL;
            _trainingPlanMemberBL = trainingPlanMemberBL;
            _trainingPlanDistanceBL = trainingPlanDistanceBL;
            _trainingPlanRaceDefinitionBL = trainingPlanRaceDefinitionBL;
        }
        #endregion

        #region Methods

        #region Read

        public ResultSetPage<TrainingPlanViewModel> GetEntityListBySearchParams(
           GridLoadParam paramList)
        {

            var propertyInfo = typeof(TrainingPlanViewModel).GetProperty(paramList.SortField);

            var data = _context.TrainingPlan;

            var resultSet = _context.TrainingPlan
                 .IgnoreQueryFilters()
                .WhereIf(!String.IsNullOrEmpty(paramList.SearchTerm), a => a.Name.Contains(paramList.SearchTerm)|| a.Objective.Contains(paramList.SearchTerm)
                || a.Event.Name.Contains(paramList.SearchTerm))
                .ToListViewModel();

            return paramList.ToResultSetPage(propertyInfo, resultSet);

        }
        public async Task<TrainingPlanViewModel> GetEntityById(
              int? Id, AuthorizationModel model = null)
        {

            var viewModel = new TrainingPlanViewModel();

            PopulateDropDowns(viewModel);

            if (Id > 0)
            {
                var entity = await _context.TrainingPlan
                    .IgnoreQueryFilters()
                    .Include(c => c.FinYear) 
                    .Include(c => c.Event)
                    .Include(c => c.TrainingPlanDistances).ThenInclude(b => b.Distance)
                    .Include(c => c.TrainingPlanMembers).ThenInclude(a=> a.Member)
                    .Include(c => c.TrainingPlanRaceDefinitions).ThenInclude(a => a.RaceDefinition)
                    .FirstOrDefaultAsync(a => a.Id == Id);
                if (entity != null)
                {
                    entity.ToViewModel(viewModel);
                }
            }
            return viewModel;
        }




        private void PopulateDropDowns(TrainingPlanViewModel model)
        {
            var selectListItem = IQueryableExtensions.Default_SelectListItem();
            model.FinYears = _finYearBL.GetLatestFinYearSelectItem();


            model.Events = _context.Event
                                .Include(a => a.EventType)
                                .Where(a => a.EventType.Discriminator == EventTypeDiscriminator.Training)
                                .ToSelectListItem(a => a.Name, x => x.Id.ToString(), true);

            model.Members = _context.Member
                                .Include(a=>a.Person)
                                .ToSelectListItem(a => a.Person.FullName, x => x.Id.ToString(), true);

            model.Distances = _context.Distance
                               .ToSelectListItem(a => a.Name, x => x.Id.ToString(),true);
            model.RaceDefinitions = _context.RaceDefinition
                               .ToSelectListItem(a => a.Name, x => x.Id.ToString(), true); ;

        }




        #endregion

        #region Create/Update
        public async Task<SaveResult> SaveEntity(TrainingPlanViewModel viewModel)
        {
            SaveResult saveResult = new SaveResult();
                Dictionary<bool, string> dictionary = new Dictionary<bool, string>();

                var entity = new TrainingPlan();
         
            try
                {


                    if (viewModel.Id != 0)
                    {
                        if (_context.TrainingPlan.IgnoreQueryFilters().Any(a => a.Id == viewModel.Id))
                        {
                            entity = await _context.TrainingPlan
                            .Include(a=>a.TrainingPlanMembers)
                            .Include(a => a.TrainingPlanDistances)
                            .Include(a => a.TrainingPlanRaceDefinitions)
                            .IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == viewModel.Id);
                        }
                        entity = viewModel.ToEntity(entity);
                        _context.TrainingPlan.Update(entity);
                    }
                    else
                    {
                        entity = viewModel.ToEntity(entity);
                        _context.TrainingPlan.Add(entity);
                    }

                    await _context.SaveChangesAsync();

                    if (entity.Id > 0)
                    {
                        saveResult.IsSuccess = true;
                        saveResult.Id = entity.Id;
                        viewModel.Id = entity.Id;
                    saveResult = await _trainingPlanMemberBL.SaveEntityList(viewModel,entity);

                    if(saveResult.IsSuccess)
                    {
                        saveResult = await _trainingPlanDistanceBL.SaveEntityList(viewModel, entity);
                    }

                    if (saveResult.IsSuccess)
                    {
                        saveResult = await _trainingPlanRaceDefinitionBL.SaveEntityList(viewModel, entity);
                    }

                }
                }
                catch (DbUpdateException upDateEx)
                {
                    var results = upDateEx.GetSqlerrorNo();
                    string msg = results == (int)SqlErrNo.FK ? ConstEntity.MissingValueMsg : ConstEntity.UniqueKeyMsg;
                    saveResult = dictionary.GetValidateEntityResults(msg).ToSaveResult();

                }
                catch (Exception ex)
                {

                    saveResult.Message = CrudError.SaveErrorMsg;
                }


                return saveResult;
            }
            
  
        #endregion

        #region Delete
        public async Task<SaveResult> DeleteEntity(int Id)
        {
            SaveResult resultSet = new SaveResult();
            Dictionary<bool, string> dictionary = new Dictionary<bool, string>();
            try
            {
                var entity = await _context.TrainingPlan.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == Id);
                _context.TrainingPlan.Remove(entity);
                await _context.SaveChangesAsync();

                resultSet.IsSuccess = true;
            }
            catch (DbUpdateException upDateEx)
            {
                var results = upDateEx.GetSqlerrorNo();

                string msg = results == (int)SqlErrNo.FK ? ConstEntity.ForeignKeyDelMsg : CrudError.DeleteErrorMsg;
                resultSet = dictionary.GetValidateEntityResults(msg).ToSaveResult();

            }
            catch (Exception ex)
            {

                resultSet.Message = CrudError.DeleteErrorMsg;
            }
            return resultSet;

        }
        #endregion

        #endregion
     
    }
}
