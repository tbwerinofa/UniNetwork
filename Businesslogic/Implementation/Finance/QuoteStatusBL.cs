using BusinessLogic.Interface;
using BusinessObject;
using BusinessObject.Component;
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
    public class QuoteStatusBL : IEntityViewLogic<QuoteStatusViewModel>, IQuoteStatusBL
    {
        protected readonly SqlServerApplicationDbContext _context;


        #region Constructors
        public QuoteStatusBL(SqlServerApplicationDbContext context)
        {
            _context = context;
        }


        #region Methods

        #region Read

        public ResultSetPage<QuoteStatusViewModel> GetEntityListBySearchParams(
          GridLoadParam paramList)
        {

            var propertyInfo = typeof(QuoteStatusViewModel).GetProperty(paramList.SortField);

            var resultSet = _context.QuoteStatus
                 .IgnoreQueryFilters()
                .WhereIf(!string.IsNullOrEmpty(paramList.SearchTerm), a => a.Name.Contains(paramList.SearchTerm))
                .ToListViewModel();

            return paramList.ToResultSetPage(propertyInfo, resultSet);

        }
        public async Task<QuoteStatusViewModel> GetEntityById(
              int? Id, AuthorizationModel model = null)
        {

            var viewModel = new QuoteStatusViewModel();

            if (Id > 0)
            {
                var entity = await _context.QuoteStatus
                    .IgnoreQueryFilters()
                    .FirstOrDefaultAsync(a => a.Id == Id);

                viewModel = entity.ToViewModel(viewModel);
            }

            return viewModel;
        }

        public IEnumerable<SelectListItem> GetSelectListItems()
        {

            return _context.QuoteStatus
                .ToSelectListItem(x => x.Name.ToString(),
                                                     x => x.Id.ToString());
        }

        public IEnumerable<QuoteStatusViewModel> GetEntityList()
        {
            var entity = _context.QuoteStatus;
            var model = entity.Include(a => a.Quotes)
                .ToListViewModel();

            return model;
        }

        public async Task<QuoteStatusViewModel> GetEntityByDiscr(
      string discriminator)
        {
            var viewModel = new QuoteStatusViewModel();

            try
            {

           
            var entity = await _context.QuoteStatus
                .Include(a=>a.MessageTemplate)
                    .IgnoreQueryFilters()
                    .FirstOrDefaultAsync(a => a.Discriminator == discriminator);

         
            return entity.ToViewModel(viewModel);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        #endregion

        #region Create/Update
        public async Task<SaveResult> SaveEntity(QuoteStatusViewModel viewModel)
        {
            SaveResult saveResult = new SaveResult();
            Dictionary<bool, string> dictionary = new Dictionary<bool, string>();

            var entity = new QuoteStatus();
            try
            {
                if (viewModel.Id != 0)
                {
                    if (_context.QuoteStatus.IgnoreQueryFilters().Any(a => a.Id == viewModel.Id))
                    {
                        entity = await _context.QuoteStatus.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == viewModel.Id);
                    }
                    entity = viewModel.ToEntity(entity);
                    _context.QuoteStatus.Update(entity);
                }
                else
                {
                    entity = viewModel.ToEntity(entity);
                    _context.QuoteStatus.Add(entity);
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
                var entity = await _context.QuoteStatus.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == Id);
                _context.QuoteStatus.Remove(entity);
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
