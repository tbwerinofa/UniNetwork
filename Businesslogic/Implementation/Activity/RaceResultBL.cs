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
    public class RaceResultBL : IEntityViewLogic<RaceResultViewModel>, IRaceResultBL
    {
        protected readonly SqlServerApplicationDbContext _context;
        private readonly IRaceResultMappingBL _raceResultMappingBL;

        #region Constructors
        public RaceResultBL(SqlServerApplicationDbContext context,
            IRaceResultMappingBL raceResultMappingBL)
        {
            _context = context;
            _raceResultMappingBL = raceResultMappingBL;
        }

        #endregion
        #region Methods

        #region Read

        public ResultSetPage<RaceResultViewModel> GetEntityListBySearchParams(
         GridLoadParam paramList)
        {


            var propertyInfo = typeof(RaceResultViewModel).GetProperty(paramList.SortField);

            var resultSet = _context.RaceResult
                .Include(a=> a.Member.Person)
                 .Include(a => a.RaceDistance.Race)
                 .IgnoreQueryFilters()
                 .Where(a=> a.RaceDistance.RaceId == paramList.ParentId)
                .WhereIf(!String.IsNullOrEmpty(paramList.SearchTerm), a => a.Member.Person.FullName.Contains(paramList.SearchTerm))
                .ToListViewModel();

            return paramList.ToResultSetPage(propertyInfo, resultSet);

        }

        public ResultSetPage<RaceResultViewModel> GetEntityListBySearchParams(
       GridLoadParam paramList,
       RaceResultViewModel model)
        {


            var propertyInfo = typeof(RaceResultViewModel).GetProperty(paramList.SortField);

            var resultSet = _context.RaceResult
                .Include(a => a.Member.Person)
                 .Include(a => a.RaceDistance.Race)
                 .IgnoreQueryFilters()
                 .Where(a => a.RaceDistance.RaceId == paramList.ParentId)
                 .WhereIf(model.RaceDistanceId != 0,a=>a.RaceDistanceId == model.RaceDistanceId)
                .WhereIf(!String.IsNullOrEmpty(paramList.SearchTerm), a => a.Member.Person.FullName.Contains(paramList.SearchTerm))
                .ToListViewModel();

            return paramList.ToResultSetPage(propertyInfo, resultSet);

        }

        public async Task<RaceResultViewModel> GetEntityById(
              int? Id, AuthorizationModel model = null)
        {

            var viewModel = new RaceResultViewModel();

            if (Id > 0)
            {
                var entity = await _context.RaceResult.IgnoreQueryFilters()
                    .FirstOrDefaultAsync(a => a.Id == Id);

                viewModel = entity.ToViewModel(viewModel);
            }
            return viewModel;
        }

        #endregion

        #region Create/Update
        public async Task<SaveResult> SaveEntity(RaceResultViewModel viewModel)
        {
            SaveResult saveResult = new SaveResult();
                Dictionary<bool, string> dictionary = new Dictionary<bool, string>();

            
            try
            {
                RaceDistance parentEntity = await _context.RaceDistance.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == viewModel.RaceDistanceId);
            saveResult = await _raceResultMappingBL.SaveEntityList(viewModel, parentEntity);

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
                var entity = await _context.RaceResult.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == Id);
                _context.RaceResult.Remove(entity);
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
