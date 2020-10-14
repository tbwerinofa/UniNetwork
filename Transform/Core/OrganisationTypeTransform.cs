using BusinessObject.Component;
using BusinessObject.ViewModel;
using DataAccess;
using DomainObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Transform
{
    public static class OrganisationTypeTransform
    {

        /// <summary>
        /// Convert OrganisationType Object into OrganisationType Entity
        /// </summary>
        ///<param name="entity">IQueryable DataAccess.OrganisationType</param>
        ///<returns>IEnumerable OrganisationType</returns>
        ///
        public static IEnumerable<OrganisationTypeViewModel> ToListViewModel(
            this IQueryable<DataAccess.OrganisationType> entity)
        {
            return entity
                    .Include(c => c.CreatedUser)
               .Include(c => c.UpdatedUser)
                .AsNoTracking()
                .Select(a =>
                        new OrganisationTypeViewModel
                        {
                            Id = a.Id,
                            Name = a.Name,
                            IsActive = a.IsActive,
                            LastUpdate = a.UpdatedTimestamp.HasValue ? a.UpdatedTimestamp.ToCustomLongDate() : a.CreatedTimestamp.ToCustomLongDate(),
                            LastUpdatedUser = a.UpdatedUserId != null ? a.UpdatedUser.FullName : a.CreatedUser.FullName
                        });
        }

        /// <summary>
        /// Convert OrganisationType Object into OrganisationType Entity
        /// </summary>
        ///<param name="model">OrganisationType</param>
        ///<param name="OrganisationTypeEntity">DataAccess.OrganisationType</param>
        ///<returns>DataAccess.OrganisationType</returns>
        public static DataAccess.OrganisationType ToEntity(this OrganisationTypeViewModel model,
             DataAccess.OrganisationType entity)
        {
            if (entity.Id == 0)
            {
                entity.CreatedUserId = model.SessionUserId;
                entity.IsActive = model.IsActive;
            }
            else
            {
                entity.UpdatedUserId = model.SessionUserId;
                entity.UpdatedTimestamp = DateTime.Now;
            }
            entity.Name = model.Name;

            return entity;
        }

        /// <summary>
        /// Convert OrganisationType Entity  into OrganisationType Object
        /// </summary>
        ///<param name="model">OrganisationTypeViewModel</param>
        ///<param name="OrganisationTypeEntity">DataAccess.OrganisationType</param>
        ///<returns>OrganisationTypeViewModel</returns>
        public static OrganisationTypeViewModel ToViewModel(
         this DataAccess.OrganisationType entity,
         OrganisationTypeViewModel model)
        {
            model.Id = entity.Id;
            model.Name = entity.Name;
            model.IsActive = entity.IsActive;
            return model;
        }

    }

}
