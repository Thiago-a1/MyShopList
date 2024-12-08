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
        var findedList = await _context.ShoppingLists.FirstOrDefaultAsync(x => x.Id == id);
        var findedListItems = await _context.ShoppingListItems.Where(x => x.ShoppingList_Id == id).ToListAsync();

        if (findedList != null)
        {
            _context.ShoppingLists.Remove(findedList);
            _context.ShoppingListItems.RemoveRange(findedListItems);
            await _context.SaveChangesAsync();

            return true;
        }

        return false;
    }

    public Task<ShoppingList> UpdateList(ShoppingList shoppingList)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> SetListAsCompleted(Guid listId, decimal totalAmount, decimal totalPrice)
    {
        var findedList = await _context.ShoppingLists.FirstOrDefaultAsync(x => x.Id == listId);

        if (findedList != null)
        {
            findedList.Completed_At = DateTime.Now;
            findedList.IsCompleted = true;
            findedList.TotalAmount = totalAmount;
            findedList.TotalPrice = totalPrice;

            await _context.SaveChangesAsync();

            return true;
        }

        return false;
    }
}
