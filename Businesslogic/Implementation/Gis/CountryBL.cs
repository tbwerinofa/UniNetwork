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
using System.Text;
using System.Threading.Tasks;
using Transform;

namespace BusinessLogic.Implementation
{
    public class CountryBL : IEntityViewLogic<CountryViewModel>,ICountryBL
    {
        protected readonly SqlServerApplicationDbContext _context;


        #region Constructors
        public CountryBL(SqlServerApplicationDbContext context)
        {
            _context = context;
        }


        #region Methods

        #region Read

        public ResultSetPage<CountryViewModel> GetEntityListBySearchParams(
   GridLoadParam paramList)
        {

            var propertyInfo = typeof(CountryViewModel).GetProperty(paramList.SortField);

            var resultSet = _context.Country
                 .IgnoreQueryFilters()
                .WhereIf(!String.IsNullOrEmpty(paramList.SearchTerm), a => a.Name.Contains(paramList.SearchTerm))
                .ToListViewModel();

            return paramList.ToResultSetPage(propertyInfo, resultSet);

        }
        public async Task<CountryViewModel> GetEntityById(
              int? Id, AuthorizationModel model = null)
        {

            var viewModel = new CountryViewModel();

            if (Id > 0)
            {
                var entity = await _context.Country
                    .IgnoreQueryFilters()
                    .Include(a => a.GlobalRegion)
                    .FirstOrDefaultAsync(a => a.Id == Id);

                viewModel = entity.ToViewModel(viewModel);
            }

            viewModel.GlobalRegions = _context.GlobalRegion
                .ToSelectListItem(x => x.Name.ToString(),
                                                     x => x.Id.ToString());
            return viewModel;
        }

        public IEnumerable<SelectListItem> GetSelectListItems()
        {

            return _context.Country
                .ToSelectListItem(x => x.Name.ToString(),
                                                     x => x.Id.ToString());
        }

        public IEnumerable<SelectListItem> GetEntitySelectListItem_ByParentId(
       int parentId)
        {
            var selectListItem = this._context.Country.Where(a => a.GlobalRegionId == parentId).ToSelectListItem(a => a.Name, x => x.Id.ToString());

            return selectListItem;
        }


        public IEnumerable<DropDownListItems> GetEntityDropDownListItems()
        {
            var selectListItem = this._context.Country.ToDropDownListItem(Enumerable.Empty<int>());

            return selectListItem;
        }

        #endregion

        #region Create/Update
        public async Task<SaveResult> SaveEntity(CountryViewModel viewModel)
        {
            SaveResult saveResult = new SaveResult();
                Dictionary<bool, string> dictionary = new Dictionary<bool, string>();

                var entity = new Country();
                try
                {
                    if (viewModel.Id != 0)
                    {
                        if (_context.Country.Any(a => a.Id == viewModel.Id))
                        {
                            entity = await _context.Country.FirstOrDefaultAsync(a => a.Id == viewModel.Id);
                        }
                        entity = viewModel.ToEntity(entity);
                        _context.Country.Update(entity);
                    }
                    else
                    {
                        entity = viewModel.ToEntity(entity);
                        _context.Country.Add(entity);
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
                var entity = await _context.Country.IgnoreQueryFilters().FirstOrDefaultAsync(a=> a.Id == Id);
                _context.Country.Remove(entity);
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
        #endregion
    }
}
