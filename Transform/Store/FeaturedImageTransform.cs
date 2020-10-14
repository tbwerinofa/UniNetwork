using BusinessObject;
using DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Transform
{
    public static class FeaturedImageTransform
    {

        /// <summary>
        /// Convert FeaturedImage Object into FeaturedImage Entity
        /// </summary>
        ///<param name="entity">IQueryable DataAccess.FeaturedImage</param>
        ///<returns>IEnumerable FeaturedImage</returns>
        ///
        public static IEnumerable<FeaturedImageViewModel> ToListViewModel(
            this IQueryable<DataAccess.FeaturedImage> entity)
        {
            return entity.ToList().Select(a =>
                        new FeaturedImageViewModel
                        {
                            Product = a.ProductImage.Product.Name,
                            ProductId = a.ProductImage.ProductId,
                            Id = a.Id,
                            FeaturedCategoryId = a.FeaturedCategoryId,
                            ProductImageId = a.ProductImageId,
                            DocumentName = a.ProductImage.Document.Name,
                            DocumentPath = String.Format("data:image/gif;base64,{0}", Convert.ToBase64String(a.ProductImage.Document.DocumentData)),
                            IsFeatured = a.ProductImage.IsFeatured,
                            Price = a.ProductImage.Product.Price,
                            LastUpdate = a.UpdatedTimestamp.HasValue ? a.UpdatedTimestamp.ToCustomLongDate() : a.CreatedTimestamp.ToCustomLongDate(),
                            LastUpdatedUser = a.UpdatedUserId != null ? a.UpdatedUser.FullName : a.CreatedUser.FullName
                        });
        }

        /// <summary>
        /// Convert FeaturedImage Object into FeaturedImage Entity
        /// </summary>
        ///<param name="model">FeaturedImage</param>
        ///<param name="FeaturedImageEntity">DataAccess.FeaturedImage</param>
        ///<returns>DataAccess.FeaturedImage</returns>
        public static DataAccess.FeaturedImage ToEntity(
            this FeaturedImageViewModel model,
            DataAccess.FeaturedImage entity
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
            entity.FeaturedCategoryId = model.FeaturedCategoryId;
            entity.ProductImageId = model.ProductImageId;

            return entity;
        }

        /// <summary>
        /// Convert FeaturedImage Entity  into FeaturedImage Object
        /// </summary>
        ///<param name="FeaturedImageEntity">DataAccess.FeaturedImage</param>
        ///<returns>FeaturedImageViewModel</returns>
        public static FeaturedImageViewModel ToViewModel(
            this DataAccess.FeaturedImage entity)
        {
            return new FeaturedImageViewModel
            {
                SessionUserId = entity.CreatedUserId,
                Id = entity.Id,
                FeaturedCategoryId = entity.FeaturedCategoryId,
            };

        }

    }
}
