using BusinessObject.ViewModel;
using DataAccess;
using DomainObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Transform
{
    public static class TrainingPlanDistanceTransform
    {
        public static TrainingPlanDistance ToEntity(this TrainingPlanDistance entity, int referralId, int containerId, string sessionUserID)
        {

            if (entity.Id > 0)
            {
                entity.UpdatedUserId = sessionUserID;
                entity.UpdatedTimestamp = DateTime.Now;
            }
            else
            {
                entity.TrainingPlanId = containerId;
                entity.DistanceId = referralId;
                entity.CreatedUserId = sessionUserID;
            }
            return entity;
        }
    }
}
