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
using System.Text;
using System.Threading.Tasks;
using Transform;

namespace BusinessLogic.Implementation
{
    public class QueuedEmailBL : IEntityViewLogic<QueuedEmailViewModel>, IQueuedEmailBL
    {
        #region Global fields

        private readonly SqlServerApplicationDbContext _context;
        private readonly IMessageTemplateBL _messageTemplateBL;
        private readonly IEmailerBL _emailerBL;

        #endregion

        #region Constructors
        public QueuedEmailBL(SqlServerApplicationDbContext context,
             IMessageTemplateBL messageTemplateBL,
             IEmailerBL emailerBL)
        {
            _context = context;
            _messageTemplateBL = messageTemplateBL;
            _emailerBL = emailerBL;
        }
        #endregion

        #region Methods

        #region Action Methods

        public SaveResult GenerateRegistrationEmail(
            MemberStagingViewModel model,
            string messageBody)
        {
            SaveResult saveResult = new SaveResult();
            string recipient = model.Title + " " + model.FirstName + " " + model.Surname;

            var subscriptionTypes = _context.SubscriptionTypeRuleAudit.Include(a => a.SubscriptionTypeRule.SubscriptionType).ToList();

            List<string> messageTemplateList = new List<string>();
            messageTemplateList.Add(MessageTemplateConst.AccountConfirmation);
            messageTemplateList.Add(MessageTemplateConst.AccountSubscription);
            messageTemplateList.Add(MessageTemplateConst.AccountCredential);
            var mailTemplates = GetEmailTamplate(messageTemplateList, recipient);

            List<QueuedEmail> msqList = new List<QueuedEmail>();
            foreach (var item in mailTemplates)
            {


                if (item != null)
                {
                    var queueEmail = new QueuedEmail
                    {
                        EmailAccountId = item.EmailAccountId,
                        To = model.Email,
                        Subject = item.Subject,
                        From = item.FromAddress,
                        ToName = recipient,
                        Priority = 5,
                        SentTries = 3,
                        CreatedTimestamp = DateTime.Now,
                        CreatedUserId = model.SessionUserId
                    };

                    if (item.Name == MessageTemplateConst.AccountCredential)
                    {
                        queueEmail.Body = messageBody;
                    }
                    else if(item.Name == MessageTemplateConst.AccountConfirmation)
                    {
                        queueEmail.Body = GenerateBody(item, recipient, null,null);
                    }
                    else
                    {
                        queueEmail.Body = GenerateSubscriptionBody(subscriptionTypes,item, recipient,  model.RequestUrl);
                    }

                    if (item.DelayHours > 0)
                    {
                        queueEmail.DontSendBeforeDate = DateTime.Now.AddHours(item.DelayHours);
                    }

                    msqList.Add(queueEmail);
                }


            }
            if (msqList.Any())
            {
                try { 
                 _context.QueuedEmail.AddRange(msqList);
                _context.SaveChanges();
                saveResult.IsSuccess = true;

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
            else
            {
                saveResult.IsSuccess = true;
            }
            return saveResult;
        }

        private IQueryable<DataAccess.MessageTemplate> GetEmailTamplate(List<string> templateNames, string recipient)
        {
            var templates = _messageTemplateBL.GetRegistrationTemplates(templateNames);

            foreach (var item in templates.ToList())
            {
                item.Body = item.Body.Replace("%Recipient%", recipient);
            }

            return templates;
        }

        private string GenerateBody(
            DataAccess.MessageTemplate messageTemplate,
            string recipient,
            string requestUrl,
            string messagebody)
        {
            string body = messageTemplate.Body.Replace("%Recipient%", recipient);

            if (!String.IsNullOrEmpty(requestUrl))
            {
                body = body.Replace("%Url%", string.Format("<a href='{0}'>Member Application</a>", requestUrl));
            }
            else
            {
                body = body.Replace("%Url%", string.Empty);
            }
                body = body.Replace("%MessageBody%", messagebody);

            return body;
        }

        private string GenerateSubscriptionBody(
            IEnumerable<SubscriptionTypeRuleAudit> subscriptionTypes,
            DataAccess.MessageTemplate messageTemplate,
            string recipient,string url)
        {


                string body = messageTemplate.Body.Replace("%Recipient%", recipient);
            body = body.Replace("%Url%",string.Format("<a href='{0}'>Subscribe</a>", url));

            StringBuilder messageBodyBuilder = new StringBuilder();
            messageBodyBuilder.Append(
                        string.Format("<table><thead><tr><td>Subscription Type</td><td>Months</td><td>Amount</td></tr></thead><tbody>"));

            if (subscriptionTypes.Any())
            {

                foreach (var row in subscriptionTypes)
                {
                    messageBodyBuilder.Append(
                        string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td></tr>",
                            row.SubscriptionTypeRule.SubscriptionType.Name,
                            row.ActiveMonths,
                            row.AmountRand.ToMonetaryValue()));
                }
            }
            else
            {
                messageBodyBuilder.Append(
                  string.Format("<tr><td colspan='3'>No Subscription Records Records</td></tr>"));
            }


            if (!String.IsNullOrEmpty(messageBodyBuilder.ToString()))
            {

                messageBodyBuilder.Append(
                        string.Format("</tbody></table>"));

                body = body.Replace("%MessageBody%", messageBodyBuilder.ToString());
            }
            return body;


        }


        public async Task<SaveResult> GenerateRegistrationNotificationEmail(
            MemberStagingViewModel model,
            string messageTemplateDiscr)
        {
            SaveResult saveResult = new SaveResult();

            var mailTemplate = await _messageTemplateBL.GetEntityByName(messageTemplateDiscr);
            string messageBody = model.IsFinalised ? model.Comments : string.Empty;

            List<QueuedEmail> msqList = new List<QueuedEmail>();


            if (model.NotificationRoles != null && MessageTemplateConst.RegistrationNotification.Equals(messageTemplateDiscr))
            {
                foreach (var recipient in model.NotificationRoles)
                {
                    BuildEmailQueue(mailTemplate,
                        messageBody,
                        msqList,
                        recipient.Email,
                        recipient.FullName,
                        model.RequestUrl,
                        model.SessionUserId);
                }
            }
            else
            {
                BuildEmailQueue(mailTemplate,
                    model.Comments,
                    msqList,
                    model.Email,
                    model.FullName,
                    model.RequestUrl,
                    model.SessionUserId);
            }

            if (msqList.Any())
            {
                try
                {
                    _context.QueuedEmail.AddRange(msqList);
                    _context.SaveChanges();
                    saveResult.IsSuccess = true;

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
            else
            {
                saveResult.IsSuccess = true;
            }
            return saveResult;
        }

        private void BuildEmailQueue(
            MessageTemplate mailTemplate,
            string messageBody,
            List<QueuedEmail> msqList,
            string email,
            string fullName,
            string url,
            string sessionUserId)
        {
            if (mailTemplate != null)
            {
                var queueEmail = new QueuedEmail
                {
                    EmailAccountId = mailTemplate.EmailAccountId,
                    To = email,
                    Subject = mailTemplate.Subject,
                    Body = GenerateBody(mailTemplate, fullName, url, messageBody),
                    From = mailTemplate.FromAddress,
                    ToName = fullName,
                    Priority = 5,
                    SentTries = 3,
                    CreatedTimestamp = DateTime.Now,
                    CreatedUserId = sessionUserId
                };

                if (mailTemplate.DelayHours > 0)
                {
                    queueEmail.DontSendBeforeDate = DateTime.Now.AddHours(mailTemplate.DelayHours);
                }

                msqList.Add(queueEmail);
            }
        }



        #endregion

        #region Read
        public async Task<QueuedEmailViewModel> GetEntityById(
              int? Id, AuthorizationModel model = null)
        {

            var viewModel = new QueuedEmailViewModel();

            if (Id.HasValue)
            {
                var entity = await _context.QueuedEmail
                    .IgnoreQueryFilters()
                    .FirstOrDefaultAsync(a => a.Id == Id);

                viewModel = entity.ToViewModel(viewModel);
            }
 
            return viewModel;
        }


        public ResultSetPage<QueuedEmailViewModel> GetEntityListBySearchParams(
         GridLoadParam param)
        {

            var sortfield = param.SortField ?? "From";
            var propertyInfo = typeof(QueuedEmailViewModel).GetProperty(sortfield);

            var resultSet = _context.QueuedEmail
                 .IgnoreQueryFilters()
                .WhereIf(!String.IsNullOrEmpty(param.SearchTerm), a => a.Subject.Contains(param.SearchTerm))
                .ToListViewModel();

            return param.ToResultSetPage(propertyInfo, resultSet);

        }

        public async Task SendQueuedEmail()
        {
            var queuedEmails = _context.QueuedEmail.Include(a=> a.EmailAccount)
                .Where(a => a.SentOn == null).OrderByDescending(a => a.CreatedTimestamp).Take(5);

            SaveResult saveResult = new SaveResult();
            if (queuedEmails.Any())
            {
                var applicableEmails = queuedEmails.Where(a => a.DontSendBeforeDate == null || a.DontSendBeforeDate <= DateTime.Now);
                foreach (var record in applicableEmails.ToList())
                {
                   saveResult = _emailerBL.SendEmail(record).ToSaveResult();

                    if (saveResult.IsSuccess)
                    {
                        record.SentOn = DateTime.Now;
                        record.UpdatedTimestamp = DateTime.Now;
                        _context.QueuedEmail.Update(record);
                        await _context.SaveChangesAsync();

                    }
                }

            }
        }
        #endregion

        #region Create/Update
        public async Task<SaveResult> SaveEntity(QueuedEmailViewModel viewModel)
        {
            try
            {


                SaveResult saveResult = new SaveResult();

                var entity = new QueuedEmail();

                if (viewModel.Id != 0)
                {
                    if (_context.QueuedEmail.Any(a => a.Id == viewModel.Id))
                    {
                        entity = await _context.QueuedEmail.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == viewModel.Id);
                    }

                    if(viewModel.IsRequeue)
                    {
                        entity = entity.ToEntity();
                        entity.CreatedUserId = viewModel.SessionUserId;
                        _context.QueuedEmail.Add(entity);
                    }
                    else
                    {
                        entity = viewModel.ToEntity(entity);
                        _context.QueuedEmail.Update(entity);
                    }
                   
                }
                else
                {
                    entity = viewModel.ToEntity(entity);
                    _context.QueuedEmail.Add(entity);
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

        public async Task<SaveResult> GenerateAccountEmail(string fullName,string email,string messageBody,string messageTemplate,string sessionUserId)
        {
            try
            {

           
            SaveResult saveResult = new SaveResult();
            string recipient = fullName;
           // var mailTemplate = await _messageTemplateBL.GetEntityByName(messageTemplate);
              var  mailTemplate= await GetEmailTamplate(messageTemplate, recipient,messageBody);

            if(mailTemplate != null)
            { 
            QueuedEmail queueEmail = new QueuedEmail
                    {
                        EmailAccountId = mailTemplate.EmailAccountId,
                        To = email,
                        Subject = mailTemplate.Subject,
                        Body = mailTemplate.Body,
                        From = mailTemplate.FromAddress,
                        ToName = recipient,
                        Priority = 5,
                        SentTries = 3,
                        CreatedTimestamp = DateTime.Now,
                        CreatedUserId = sessionUserId
            };

                    if (mailTemplate.DelayHours > 0)
                    {
                        queueEmail.DontSendBeforeDate = DateTime.Now.AddHours(mailTemplate.DelayHours);
                    }

                await _context.QueuedEmail.AddRangeAsync(queueEmail);
                await _context.SaveChangesAsync();
                saveResult.IsSuccess = true;
            }
            else
            {
                saveResult.IsSuccess = true;
            }
            return saveResult;
            }
            catch (DbUpdateException ex)
            {

                throw ex;
            }
        }


        private async Task<MessageTemplate> GetEmailTamplate(string templateName, string recipient, string messageBody)
        {
            var template = await _messageTemplateBL.GetEntityByName(templateName);

            if(template != null)
            {
                template.Body = template.Body.Replace("%Recipient%", recipient);
                template.Body = template.Body.Replace("%MessageBody%", messageBody);
            }

            return template;
        }



        #endregion

        #region Delete
        public async Task<SaveResult> DeleteEntity(int Id)
        {
            SaveResult resultSet = new SaveResult();

            try
            {
                var entity = await _context.QueuedEmail.IgnoreQueryFilters().FirstOrDefaultAsync(a=> a.Id == Id);
            _context.QueuedEmail.Remove(entity);
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
