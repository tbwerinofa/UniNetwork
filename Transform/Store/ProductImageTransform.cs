using BusinessObject;
using DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Transform
{
    public static class ProductImageTransform
    {

        /// <summary>
        /// Convert ProductImage Object into ProductImage Entity
        /// </summary>
        ///<param name="entity">IQueryable DataAccess.ProductImage</param>
        ///<returns>IEnumerable ProductImage</returns>
        ///
        public static IEnumerable<ProductImageViewModel> ToListViewModel(
            this IQueryable<DataAccess.ProductImage> entity)
        {
            return entity.ToList().Select(a =>
                        new ProductImageViewModel
                        {
                            ProductCategory = a.Product.ProductCategory.Name,
                            ProductId = a.ProductId,
                            Product = a.Product.Name,
                            Price = a.Product.Price,
                            Hits = a.Product.Hits,
                            Sold = a.Product.Sold,
                            IsFeatured = a.IsFeatured,
                            Id = a.Id,
                            DocumentName = a.Document.Name,
                            DocumentNameGuId = a.Document.DocumentNameGuid,
                           // DocumentPath = String.Format("data:image/gif;base64,{0}", Convert.ToBase64String(a.Document.DocumentData)),
                            Ordinal = a.Ordinal,
                            FeaturedImages = a.FeaturedImages.AsQueryable().ToListViewModel(),
                            ProductCreatedTimestamp = a.Product.CreatedTimestamp,
                            LastUpdate = a.UpdatedTimestamp.HasValue ? a.UpdatedTimestamp.ToCustomLongDate() : a.CreatedTimestamp.ToCustomLongDate(),
                            LastUpdatedUser = a.UpdatedUserId != null ? a.UpdatedUser.FullName : a.CreatedUser.FullName
                        })
                        .OrderBy(e => e.Ordinal)
                        .ThenBy(e => e.DocumentName);
        }

        /// <summary>
        /// Convert ProductImage Object into ProductImage Entity
        /// </summary>
        ///<param name="model">ProductImage</param>
        ///<param name="ProductImageEntity">DataAccess.ProductImage</param>
        ///<returns>DataAccess.ProductImage</returns>
        public static DataAccess.ProductImage ToEntity(
            this ProductImageViewModel model,
            DataAccess.ProductImage entity)
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
            entity.ProductId = model.ProductId;
            entity.DocumentId = model.DocumentId;
            entity.Ordinal = model.Ordinal;
            entity.IsFeatured = model.IsFeatured;

            return entity;
        }

        /// <summary>
        /// Convert ProductImage Entity  into ProductImage Object
        /// </summary>
        ///<param name="ProductImageEntity">DataAccess.ProductImage</param>
        ///<returns>ProductImageViewModel</returns>
        public static ProductImageViewModel ToViewModel(
            this DataAccess.ProductImage entity, ProductImageViewModel viewModel)
        {

            viewModel.SessionUserId = entity.CreatedUserId;
            viewModel.Id = entity.Id;
            viewModel.ProductId = entity.ProductId;
            viewModel.DocumentId = entity.DocumentId;
            viewModel.DocumentNameGuId = entity.Document.DocumentNameGuid;
            viewModel.DocumentName = entity.Document.Name;
            viewModel.IsActive = entity.IsActive;
            viewModel.Ordinal = entity.Ordinal;
            viewModel.IsFeatured = entity.IsFeatured;
            viewModel.FeaturedImages = entity.FeaturedImages.AsQueryable().ToListViewModel();

            return viewModel;
        }

        public static IEnumerable<ProductImageViewModel> ToQueryListViewModel(
         this IQueryable<DataAccess.ProductImage> entity)
        {
            return entity.ToList().Select(a =>
                        new ProductImageViewModel
                        {
                            ProductCategory = a.Product.ProductCategory.Name,
                            ProductId = a.ProductId,
                            Product = a.Product.Name,
                            Price = a.Product.Price,
                            Hits = a.Product.Hits,
                            Sold = a.Product.Sold,
                            IsFeatured = a.IsFeatured,
                            Id = a.Id,
                            DocumentName = a.Document.Name,
                            DocumentNameGuId = a.Document.DocumentNameGuid,
                            //DocumentPath = String.Format("data:image/gif;base64,{0}", Convert.ToBase64String(a.Document.DocumentData)),
                            Ordinal = a.Ordinal,
                           // FeaturedImages = a.FeaturedImages.AsQueryable().ToListViewModel(),
                            ProductCreatedTimestamp = a.Product.CreatedTimestamp,
                            //LastUpdate = a.UpdatedTimestamp.HasValue ? a.UpdatedTimestamp.ToCustomLongDate() : a.CreatedTimestamp.ToCustomLongDate(),
                            //LastUpdatedUser = a.UpdatedUserId != null ? a.UpdatedUser.FullName : a.CreatedUser.FullName
                        })
                        .OrderBy(e => e.Ordinal)
                        .ThenBy(e => e.DocumentName);
        }

    }
}
