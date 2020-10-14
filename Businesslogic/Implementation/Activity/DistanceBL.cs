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
    public class DistanceBL : IEntityViewLogic<DistanceViewModel>
    {
        protected readonly SqlServerApplicationDbContext _context;


        #region Constructors
        public DistanceBL(SqlServerApplicationDbContext context)
        {
            _context = context;
        }

        #endregion
        #region Methods

        #region Read

        public ResultSetPage<DistanceViewModel> GetEntityListBySearchParams(
         GridLoadParam paramList)
        {


            var propertyInfo = typeof(DistanceViewModel).GetProperty(paramList.SortField);

            var resultSet = _context.Distance
                 .IgnoreQueryFilters()
                .WhereIf(!String.IsNullOrEmpty(paramList.SearchTerm), a => a.Name.Contains(paramList.SearchTerm))
                .ToListViewModel();

            return paramList.ToResultSetPage(propertyInfo, resultSet);

        }
        public async Task<DistanceViewModel> GetEntityById(
              int? Id, AuthorizationModel model = null)
        {

            var viewModel = new DistanceViewModel();

            PopulateDropDowns(viewModel);

            if (Id > 0)
            {
                var entity = await _context.Distance.IgnoreQueryFilters()
                    .FirstOrDefaultAsync(a => a.Id == Id);

                viewModel = entity.ToViewModel(viewModel);
            }
            return viewModel;
        }

        private void PopulateDropDowns(DistanceViewModel model)
        {
            model.MeasurementUnits = _context.MeasurementUnit.ToSelectListItem(a => a.Name, x => x.Id.ToString());
        }
        public IEnumerable<SelectListItem> GetSelectListItems()
        {

            return _context.Distance
                .ToSelectListItem(x => x.Name.ToString(),
                                                     x => x.Id.ToString());
        }


        #endregion

        #region Create/Update
        public async Task<SaveResult> SaveEntity(DistanceViewModel viewModel)
        {
            SaveResult saveResult = new SaveResult();
                Dictionary<bool, string> dictionary = new Dictionary<bool, string>();

                
                try
                {

                var entity = new Distance();
                if (viewModel.Id != 0)
                    {
                        if (_context.Distance.IgnoreQueryFilters().Any(a => a.Id == viewModel.Id))
                        {
                            entity = await _context.Distance.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == viewModel.Id);
                        }
                        entity = viewModel.ToEntity(entity);
                        _context.Distance.Update(entity);
                    }
                    else
                    {
                        entity = viewModel.ToEntity(entity);
                        _context.Distance.Add(entity);
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
                var entity = await _context.Distance.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == Id);
                _context.Distance.Remove(entity);
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
