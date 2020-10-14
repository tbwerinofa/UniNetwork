using BusinessLogic.Interface;
using BusinessObject;
using BusinessObject.Component;
using DataAccess;
using DomainObjects;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BusinessLogic.Implementation
{
    public class ProductSizeBL : IProductSizeBL
    {
        protected readonly SqlServerApplicationDbContext _context;


        public ProductSizeBL(SqlServerApplicationDbContext context)
        {
            _context = context;

        }


        public async Task<SaveResult> SaveProductSize(
            ProductViewModel parentViewModel)
        {

            SaveResult saveResult = new SaveResult();
            Dictionary<bool, string> dictionary = new Dictionary<bool, string>();


            try
            {
                var designSizCollection = this._context.ProductSize.Where(a => a.ProductId == parentViewModel.Id).ToList();
            if (designSizCollection != null)
            {
                if (parentViewModel.SizesId == null)
                {
                    this._context.ProductSize.RemoveRange(designSizCollection);
                }
                else
                {
                    var nonExistantProductSizes = designSizCollection.Where(a => !parentViewModel.SizesId.Contains(a.SizeId));
                        this._context.ProductSize.RemoveRange(nonExistantProductSizes);
                    }
            }

            if (parentViewModel.SizesId != null)
            {

                foreach (var item in parentViewModel.SizesId)
                {
                    var entity = this._context.ProductSize.Where(a => a.SizeId == item && a.ProductId == parentViewModel.Id).FirstOrDefault();
                    if (entity == null)
                    {
                        entity = new ProductSize();
                        entity.ProductId = parentViewModel.Id;
                        entity.SizeId = item;
                        entity.CreatedUserId = parentViewModel.SessionUserId;
                        AddDefaultStockItem(entity);
                        _context.ProductSize.Add(entity);
                    }
                    else
                    {
                        entity.UpdatedTimestamp = DateTime.Now;
                        entity.UpdatedUserId = parentViewModel.SessionUserId;

                        _context.ProductSize.Update(entity);
                    }
                }
            }

                await _context.SaveChangesAsync();
                saveResult.IsSuccess = true;
               }
                catch (DbUpdateException upDateEx)
                {
                    var results = upDateEx.GetSqlerrorNo();
                    string msg = results == (int)SqlErrNo.FK ? ConstEntity.MissingValueMsg : ConstEntity.UniqueKeyMsg;
                    saveResult = dictionary.GetValidateEntityResults(msg).ToSaveResult();
                }
                catch (Exception ex)
                {

                    saveResult.Message = CrudError.SaveErrorMsg;
                }


                return saveResult;
            }

        private void AddDefaultStockItem(ProductSize entity)
        {

                var stockItem = new DataAccess.Item
                {
                    ProductSizeId = entity.Id,
                    Quantity = 0,
                    CreatedUserId = entity.CreatedUserId
                };

            entity.Items.Add(stockItem);
        }

        public IEnumerable<SelectListItem> GetSelectListItems()
        {
            return this._context.ProductSize
                .ToSelectListItem(x => x.Size.ShortName, x => x.Id.ToString());
        }

        public IEnumerable<SelectListItem> GetSelectListItemByParentId(
          int productId)
        {
            return this._context.ProductSize.Include(a=>a.Size).Where(a=> a.ProductId == productId)
                .ToSelectListItem(x => x.Size.ShortName, x => x.SizeId.ToString());
        }

        public IQueryable<DataAccess.ProductSize> GetProductSizesByProductId(
           int productId)
        {
            return this._context.ProductSize.Include(a => a.Size).Where(a => a.ProductId == productId);
        }

    }
}
