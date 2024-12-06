using MyShopList.Models;

namespace MyShopList.Services;
internal interface IShoppingListItemsService
{
    Task<List<ShoppingListItem>> GetListItems(Guid ListId);
    Task<ShoppingListItem> CreateListItems(ShoppingListItem shoppingListItem);
    Task<ShoppingListItem> UpdateListItem(ShoppingListItem shoppingListItem);
    Task<bool> DeleteListItems(int Id);
}
