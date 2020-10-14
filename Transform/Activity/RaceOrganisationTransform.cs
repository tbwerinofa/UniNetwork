using BusinessObject.ViewModel;
using DataAccess;
using DomainObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Transform
{
    public static class RaceOrganisationTransform
    {
        public static RaceOrganisation ToEntity(this RaceOrganisation entity, int referralId, int containerId, string sessionUserID)
        {

            if (entity.Id > 0)
            {
                entity.UpdatedUserId = sessionUserID;
                entity.UpdatedTimestamp = DateTime.Now;
            }
            else
            {
                entity.RaceId = containerId;
                entity.OrganisationId = referralId;
                entity.CreatedUserId = sessionUserID;
            }
            return entity;
        }
    }
}
