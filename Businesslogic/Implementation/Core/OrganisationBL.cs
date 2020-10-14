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
    public class OrganisationBL : IEntityViewLogic<OrganisationViewModel>, IOrganisationBL
    {
        protected readonly SqlServerApplicationDbContext _context;

        #region Constructors
        public OrganisationBL(SqlServerApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods

        #region Read

        public ResultSetPage<OrganisationViewModel> GetEntityListBySearchParams(
           GridLoadParam paramList)
        {

            var propertyInfo = typeof(OrganisationViewModel).GetProperty(paramList.SortField);

            var data = _context.Organisation;

            var resultSet = _context.Organisation
                 .IgnoreQueryFilters()
                .WhereIf(!String.IsNullOrEmpty(paramList.SearchTerm), a => a.Name.Contains(paramList.SearchTerm))
                .ToListViewModel();

            return paramList.ToResultSetPage(propertyInfo, resultSet);

        }
        public async Task<OrganisationViewModel> GetEntityById(
              int? Id, AuthorizationModel model = null)
        {

            var viewModel = new OrganisationViewModel();

            PopulateDropDowns(viewModel);

            if (Id > 0)
            {
                var entity = await _context.Organisation
                    .IgnoreQueryFilters()
                      .Include(c => c.OrganisationType)
                      .Include(c => c.Province.Country)
                      .FirstOrDefaultAsync(a => a.Id == Id);
                if (entity != null)
                {
                    entity.ToViewModel(viewModel);
                    viewModel.Provinces = entity.Province.Country.Provinces.AsQueryable().ToSelectListItem(a => a.Name, x => x.Id.ToString());
                }
            }
            return viewModel;
        }

        private void PopulateDropDowns(OrganisationViewModel model)
        {
            model.Provinces = IQueryableExtensions.Default_SelectListItem();
            model.Countries = _context.Country
                                .Include(a=>a.Provinces)
                                .Where(a=> a.Provinces.Any())
                                .ToSelectListItem(a => a.Name, x => x.Id.ToString());
            model.OrganisationTypes = _context.OrganisationType.ToSelectListItem(a => a.Name, x => x.Id.ToString());
        }

        public IEnumerable<SelectListItem> GetEntitySelectListItem_ByParentId(
               int parentId)
        {
            var selectListItem = this._context.Organisation.Where(a => a.OrganisationTypeId == parentId).ToSelectListItem(a => a.Name, x => x.Id.ToString());

            return selectListItem;
        }

        public IEnumerable<SelectListItem> GetSelectListItems()
        {

            return _context.Organisation
                .ToSelectListItem(x => x.Name.ToString(),
                                                     x => x.Id.ToString());
        }



        #endregion

        #region Create/Update
        public async Task<SaveResult> SaveEntity(OrganisationViewModel viewModel)
        {
            SaveResult saveResult = new SaveResult();
                Dictionary<bool, string> dictionary = new Dictionary<bool, string>();

                var entity = new Organisation();
         
            try
                {

                    if (viewModel.Id != 0)
                    {
                        if (_context.Organisation.IgnoreQueryFilters().Any(a => a.Id == viewModel.Id))
                        {
                            entity = await _context.Organisation.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == viewModel.Id);
                        }
                        entity = viewModel.ToEntity(entity);
                        _context.Organisation.Update(entity);
                    }
                    else
                    {
                        entity = viewModel.ToEntity(entity);
                        _context.Organisation.Add(entity);
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
                var entity = await _context.Organisation.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == Id);
                _context.Organisation.Remove(entity);
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
