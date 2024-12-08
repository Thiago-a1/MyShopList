using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using MyShopList.Pages;

namespace MyShopList
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ShoppingListItemsPage), typeof(ShoppingListItemsPage));
        }

        public static async Task DysplaySnackBar(string message, string type)
        {
            CancellationTokenSource cancellationToken = new CancellationTokenSource();

            var snackbarOptions = new SnackbarOptions
            {
                BackgroundColor = type == "Success" ? Colors.Green : Color.FromRgba("#CC0000"),
                TextColor = Colors.White,
                CornerRadius = new CornerRadius(4),
            };

            var snackbar = Snackbar.Make(message, duration: TimeSpan.FromSeconds(3), visualOptions: snackbarOptions);

            await snackbar.Show(cancellationToken.Token);
        }
    }
}
