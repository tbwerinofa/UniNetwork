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
using System.Transactions;
using Transform;

namespace BusinessLogic.Implementation
{
    public class QuoteBL : IEntityViewLogic<QuoteViewModel>, IQuoteBL
    {
        protected readonly SqlServerApplicationDbContext _context;
        protected readonly IQuoteStatusBL _quoteStatusBL;
        protected readonly ISubscriptionBL _subscriptionBL;
        protected readonly IPayFastNotifyBL _payFastNotifyBL;
        protected readonly IQueuedEmailBL _queuedEmail;
        protected readonly IFinYearBL _finYearBL;

        #region Constructors
        public QuoteBL(SqlServerApplicationDbContext context,
            IQuoteStatusBL quoteStatusBL,
        ISubscriptionBL subscriptionBL,
        IPayFastNotifyBL payFastNotifyBL,
        IQueuedEmailBL queuedEmail,
        IFinYearBL finYearBL
         )
        {
            _context = context;
            _quoteStatusBL = quoteStatusBL;
            _payFastNotifyBL = payFastNotifyBL;
            _subscriptionBL = subscriptionBL;
            _finYearBL = finYearBL;
        }


        #region Methods

        #region Read

        public ResultSetPage<QuoteViewModel> GetEntityListBySearchParams(
          GridLoadParam paramList)
        {

            var propertyInfo = typeof(QuoteViewModel).GetProperty(paramList.SortField);

            var resultSet = _context.Quote
                .Include(a=> a.QuoteStatus)
                .Include(a => a.QuoteDetails).ThenInclude(b=>b.SubscriptionTypeRuleAudit)
                .Include(a => a.FinYear)
                .Include(a => a.QuoteUser)
                .Include(a => a.QuoteDetails).ThenInclude(b=>b.CreatedUser)
                .Include(a => a.QuoteDetails).ThenInclude(a => a.UpdatedUser)
                 .IgnoreQueryFilters()
               .WhereIf(!String.IsNullOrEmpty(paramList.SearchTerm), a => a.QuoteNo.Contains(paramList.SearchTerm) || a.QuoteStatus.Name.Contains(paramList.SearchTerm))
                .ToListViewModel();

            return paramList.ToResultSetPage(propertyInfo, resultSet);

        }
        public async Task<QuoteViewModel> GetEntityById(
              int? Id, AuthorizationModel model = null)
        {

            var viewModel = new QuoteViewModel();
            PopulateDropDowns(viewModel);

            if (Id > 0)
            {
                var entity = await _context.Quote
                    .IgnoreQueryFilters()
                    .Include(a=> a.QuoteStatus)
                    .Include(a=>a.QuoteUser)
                    .Include(a => a.QuoteDetails).ThenInclude(a=> a.Subscriptions)
                    .Include(a => a.QuoteDetails).ThenInclude(a=> a.SubscriptionTypeRuleAudit.SubscriptionTypeRule.SubscriptionType)
                    .Include(a => a.OrderDetails).ThenInclude(a => a.ProductSize.Product)
                    .FirstOrDefaultAsync(a => a.Id == Id);

                viewModel = entity.ToViewModel(viewModel);

                viewModel.PayFast = entity.ToPayFastViewModel(viewModel.TotalAmount
                        , new SaveResult());
            }

            viewModel.QuoteStatuses = _quoteStatusBL.GetEntityList();
            return viewModel;
        }

        private void PopulateDropDowns(QuoteViewModel model)
        {
            model.QuoteStatuses = _context.QuoteStatus.ToListViewModel();
        }

        public IEnumerable<QuoteViewModel> GetEntityList(
          string userId)
        {
            var entity = _context.Quote;
            var model = entity
                .Include(a=> a.QuoteDetails)
                 .Include(a => a.QuoteDetails).ThenInclude(b => b.CreatedUser)
                .Include(a => a.QuoteDetails).ThenInclude(a => a.UpdatedUser)
                .ToListViewModel();

            return model;
        }


        #endregion

        #region Create/Update

