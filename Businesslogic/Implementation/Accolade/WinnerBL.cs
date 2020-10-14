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
    public class WinnerBL : IEntityViewLogic<WinnerViewModel>
    {
        protected readonly SqlServerApplicationDbContext _context;
        protected readonly IFinYearBL _finYearBL;
        protected readonly IAwardBL _awardBL;
        #region Constructors
        public WinnerBL(SqlServerApplicationDbContext context, IFinYearBL finYearBL, IAwardBL awardBL)
        {
            _context = context;
            _finYearBL = finYearBL;
            _awardBL = awardBL;
        }
        #endregion

        #region Methods

        #region Read

        public ResultSetPage<WinnerViewModel> GetEntityListBySearchParams(
           GridLoadParam paramList)
        {

            var propertyInfo = typeof(WinnerViewModel).GetProperty(paramList.SortField);

            var data = _context.Winner;

            var resultSet = _context.Winner
                 .IgnoreQueryFilters()
                .WhereIf(!String.IsNullOrEmpty(paramList.SearchTerm), a => a.Award.Name.Contains(paramList.SearchTerm))
                .ToListViewModel();

            return paramList.ToResultSetPage(propertyInfo, resultSet);

        }
        public async Task<WinnerViewModel> GetEntityById(
              int? Id, AuthorizationModel model = null)
        {

            var viewModel = new WinnerViewModel();

            PopulateDropDowns(viewModel);

            if (Id > 0)
            {
                var entity = await _context.Winner
                    .IgnoreQueryFilters()
                      .Include(c => c.FinYear)
                       .Include(c => c.Award.Gender)
                       .Include(c => c.Member.Person)
                        .Include(c => c.Award.Frequency)
                       .Include(c => c.CalendarMonth)
                       .Include(c => c.Award.Gender)
                      .FirstOrDefaultAsync(a => a.Id == Id);
                if (entity != null)
                {
                    entity.ToViewModel(viewModel);
                    viewModel.Awards = _awardBL.GetSelectListItems_byFrequencyId(entity.Award.FrequencyId);
                   viewModel.Members = _context.Member.Include(a => a.Person).Where(a=>a.Person.GenderId == entity.Award.GenderId).ToSelectListItem(a => a.Person.FullName, x => x.Id.ToString());
                }
            }
            return viewModel;
        }

        private void PopulateDropDowns(WinnerViewModel model)
        {
            model.Frequencies = _context.Frequency
                                          .ToSelectListItem(a => a.Name, x => x.Id.ToString());
            model.FinYears = _finYearBL.GetLatestFinYearSelectItem();
            model.CalendarMonths = _context.CalendarMonth.OrderBy(a=>a.Ordinal).ToSelectListItem(a => a.Name, x => x.Id.ToString(),excludeSort:true);
            model.Awards = IQueryableExtensions.Default_SelectListItem();
            model.Members = IQueryableExtensions.Default_SelectListItem();
        }




        #endregion

        #region Create/Update
        public async Task<SaveResult> SaveEntity(WinnerViewModel viewModel)
        {
            SaveResult saveResult = new SaveResult();
                Dictionary<bool, string> dictionary = new Dictionary<bool, string>();

                var entity = new Winner();
         
            try
                {

                    if (viewModel.Id != 0)
                    {
                        if (_context.Winner.IgnoreQueryFilters().Any(a => a.Id == viewModel.Id))
                        {
                            entity = await _context.Winner.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == viewModel.Id);
                        }
                        entity = viewModel.ToEntity(entity);
                        _context.Winner.Update(entity);
                    }
                    else
                    {
                        entity = viewModel.ToEntity(entity);
                        _context.Winner.Add(entity);
                    }

                    await _context.SaveChangesAsync();

                    if (entity.Id > 0)
                    {
                        saveResult.IsSuccess = true;
                        saveResult.Id = entity.Id;
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
                var entity = await _context.Winner.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == Id);
                _context.Winner.Remove(entity);
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
