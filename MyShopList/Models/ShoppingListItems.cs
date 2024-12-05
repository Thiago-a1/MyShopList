namespace MyShopList.Models;
public class ShoppingListItems
{
    public ShoppingListItems() { }

    public ShoppingListItems(string name, decimal amount, decimal price, Guid shopping_Id)
    {
        Name = name;
        Amount = amount;
        Price = price;
        Shopping_Id = shopping_Id;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Amount { get; set; }
    public decimal Price { get; set; }
    public Guid Shopping_Id { get; set; }
}
