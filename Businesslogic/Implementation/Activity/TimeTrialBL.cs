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
    public class TimeTrialBL : IEntityViewLogic<TimeTrialViewModel>
    {
        protected readonly SqlServerApplicationDbContext _context;
        protected readonly IFinYearBL _finYearBL;
        protected readonly IRaceOrganisationBL _raceOrganisationBL;
        protected readonly ITimeTrialDistanceBL _timeTrialDistanceBL;

        #region Constructors
        public TimeTrialBL(SqlServerApplicationDbContext context,
            IFinYearBL finYearBL,
            IRaceOrganisationBL raceOrganisationBL,
            ITimeTrialDistanceBL TimeTrialDistanceBL)
        {
            _context = context;
            _finYearBL = finYearBL;
            _raceOrganisationBL = raceOrganisationBL;
            _timeTrialDistanceBL = TimeTrialDistanceBL;
        }
        #endregion

        #region Methods

        #region Read

        public ResultSetPage<TimeTrialViewModel> GetEntityListBySearchParams(
           GridLoadParam paramList)
        {

            var propertyInfo = typeof(TimeTrialViewModel).GetProperty(paramList.SortField);


            var resultSet = _context.TimeTrial
                .Include(a=> a.Calendar.Event.EventType)
                 .IgnoreQueryFilters()
                .WhereIf(!String.IsNullOrEmpty(paramList.SearchTerm), a => a.Calendar.Event.EventType.Name.Contains(paramList.SearchTerm))
                .ToListViewModel();

            return paramList.ToResultSetPage(propertyInfo, resultSet);

        }
        public async Task<TimeTrialViewModel> GetEntityById(
              int? Id, AuthorizationModel model = null)
        {

            var viewModel = new TimeTrialViewModel();

            PopulateDropDowns(viewModel);

            if (Id > 0)
            {
                var entity = await _context.TimeTrial
                    .IgnoreQueryFilters()
                      .Include(c => c.Calendar.FinYear)
                      .Include(c => c.Calendar.Venue)
                      .Include(c => c.Calendar.Event)
                      .Include(c => c.Calendar.Moderators)
                      .Include(c => c.TimeTrialDistances).ThenInclude(b=>b.Distance)
                      .FirstOrDefaultAsync(a => a.Id == Id);
                if (entity != null)
                {
                    entity.ToViewModel(viewModel);


                    if (entity.TimeTrialDistances.Any())
                    {
                        viewModel.TimeTrialDistances = entity.TimeTrialDistances.AsQueryable().ToListViewModel();
                        viewModel.TimeTrialDistancesSelectList = entity.TimeTrialDistances.AsQueryable().ToSelectListItem(a => a.Distance.Name, x => x.Id.ToString());
                        viewModel.Genders = _context.Gender.ToSelectListItem(a => a.Name, x => x.Id.ToString());
                    }


                    //viewModel.Provinces = entity.RaceDefinition.Province.Country.Provinces.AsQueryable().ToSelectListItem(a => a.Name, x => x.Id.ToString());
                    //viewModel.RaceDefinitions = entity.RaceDefinition.Province.RaceDefinitions.AsQueryable().ToSelectListItem(a => a.Name, x => x.Id.ToString());
                }
            }
            return viewModel;
        }




        private void PopulateDropDowns(TimeTrialViewModel model)
        {
            var selectListItem = IQueryableExtensions.Default_SelectListItem();
            model.FinYears = _finYearBL.GetLatestFinYearSelectItem();


            model.CalendarMonths = _context.CalendarMonth
                                .OrderBy(a=>a.Ordinal)
                                .ToSelectListItem(a => a.Name, x => x.Id.ToString(),excludeSort:true);


            model.Distances = _context.Distance
                               .ToSelectListItem(a => a.Name, x => x.Id.ToString(),true);

            model.RaceTypes = _context.RaceType
                              .ToSelectListItem(a => a.Name, x => x.Id.ToString(), true);

            model.TimeTrialDistances = new List<TimeTrialDistanceViewModel>();
            model.TimeTrialDistancesSelectList = selectListItem;
            model.Genders = selectListItem;

        }




        #endregion

        #region Create/Update


        public async Task<SaveResult> SaveEntity(TimeTrialViewModel viewModel)
        {
            SaveResult saveResult = new SaveResult();
                Dictionary<bool, string> dictionary = new Dictionary<bool, string>();

                var entity = new TimeTrial();
         
            try
                {

                    saveResult = AtLeastOneEvent(viewModel, saveResult);
                    if(!saveResult.IsSuccess)
                        {
                            return saveResult;
                        }

                    if (viewModel.Id != 0)
                    {
                        if (_context.TimeTrial.IgnoreQueryFilters().Any(a => a.Id == viewModel.Id))
                        {
                            entity = await _context.TimeTrial
                            .Include(a=>a.Calendar)
                            .Include(a => a.TimeTrialDistances)
                            .IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == viewModel.Id);
                        }
                        entity = viewModel.ToEntity(entity);
                        _context.TimeTrial.Update(entity);
                    }
                    else
                    {
                        entity = viewModel.ToEntity(entity);
                        _context.TimeTrial.Add(entity);
                    }

                    await _context.SaveChangesAsync();

                    if (entity.Id > 0)
                    {
                        saveResult.IsSuccess = true;
                        saveResult.Id = entity.Id;
                        viewModel.Id = entity.Id;

                    if(saveResult.IsSuccess)
                    {
                        saveResult = await _timeTrialDistanceBL.SaveEntityList(viewModel, entity);
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
            
        public SaveResult AtLeastOneEvent(TimeTrialViewModel viewModel, SaveResult saveResult)
        {
            bool isValid = false;
            if(viewModel.DistanceIds == null)
            {
                isValid = false;
            }
            else
            {
                        isValid = true;
            }


            if(!isValid)
            {
                saveResult.Message = "At least one race distance is required!!";
            }
            else
            {
                saveResult.IsSuccess = true;
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
                var entity = await _context.TimeTrial.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == Id);
                _context.TimeTrial.Remove(entity);
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
