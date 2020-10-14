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
    public class AwardBL : IEntityViewLogic<AwardViewModel>, IAwardBL
    {
        protected readonly SqlServerApplicationDbContext _context;

        #region Constructors
        public AwardBL(SqlServerApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods

        #region Read

        public ResultSetPage<AwardViewModel> GetEntityListBySearchParams(
           GridLoadParam paramList)
        {

            var propertyInfo = typeof(AwardViewModel).GetProperty(paramList.SortField);

            var data = _context.Award;

            var resultSet = _context.Award
                 .IgnoreQueryFilters()
                .WhereIf(!String.IsNullOrEmpty(paramList.SearchTerm), a => a.Name.Contains(paramList.SearchTerm))
                .ToListViewModel();

            return paramList.ToResultSetPage(propertyInfo, resultSet);

        }
        public async Task<AwardViewModel> GetEntityById(
              int? Id, AuthorizationModel model = null)
        {

            var viewModel = new AwardViewModel();

            PopulateDropDowns(viewModel);

            if (Id > 0)
            {
                var entity = await _context.Award
                    .IgnoreQueryFilters()
                      .Include(c => c.Frequency)
                      .FirstOrDefaultAsync(a => a.Id == Id);
                if (entity != null)
                {
                    entity.ToViewModel(viewModel);
                }
            }
            return viewModel;
        }

        private void PopulateDropDowns(AwardViewModel model)
        {
            model.Frequencies = _context.Frequency
                                .ToSelectListItem(a => a.Name, x => x.Id.ToString());
            model.Genders = _context.Gender
                                          .ToSelectListItem(a => a.Name, x => x.Id.ToString());
        }

        public IEnumerable<SelectListItem> GetEntitySelectListItem_ByParentId(
               int parentId)
        {
            var selectListItem = this._context.Award.Where(a => a.FrequencyId == parentId).ToSelectListItem(a => a.Name, x => x.Id.ToString());

            return selectListItem;
        }

        public IEnumerable<SelectListItem> GetSelectListItems()
        {

            return _context.Award.Include(a => a.Gender)
                .ToSelectListItem(a => a.Name + (a.Gender != null ? " Gender: " + a.Gender.Name + "," : string.Empty) + " Position:" + a.Ordinal + "", x => x.Id.ToString());
        }

        public IEnumerable<SelectListItem> GetSelectListItems_byFrequencyId(int frequencyId)
        {

            return _context.Award.Include(a => a.Gender)
                .Where(a=>a.FrequencyId ==frequencyId)
                .ToSelectListItem(a => a.Name + (a.Gender != null ? " Gender: " + a.Gender.Name + "," : string.Empty) + " Position:" + a.Ordinal + "", x => x.Id.ToString());
        }

        #endregion

        #region Create/Update
        public async Task<SaveResult> SaveEntity(AwardViewModel viewModel)
        {
            SaveResult saveResult = new SaveResult();
                Dictionary<bool, string> dictionary = new Dictionary<bool, string>();

                var entity = new Award();
         
            try
                {

                    if (viewModel.Id != 0)
                    {
                        if (_context.Award.IgnoreQueryFilters().Any(a => a.Id == viewModel.Id))
                        {
                            entity = await _context.Award.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == viewModel.Id);
                        }
                        entity = viewModel.ToEntity(entity);
                        _context.Award.Update(entity);
                    }
                    else
                    {
                        entity = viewModel.ToEntity(entity);
                        _context.Award.Add(entity);
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
                var entity = await _context.Award.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == Id);
                _context.Award.Remove(entity);
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
