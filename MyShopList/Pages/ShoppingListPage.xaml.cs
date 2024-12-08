using CommunityToolkit.Maui.Views;
using MyShopList.Controls;
using MyShopList.Models;
using MyShopList.Models.ViewModels;

namespace MyShopList.Pages;

public partial class ShoppingListPage : ContentPage
{
    private readonly ShoppingListViewModel _shoppingListViewModel;

    public ShoppingListPage(ShoppingListViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = _shoppingListViewModel = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (_shoppingListViewModel.UpdateScreen == 0)
        {
            _shoppingListViewModel.LoadListsCommand.Execute(null);
        }
    }

    private async void SwipeItem_Clicked(object sender, EventArgs e)
    {
        var list = ((SwipeItem)sender).BindingContext as ShoppingList;

        var result = await DisplayAlert("Confimação", "Tem certeza que deseja excluir a lista selecionado ?", "Confirmar", "Cancelar");

        if (result == true && list != null)
        {
            await _shoppingListViewModel.DeleteList(list);
        }
    }

    private void NewList_Button_Clicked(object sender, EventArgs e)
    {
        var popup = new ShoppingListPopup(_shoppingListViewModel);

        this.ShowPopupAsync(popup);
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        var list = ((Border)sender).BindingContext as ShoppingList;

        if (list == null)
        {
            return;
        }

        Dictionary<string, object> shoppingList = new Dictionary<string, object>
        {
            { "ShoppingList", list }
        };

        await Shell.Current.GoToAsync(nameof(ShoppingListItemsPage), true, shoppingList);
    }
}