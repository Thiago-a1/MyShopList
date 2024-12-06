using CommunityToolkit.Maui.Views;
using MyShopList.Models.ViewModels;

namespace MyShopList.Controls;

public partial class ShoppingListPopup : Popup
{
    private readonly ShoppingListViewModel _shoppingListViewModel;

    public ShoppingListPopup(ShoppingListViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = _shoppingListViewModel = viewModel;
    }

    private void SaveList_Button_Clicked(object sender, EventArgs e)
    {
        if (ListFormView.IsValidated)
        {
            _shoppingListViewModel.CreateListCommand.Execute(this);

            Close();
        }
    }

    private void CancelList_Button_Clicked(object sender, EventArgs e)
    {
        Close();
    }
}