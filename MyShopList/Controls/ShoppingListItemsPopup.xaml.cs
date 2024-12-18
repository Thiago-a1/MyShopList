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

    private void ItemAmountInput_Focused(object sender, FocusEventArgs e)
    {
        if (ItemAmountInput.Text.Length <= 0)
            return;

        if (float.Parse(ItemAmountInput.Text) == 0)
        {
            ItemAmountInput.Text = string.Empty;
        }
    }

    private void ItemPriceInput_Focused(object sender, FocusEventArgs e)
    {
        if (ItemPriceInput.Text.Length <= 0)
            return;

        if (float.Parse(ItemPriceInput.Text) == 0)
        {
            ItemPriceInput.Text = string.Empty;
        }
    }
}