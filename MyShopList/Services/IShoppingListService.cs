using MyShopList.Models;

namespace MyShopList.Sevices;
internal interface IShoppingListService
{
    Task<List<ShoppingList>> GetLists();
    Task<ShoppingList> CreateList(ShoppingList shoppingList);
    Task<ShoppingList> UpdateList(ShoppingList shoppingList);
    Task<bool> DeleteList(Guid id);
    Task<bool> SetListAsCompleted(Guid listId, decimal totalAmount, decimal totalPrice);
}

