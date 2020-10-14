using BusinessObject;
using DomainObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Transform
{
    public static class FeaturedCategoryTransform
    {
        public static IEnumerable<FeaturedCategoryViewModel> ToListViewModel(
this IQueryable<DataAccess.FeaturedCategory> entity)
        {
            return entity
                    .Include(a => a.FeaturedImages).ThenInclude(b => b.ProductImage.Product)
                   .Include(a => a.FeaturedImages).ThenInclude(b => b.ProductImage.Document)
                   .Include(a => a.FeaturedImages).ThenInclude(b => b.UpdatedUser)
                   .Include(a => a.FeaturedImages).ThenInclude(b => b.CreatedUser)
                .Include(a => a.UpdatedUser)
                .Include(a => a.CreatedUser)
                .AsEnumerable().Select(a =>
                        new FeaturedCategoryViewModel
                        {
                            IsActive = a.IsActive,
                            Id = a.Id,
                            Name = a.Name,
                            Ordinal = a.Ordinal,
                            FeaturedImages = a.FeaturedImages.AsQueryable().ToListViewModel(),
                            LastUpdate = a.UpdatedTimestamp.HasValue ? a.UpdatedTimestamp.ToCustomLongDate() : a.CreatedTimestamp.ToCustomLongDate(),
                            LastUpdatedUser = a.UpdatedUserId != null ? a.UpdatedUser.FullName : a.CreatedUser.FullName
                        });
        }
        /// <summary>
        /// Convert FeaturedCategoryViewModel Object into FeaturedCategory Entity
        /// </summary>
        ///<param name="model">FeaturedCategoryViewModel</param>
        ///<param name="RegionEntity">DataAccess.FeaturedCategory</param>
        ///<returns>DataAccess.FeaturedCategory</returns>
        public static DataAccess.FeaturedCategory ToEntity(this FeaturedCategoryViewModel model,
            DataAccess.FeaturedCategory entity)
        {
            if (entity.Id == 0)
            {
                entity.CreatedUserId = model.SessionUserId;
            }
            else
            {
                entity.IsActive = model.IsActive;
                entity.UpdatedUserId = model.SessionUserId;
                entity.UpdatedTimestamp = DateTime.Now;
            }

            entity.Name = model.Name;
            entity.Ordinal = model.Ordinal;

            return entity;
        }

        /// <summary>
        /// Convert FeaturedCategoryViewModel Entity  into FeaturedCategory Object
        /// </summary>
        ///<param name="model">FeaturedCategoryViewModel</param>
        ///<param name="RegionEntity">DataAccess.FeaturedCategory</param>
        ///<returns>FeaturedCategoryViewModel</returns>
        public static FeaturedCategoryViewModel ToViewModel(this DataAccess.FeaturedCategory entity,
            FeaturedCategoryViewModel model)
        {

            model.SessionUserId = entity.CreatedUserId;
            model.Id = entity.Id;
            model.Name = entity.Name;
            model.Ordinal = entity.Ordinal;
            model.IsActive = entity.IsActive;
            return model;
        }
    }
}
