using CommunityToolkit.Maui;
using InputKit.Handlers;
using Microsoft.Extensions.Logging;
using MyShopList.Contexts;
using MyShopList.Controls;
using MyShopList.Models.ViewModels;
using MyShopList.Pages;
using MyShopList.Services;
using MyShopList.Sevices;
using UraniumUI;

namespace MyShopList
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseUraniumUI()
                .UseUraniumUIMaterial()
                .ConfigureMauiHandlers(handlers =>
                {
                    handlers.AddInputKitHandlers();
                })
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("Inter-Regular.ttf", "InterRegular");
                    fonts.AddFont("Inter-Semibold.ttf", "InterSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            builder.Services.AddDbContext<DataContext>();

            builder.Services.AddSingleton<ShoppingListPage>();
            builder.Services.AddTransient<ShoppingListItemsPage>();

            builder.Services.AddTransient<ShoppingListPopup>();
            builder.Services.AddTransient<ShoppingListItemsPopup>();

            builder.Services.AddSingleton<ShoppingListViewModel>();
            builder.Services.AddTransient<ShoppingListItemsViewModel>();

            builder.Services.AddSingleton<IShoppingListService, ShoppingListService>();
            builder.Services.AddSingleton<IShoppingListItemsService, ShoppingListItemsService>();

            return builder.Build();
        }
    }
}
