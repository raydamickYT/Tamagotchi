namespace Tomogachi
{
    public partial class App : Application
    {
        public App()
        {
            DependencyService.RegisterSingleton<IDataStore<Creature>>(new RemoteCreatureDataStore());

            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override void OnSleep()
        {
            base.OnSleep();{

            }
        }
    }
}