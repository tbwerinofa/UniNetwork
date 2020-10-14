using BusinessObject;
using DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Transform
{
    public static class SortCategoryTransform
    {
        /// <summary>
        /// Convert SortCategory Object into SortCategory Entity
        /// </summary>
        ///<param name="entity">IQueryable DataAccess.SortCategory</param>
        ///<returns>IEnumerable SortCategory</returns>
        ///
        public static IEnumerable<SortCategoryViewModel> ToListViewModel(
            this IQueryable<DataAccess.SortCategory> entity)
        {


            return entity.Select(a =>
                        new SortCategoryViewModel
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Discriminator = a.Discriminator,
                            Ordinal = a.Ordinal,
                            SessionUserId = a.CreatedUserId,
                            IsActive = a.IsActive,
                            LastUpdate = a.UpdatedTimestamp.HasValue ? a.UpdatedTimestamp.ToCustomLongDate() : a.CreatedTimestamp.ToCustomLongDate(),
                            LastUpdatedUser = a.UpdatedUserId != null ? a.UpdatedUser.FullName : a.CreatedUser.FullName
                        }).AsEnumerable();
        }

        /// <summary>
        /// Convert SortCategory Object into SortCategory Entity
        /// </summary>
        ///<param name="model">SortCategory</param>
        ///<param name="SortCategoryEntity">DataAccess.SortCategory</param>
        ///<returns>DataAccess.SortCategory</returns>
        public static DataAccess.SortCategory ToEntity(this SortCategoryViewModel model,
            DataAccess.SortCategory entity)
        {
            if (entity.Id == 0)
            {
                entity.CreatedUserId = model.SessionUserId;
            }
            else
            {
                entity.UpdatedUserId = model.SessionUserId;
                entity.UpdatedTimestamp = DateTime.Now;
            }
            entity.Name = model.Name;
            entity.Discriminator = model.Discriminator;
            entity.Ordinal = model.Ordinal;

            return entity;
        }

        /// <summary>
        /// Convert SortCategory Entity  into SortCategory Object
        /// </summary>
        ///<param name="model">SortCategoryViewModel</param>
        ///<param name="SortCategoryEntity">DataAccess.SortCategory</param>
        ///<returns>SortCategoryViewModel</returns>
        public static SortCategoryViewModel ToViewModel(
         this DataAccess.SortCategory entity,
         SortCategoryViewModel model)
        {

            model.SessionUserId = entity.CreatedUserId;
            model.Id = entity.Id;
            model.Name = entity.Name;
            model.Discriminator = entity.Discriminator;
            model.Ordinal = entity.Ordinal;
            return model;
        }
    }
}
