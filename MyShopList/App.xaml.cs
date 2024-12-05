using MyShopList.Contexts;

namespace MyShopList
{
    public partial class App : Application
    {
        public App(DataContext context)
        {
            InitializeComponent();

            context.Database.EnsureCreated();

            MainPage = new AppShell();
        }
    }
}
