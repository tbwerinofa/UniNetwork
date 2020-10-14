using BusinessLogic.Interface;
using BusinessObject;
using BusinessObject.Component;
using DataAccess;
using DomainObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transform;

namespace BusinessLogic.Implementation
{
    public class CartBL : IEntityViewLogic<CartViewModel>,ICartBL
    {

        #region Global Region
        protected readonly SqlServerApplicationDbContext _context;
        #endregion


        #region Constructors
        public CartBL(SqlServerApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods

        #region Read

        public ResultSetPage<CartViewModel> GetEntityListBySearchParams(
           GridLoadParam paramList)
        {

            var propertyInfo = typeof(CartViewModel).GetProperty(paramList.SortField);

            var resultSet = _context.Cart
                .Include(a=>a.ProductSize.Product.ProductCategory)
                  .Include(a => a.ProductSize.Size)
                 .Include(a => a.UpdatedUser)
                .Include(a => a.CreatedUser)
                 .IgnoreQueryFilters()
                .WhereIf(!String.IsNullOrEmpty(paramList.SearchTerm), a => a.ProductSize.Product.Name.Contains(paramList.SearchTerm))
                .ToListViewModel();

            return paramList.ToResultSetPage(propertyInfo, resultSet);

        }
        public async Task<CartViewModel> GetEntityById(
              int? Id, AuthorizationModel model = null)
        {

            var viewModel = new CartViewModel {
            };

            PopulateDropDowns(viewModel);

            if (Id > 0)
            {
                var entity = await _context.Cart
                    .IgnoreQueryFilters()
                   .Include(a => a.ProductSize.Product.ProductCategory)
                  .Include(a => a.ProductSize.Size)
                      .FirstOrDefaultAsync(a => a.Id == Id);
                if (entity != null)
                {
                    entity.ToViewModel(viewModel);
                }
            }
            return viewModel;
        }

        private void PopulateDropDowns(CartViewModel model)
        {
            //model.ProductCategories = _context.ProductCategory.ToSelectListCart(a => a.Name, x => x.Id.ToString());
            //model.Products = IQueryableExtensions.Default_SelectListCart();
            //model.ProductSizes = IQueryableExtensions.Default_SelectListCart();
        }



        //public IEnumerable<SelectListCart> GetSelectListCarts()
        //{

        //    return _context.Cart
        //        .ToSelectListCart(x => x.ProductSize.Product.Name.ToString(),
        //                                             x => x.Id.ToString());
        //}



        #endregion

        #region Create/Update
        public async Task<SaveResult> SaveEntity(CartViewModel viewModel)
        {
            SaveResult saveResult = new SaveResult();
                Dictionary<bool, string> dictionary = new Dictionary<bool, string>();

                var entity = new Cart();
         
            try
                {

                    if (viewModel.Id != 0)
                    {
                        if (_context.Cart.IgnoreQueryFilters().Any(a => a.Id == viewModel.Id))
                        {
                            entity = await _context.Cart
                            .IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == viewModel.Id);
                        }
                        entity = viewModel.ToEntity(entity);
                       _context.Cart.Update(entity);
                    }
                    else
                    {
                        entity = viewModel.ToEntity(entity);
                        _context.Cart.Add(entity);
                    }

                    await _context.SaveChangesAsync();


                    if (entity.Id > 0)
                    {
                        saveResult.IsSuccess = true;
                        saveResult.Id = entity.Id;
                        viewModel.Id = entity.Id;
                    }
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

        public void AddToCart(string id,
           ProductViewModel product)
        {
            // Get the matching cart and Product instances


            var cartItem = _context.Cart.FirstOrDefault(a => a.RecordId == id && a.ProductId == product.Id);

            if (cartItem == null || cartItem.Count == 0)
            {
                // Create a new cart item if no cart item exists
                cartItem = new DataAccess.Cart
                {
                    ProductId = product.Id,
                    ProductSizeId = product.ProductSizeId,
                    RecordId = id,
                    Count = product.Quantity,
                    CreatedUserId = product.SessionUserId

                };

                _context.Cart.Add(cartItem);

            }
            else
            {
                // If the item does exist in the cart, then add one to the quantity
                cartItem.Count = cartItem.Count + product.Quantity;
                cartItem.UpdatedUserId = product.SessionUserId;
                cartItem.UpdatedTimestamp = DateTime.Now;
                _context.Cart.Update(cartItem);
            }
            _context.SaveChanges();
        }

        public ShoppingCartViewModel GetShoppingCartTotal(
         string cartId)
        {
            // Multiply Design price by count of that Design to get
            // the current price for each of those Designs in the cart
            // sum all Design price totals to get the cart total
            var resultList = _context.Cart
                .Include(a=>a.Product.ProductCategory)
                .Include(a => a.Product.ProductSizes).ThenInclude(b=>b.Size)
                .Include(a => a.Product.ProductImages).ThenInclude(b => b.Document)
                .Include(a => a.Product.ProductImages).ThenInclude(b => b.Product)
                .Where(a => a.RecordId == cartId);
            var viewModel = new ShoppingCartViewModel();

            if (resultList.Count() > 0)
            {
                var data = resultList
                    .ToList()
                    .Select(a => new ShoppingCartViewModel
                    {
                        CartTotal = a.Product.Price * a.Count,
                        VAT = (a.Product.Vat ?? 0) * a.Count,
                        TotalExcludingVAT = ((a.Product.Price) * a.Count) - (a.Product.Vat ?? 0 * a.Count)
                    });


                viewModel = new ShoppingCartViewModel
                {
                    CartTotal = data.Sum(a => a.CartTotal),
                    VAT = data.Sum(a => a.VAT),
                    TotalExcludingVAT = data.Sum(a => a.TotalExcludingVAT),
                    CartItems = resultList.ToListViewModel().ToList(),
                    CartCount = resultList.Sum(a => a.Count)
                };
            }

            return viewModel;

        }


        public int CreateOrder(
          string cartID,
          OrderViewModel order)
        {
            return 1;
        }


        #endregion

        #region Delete
        public async Task<SaveResult> DeleteEntity(int Id)
        {
            SaveResult resultSet = new SaveResult();
            Dictionary<bool, string> dictionary = new Dictionary<bool, string>();
            try
            {
                var entity = await _context.Cart.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == Id);
                _context.Cart.Remove(entity);
                await _context.SaveChangesAsync();

                resultSet.IsSuccess = true;
            }
            catch (DbUpdateException upDateEx)
            {
                var results = upDateEx.GetSqlerrorNo();

                string msg = results == (int)SqlErrNo.FK ? ConstEntity.ForeignKeyDelMsg : CrudError.DeleteErrorMsg;
                resultSet = dictionary.GetValidateEntityResults(msg).ToSaveResult();

            }
            catch (Exception ex)
            {

                resultSet.Message = CrudError.DeleteErrorMsg;
            }
            return resultSet;

        }

        public async Task<int> RemoveFromCart(int entityId)
        {
            // Get the cart
            var cartItem = _context.Cart.FirstOrDefault(a => a.Id == entityId);

            int itemCount = 0;
            if (cartItem != null)
            {
                if (cartItem.Count > 1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                    _context.Cart.Update(cartItem);
                }
                else
                {
                    _context.Cart.Remove(cartItem);
                }
                // Save changes
                await _context.SaveChangesAsync();
            }
            return itemCount;
        }
        #endregion

        #endregion

    }
}
