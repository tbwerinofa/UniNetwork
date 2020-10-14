using BusinessObject;
using BusinessObject.Component;
using DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Transform
{
    public static class ItemTransform
    {
        public static IEnumerable<ItemViewModel> ToListViewModel(
        this IQueryable<DataAccess.Item> entity)
        {
            return entity.AsEnumerable().Select(a =>
                        new ItemViewModel
                        {

                            Id = a.Id,
                            ProductSizeId = a.ProductSizeId,
                            ProductId = a.ProductSize.ProductId,
                            ProductCategoryId = a.ProductSize.Product.ProductCategoryId,
                            Quantity = a.Quantity,
                            Product = a.ProductSize.Product.Name,
                            ProductCategory = a.ProductSize.Product.ProductCategory.Name,
                            Size = a.ProductSize.Size.Name,
                            ProductSizes = a.ProductSize.Product.ProductSizes.ToSelectListItem(x => x.Size.ShortName, x => x.Id.ToString()),
                            Products = a.ProductSize.Product.ProductCategory.Products.ToSelectListItem(x => x.Name, x => x.Id.ToString()),
                            LastUpdate = a.UpdatedTimestamp.HasValue ? a.UpdatedTimestamp.ToCustomLongDate() : a.CreatedTimestamp.ToCustomLongDate(),
                            LastUpdatedUser = a.UpdatedUserId != null ? a.UpdatedUser.FullName : a.CreatedUser.FullName

                        });
        }
        /// <summary>
        /// Convert ItemViewModel Object into Item Entity
        /// </summary>
        ///<param name="model">ItemViewModel</param>
        ///<param name="RegionEntity">DataAccess.Item</param>
        ///<returns>DataAccess.Item</returns>
        public static DataAccess.Item ToEntity(this ItemViewModel model,
            DataAccess.Item entity
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

            entity.ProductSizeId = model.ProductSizeId;
            entity.Quantity = model.Quantity;

            return entity;
        }

        /// <summary>
        /// Convert ItemViewModel Entity  into Item Object
        /// </summary>
        ///<param name="model">ItemViewModel</param>
        ///<param name="RegionEntity">DataAccess.Item</param>
        ///<returns>ItemViewModel</returns>
        public static ItemViewModel ToViewModel(this DataAccess.Item entity,
            ItemViewModel model)
        {

            model.SessionUserId = entity.CreatedUserId;
            model.Id = entity.Id;
            model.ProductCategoryId = entity.ProductSize.Product.ProductCategoryId;
            model.ProductId = entity.ProductSize.ProductId;
            model.ProductSizeId = entity.ProductSizeId;
            model.Product = entity.ProductSize.Product.Name;
            model.ProductCategory = entity.ProductSize.Product.Name;
            model.Quantity = entity.Quantity;
            model.Size = entity.ProductSize.Size.Name;
            model.ProductSizes = entity.ProductSize.Product.ProductSizes.ToSelectListItem(x => x.Size.ShortName, x => x.Id.ToString());
            model.Products = entity.ProductSize.Product.ProductCategory.Products.ToSelectListItem(x => x.Name, x => x.Id.ToString());
            return model;
        }

        public static IEnumerable<DropDownListItems> ToDropDownListItems(
      this IQueryable<DataAccess.Item> entity)
        {
            return entity.AsEnumerable().Select(a =>
                        new DropDownListItems
                        {
                            Value3 = a.ProductSizeId.ToString(),
                            Text = a.Quantity.ToString(),
                            Value = a.Quantity
                        });
        }

    }
}
