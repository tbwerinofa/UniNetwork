﻿using BusinessLogic.Interface;
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
    public class FeaturedCategoryBL : IEntityViewLogic<FeaturedCategoryViewModel>
    {
        protected readonly SqlServerApplicationDbContext _context;

        #region Constructors
        public FeaturedCategoryBL(SqlServerApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods

        #region Read

        public ResultSetPage<FeaturedCategoryViewModel> GetEntityListBySearchParams(
           GridLoadParam paramList)
        {

            var propertyInfo = typeof(FeaturedCategoryViewModel).GetProperty(paramList.SortField);

            var data = _context.FeaturedCategory;

            var resultSet = _context.FeaturedCategory
                 .IgnoreQueryFilters()
                .WhereIf(!String.IsNullOrEmpty(paramList.SearchTerm), a => a.Name.Contains(paramList.SearchTerm))
                .ToListViewModel();

            return paramList.ToResultSetPage(propertyInfo, resultSet);

        }
        public async Task<FeaturedCategoryViewModel> GetEntityById(
              int? Id, AuthorizationModel model = null)
        {

            var viewModel = new FeaturedCategoryViewModel();

            PopulateDropDowns(viewModel);

            if (Id > 0)
            {
                var entity = await _context.FeaturedCategory
                    .IgnoreQueryFilters()
                      .FirstOrDefaultAsync(a => a.Id == Id);
                if (entity != null)
                {
                    entity.ToViewModel(viewModel);
                }
            }
            return viewModel;
        }

        private void PopulateDropDowns(FeaturedCategoryViewModel model)
        {
        }

        public IEnumerable<SelectListItem> GetSelectListItems()
        {

            return _context.FeaturedCategory
                .ToSelectListItem(x => x.Name.ToString(),
                                                     x => x.Id.ToString());
        }



        #endregion

        #region Create/Update
        public async Task<SaveResult> SaveEntity(FeaturedCategoryViewModel viewModel)
        {
            SaveResult saveResult = new SaveResult();
                Dictionary<bool, string> dictionary = new Dictionary<bool, string>();

                var entity = new FeaturedCategory();
         
            try
                {

                    if (viewModel.Id != 0)
                    {
                        if (_context.FeaturedCategory.IgnoreQueryFilters().Any(a => a.Id == viewModel.Id))
                        {
                            entity = await _context.FeaturedCategory.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == viewModel.Id);
                        }
                        entity = viewModel.ToEntity(entity);
                        _context.FeaturedCategory.Update(entity);
                    }
                    else
                    {
                        entity = viewModel.ToEntity(entity);
                        _context.FeaturedCategory.Add(entity);
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
                var entity = await _context.FeaturedCategory.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == Id);
                _context.FeaturedCategory.Remove(entity);
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
