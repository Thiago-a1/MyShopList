using Microsoft.EntityFrameworkCore;
using MyShopList.Contexts;
using MyShopList.Models;
using ShopList.Utils;

namespace MyShopList.Sevices;
public class ShoppingListService : IShoppingListService
{
    private readonly DataContext _context;

    public ShoppingListService()
    {
        this._context = ServiceHelper.GetService<DataContext>();
    }

    public async Task<List<ShoppingList>> GetLists()
    {
        var response = await _context.ShoppingLists
                                     .AsNoTracking()
                                     .Include(x => x.ShoppingListItems)
                                     .ToListAsync();

        return response;
    }

    public async Task<ShoppingList> CreateList(ShoppingList shoppingList)
    {
        var list = new ShoppingList(shoppingList.ListName);

        await _context.ShoppingLists.AddAsync(list);
        await _context.SaveChangesAsync();

        return new ShoppingList
        {
            Id = list.Id,
            ListName = list.ListName,
            TotalAmount = list.TotalAmount,
            TotalPrice = list.TotalPrice,
            Created_At = list.Created_At,
            IsCompleted = list.IsCompleted,
            ShoppingListItems = list.ShoppingListItems
        };
    }

    public async Task<bool> DeleteList(Guid id)
    {
        var findedList = _context.ShoppingLists.FirstOrDefaultAsync(x => x.Id == id);

        if (findedList != null)
        {
            _context.Remove(findedList);
            await _context.SaveChangesAsync();

            return true;
        }

        return false;
    }

    public Task<ShoppingList> UpdateList(ShoppingList shoppingList)
    {
        throw new NotImplementedException();
    }
}
