using Microsoft.EntityFrameworkCore;
using MyShopList.Contexts;
using MyShopList.Models;
using ShopList.Utils;

namespace MyShopList.Services;
public class ShoppingListItemsService : IShoppingListItemsService
{
    private readonly DataContext _context;

    public ShoppingListItemsService()
    {
        _context = ServiceHelper.GetService<DataContext>();
    }
    public async Task<List<ShoppingListItem>> GetListItems(Guid ListId)
    {
        var response = await _context.ShoppingListItems
                               .Where(x => x.ShoppingList_Id == ListId)
                               .AsNoTracking()
                               .ToListAsync();

        return response;
    }

    public async Task<ShoppingListItem> CreateListItems(ShoppingListItem shoppingListItem)
    {
        var listItem = new ShoppingListItem(shoppingListItem.Name,
                                            shoppingListItem.Amount,
                                            shoppingListItem.Price,
                                            shoppingListItem.ShoppingList_Id);

        await _context.ShoppingListItems.AddAsync(shoppingListItem);
        await _context.SaveChangesAsync();

        return listItem;
    }

    public Task<bool> DeleteListItems(int Id)
    {
        throw new NotImplementedException();
    }


    public Task<ShoppingListItem> UpdateListItem(ShoppingListItem shoppingListItem)
    {
        throw new NotImplementedException();
    }
}
