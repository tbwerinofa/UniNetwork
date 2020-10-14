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
    public class ProvinceBL : IEntityViewLogic<ProvinceViewModel>, IProvinceBL
    {
        #region global fields
        protected readonly SqlServerApplicationDbContext _context;
        #endregion

        #region Constructors
        public ProvinceBL(SqlServerApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Methods

        #region Read

        public ResultSetPage<ProvinceViewModel> GetEntityListBySearchParams(
   GridLoadParam paramList)
        {

            var sortfield = paramList.SortField ?? "Name";
            var propertyInfo = typeof(ProvinceViewModel).GetProperty(sortfield);

            var resultSet = _context.Province
                 .IgnoreQueryFilters()
                .WhereIf(!String.IsNullOrEmpty(paramList.SearchTerm), a => a.Name.Contains(paramList.SearchTerm))
                .ToListViewModel();

            return paramList.ToResultSetPage(propertyInfo, resultSet);

        }
        public async Task<ProvinceViewModel> GetEntityById(
              int? Id, AuthorizationModel model = null)
        {

            var viewModel = new ProvinceViewModel();

            if (Id > 0)
            {
                var entity = await _context.Province.IgnoreQueryFilters()
                    .Include(a=> a.Country)
                    .FirstOrDefaultAsync(a => a.Id == Id);

                viewModel = entity.ToViewModel(viewModel);
            }

            viewModel.Countries = _context.Country
                .ToSelectListItem(x => x.Name.ToString(),
                                                     x => x.Id.ToString());
            return viewModel;
        }


        public IEnumerable<SelectListItem> GetEntitySelectListItem_ByParentId(
          int parentId)
        {
            var selectListItem = this._context.Province.Include(a=>a.Cities).Where(a => a.CountryId == parentId && a.Cities.Any()).ToSelectListItem(a => a.Name, x => x.Id.ToString());

            return selectListItem;
        }


        public IEnumerable<DropDownListItems> GetEntityDropDownListItems(AuthorizationModel authorizationFilter)
        {
            var selectListItem = this._context.Province
                 .WhereIf(authorizationFilter != null && authorizationFilter.RegionIds.Any(), c => authorizationFilter.RegionIds.Contains(c.Id))
                 .ToDropDownListItem(Enumerable.Empty<int>());

            return selectListItem;
        }

        #endregion

        #region Create/Update
        public async Task<SaveResult> SaveEntity(ProvinceViewModel viewModel)
        {
            SaveResult saveResult = new SaveResult();
                Dictionary<bool, string> dictionary = new Dictionary<bool, string>();

                var entity = new Province();
                try
                {
                    if (viewModel.Id != 0)
                    {
                        if (_context.Province.IgnoreQueryFilters().Any(a => a.Id == viewModel.Id))
                        {
                            entity = await _context.Province.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == viewModel.Id);
                        }
                        entity = viewModel.ToEntity(entity);
                        _context.Province.Update(entity);
                    }
                    else
                    {
                        entity = viewModel.ToEntity(entity);
                        _context.Province.Add(entity);
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
                var entity = await _context.Province.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == Id);
                _context.Province.Remove(entity);
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
