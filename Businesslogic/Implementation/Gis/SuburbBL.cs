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
    public class SuburbBL : IEntityViewLogic<SuburbViewModel>, ISuburbBL
    {
        protected readonly SqlServerApplicationDbContext _context;


        #region Constructors
        public SuburbBL(SqlServerApplicationDbContext context)
        {
            _context = context;
        }


        #region Methods

        #region Read



        public ResultSetPage<SuburbViewModel> GetEntityListBySearchParams(
   GridLoadParam paramList)
        {

            var sortfield = paramList.SortField ?? "Name";
            var propertyInfo = typeof(SuburbViewModel).GetProperty(sortfield);

            var resultSet = _context.Suburb
                 .IgnoreQueryFilters()
                .WhereIf(!String.IsNullOrEmpty(paramList.SearchTerm), a => a.Name.Contains(paramList.SearchTerm))
                .ToListViewModel();

            return paramList.ToResultSetPage(propertyInfo, resultSet);

        }
        public async Task<SuburbViewModel> GetEntityById(
              int? Id, AuthorizationModel model = null)
        {

            var viewModel = new SuburbViewModel();

            if (Id > 0)
            {
                var entity = await _context.Suburb
                    .IgnoreQueryFilters()
                    .Include(a => a.Town)
                    .FirstOrDefaultAsync(a => a.Id == Id);

                viewModel = entity.ToViewModel(viewModel);
            }

            viewModel.Cities = _context.City
                .ToSelectListItem(x => x.Name.ToString(),
                                                     x => x.Id.ToString());
            return viewModel;
        }


        public IEnumerable<SelectListItem> GetEntitySelectListItem_ByParentId(
          int parentId)
        {
            var selectListItem = this._context.Suburb.Where(a => a.TownId == parentId).ToSelectListItem(a => a.Name + "("+ a.StreetCode + ")", x => x.Id.ToString()).OrderBy(a => a.Text);

            return selectListItem;
        }

        #endregion

        #region Create/Update
        public async Task<SaveResult> SaveEntity(SuburbViewModel viewModel)
        {
            SaveResult saveResult = new SaveResult();
                Dictionary<bool, string> dictionary = new Dictionary<bool, string>();

                var entity = new Suburb();
                try
                {
                    if (viewModel.Id != 0)
                    {
                        if (_context.Suburb.IgnoreQueryFilters().Any(a => a.Id == viewModel.Id))
                        {
                            entity = await _context.Suburb.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == viewModel.Id);
                        }
                        entity = viewModel.ToEntity(entity);
                        _context.Suburb.Update(entity);
                    }
                    else
                    {
                        entity = viewModel.ToEntity(entity);
                        _context.Suburb.Add(entity);
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
                var entity = await _context.Suburb.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == Id);
                _context.Suburb.Remove(entity);
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
