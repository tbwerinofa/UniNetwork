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
    public class RaceResultImportBL : IEntityViewLogic<RaceResultImportViewModel>
    {
        protected readonly SqlServerApplicationDbContext _context;
        protected readonly IFinYearBL _finYearBL;
        protected readonly IRaceOrganisationBL _raceOrganisationBL;
        protected readonly IRaceDistanceBL _raceDistanceBL;

        #region Constructors
        public RaceResultImportBL(SqlServerApplicationDbContext context,
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

        public ResultSetPage<RaceResultImportViewModel> GetEntityListBySearchParams(
           GridLoadParam paramList)
        {

            var propertyInfo = typeof(RaceResultImportViewModel).GetProperty(paramList.SortField);



            var resultSet = _context.RaceResultImport
                 .IgnoreQueryFilters()
                 .Where(a=> a.PersonId.HasValue)
                .WhereIf(!String.IsNullOrEmpty(paramList.SearchTerm), a => a.Person.FullName.Contains(paramList.SearchTerm))
                .ToListViewModel();

            return paramList.ToResultSetPage(propertyInfo, resultSet);

        }
        public async Task<RaceResultImportViewModel> GetEntityById(
              int? Id, AuthorizationModel model = null)
        {

            var viewModel = new RaceResultImportViewModel();

            PopulateDropDowns(viewModel);

            if (Id > 0)
            {
                var entity = await _context.RaceDistance
                    .IgnoreQueryFilters()
                      .Include(c => c.RaceResultImports)
                      .FirstOrDefaultAsync(a => a.Id == Id);
                if (entity != null)
                {
                  viewModel.RaceResultImportDetailViewModel =  entity.RaceResultImports.ToDetailListViewModel();

                    entity.RaceResultImports.First().ToViewModel(viewModel);
                   
                }
            }
            return viewModel;
        }




        private void PopulateDropDowns(RaceResultImportViewModel model)
        {
            var selectListItem = IQueryableExtensions.Default_SelectListItem();
            model.People = _context.Person
                                .Where(a => a.Members.Any())
                                .ToSelectListItem(a => a.FullName, x => x.Id.ToString());
        }




        #endregion

        #region Create/Update
        public async Task<SaveResult> SaveEntity(RaceResultImportViewModel viewModel)
        {
            SaveResult saveResult = new SaveResult();
                Dictionary<bool, string> dictionary = new Dictionary<bool, string>();

            try
                {

                List<DataAccess.RaceResultImport> entityList = _context.RaceResultImport
                    .Where(a => a.RaceDistanceId == viewModel.RaceDistanceId && !a.PersonId.HasValue)
                    .ToList();

                if (entityList.Any())
                {
                    for (int row = 0; row <= entityList.Count() - 1; row++)
                    {
                        var raceResultImportId = viewModel.SelectedIdArr[row];

                        if (viewModel.PersonIdArr[row] != 0)
                        {
                            var currentEntity = entityList.Where(a => a.Id == raceResultImportId).FirstOrDefault();
                            if (currentEntity != null)
                            {
                                currentEntity.PersonId = viewModel.PersonIdArr[row];
                                currentEntity.UpdatedTimestamp = DateTime.Now;
                                currentEntity.UpdatedUserId = viewModel.SessionUserId;
                            };
                        }
                    }

                    _context.UpdateRange(entityList);
                    await _context.SaveChangesAsync();
                    saveResult.IsSuccess = true;
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
                var entity = await _context.RaceResultImport.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == Id);
                _context.RaceResultImport.Remove(entity);
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
