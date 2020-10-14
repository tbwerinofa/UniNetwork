using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Transform
{
    public static class CartTransform
    {
        public static IEnumerable<CartViewModel> ToListViewModel(
        this IQueryable<DataAccess.Cart> entity)
        {
            return entity.AsEnumerable().Select(a =>
                        new CartViewModel
                        {
                            RecordId = a.Id,
                            CartId = a.RecordId,
                            ProductId = a.ProductId,
                            SizeId = a.ProductSize.SizeId,
                            ProductSizeId = a.ProductSizeId,
                            Count = a.Count,
                            Product = a.Product.ToQueryViewModel(new ProductViewModel()),
                            Size = a.ProductSize.Size.ToViewModel(new SizeViewModel())
                        });
        }
        /// <summary>
        /// Convert ShoppingCartViewModel Object into Cart Entity
        /// </summary>
        ///<param name="model">ShoppingCartViewModel</param>
        ///<param name="RegionEntity">DataAccess.Cart</param>
        ///<returns>DataAccess.Cart</returns>
        public static DataAccess.Cart ToEntity(this CartViewModel model,
            DataAccess.Cart entity)
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

            //entity.Name = model.Name;
            //entity.Description = model.Description;
            //entity.CartCategoryId = model.CartCategoryId;
            //entity.Ordinal = model.Ordinal;
            //entity.Price = model.Price;

            return entity;
        }

        /// <summary>
        /// Convert ShoppingCartViewModel Entity  into Cart Object
        /// </summary>
        ///<param name="model">ShoppingCartViewModel</param>
        ///<param name="RegionEntity">DataAccess.Cart</param>
        ///<returns>ShoppingCartViewModel</returns>
        public static CartViewModel ToViewModel(this DataAccess.Cart entity,
            CartViewModel model)
        {

            model.SessionUserId = entity.CreatedUserId;
            model.CartId = entity.RecordId;
            model.RecordId = entity.Id;
            model.CartId = entity.RecordId;
            model.ProductId = entity.ProductId;
            model.SizeId = entity.ProductSize.SizeId;
            model.ProductSizeId = entity.ProductSizeId;
            model.Count = entity.Count;
            model.Price = entity.Product.Price * entity.Count;
            model.ProductName = entity.Product.Name;

            return model;
        }
    }
}
