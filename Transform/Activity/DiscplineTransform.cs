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
    public static class DiscplineTransform
    {

        /// <summary>
        /// Convert Discpline Object into Discpline Entity
        /// </summary>
        ///<param name="entity">IQueryable DataAccess.Discpline</param>
        ///<returns>IEnumerable Discpline</returns>
        ///
        public static IEnumerable<DiscplineViewModel> ToListViewModel(
            this IQueryable<DataAccess.Discpline> entity)
        {
            return entity
                    .Include(c => c.CreatedUser)
               .Include(c => c.UpdatedUser)
                .AsNoTracking()
                .Select(a =>
                        new DiscplineViewModel
                        {
                            Id = a.Id,
                            Name = a.Name,
                            IsActive = a.IsActive,
                            LastUpdate = a.UpdatedTimestamp.HasValue ? a.UpdatedTimestamp.ToCustomLongDate() : a.CreatedTimestamp.ToCustomLongDate(),
                            LastUpdatedUser = a.UpdatedUserId != null ? a.UpdatedUser.FullName : a.CreatedUser.FullName
                        });
        }

        /// <summary>
        /// Convert Discpline Object into Discpline Entity
        /// </summary>
        ///<param name="model">Discpline</param>
        ///<param name="DiscplineEntity">DataAccess.Discpline</param>
        ///<returns>DataAccess.Discpline</returns>
        public static DataAccess.Discpline ToEntity(this DiscplineViewModel model,
             DataAccess.Discpline entity)
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
        /// Convert Discpline Entity  into Discpline Object
        /// </summary>
        ///<param name="model">DiscplineViewModel</param>
        ///<param name="DiscplineEntity">DataAccess.Discpline</param>
        ///<returns>DiscplineViewModel</returns>
        public static DiscplineViewModel ToViewModel(
         this DataAccess.Discpline entity,
         DiscplineViewModel model)
        {
            model.Id = entity.Id;
            model.Name = entity.Name;
            model.IsActive = entity.IsActive;
            return model;
        }

    }

}
