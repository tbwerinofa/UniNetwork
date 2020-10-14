using BusinessObject;
using DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Transform
{
    public static class StockAlertTransform
    {

        /// <summary>
        /// Convert StockAlert Object into StockAlert Entity
        /// </summary>
        ///<param name="entity">IQueryable DataAccess.StockAlert</param>
        ///<returns>IEnumerable StockAlert</returns>
        ///
        public static IEnumerable<StockAlertViewModel> ToListViewModel(
            this IQueryable<DataAccess.StockAlert> entity)
        {
            return entity.ToList().Select(a =>
                        new StockAlertViewModel
                        {
                            Id = a.Id,
                            ProductId = a.Product.Id,
                            ProductSizeId = a.ProductSizeId,
                            Email = a.Email,
                            Size = a.ProductSize == null ? string.Empty :a.ProductSize.Size.ShortName,
                            Product = a.Product.Name,
                            IsAlertSent = a.IsAlertSent,
                            LastUpdate = a.UpdatedTimestamp.HasValue ? a.UpdatedTimestamp.ToCustomLongDate() : a.CreatedTimestamp.ToCustomLongDate(),
                            LastUpdatedUser = a.UpdatedUserId != null ? a.UpdatedUser.FullName : a.CreatedUser.FullName
                        })
                        .OrderBy(e => e.Email);
        }

        /// <summary>
        /// Convert StockAlert Object into StockAlert Entity
        /// </summary>
        ///<param name="model">StockAlert</param>
        ///<param name="StockAlertEntity">DataAccess.StockAlert</param>
        ///<returns>DataAccess.StockAlert</returns>
        public static DataAccess.StockAlert ToEntity(
            this StockAlertViewModel model,
            DataAccess.StockAlert entity)
        {
            if (entity.Id == 0)
            {
                entity.Email = model.Email;
            }
            else
            {
                entity.IsActive = model.IsActive;
                entity.UpdatedTimestamp = DateTime.Now;
            }

            entity.ProductId = model.ProductId;
            entity.IsAlertSent = model.IsAlertSent;

            if (!String.IsNullOrEmpty(model.SessionUserId)) { 
            entity.UserId = model.SessionUserId;
            }

            if(model.ProductSizeId != 0)
            { 
            entity.ProductSizeId = model.ProductSizeId;
            }

            return entity;
        }

        /// <summary>
        /// Convert StockAlert Entity  into StockAlert Object
        /// </summary>
        ///<param name="StockAlertEntity">DataAccess.StockAlert</param>
        ///<returns>StockAlertViewModel</returns>
        public static StockAlertViewModel ToViewModel(
            this DataAccess.StockAlert entity)
        {
            return new StockAlertViewModel
            {
                SessionUserId = entity.UserId,
                Id = entity.Id,
                ProductSizeId = entity.ProductSizeId,
                ProductId = entity.Product.Id,
                Email = entity.Email,
                Size = entity.ProductSize == null ? string.Empty: entity.ProductSize.Size.ShortName,
                Product = entity.Product.Name,
                IsAlertSent = entity.IsAlertSent,
                IsActive = entity.IsActive,

            };

        }

    }
}
