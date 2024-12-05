namespace ShopList.Utils;

public static class ServiceHelper
{
    public static TService GetService<TService>()
    {
        return Current.GetService<TService>();
    }

    private static IServiceProvider Current =>
#if ANDROID
    IPlatformApplication.Current.Services;
#elif IOS
    IPlatformApplication.Current.Services;
#endif
}

