using BusinessObject.ViewModel;
using DataAccess;
using DomainObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Transform
{
    public static class TrainingPlanMemberTransform
    {
        public static TrainingPlanMember ToEntity(this TrainingPlanMember entity, int referralId, int containerId, string sessionUserID)
        {

            if (entity.Id > 0)
            {
                entity.UpdatedUserId = sessionUserID;
                entity.UpdatedTimestamp = DateTime.Now;
            }
            else
            {
                entity.TrainingPlanId = containerId;
                entity.MemberId = referralId;
                entity.CreatedUserId = sessionUserID;
            }
            return entity;
        }
    }
}
