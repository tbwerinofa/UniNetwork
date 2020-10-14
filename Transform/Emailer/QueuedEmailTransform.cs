using BusinessObject.ViewModel;
using DomainObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Transform
{
    public static class QueuedEmailTransform
    {
        /// <summary>
        /// Convert QueuedEmail Object into QueuedEmail Entity
        /// </summary>
        ///<param name="entity">IQueryable DataAccess.QueuedEmail</param>
        ///<returns>IEnumerable QueuedEmail</returns>
        ///
        public static IEnumerable<QueuedEmailViewModel> ToListViewModel(
            this IQueryable<DataAccess.QueuedEmail> entity)
        {
            return entity
                   .Include(c => c.UpdatedUser)
                       .Include(c => c.CreatedUser)
                    .ToList().Select(a =>
                        new QueuedEmailViewModel
                        {
                            Id = a.Id,
                            Priority = a.Priority,
                            From = a.From,
                            FromName = a.FromName,
                            To = a.To,
                            ToName = a.ToName,
                            ReplyTo = a.ReplyTo,
                            ReplyToName = a.ReplyToName,
                            CC = a.CC,
                            BCC = a.BCC,
                            Subject = a.Subject,
                            Body = a.Body,
                            AttachmentFilePath = a.AttachmentFilePath,
                            AttachmentFileName = a.AttachmentFileName,
                            DontSendBeforeDate = a.DontSendBeforeDate,
                            SentTries = a.SentTries,
                            SentOnString = a.SentOn.ToCustomLongDateTime(),
                            EmailAccountId = a.EmailAccountId,
                            LastUpdate = a.UpdatedTimestamp.HasValue ? a.UpdatedTimestamp.ToCustomLongDate() : a.CreatedTimestamp.ToCustomLongDate(),
                            LastUpdatedUser = a.UpdatedUserId != null ? a.UpdatedUser.FullName : a.CreatedUser.FullName
                        });
        }

        /// <summary>
        /// Convert QueuedEmail Object into QueuedEmail Entity
        /// </summary>
        ///<param name="model">QueuedEmail</param>
        ///<param name="QueuedEmailEntity">DataAccess.QueuedEmail</param>
        ///<returns>DataAccess.QueuedEmail</returns>
        public static DataAccess.QueuedEmail ToEntity(this QueuedEmailViewModel model,
            DataAccess.QueuedEmail entity
          )
        {
            if (entity.Id == 0)
            {

            }
            else
            {
                entity.UpdatedUserId = model.SessionUserId;
                entity.UpdatedTimestamp = DateTime.Now;
            }

            entity.Id = model.Id;
            entity.Priority = model.Priority;
            entity.From = model.From;
            entity.FromName = model.FromName;
            entity.To = model.To;
            entity.ToName = model.ToName;
            entity.ReplyTo = model.ReplyTo;
            entity.ReplyToName = model.ReplyToName;
            entity.CC = model.CC;
            entity.BCC = model.BCC;
            entity.Subject = model.Subject;
            entity.Body = model.Body;
            entity.AttachmentFilePath = model.AttachmentFilePath;
            entity.AttachmentFileName = model.AttachmentFileName;
            entity.DontSendBeforeDate = model.DontSendBeforeDate;
            entity.SentTries = model.SentTries;
            entity.SentOn = model.SentOn;
            entity.EmailAccountId = model.EmailAccountId;

            return entity;
        }


        public static DataAccess.QueuedEmail ToEntity(this
          DataAccess.QueuedEmail entity
        )
        {
            return new DataAccess.QueuedEmail
            {

                Priority = entity.Priority,
                From = entity.From,
                FromName = entity.FromName,
                To = entity.To,
                ToName = entity.ToName,
                ReplyTo = entity.ReplyTo,
                ReplyToName = entity.ReplyToName,
                CC = entity.CC,
                BCC = entity.BCC,
                Subject = entity.Subject,
                Body = entity.Body,
                AttachmentFilePath = entity.AttachmentFilePath,
                AttachmentFileName = entity.AttachmentFileName,
                DontSendBeforeDate = entity.DontSendBeforeDate,
                SentTries = entity.SentTries,
                EmailAccountId = entity.EmailAccountId,
            };
        }

        /// <summary>
        /// Convert QueuedEmail Entity  into QueuedEmail Object
        /// </summary>
        ///<param name="model">QueuedEmailViewModel</param>
        ///<param name="QueuedEmailEntity">DataAccess.QueuedEmail</param>
        ///<returns>QueuedEmailViewModel</returns>
        public static QueuedEmailViewModel ToViewModel(
         this DataAccess.QueuedEmail entity,
         QueuedEmailViewModel model)
        {

            model.Id = entity.Id;
            model.Priority = entity.Priority;
            model.From = entity.From;
            model.FromName = entity.FromName;
            model.To = entity.To;
            model.ToName = entity.ToName;
            model.ReplyTo = entity.ReplyTo;
            model.ReplyToName = entity.ReplyToName;
            model.CC = entity.CC;
            model.BCC = entity.BCC;
            model.Subject = entity.Subject;
            model.Body = entity.Body;
            model.AttachmentFilePath = entity.AttachmentFilePath;
            model.AttachmentFileName = entity.AttachmentFileName;
            model.DontSendBeforeDate = entity.DontSendBeforeDate;
            model.SentTries = entity.SentTries;
            model.SentOn = entity.SentOn;
            model.SentOnString = entity.SentOn.ToCustomLongDateTime();
            model.EmailAccountId = entity.EmailAccountId;
            return model;
        }
    }
}