        public async Task<PayFastViewModel> SavePayFast(QuoteViewModel model)
        {
            var quoteEntity = new DataAccess.Quote();
            var orderNo = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
            model.QuoteNo = orderNo;
            QuoteStatusViewModel quoteStatus = await _quoteStatusBL.GetEntityByDiscr(OrderStatusRef.New);
            model.QuoteStatusId = quoteStatus.Id;
            model.FinYearId = _finYearBL.GetCurrentFinYearId();
            SaveResult saveResult = new SaveResult();
            Dictionary<bool, string> dictionary = new Dictionary<bool, string>();

            try
            {
                this.GenerateQuoteDetail(model, quoteEntity);


             saveResult = await AddEntity(model, quoteEntity);


            saveResult.Discriminator = model.QuoteNo;
            saveResult.Id = quoteEntity.Id;


            quoteEntity =  await _context.Quote.Include(a=> a.QuoteUser).IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == quoteEntity.Id);
            var payFastVieModel = new PayFastViewModel
            {
                SaveResult = saveResult,
                name_first = quoteEntity.QuoteUser.FirstName,
                name_last = quoteEntity.QuoteUser.Surname,
                email_address = quoteEntity.QuoteUser.Email,
                cell_number = quoteEntity.QuoteUser.ContactNo,
                m_payment_Id = quoteEntity.QuoteNo,
                amount = model.TotalAmount,
                item_name = "Subscription",
                item_description = string.Empty,
                custom_int1 = quoteEntity.Id,
                email_confirmation = 1,
                confirmation_address = quoteEntity.QuoteUser.Email,
                signature = string.Empty,
                FullName = quoteEntity.QuoteUser.FirstName + " " + quoteEntity.QuoteUser.Surname,
                MessageTemplate = quoteEntity.QuoteStatus.MessageTemplate.ToViewModel(new MessageTemplateViewModel())
            };
            //if(saveResult.IsSuccess)
            //{
            //    //queuemail
            //    var fullName = quoteEntity.QuoteUser.People.FirstOrDefault().Title.Name + " "  + quoteEntity.QuoteUser.People.FirstOrDefault().FirstName + " " + quoteEntity.QuoteUser.People.FirstOrDefault().Surname;
            //    _queuedEmail.QueueQuote(quoteEntity.QuoteStatu.MessageTemplate, quoteEntity.QuoteUser.Email, fullName,saveResult.ByteArray);
            //}

            return payFastVieModel;
            }
            catch (DbUpdateException upDateEx)
            {
                var results = upDateEx.GetSqlerrorNo();
                string msg = results == (int)SqlErrNo.FK ? ConstEntity.MissingValueMsg : ConstEntity.UniqueKeyMsg;
                saveResult = dictionary.GetValidateEntityResults(msg).ToSaveResult();
                throw upDateEx;
            }
            catch (Exception ex)
            {

                saveResult.Message = CrudError.SaveErrorMsg;
                throw ex;
            }


        }


        private void GenerateQuoteDetail(
            QuoteViewModel model,
            DataAccess.Quote quoteEntity)
        {
            if (model.SubscriptionTypeRuleAuditIds.Any())
            {

                List<DataAccess.QuoteDetail> detailList = new List<DataAccess.QuoteDetail>();

                var distinctSubTypeRuleAudit = model.SubscriptionTypeRuleAuditIds.Distinct().ToArray();

                for (int i = 0; i < distinctSubTypeRuleAudit.Count(); i++)
                {

                    var currentRow = new DataAccess.QuoteDetail
                    {
                        QuoteId = quoteEntity.Id,
                        SubscriptionTypeRuleAuditId = distinctSubTypeRuleAudit[i],
                        Quantity = model.QuantityIds[i],
                        ItemNo = i + 1,
                        CreatedUserId = model.UserId,
                    };
                    quoteEntity.QuoteDetails.Add(currentRow);
                }

            }
        }


