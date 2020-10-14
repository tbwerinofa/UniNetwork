using BusinessObject;
using DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Transform
{
    public static class OrderDetailTransform
    {
        public static IEnumerable<OrderDetailViewModel> ToListViewModel(
        this IQueryable<DataAccess.OrderDetail> entity)
        {
            return entity.AsEnumerable().Select(a =>
                        new OrderDetailViewModel
                        {
                            IsActive = a.IsActive, 
                            Id = a.Id,
                            QuoteId = a.QuoteId,
                            ProductSizeId = a.ProductSizeId,
                            Quantity = a.Quantity,
                            UnitPrice = a.UnitPrice,
                            LastUpdate = a.UpdatedTimestamp.HasValue ? a.UpdatedTimestamp.ToCustomLongDate() : a.CreatedTimestamp.ToCustomLongDate(),
                            LastUpdatedUser = a.UpdatedUserId != null ? a.UpdatedUser.FullName : a.CreatedUser.FullName
                        });
        }
        /// <summary>
        /// Convert OrderDetailViewModel Object into OrderDetail Entity
        /// </summary>
        ///<param name="model">OrderDetailViewModel</param>
        ///<param name="RegionEntity">DataAccess.OrderDetail</param>
        ///<returns>DataAccess.OrderDetail</returns>
        public static DataAccess.OrderDetail ToEntity(this OrderDetailViewModel model,
            DataAccess.OrderDetail entity)
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

            entity.QuoteId = model.QuoteId;
            entity.ProductSizeId = model.ProductSizeId;
            entity.Quantity = model.Quantity;
            entity.UnitPrice = model.UnitPrice;

            return entity;
        }

        /// <summary>
        /// Convert OrderDetailViewModel Entity  into OrderDetail Object
        /// </summary>
        ///<param name="model">OrderDetailViewModel</param>
        ///<param name="RegionEntity">DataAccess.OrderDetail</param>
        ///<returns>OrderDetailViewModel</returns>
        public static OrderDetailViewModel ToViewModel(this DataAccess.OrderDetail entity,
            OrderDetailViewModel model)
        {

            model.SessionUserId = entity.CreatedUserId;
            model.Id = entity.Id;
            model.QuoteId = entity.QuoteId;
            model.Quantity = entity.Quantity;
            model.UnitPrice = entity.UnitPrice;
            model.IsActive = entity.IsActive;
            return model;
        }

        public static IEnumerable<OrderDetailViewModel> ToQueryListViewModel(
   this IQueryable<DataAccess.OrderDetail> entity)
        {
            return entity.AsEnumerable().Select(a =>
                        new OrderDetailViewModel
                        {
                            IsActive = a.IsActive,
                            Id = a.Id,
                            QuoteId = a.QuoteId,
                            ProductSizeId = a.ProductSizeId,
                            Quantity = a.Quantity,
                            UnitPrice = a.UnitPrice,
                        });
        }

        public static OrderDetailViewModel ToQueryViewModel(this DataAccess.OrderDetail entity,
            OrderDetailViewModel model)
        {

            model.SessionUserId = entity.CreatedUserId;
            model.Id = entity.Id;
            model.QuoteId = entity.QuoteId;
            model.Quantity = entity.Quantity;
            model.UnitPrice = entity.UnitPrice;
            model.IsActive = entity.IsActive;
            return model;
        }

    }
}
