using CommunityToolkit.Maui.Views;
using MyShopList.Controls;
using MyShopList.Models;
using MyShopList.Models.ViewModels;

namespace MyShopList.Pages;

public partial class ShoppingListItemsPage : ContentPage
{
    private readonly ShoppingListItemsViewModel _shoppingListItemsViewModel;

    public ShoppingListItemsPage(ShoppingListItemsViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = _shoppingListItemsViewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _shoppingListItemsViewModel.LoadItems();
    }

    private void NewItem_Button_Clicked(object sender, EventArgs e)
    {
        var popup = new ShoppingListItemsPopup(_shoppingListItemsViewModel);

        this.ShowPopupAsync(popup);
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        if (((ShoppingListItemsViewModel)AncestorCollectionView.BindingContext).ShoppingList.IsCompleted)
        {
            return;
        }

        var item = ((Border)sender).BindingContext as ShoppingListItem;

        ((ShoppingListItemsViewModel)AncestorCollectionView.BindingContext).Item = item;

        var popup = new ShoppingListItemsPopup(_shoppingListItemsViewModel);

        popup.IsUpdate = true;

        this.ShowPopupAsync(popup);
    }

    private async void SwipeItem_Clicked(object sender, EventArgs e)
    {
        var item = ((SwipeItem)sender).BindingContext as ShoppingListItem;

        var result = await DisplayAlert("Confimação", "Tem certeza que deseja excluir o item selecionado ?", "Confirmar", "Cancelar");

        if (result == true && item != null)
        {
            await _shoppingListItemsViewModel.DeleteItem(item.Id);
        }
    }

    private async void MarkAsCompleted_Button_Clicked(object sender, EventArgs e)
    {
        var result = await DisplayAlert("Confimação", "Tem certeza que deseja Finalizar esta compra ?", "Confirmar", "Cancelar");

        if (result)
        {
            await _shoppingListItemsViewModel.MarkListAsCompleted();
        }
    }
}