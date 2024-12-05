using CommunityToolkit.Maui;
using InputKit.Handlers;
using Microsoft.Extensions.Logging;
using MyShopList.Contexts;
using MyShopList.Models.ViewModels;
using MyShopList.Pages;
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

            builder.Services.AddSingleton<ShoppingListViewModel>();

            builder.Services.AddSingleton<IShoppingListService, ShoppingListService>();

            return builder.Build();
        }
    }
}
