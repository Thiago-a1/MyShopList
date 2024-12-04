using Microsoft.EntityFrameworkCore;
using MyShopList.Utils;

namespace MyShopList.Contexts;
public class DataContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"FILENAME={DBPath.GetPath()}");
    }
}
