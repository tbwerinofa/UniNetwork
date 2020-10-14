using BusinessObject;
using DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Transform
{
    public static class SizeTransform
    {
        public static IEnumerable<SizeViewModel> ToListViewModel(
           this IQueryable<DataAccess.Size> entity)
        {
            return entity.AsEnumerable().Select(a =>
                        new SizeViewModel
                        {
                            Id = a.Id,
                            Name = a.Name,
                            ShortName = a.ShortName,
                            Ordinal = a.Ordinal,
                            IsActive = a.IsActive,
                            LastUpdate = a.UpdatedTimestamp.HasValue ? a.UpdatedTimestamp.ToCustomLongDate() : a.CreatedTimestamp.ToCustomLongDate(),
                            LastUpdatedUser = a.UpdatedUserId != null ? a.UpdatedUser.FullName : a.CreatedUser.FullName
                        });
        }
        /// <summary>
        /// Convert SizeViewModel Object into Size Entity
        /// </summary>
        ///<param name="model">SizeViewModel</param>
        ///<param name="RegionEntity">DataAccess.Size</param>
        ///<returns>DataAccess.Size</returns>
        public static DataAccess.Size ToEntity(this SizeViewModel model,DataAccess.Size entity
           )
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
            entity.ShortName = model.ShortName;
            entity.Ordinal = model.Ordinal;

            return entity;
        }

        /// <summary>
        /// Convert SizeViewModel Entity  into Size Object
        /// </summary>
        ///<param name="model">SizeViewModel</param>
        ///<param name="RegionEntity">DataAccess.Size</param>
        ///<returns>SizeViewModel</returns>
        public static SizeViewModel ToViewModel(this DataAccess.Size entity,
            SizeViewModel model)
        {

            model.SessionUserId = entity.CreatedUserId;
            model.Id = entity.Id;
            model.Name = entity.Name;
            model.ShortName = entity.ShortName;
            model.Ordinal = entity.Ordinal;
            model.IsActive = entity.IsActive;
            return model;
        }
    }
}
