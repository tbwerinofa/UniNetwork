using BusinessLogic.Interface;
using BusinessObject;
using BusinessObject.Component;
using DataAccess;
using DomainObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transform;

namespace BusinessLogic.Implementation
{
    public class PayFastNotifyBL : IEntityViewLogic<PayFastNotifyViewModel>, IPayFastNotifyBL
    {
        protected readonly SqlServerApplicationDbContext _context;


        #region Constructors
        public PayFastNotifyBL(SqlServerApplicationDbContext context)
        {
            _context = context;
        }


        #region Methods

        #region Read

        public ResultSetPage<PayFastNotifyViewModel> GetEntityListBySearchParams(
          GridLoadParam paramList)
        {

            var propertyInfo = typeof(PayFastNotifyViewModel).GetProperty(paramList.SortField);

            var resultSet = _context.PayFastNotify
                 .IgnoreQueryFilters()
                .WhereIf(!string.IsNullOrEmpty(paramList.SearchTerm), a => a.Name_first.Contains(paramList.SearchTerm))
                .ToListViewModel();

            return paramList.ToResultSetPage(propertyInfo, resultSet);

        }
        public async Task<PayFastNotifyViewModel> GetEntityById(
              int? Id, AuthorizationModel model = null)
        {

            var viewModel = new PayFastNotifyViewModel();

            if (Id > 0)
            {
                var entity = await _context.PayFastNotify
                    .IgnoreQueryFilters()
                    .FirstOrDefaultAsync(a => a.Id == Id);

                viewModel = entity.ToViewModel(viewModel);
            }

            return viewModel;
        }


        #endregion

        #region Create/Update
        public async Task<SaveResult> SaveEntity(PayFastNotifyViewModel viewModel)
        {
            SaveResult saveResult = new SaveResult();
                Dictionary<bool, string> dictionary = new Dictionary<bool, string>();

                var entity = new PayFastNotify();
                try
                {
                    if (viewModel.Id != 0)
                    {
                        if (_context.PayFastNotify.IgnoreQueryFilters().Any(a => a.Id == viewModel.Id))
                        {
                            entity = await _context.PayFastNotify.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == viewModel.Id);
                        }
                        entity = viewModel.ToEntity(entity);
                        _context.PayFastNotify.Update(entity);
                    }
                    else
                    {
                        entity = viewModel.ToEntity(entity);
                        _context.PayFastNotify.Add(entity);
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

        public async Task<SaveResult> UpdateStatus(
        DataAccess.PayFastNotify entity)
        {
            SaveResult saveResult = new SaveResult();
            var localentity = await _context.PayFastNotify.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == entity.Id);
            if(localentity == null)
            {
                throw new ArgumentNullException("PayFastUpdate");
            }
                localentity.Isprocessed = entity.Isprocessed;

            _context.PayFastNotify.Update(entity);
            await _context.SaveChangesAsync();
            saveResult.IsSuccess = true;

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
                var entity = await _context.PayFastNotify.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == Id);
                _context.PayFastNotify.Remove(entity);
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
