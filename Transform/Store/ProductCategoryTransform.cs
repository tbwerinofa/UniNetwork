using BusinessObject;
using DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Transform
{
    public static class ProductCategoryTransform
    {
        public static IEnumerable<ProductCategoryViewModel> ToListViewModel(
this IQueryable<DataAccess.ProductCategory> entity)
        {
            return entity.AsEnumerable().Select(a =>
                        new ProductCategoryViewModel
                        {
                            IsActive = a.IsActive,
                            Id = a.Id,
                            Name = a.Name,
                            Description = a.Description,
                            Products = a.Products.AsQueryable().ToListViewModel(),
                            Ordinal = a.Ordinal,
                            LastUpdate = a.UpdatedTimestamp.HasValue ? a.UpdatedTimestamp.ToCustomLongDate() : a.CreatedTimestamp.ToCustomLongDate(),
                            LastUpdatedUser = a.UpdatedUserId != null ? a.UpdatedUser.FullName : a.CreatedUser.FullName
                        });
        }
        /// <summary>
        /// Convert ProductCategoryViewModel Object into ProductCategory Entity
        /// </summary>
        ///<param name="model">ProductCategoryViewModel</param>
        ///<param name="RegionEntity">DataAccess.ProductCategory</param>
        ///<returns>DataAccess.ProductCategory</returns>
        public static DataAccess.ProductCategory ToEntity(this ProductCategoryViewModel model,DataAccess.ProductCategory entity)
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
            entity.Description = model.Description;
            entity.Ordinal = model.Ordinal;

            return entity;
        }

        /// <summary>
        /// Convert ProductCategoryViewModel Entity  into ProductCategory Object
        /// </summary>
        ///<param name="model">ProductCategoryViewModel</param>
        ///<param name="RegionEntity">DataAccess.ProductCategory</param>
        ///<returns>ProductCategoryViewModel</returns>
        public static ProductCategoryViewModel ToViewModel(this DataAccess.ProductCategory entity,
            ProductCategoryViewModel model)
        {

            model.SessionUserId = entity.CreatedUserId;
            model.Id = entity.Id;
            model.Name = entity.Name;
            model.Description = entity.Description;
            model.Ordinal = entity.Ordinal;
            model.IsActive = entity.IsActive;
            return model;
        }

        public static IEnumerable<ProductCategoryViewModel> ToQueryListViewModel(
this IQueryable<DataAccess.ProductCategory> entity)
        {
            return entity.AsEnumerable().Select(a =>
                        new ProductCategoryViewModel
                        {
                            IsActive = a.IsActive,
                            Id = a.Id,
                            Name = a.Name,
                            Description = a.Description,
                            Products = a.Products.AsQueryable().ToQueryListViewModel(),
                            Ordinal = a.Ordinal                        });
        }
    }
}
