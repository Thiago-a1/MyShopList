namespace MyShopList.Utils;
public static class DBPath
{
    public static string GetPath()
    {
        string pathDB = string.Empty;

        if (DeviceInfo.Platform == DevicePlatform.Android)
        {
            pathDB = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            pathDB = Path.Combine(pathDB, "MyDB");
        }
        else if (DeviceInfo.Platform == DevicePlatform.iOS)
        {
            pathDB = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            pathDB = Path.Combine(pathDB, "..", "Library", "MyDB");
        }
        return pathDB;
    }
}
