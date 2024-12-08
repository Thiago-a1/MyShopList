using MyShopList.Models;

namespace MyShopList.Services;
internal interface IShoppingListItemsService
{
    Task<List<ShoppingListItem>> GetListItems(Guid ListId);
    Task<ShoppingListItem?> CreateListItem(ShoppingListItem shoppingListItem);
    Task<ShoppingListItem?> UpdateListItem(ShoppingListItem shoppingListItem);
    Task<bool> DeleteListItem(int Id);
}
