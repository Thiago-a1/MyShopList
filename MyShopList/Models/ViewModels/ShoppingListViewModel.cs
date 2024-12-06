using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyShopList.Sevices;
using ShopList.Utils;
using System.Collections.ObjectModel;

namespace MyShopList.Models.ViewModels;
public partial class ShoppingListViewModel : ObservableObject
{
    private readonly IShoppingListService _shoppingListService;

    [ObservableProperty]
    private bool _isLoading = false;

    [ObservableProperty]
    private ShoppingList _shoppingList = new ShoppingList();

    [ObservableProperty]
    private ObservableCollection<ShoppingList> _lists = new ObservableCollection<ShoppingList>();

    public ShoppingListViewModel()
    {
        _shoppingListService = ServiceHelper.GetService<IShoppingListService>();

        LoadLists();
    }

    [RelayCommand]
    private async Task LoadLists()
    {
        IsLoading = true;

        try
        {
            var response = await _shoppingListService.GetLists();

            Lists.Clear();

            response.ForEach(list => Lists.Add(list));
        }
        catch (Exception Error)
        {
            Console.WriteLine(Error.Message);
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task CreateList()
    {
        try
        {
            var response = await _shoppingListService.CreateList(ShoppingList);

            if (response != null)
            {
                Lists.Add(response);

                ShoppingList = new ShoppingList();

                await AppShell.DysplaySnackBar("Lista cadastrada com sucesso.", "Success");
            }
        }
        catch (Exception Error)
        {
            Console.WriteLine(Error.Message);

            await AppShell.DysplaySnackBar("Erro ao cadastrar a lista.", "Error");
        }

    }

    [RelayCommand]
    public async Task DeleteList(ShoppingList shoppingList)
    {
        try
        {
            var response = await _shoppingListService.DeleteList(shoppingList.Id);

            if (response)
            {
                var findedList = Lists.FirstOrDefault(list => list.Id == shoppingList.Id);

                Lists.Remove(findedList);

                if (!Lists.Contains(findedList))
                {
                    await AppShell.DysplaySnackBar("Lista excluida com sucesso.", "Success");
                }
            }
        }
        catch (Exception Error)
        {
            Console.WriteLine(Error.Message);

            await AppShell.DysplaySnackBar("Erro ao excluir a lista.", "Error");
        }
    }

}
