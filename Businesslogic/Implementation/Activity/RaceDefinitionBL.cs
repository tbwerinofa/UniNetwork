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
    public class RaceDefinitionBL : IEntityViewLogic<RaceDefinitionViewModel>,IRaceDefinitionBL
    {
        protected readonly SqlServerApplicationDbContext _context;

        #region Constructors
        public RaceDefinitionBL(SqlServerApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods

        #region Read

        public ResultSetPage<RaceDefinitionViewModel> GetEntityListBySearchParams(
           GridLoadParam paramList)
        {

            var propertyInfo = typeof(RaceDefinitionViewModel).GetProperty(paramList.SortField);

            var data = _context.RaceDefinition;

            var resultSet = _context.RaceDefinition
                 .IgnoreQueryFilters()
                .WhereIf(!String.IsNullOrEmpty(paramList.SearchTerm), a => a.Name.Contains(paramList.SearchTerm))
                .ToListViewModel();

            return paramList.ToResultSetPage(propertyInfo, resultSet);

        }
        public async Task<RaceDefinitionViewModel> GetEntityById(
              int? Id, AuthorizationModel model = null)
        {

            var viewModel = new RaceDefinitionViewModel();

            PopulateDropDowns(viewModel);

            if (Id > 0)
            {
                var entity = await _context.RaceDefinition
                    .IgnoreQueryFilters()
                      .Include(c => c.Discpline)
                      .Include(c => c.RaceType)
                      .Include(c => c.Province.Country.Provinces)
                      .FirstOrDefaultAsync(a => a.Id == Id);
                if (entity != null)
                {
                    entity.ToViewModel(viewModel);
                    viewModel.Provinces = entity.Province.Country.Provinces.AsQueryable().ToSelectListItem(a => a.Name, x => x.Id.ToString());
                }
            }
            return viewModel;
        }

        private void PopulateDropDowns(RaceDefinitionViewModel model)
        {
            model.Provinces = IQueryableExtensions.Default_SelectListItem();
            model.Countries = _context.Country
                                .Include(a=>a.Provinces)
                                .Where(a=> a.Provinces.Any())
                                .ToSelectListItem(a => a.Name, x => x.Id.ToString());
            model.Discplines = _context.Discpline.ToSelectListItem(a => a.Name, x => x.Id.ToString());
            model.RaceTypes = _context.RaceType.ToSelectListItem(a => a.Name, x => x.Id.ToString());
        }

        public IEnumerable<SelectListItem> GetSelectListItems()
        {

            return _context.RaceDefinition
                .ToSelectListItem(x => x.Name.ToString(),
                                                     x => x.Id.ToString());
        }

        public IEnumerable<SelectListItem> GetEntitySelectListItem_ByParentId(
           int parentId)
        {
            var selectListItem = this._context.RaceDefinition.Where(a => a.ProvinceId == parentId).ToSelectListItem(a => a.Name, x => x.Id.ToString());

            return selectListItem;
        }


        #endregion

        #region Create/Update
        public async Task<SaveResult> SaveEntity(RaceDefinitionViewModel viewModel)
        {
            SaveResult saveResult = new SaveResult();
                Dictionary<bool, string> dictionary = new Dictionary<bool, string>();

                var entity = new RaceDefinition();
         
            try
                {

                    if (viewModel.Id != 0)
                    {
                        if (_context.RaceDefinition.IgnoreQueryFilters().Any(a => a.Id == viewModel.Id))
                        {
                            entity = await _context.RaceDefinition.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == viewModel.Id);
                        }
                        entity = viewModel.ToEntity(entity);
                        _context.RaceDefinition.Update(entity);
                    }
                    else
                    {
                        entity = viewModel.ToEntity(entity);
                        _context.RaceDefinition.Add(entity);
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
                var entity = await _context.RaceDefinition.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == Id);
                _context.RaceDefinition.Remove(entity);
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
