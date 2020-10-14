using BusinessLogic.Interface;
using BusinessObject;
using BusinessObject.Component;
using BusinessObject.ViewModel;
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
    public class MessageTemplateBL : IEntityViewLogic<MessageTemplateViewModel>, IMessageTemplateBL
    {
        #region Global Fields
        protected readonly SqlServerApplicationDbContext _context;
        IMessageTokenBL _messageTokensBL;
        private readonly IApplicationRoleMessageTemplateBL _applicationRoleMessageTemplateBL;

        #endregion

        #region Constructors
        public MessageTemplateBL(SqlServerApplicationDbContext context,
             IMessageTokenBL messageTokensBL,
             IApplicationRoleMessageTemplateBL applicationRoleMessageTemplateBL)
        {
            _context = context;
            _messageTokensBL = messageTokensBL;
            _applicationRoleMessageTemplateBL = applicationRoleMessageTemplateBL;
        }

        #endregion
       
        #region Methods

        #region Read
        public async Task<MessageTemplateViewModel> GetEntityById(
              int? Id, AuthorizationModel model = null)
        {

            var viewModel = new MessageTemplateViewModel
            {

                EmailAccounts = _context.EmailAccount.ToSelectListItem(a => a.DisplayName, a => a.Id.ToString()),
                Roles = _context.ApplicationRole.ToSelectListItem(a => a.Name, x => x.Id)
        };

            if (Id > 0)
            {
                var entity = await _context.MessageTemplate
                    .IgnoreQueryFilters()
                    .Include(a=>a.ApplicationRoleMessageTemplates)
                    .FirstOrDefaultAsync(a => a.Id == Id);

                viewModel = entity.ToViewModel(viewModel);
            }
            viewModel.MessageTokens = string.Join(",", _messageTokensBL.GetListOfAllowedTokens());
    


            return viewModel;
        }

        public IEnumerable<MessageTemplateViewModel> GetMessageTemplates()
        {

            return _context.MessageTemplate
                .ToListViewModel();
        }

        public ResultSetPage<MessageTemplateViewModel> GetEntityListBySearchParams(
          GridLoadParam param)
        {

            var propertyInfo = typeof(MessageTemplateViewModel).GetProperty(param.SortField);

            var resultSet = _context.MessageTemplate
                 .IgnoreQueryFilters()
                .WhereIf(!String.IsNullOrEmpty(param.SearchTerm), a => a.Name.Contains(param.SearchTerm))
                .ToListViewModel();

            return param.ToResultSetPage(propertyInfo, resultSet);

        }

        public virtual async Task<MessageTemplate> GetEntityByName(string messageTemplateName)
        {
            var entity = await _context.MessageTemplate
                .Include(a=>a.ApplicationRoleMessageTemplates).ThenInclude(a => a.ApplicationRole)
                     .Include(a => a.ApplicationRoleMessageTemplates).ThenInclude(a => a.MessageTemplate)
                .AsNoTracking()
                .FirstOrDefaultAsync(a=> a.Name ==messageTemplateName);

            return entity;
        }


        public virtual IQueryable<DataAccess.MessageTemplate> GetRegistrationTemplates(
            IEnumerable<string> messageTemplates)
        {
            var entityList = _context.MessageTemplate
                .Where(a => messageTemplates.Contains(a.Name))
                .AsNoTracking();

            return entityList;
        }
        #endregion

        #region Create/Update
        public async Task<SaveResult> SaveEntity(MessageTemplateViewModel viewModel)
        {
            try
            {


                SaveResult saveResult = new SaveResult();

                var entity = new MessageTemplate();

                if (viewModel.Id != 0)
                {
                    if (_context.MessageTemplate.IgnoreQueryFilters().Any(a => a.Id == viewModel.Id))
                    {
                        entity = await _context.MessageTemplate
                            .Include(a=>a.ApplicationRoleMessageTemplates).ThenInclude(b=>b.ApplicationRole)
                            .Include(a => a.ApplicationRoleMessageTemplates).ThenInclude(b => b.MessageTemplate)
                            .IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == viewModel.Id);
                    }
                    entity = viewModel.ToEntity(entity);
                    _context.MessageTemplate.Update(entity);
                }
                else
                {
                    entity = viewModel.ToEntity(entity);
                    _context.MessageTemplate.Add(entity);
                }

                await _context.SaveChangesAsync();

                if (entity.Id > 0)
                {
                    saveResult.IsSuccess = true;
                    saveResult.Id = entity.Id;
                }

                saveResult = await _applicationRoleMessageTemplateBL.SaveEntityList(viewModel, entity);
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
                var entity = await _context.MessageTemplate.IgnoreQueryFilters()
                    .FirstOrDefaultAsync(a => a.Id == Id);
                _context.MessageTemplate.Remove(entity);
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
    }
}
