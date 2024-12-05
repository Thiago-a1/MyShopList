using Microsoft.EntityFrameworkCore;
using MyShopList.Models;
using MyShopList.Utils;

namespace MyShopList.Contexts;
public class DataContext : DbContext
{
    public DbSet<ShoppingList> ShoppingList { get; set; }
    public DbSet<ShoppingListItems> ShoppingListItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"FILENAME={DBPath.GetPath()}");
    }
}