        public async Task<SaveResult> AddEntity(QuoteViewModel viewModel,Quote entity)
        {
            SaveResult saveResult = new SaveResult();
            try
            {

         

            entity = viewModel.ToEntity(entity);
            _context.Quote.Add(entity);

                await _context.SaveChangesAsync();

                if (entity.Id > 0)
                {
                    saveResult.IsSuccess = true;
                    saveResult.Id = entity.Id;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return saveResult;
        }

        public async Task<SaveResult> AddEntity(Quote entity)
        {
            SaveResult saveResult = new SaveResult();
            try
            {
                _context.Quote.Add(entity);

                await _context.SaveChangesAsync();

                if (entity.Id > 0)
                {
                    saveResult.IsSuccess = true;
                    saveResult.Id = entity.Id;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return saveResult;
        }


        public async Task<SaveResult> SaveEntity(QuoteViewModel viewModel)
        {
            SaveResult saveResult = new SaveResult();
                Dictionary<bool, string> dictionary = new Dictionary<bool, string>();

                var entity = new Quote();
                try
                {
                    if (viewModel.Id != 0)
                    {
                        if (_context.Quote.IgnoreQueryFilters().Any(a => a.Id == viewModel.Id))
                        {
                            entity = await _context.Quote.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == viewModel.Id);
                        }
                        entity = viewModel.ToEntity(entity);
                        _context.Quote.Update(entity);
                    }
                    else
                    {
                        entity = viewModel.ToEntity(entity);
                        _context.Quote.Add(entity);
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

        public async Task ProcessOnlinePayment()
        {

            var entityList = this._context.Quote.Include(a=>a.PayFastNotifies).Where(a => a.PayFastNotifies.Any(b => !b.Isprocessed && b.Payment_status == NotifyStatusRef.Complete));

            if (entityList.Any())
            {

                var entity = entityList.First();
                var payFastList = this._context.Quote
                    .Include(a => a.PayFastNotifies)
                    .Include(a => a.QuoteUser)
                    .Where(a => a.PayFastNotifies.Any(b => !b.Isprocessed && b.Payment_status == NotifyStatusRef.Complete)).Select(a => a.PayFastNotifies).First();

                bool isValId = ValIdatePayment(entity, payFastList.First());

                if (isValId)
                {
                    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
                    {

                        int quoteStatusId = this._quoteStatusBL.GetEntityByDiscr(OrderStatusRef.Paid).Id;
                        string fullName = string.Empty;
                        string userCode = string.Empty;

                        if (entity.QuoteUser != null)
                        {
                            var person = entity.QuoteUser;
                            fullName = entity.QuoteUser.FullName;
                        }

                        QuoteViewModel viewModel = new QuoteViewModel
                        {
                            RequiresPayment = true,
                            SessionUserId = entity.CreatedUserId,
                            PaymentDate = DateTime.Now,
                            PaymentReference = entity.QuoteNo,
                            QuoteStatusId = quoteStatusId,
                            FullName = fullName,
                            UserCode = userCode,
                            Id = entity.Id
                        };
                        var payFast = payFastList.First();
                        payFast.Isprocessed = true;
                        SaveResult result = await _payFastNotifyBL.UpdateStatus(payFast);

                        if (result.IsSuccess)
                        {
                            result = await UpdateQuoteStatus(viewModel);
                            if (result.IsSuccess)
                            {
                                scope.Complete();
                            }
                        }
                    }
                }
            }
            else
            {

            }

        }

        public async Task ProcessOnlinePayment(int id)
        {
            var saveResult = new SaveResult();
            var entity = this._context.Quote.Include(a => a.PayFastNotifies).FirstOrDefault(a => a.Id == id);

            if (entity != null)
            {

                //var entity = entityList.First();
                //var payFastList = this._context.Quote
                //    .Include(a => a.PayFastNotifies)
                //    .Include(a => a.QuoteUser)
                //    .Where(a => a.PayFastNotifies.Any(b => !b.Isprocessed && b.Payment_status == NotifyStatusRef.Complete)).Select(a => a.PayFastNotifies).First();

                //bool isValId = ValIdatePayment(entity, payFastList.First());

                //if (isValId)
                //{
                    //using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
                    //{

                        var quoteStatusList =  await this._quoteStatusBL.GetEntityByDiscr(OrderStatusRef.Paid);
                int quoteStatusId = quoteStatusList.Id;
                string fullName = string.Empty;
                        string userCode = string.Empty;

                        if (entity.QuoteUser != null)
                        {
                            var person = entity.QuoteUser;
                            fullName = entity.QuoteUser.FullName;
                        }

                        QuoteViewModel viewModel = new QuoteViewModel
                        {
                            RequiresPayment = true,
                            SessionUserId = entity.CreatedUserId,
                            PaymentDate = DateTime.Now,
                            PaymentReference = entity.QuoteNo,
                            QuoteStatusId = quoteStatusId,
                            FullName = fullName,
                            UserCode = userCode,
                            Id = entity.Id
                        };
                    //var payFast = payFastList.First();
                    //payFast.Isprocessed = true;
                    //SaveResult result = await _payFastNotifyBL.UpdateStatus(payFast);

                    //if (result.IsSuccess)
                    //{
                    saveResult = await UpdateQuoteStatus(viewModel);
                            //if (saveResult.IsSuccess)
                            //{
                            //    scope.Complete();
                            //}
                        //}
                   // }
                }
            //}
            //else
            //{

            //}

        }


        public bool ValIdatePayment(DataAccess.Quote entity, DataAccess.PayFastNotify payFast)
        {
            bool success = true;

            var totalAmount = entity.QuoteDetails.Sum(a => a.SubscriptionTypeRuleAudit.AmountRand * a.Quantity);
            var payFastAmount = entity.PayFastNotifies.Sum(a => a.Amount_gross);

            if (payFastAmount != totalAmount)
            {
                success = false;
            }
            return success;
        }


        public async Task<SaveResult> UpdateQuoteStatus(
            QuoteViewModel model)
        {
            SaveResult saveResult = new SaveResult();
            Dictionary<bool, string> dictionary = new Dictionary<bool, string>();
            var entity = await _context.Quote
                .Include(a=> a.QuoteDetails).ThenInclude(a=>a.SubscriptionTypeRuleAudit.SubscriptionTypeRule.SubscriptionType)
                 .Include(a => a.QuoteDetails).ThenInclude(a => a.Subscriptions)
                .Include(a => a.QuoteStatus.MessageTemplate)
                .Include(a=> a.QuoteUser)
                .IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == model.Id);

            if (model.RequiresPayment)
            {
                entity.PaymentDate = model.PaymentDate;
                entity.PaymentReference = model.PaymentReference;
            }
            else
            {
                entity.PaymentDate = null;
                entity.PaymentReference = null;
            }
            entity.QuoteStatusId = model.QuoteStatusId;
            entity.UpdatedUserId = model.SessionUserId;
            entity.UpdatedTimestamp = DateTime.Now;

            if (entity.QuoteStatus != null)
            {
                model.MessageTemplate = entity.QuoteStatus.MessageTemplate.ToViewModel(new MessageTemplateViewModel());
                model.QuoteStatusRef = entity.QuoteStatus.Discriminator;
            }
            else
            {
                var currentQuoteStatus = await _quoteStatusBL.GetEntityById(model.QuoteStatusId,null);
                model.MessageTemplate = currentQuoteStatus.MessageTemplate;
                model.QuoteStatusRef = currentQuoteStatus.Discriminator;
            }
            try { 
            var hasSubscriptions = entity.QuoteDetails.Any(a => a.Subscriptions.Any());
            if (!hasSubscriptions && model.QuoteStatusRef == OrderStatusRef.Paid)
            {
                var members = _context.MemberMapping.Where(a => a.Member.PersonId == entity.QuoteUser.PersonId).ToList();
                if (members.Any())
                {
                    model.ParentMemberId = members.First().MemberId;
                    model.MemberListIds = members.Select(a => a.RelationMemberId);
                }

                _subscriptionBL.GenerateList(model, entity);
            }

            model.FullName =  entity.QuoteUser.FullName;
            model.Email = entity.QuoteUser.Email;

            _context.Quote.Update(entity);
            await _context.SaveChangesAsync();
            saveResult.IsSuccess = true;



            if (model.QuoteStatusRef == OrderStatusRef.Paid)
            {
                var fullName = entity.QuoteUser.FullName;
                //_queuedEmail.QueueQuote(model.MessageTemplate, entity.QuoteUser.Email, fullName, new byte[0]);
            }
            return saveResult;

        }
            catch (DbUpdateException upDateEx)
            {
                var results = upDateEx.GetSqlerrorNo();
        string msg = results == (int)SqlErrNo.FK ? ConstEntity.MissingValueMsg : ConstEntity.UniqueKeyMsg;
        saveResult = dictionary.GetValidateEntityResults(msg).ToSaveResult();
                throw upDateEx;
            }
            catch (Exception ex)
            {

                saveResult.Message = CrudError.SaveErrorMsg;
                throw ex;
            }

        }



        public async Task<PayFastViewModel> SaveQuoteFromCart(string cartId,string sessionUserId)
        {
            var payFastVieModel = new PayFastViewModel();
            QuoteStatusViewModel quoteStatus = await _quoteStatusBL.GetEntityByDiscr(OrderStatusRef.New);

            var quoteEntity = new DataAccess.Quote {

                QuoteNo = Guid.NewGuid().ToString().Substring(0, 8).ToUpper(),
                QuoteStatusId = quoteStatus.Id,
                FinYearId = _finYearBL.GetCurrentFinYearId(),
                CreatedUserId = sessionUserId,
                QuoteUserId = sessionUserId

            };

     
            SaveResult saveResult = new SaveResult();
            Dictionary<bool, string> dictionary = new Dictionary<bool, string>();

            var cartList =  _context.Cart
                .Include(a=>a.ProductSize.Size)
                .Include(a => a.ProductSize.Product)
                .Include(a => a.Product)
                .Where(a => a.RecordId == cartId);

            try
            {
                this.GenerateOrderDetail(cartList, quoteEntity);


                saveResult = await AddEntity(quoteEntity);

                if (saveResult.IsSuccess)
                {
                    saveResult = await EmptyCart(cartList);
                }
                if (saveResult.IsSuccess)
                {
                    saveResult.Discriminator = quoteEntity.QuoteNo;
                    saveResult.Id = quoteEntity.Id;


                    quoteEntity = await _context.Quote.Include(a => a.QuoteUser).IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == quoteEntity.Id);
                     payFastVieModel = new PayFastViewModel
                    {
                        SaveResult = saveResult,
                        name_first = quoteEntity.QuoteUser.FirstName,
                        name_last = quoteEntity.QuoteUser.Surname,
                        email_address = quoteEntity.QuoteUser.Email,
                        cell_number = quoteEntity.QuoteUser.ContactNo,
                        m_payment_Id = quoteEntity.QuoteNo,
                        amount = cartList.Sum(a => a.Product.Price),
                        item_name = "Order",
                        item_description = string.Empty,
                        custom_int1 = quoteEntity.Id,
                        email_confirmation = 1,
                        confirmation_address = quoteEntity.QuoteUser.Email,
                        signature = string.Empty,
                        FullName = quoteEntity.QuoteUser.FirstName + " " + quoteEntity.QuoteUser.Surname,
                        MessageTemplate = quoteEntity.QuoteStatus.MessageTemplate.ToViewModel(new MessageTemplateViewModel())
                    };

                }
                //if(saveResult.IsSuccess)
                //{
                //    //queuemail
                //    var fullName = quoteEntity.QuoteUser.People.FirstOrDefault().Title.Name + " "  + quoteEntity.QuoteUser.People.FirstOrDefault().FirstName + " " + quoteEntity.QuoteUser.People.FirstOrDefault().Surname;
                //    _queuedEmail.QueueQuote(quoteEntity.QuoteStatu.MessageTemplate, quoteEntity.QuoteUser.Email, fullName,saveResult.ByteArray);
                //}

               
            }
            catch (DbUpdateException upDateEx)
            {
                var results = upDateEx.GetSqlerrorNo();
                string msg = results == (int)SqlErrNo.FK ? ConstEntity.MissingValueMsg : ConstEntity.UniqueKeyMsg;
                saveResult = dictionary.GetValidateEntityResults(msg).ToSaveResult();
                throw upDateEx;
            }
            catch (Exception ex)
            {

                saveResult.Message = CrudError.SaveErrorMsg;
                throw ex;
            }

            return payFastVieModel;
        }

        private void GenerateOrderDetail(
         IEnumerable<Cart> cartList,
         DataAccess.Quote quoteEntity)
        {
            List<DataAccess.OrderDetail> detailList = new List<DataAccess.OrderDetail>();

            foreach (var item in cartList)
            {
                var currentRow = new DataAccess.OrderDetail
                {
                    QuoteId = quoteEntity.Id,
                    UnitPrice = item.Product.Price,
                    ProductSizeId = item.ProductSizeId,
                    Quantity = item.Count,
                    CreatedUserId = quoteEntity.CreatedUserId,
                };

                quoteEntity.OrderDetails.Add(currentRow);
            }
          }

        public async Task<SaveResult> EmptyCart(IEnumerable<Cart> cartList)
        {
            SaveResult saveResult = new SaveResult();
            try
            {
                _context.Cart.RemoveRange(cartList);

                await _context.SaveChangesAsync();

               saveResult.IsSuccess = true;
                return saveResult;

            }
            catch (Exception ex)
            {

                throw;
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
                var entity = await _context.Quote.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == Id);
                _context.Quote.Remove(entity);
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
