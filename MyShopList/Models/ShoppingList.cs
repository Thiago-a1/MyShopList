namespace MyShopList.Models;
public class ShoppingList
{
    public ShoppingList() { }

    public ShoppingList(string listName)
    {
        Id = Guid.NewGuid();
        ListName = listName;
        TotalAmount = 0;
        TotalPrice = 0;
        Created_At = DateTime.Now;
        IsCompleted = false;
        ShoppingListItems = new List<ShoppingListItem>();
    }

    public Guid Id { get; set; }
    public string ListName { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime Created_At { get; set; }
    public DateTime Completed_At { get; set; }
    public bool IsCompleted { get; set; }

    public List<ShoppingListItem> ShoppingListItems { get; set; }
}
