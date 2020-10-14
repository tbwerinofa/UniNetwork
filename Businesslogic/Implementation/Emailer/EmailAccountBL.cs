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
    public class EmailAccountBL : IEntityViewLogic<EmailAccountViewModel>
    {
        protected readonly SqlServerApplicationDbContext _context;


        #region Constructors
        public EmailAccountBL(SqlServerApplicationDbContext context,
             IMessageTokenBL messageTokensBL)
        {
            _context = context;
        }


        #region Methods

        #region Read
        public async Task<EmailAccountViewModel> GetEntityById(
              int? Id, AuthorizationModel model = null)
        {

            var viewModel = new EmailAccountViewModel();
            if (Id > 0)
            {
                var entity = await _context.EmailAccount
                    .IgnoreQueryFilters()
                    .FirstOrDefaultAsync(a => a.Id == Id);

                viewModel = entity.ToViewModel(viewModel);
            }
            return viewModel;
        }

        public ResultSetPage<EmailAccountViewModel> GetEntityListBySearchParams(
          GridLoadParam param)
        {

            var propertyInfo = typeof(EmailAccountViewModel).GetProperty(param.SortField);

            var resultSet = _context.EmailAccount
                 .IgnoreQueryFilters()
                .WhereIf(!String.IsNullOrEmpty(param.SearchTerm), a => a.DisplayName.Contains(param.SearchTerm))
                .ToListViewModel();

            return param.ToResultSetPage(propertyInfo, resultSet);

        }


        #endregion

        #region Create/Update
        public async Task<SaveResult> SaveEntity(EmailAccountViewModel viewModel)
        {
            try
            {


                SaveResult saveResult = new SaveResult();

                var entity = new EmailAccount();

                if (viewModel.Id != 0)
                {
                    if (_context.EmailAccount.IgnoreQueryFilters().Any(a => a.Id == viewModel.Id))
                    {
                        entity = await _context.EmailAccount.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == viewModel.Id);
                    }
                    entity = viewModel.ToEntity(entity);
                    _context.EmailAccount.Update(entity);
                }
                else
                {
                    entity = viewModel.ToEntity(entity);
                    _context.EmailAccount.Add(entity);
                }

                await _context.SaveChangesAsync();

                if (entity.Id > 0)
                {
                    saveResult.IsSuccess = true;
                    saveResult.Id = entity.Id;
                }

                return saveResult;
            }
            catch (DbUpdateException ex)
            {

                throw ex;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #endregion

        #region Delete
        public async Task<SaveResult> DeleteEntity(int Id)
        {
            SaveResult resultSet = new SaveResult();

            try
            {
                var entity = await _context.EmailAccount.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == Id);
                _context.EmailAccount.Remove(entity);
            await _context.SaveChangesAsync();

            resultSet.IsSuccess = true;
        }
            catch (DbUpdateException ex)
            {
                resultSet.Message = "Error deleting record";

            }
            catch (Exception ex)
            {

                resultSet.Message = "Error deleting record";
            }
            return resultSet;

        }
        #endregion

        #endregion
        #endregion
    }
}
