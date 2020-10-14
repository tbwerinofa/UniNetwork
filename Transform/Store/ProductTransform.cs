using BusinessObject;
using DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Transform
{
    public static class ProductTransform
    {
        public static IEnumerable<ProductViewModel> ToListViewModel(
        this IQueryable<DataAccess.Product> entity)
        {
            return entity.AsEnumerable().Select(a =>
                        new ProductViewModel
                        {
                            IsActive = a.IsActive,
                            Id = a.Id,
                            Name = a.Name,
                            Description = a.Description,
                            Price = a.Price,
                            Ordinal = a.Ordinal,
                            ProductCategoryId = a.ProductCategoryId,
                            ProductCategory = a.ProductCategory.Name,
                            ProductImages = a.ProductImages.AsQueryable().ToListViewModel(),
                            LastUpdate = a.UpdatedTimestamp.HasValue ? a.UpdatedTimestamp.ToCustomLongDate() : a.CreatedTimestamp.ToCustomLongDate(),
                            LastUpdatedUser = a.UpdatedUserId != null ? a.UpdatedUser.FullName : a.CreatedUser.FullName
                        });
        }
        /// <summary>
        /// Convert ProductViewModel Object into Product Entity
        /// </summary>
        ///<param name="model">ProductViewModel</param>
        ///<param name="RegionEntity">DataAccess.Product</param>
        ///<returns>DataAccess.Product</returns>
        public static DataAccess.Product ToEntity(this ProductViewModel model,
            DataAccess.Product entity)
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
            entity.ProductCategoryId = model.ProductCategoryId;
            entity.Ordinal = model.Ordinal;
            entity.Price = model.Price;

            return entity;
        }

        /// <summary>
        /// Convert ProductViewModel Entity  into Product Object
        /// </summary>
        ///<param name="model">ProductViewModel</param>
        ///<param name="RegionEntity">DataAccess.Product</param>
        ///<returns>ProductViewModel</returns>
        public static ProductViewModel ToViewModel(this DataAccess.Product entity,
            ProductViewModel model)
        {

            model.SessionUserId = entity.CreatedUserId;
            model.Id = entity.Id;
            model.Name = entity.Name;
            model.Description = entity.Description;
            model.Ordinal = entity.Ordinal;
            model.IsActive = entity.IsActive;
            model.Price = entity.Price;
            model.ProductCategory = entity.ProductCategory.Name;
            model.ProductCategoryId = entity.ProductCategoryId;
            model.ProductImages = entity.ProductImages.AsQueryable().ToListViewModel();
            return model;
        }

        public static IEnumerable<ProductViewModel> ToQueryListViewModel(
   this IQueryable<DataAccess.Product> entity)
        {
            return entity.AsEnumerable().Select(a =>
                        new ProductViewModel
                        {
                            IsActive = a.IsActive,
                            Id = a.Id,
                            Name = a.Name,
                            Description = a.Description,
                            Price = a.Price,
                            Ordinal = a.Ordinal,
                            ProductCategoryId = a.ProductCategoryId,
                            ProductCategory = a.ProductCategory.Name,
                           // ProductImages = a.ProductImages.AsQueryable().ToListViewModel(),
                        });
        }

        public static ProductViewModel ToQueryViewModel(this DataAccess.Product entity,
    ProductViewModel model)
        {

            model.SessionUserId = entity.CreatedUserId;
            model.Id = entity.Id;
            model.Name = entity.Name;
            model.Description = entity.Description;
            model.Ordinal = entity.Ordinal;
            model.IsActive = entity.IsActive;
            model.Price = entity.Price;
            model.ProductCategory = entity.ProductCategory.Name;
            model.ProductCategoryId = entity.ProductCategoryId;
            model.ProductImages = entity.ProductImages.AsQueryable().ToQueryListViewModel();
            return model;
        }

    }
}
