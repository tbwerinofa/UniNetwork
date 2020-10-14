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
    public class RaceBL : IEntityViewLogic<RaceViewModel>
    {
        protected readonly SqlServerApplicationDbContext _context;
        protected readonly IFinYearBL _finYearBL;
        protected readonly IRaceOrganisationBL _raceOrganisationBL;
        protected readonly IRaceDistanceBL _raceDistanceBL;

        #region Constructors
        public RaceBL(SqlServerApplicationDbContext context,
            IFinYearBL finYearBL,
            IRaceOrganisationBL raceOrganisationBL,
            IRaceDistanceBL raceDistanceBL)
        {
            _context = context;
            _finYearBL = finYearBL;
            _raceOrganisationBL = raceOrganisationBL;
            _raceDistanceBL = raceDistanceBL;
        }
        #endregion

        #region Methods

        #region Read

        public ResultSetPage<RaceViewModel> GetEntityListBySearchParams(
           GridLoadParam paramList)
        {

            var propertyInfo = typeof(RaceViewModel).GetProperty(paramList.SortField);

            var data = _context.Race;

            var resultSet = _context.Race
                 .IgnoreQueryFilters()
                .WhereIf(!String.IsNullOrEmpty(paramList.SearchTerm), a => a.RaceDefinition.Name.Contains(paramList.SearchTerm))
                .ToListViewModel();

            return paramList.ToResultSetPage(propertyInfo, resultSet);

        }
        public async Task<RaceViewModel> GetEntityById(
              int? Id, AuthorizationModel model = null)
        {

            var viewModel = new RaceViewModel();

            PopulateDropDowns(viewModel);

            if (Id > 0)
            {
                var entity = await _context.Race
                    .IgnoreQueryFilters()
                      .Include(c => c.FinYear)
                      .Include(c => c.RaceDistances).ThenInclude(b=>b.Distance)
                      .Include(c => c.RaceDistances).ThenInclude(b => b.Race.RaceDefinition)
                      .Include(c => c.RaceDefinition.Province.RaceDefinitions)
                      .Include(c => c.RaceDefinition.Province.Country.Provinces)
                       .Include(c => c.RaceOrganisations).ThenInclude(a=> a.Organisation.OrganisationType.Organisations)
                      .FirstOrDefaultAsync(a => a.Id == Id);
                if (entity != null)
                {
                    entity.ToViewModel(viewModel);


                    if (entity.RaceDistances.Any())
                    {
                        viewModel.RaceDistances = entity.RaceDistances.AsQueryable().ToListViewModel();
                        viewModel.RaceDistancesSelectList = entity.RaceDistances.AsQueryable().ToSelectListItem(a => a.Distance.Name, x => x.Id.ToString());
                        viewModel.Genders = _context.Gender.ToSelectListItem(a => a.Name, x => x.Id.ToString());
                    }
               

                    if (entity.RaceOrganisations.Any())
                    {
                        foreach (var item in entity.RaceOrganisations)
                        {
                            viewModel.OrganisationId = item.OrganisationId;
                            viewModel.OrganisationTypeId = item.Organisation.OrganisationTypeId;
                            viewModel.Organisations = item.Organisation.OrganisationType.Organisations.AsQueryable().ToSelectListItem(a => a.Name, x => x.Id.ToString());
                        }
                    }

                    viewModel.Provinces = entity.RaceDefinition.Province.Country.Provinces.AsQueryable().ToSelectListItem(a => a.Name, x => x.Id.ToString());
                    viewModel.RaceDefinitions = entity.RaceDefinition.Province.RaceDefinitions.AsQueryable().ToSelectListItem(a => a.Name, x => x.Id.ToString());
                }
            }
            return viewModel;
        }




        private void PopulateDropDowns(RaceViewModel model)
        {
            var selectListItem = IQueryableExtensions.Default_SelectListItem();
            model.FinYears = _finYearBL.GetSelectListItem(true,true);


            model.OrganisationTypes = _context.OrganisationType
                                .Include(a => a.Organisations)
                                .Where(a => a.Organisations.Any())
                                .ToSelectListItem(a => a.Name, x => x.Id.ToString());

            model.Countries = _context.Country
                                .Include(a=>a.Provinces)
                                .Where(a=> a.Provinces.Any())
                                .ToSelectListItem(a => a.Name, x => x.Id.ToString());

            model.Distances = _context.Distance
                               .ToSelectListItem(a => a.Name, x => x.Id.ToString(),true);
            model.Organisations = selectListItem;
            model.RaceDefinitions = selectListItem;
            model.Provinces = selectListItem;
            model.RaceDistances = new List<RaceDistanceViewModel>();
            model.RaceDistancesSelectList = selectListItem;
            model.Genders = selectListItem;

        }




        #endregion

        #region Create/Update
        public async Task<SaveResult> SaveEntity(RaceViewModel viewModel)
        {
            SaveResult saveResult = new SaveResult();
                Dictionary<bool, string> dictionary = new Dictionary<bool, string>();

                var entity = new Race();
         
            try
                {

                    saveResult = AtLeastOneEvent(viewModel, saveResult);
                    if(!saveResult.IsSuccess)
                        {
                            return saveResult;
                        }

                    if (viewModel.Id != 0)
                    {
                        if (_context.Race.IgnoreQueryFilters().Any(a => a.Id == viewModel.Id))
                        {
                            entity = await _context.Race
                            .Include(a=>a.RaceOrganisations)
                            .Include(a => a.RaceDistances)
                            .IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == viewModel.Id);
                        }
                        entity = viewModel.ToEntity(entity);
                        _context.Race.Update(entity);
                    }
                    else
                    {
                        entity = viewModel.ToEntity(entity);
                        _context.Race.Add(entity);
                    }

                    await _context.SaveChangesAsync();

                    if (entity.Id > 0)
                    {
                        saveResult.IsSuccess = true;
                        saveResult.Id = entity.Id;
                        viewModel.Id = entity.Id;
                    saveResult = await _raceOrganisationBL.SaveEntityList(viewModel,entity);

                    if(saveResult.IsSuccess)
                    {
                        saveResult = await _raceDistanceBL.SaveEntityList(viewModel, entity);
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
            
        public SaveResult AtLeastOneEvent(RaceViewModel viewModel, SaveResult saveResult)
        {
            bool isValid = false;
            if(viewModel.EventDateTimes == null)
            {
                isValid = false;
            }
            else
            {
                foreach(var item  in viewModel.EventDateTimes)
                {
                    if (item != null)
                        isValid = true;
                }
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
                var entity = await _context.Race.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == Id);
                _context.Race.Remove(entity);
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
