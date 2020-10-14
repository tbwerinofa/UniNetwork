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
    public class TimeTrialResultBL : IEntityViewLogic<TimeTrialResultViewModel>, ITimeTrialResultBL
    {
        protected readonly SqlServerApplicationDbContext _context;
        private readonly ITimeTrialResultMappingBL _timeTrialResultMappingBL;

        #region Constructors
        public TimeTrialResultBL(SqlServerApplicationDbContext context,
            ITimeTrialResultMappingBL timeTrialResultMappingBL)
        {
            _context = context;
            _timeTrialResultMappingBL = timeTrialResultMappingBL;
        }

        #endregion
        #region Methods

        #region Read

        public ResultSetPage<TimeTrialResultViewModel> GetEntityListBySearchParams(
         GridLoadParam paramList)
        {


            var propertyInfo = typeof(TimeTrialResultViewModel).GetProperty(paramList.SortField);

            var resultSet = _context.TimeTrialResult
                .Include(a=> a.Member.Person)
                 .Include(a => a.TimeTrialDistance.TimeTrial)
                 .IgnoreQueryFilters()
                 .Where(a=> a.TimeTrialDistance.TimeTrialId == paramList.ParentId)
                .WhereIf(!String.IsNullOrEmpty(paramList.SearchTerm), a => a.Member.Person.FullName.Contains(paramList.SearchTerm))
                .ToListViewModel();

            return paramList.ToResultSetPage(propertyInfo, resultSet);

        }

        public ResultSetPage<TimeTrialResultViewModel> GetEntityListBySearchParams(
       GridLoadParam paramList,
       TimeTrialResultViewModel model)
        {


            var propertyInfo = typeof(TimeTrialResultViewModel).GetProperty(paramList.SortField);

            var resultSet = _context.TimeTrialResult
                .Include(a => a.Member.Person)
                 .Include(a => a.TimeTrialDistance.TimeTrial)
                 .IgnoreQueryFilters()
                 .Where(a => a.TimeTrialDistance.TimeTrialId == paramList.ParentId)
                 .WhereIf(model.TimeTrialDistanceId != 0,a=>a.TimeTrialDistanceId == model.TimeTrialDistanceId)
                .WhereIf(!String.IsNullOrEmpty(paramList.SearchTerm), a => a.Member.Person.FullName.Contains(paramList.SearchTerm))
                .ToListViewModel();

            return paramList.ToResultSetPage(propertyInfo, resultSet);

        }

        public async Task<TimeTrialResultViewModel> GetEntityById(
              int? Id, AuthorizationModel model = null)
        {

            var viewModel = new TimeTrialResultViewModel();

            if (Id > 0)
            {
                var entity = await _context.TimeTrialResult.IgnoreQueryFilters()
                    .FirstOrDefaultAsync(a => a.Id == Id);

                viewModel = entity.ToViewModel(viewModel);
            }
            return viewModel;
        }

        #endregion

        #region Create/Update
        public async Task<SaveResult> SaveEntity(TimeTrialResultViewModel viewModel)
        {
            SaveResult saveResult = new SaveResult();
                Dictionary<bool, string> dictionary = new Dictionary<bool, string>();

            
            try
            {
                TimeTrialDistance parentEntity = await _context.TimeTrialDistance.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == viewModel.TimeTrialDistanceId);
            saveResult = await _timeTrialResultMappingBL.SaveEntityList(viewModel, parentEntity);

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
                var entity = await _context.TimeTrialResult.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == Id);
                _context.TimeTrialResult.Remove(entity);
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
