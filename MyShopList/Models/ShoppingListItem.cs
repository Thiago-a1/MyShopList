namespace MyShopList.Models;
public class ShoppingListItem
{
    public ShoppingListItem() { }

    public ShoppingListItem(string name, decimal amount, decimal price, Guid shoppingList_Id)
    {
        Name = name;
        Amount = amount;
        Price = price;
        ShoppingList_Id = shoppingList_Id;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Amount { get; set; }
    public decimal Price { get; set; }
    public Guid ShoppingList_Id { get; set; }
}
