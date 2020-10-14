using BusinessObject;
using BusinessObject.Component;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Interface
{
    public interface ICartBL
    {
        //IEnumerable<CartViewModel> GetShoppingCarts();

        //IEnumerable<CartViewModel> GetShoppingCartByShoppingCartID(string cartID);

        //CartViewModel GetShoppingCartByID(
        // int id);

        ////SaveResult SaveShoppingCart(ShoppingCartViewModel model);

        ////Dictionary<bool, string> DeleteShoppingCart(string shoppingCartId);

        ////IEnumerable<SelectListItem> GetShoppingCartSelectListItem();

        ////IEnumerable<SelectListItem> GetShoppingCartsByShoppingCartCategroyIDSelectListItem(
        //// int ShoppingCartCategoryID);

        //int GetCount(
        //   string cartID);

        //decimal GetCartTotal(
        //     string cartID);

        Task<CartViewModel> GetEntityById(
         int? Id, AuthorizationModel model = null);

        ShoppingCartViewModel GetShoppingCartTotal(
            string cartId);

        int CreateOrder(
            string cartID,
            OrderViewModel order);

        void AddToCart(string id,
           ProductViewModel product);

        Task<int> RemoveFromCart(int entityId);

        //void MigrateCart(string cartID,
        //    string userName);

    }
}
