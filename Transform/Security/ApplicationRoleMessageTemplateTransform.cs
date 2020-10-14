using BusinessObject.ViewModel;
using DataAccess;
using DomainObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Transform
{
    public static class ApplicationRoleMessageTemplateTransform
    {
        public static ApplicationRoleMessageTemplate ToEntity(this ApplicationRoleMessageTemplate entity, string referralId, int containerId, string sessionUserId)
        {

            if (entity.Id > 0)
            {
                entity.UpdatedUserId = sessionUserId;
                entity.UpdatedTimestamp = DateTime.Now;
            }
            else
            {
                entity.MessageTemplateId = containerId;
                entity.ApplicationRoleId = referralId;
                entity.CreatedUserId = sessionUserId;
            }



            return entity;
        }
    }
}
