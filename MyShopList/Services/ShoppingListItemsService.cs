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

    public async Task<ShoppingListItem?> CreateListItem(ShoppingListItem shoppingListItem)
    {
        var listItem = new ShoppingListItem(shoppingListItem.Name,
                                            shoppingListItem.Amount,
                                            shoppingListItem.Price,
                                            shoppingListItem.ShoppingList_Id);

        await _context.ShoppingListItems.AddAsync(shoppingListItem);
        await _context.SaveChangesAsync();

        return listItem;
    }

    public async Task<bool> DeleteListItem(int Id)
    {
        var findedItem = await _context.ShoppingListItems.FirstOrDefaultAsync(x => x.Id == Id);

        if (findedItem != null)
        {
            _context.ShoppingListItems.Remove(findedItem);
            await _context.SaveChangesAsync();

            return true;
        }

        return false;
    }

    public async Task<ShoppingListItem?> UpdateListItem(ShoppingListItem shoppingListItem)
    {
        var findedItem = await _context.ShoppingListItems.FirstOrDefaultAsync(x => x.Id == shoppingListItem.Id);

        if (findedItem != null)
        {
            findedItem.Name = shoppingListItem.Name;
            findedItem.Amount = shoppingListItem.Amount;
            findedItem.Price = shoppingListItem.Price;

            await _context.SaveChangesAsync();

            return findedItem;
        }

        return findedItem;
    }
}
