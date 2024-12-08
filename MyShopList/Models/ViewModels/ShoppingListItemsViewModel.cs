using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyShopList.Services;
using MyShopList.Sevices;
using ShopList.Utils;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace MyShopList.Models.ViewModels;

[QueryProperty(nameof(ShoppingList), "ShoppingList")]
public partial class ShoppingListItemsViewModel : ObservableObject
{
    private readonly IShoppingListItemsService _shoppingListItemsService;
    private readonly IShoppingListService _shoppingListService;

    //[ObservableProperty]
    //private bool _isUpdate = false;

    [ObservableProperty]
    private bool _isLoadingItems = false;

    [ObservableProperty]
    private ShoppingList _shoppingList;

    [ObservableProperty]
    private ObservableCollection<ShoppingListItem> _listItems = new ObservableCollection<ShoppingListItem>();

    [ObservableProperty]
    private ShoppingListItem _item = new ShoppingListItem();

    [ObservableProperty]
    private decimal _totalPrice = 0;

    [ObservableProperty]
    private decimal _totalAmount = 0;

    public ShoppingListItemsViewModel()
    {
        _shoppingListItemsService = ServiceHelper.GetService<IShoppingListItemsService>();
        _shoppingListService = ServiceHelper.GetService<IShoppingListService>();

        ListItems.CollectionChanged += OnCollectionChanged;
    }

    private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        TotalAmount = ListItems.Aggregate((decimal)0.00, (acc, item) => (acc + item.Amount));
        TotalPrice = ListItems.Aggregate((decimal)0.00, (acc, item) => (acc + (item.Price * item.Amount)));
    }

    [RelayCommand]
    public async Task LoadItems()
    {
        IsLoadingItems = true;

        try
        {
            ListItems.Clear();

            var items = await _shoppingListItemsService.GetListItems(ShoppingList.Id);

            items.ForEach(item => ListItems.Add(item));

            await Task.Delay(1000);
        }
        catch (Exception Error)
        {
            Console.WriteLine(Error.Message);
        }
        finally
        {
            IsLoadingItems = false;
        }
    }

    [RelayCommand]
    public async Task CreateItem()
    {
        try
        {
            Item.ShoppingList_Id = ShoppingList.Id;

            var result = await _shoppingListItemsService.CreateListItem(Item);

            if (result != null)
            {
                ListItems.Add(result);

                Item = new ShoppingListItem();

                await AppShell.DysplaySnackBar("Item criado com sucesso.", "Success");
            }
        }
        catch (Exception Error)
        {
            Console.WriteLine(Error.Message);

            await AppShell.DysplaySnackBar("Erro ao criar o item.", "Error");
        }
    }

    [RelayCommand]
    public async Task UpdateItem()
    {
        try
        {
            var result = await _shoppingListItemsService.UpdateListItem(Item);

            if (result != null)
            {
                var findedItem = ListItems.FirstOrDefault(x => x.Id == Item.Id);

                var itemIndex = ListItems.IndexOf(findedItem);

                ListItems[itemIndex] = Item;

                Item = new ShoppingListItem();

                await AppShell.DysplaySnackBar("Item atualizado com sucesso.", "Success");
            }
        }
        catch (Exception Error)
        {
            Console.WriteLine(Error.Message);

            await AppShell.DysplaySnackBar("Erro ao atualizar o item.", "Error");
        }
    }

    [RelayCommand]
    public async Task DeleteItem(int Id)
    {
        try
        {
            var result = await _shoppingListItemsService.DeleteListItem(Id);

            if (result == true)
            {
                var findedItem = ListItems.FirstOrDefault(x => x.Id == Id);

                if (findedItem != null)
                {
                    ListItems.Remove(findedItem);

                    await AppShell.DysplaySnackBar("Item excluido com sucesso.", "Success");
                }
            }
        }
        catch (Exception Error)
        {
            Console.WriteLine(Error.Message);

            await AppShell.DysplaySnackBar("Erro ao excluir o item.", "Error");
        }
    }

    [RelayCommand]
    public async Task MarkListAsCompleted()
    {
        try
        {
            var result = await _shoppingListService.SetListAsCompleted(ShoppingList.Id, TotalAmount, TotalPrice);

            if (result == true)
            {
                Dictionary<string, object> UpdateScreen = new Dictionary<string, object>
                {
                    { "UpdateScreen", 0 }
                };

                await Shell.Current.GoToAsync("../", true, UpdateScreen);

                await AppShell.DysplaySnackBar("Compra Finalizada.", "Success");
            }
        }
        catch (Exception Error)
        {
            Console.WriteLine(Error.Message);

            await AppShell.DysplaySnackBar("Erro ao finalizar compra.", "Error");
        }
    }

    [RelayCommand]
    public static async Task BackButton()
    {
        Dictionary<string, object> UpdateScreen = new Dictionary<string, object>
        {
            { "UpdateScreen", 1 }
        };

        await Shell.Current.GoToAsync("../", true, UpdateScreen);
    }

}
