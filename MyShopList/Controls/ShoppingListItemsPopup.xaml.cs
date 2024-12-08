using CommunityToolkit.Maui.Views;
using MyShopList.Models;
using MyShopList.Models.ViewModels;

namespace MyShopList.Controls;

public partial class ShoppingListItemsPopup : Popup
{
    public bool IsUpdate = false;

    private readonly ShoppingListItemsViewModel _shoppingListItemsViewModel;

    public ShoppingListItemsPopup(ShoppingListItemsViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = _shoppingListItemsViewModel = viewModel;
    }

    private async void SaveItem_Button_Clicked(object sender, EventArgs e)
    {
        if (ItemFormView.IsValidated)
        {
            if (!IsUpdate)
            {
                await _shoppingListItemsViewModel.CreateItem();
            }
            else
            {
                await _shoppingListItemsViewModel.UpdateItem();
            }

            Close();
        }
    }

    private void CancelItem_Button_Clicked(object sender, EventArgs e)
    {
        Close();

        _shoppingListItemsViewModel.Item = new ShoppingListItem();
    }
}