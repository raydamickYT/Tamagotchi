namespace Tomogachi
{
    public partial class App : Application
    {
        string ExitedTimeStamp = string.Empty;
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
            DateTime currentDateTime = DateTime.Now;
            Preferences.Set(ExitedTimeStamp, currentDateTime.ToString("o")); //o is a round trip format specifier
        }

        protected override void OnResume()
        {
            base.OnResume();
            string exitTimestampString = Preferences.Get(ExitedTimeStamp, null);
            if(exitTimestampString != null)
            {
                DateTime exitedTimeStamp = DateTime.Parse(exitTimestampString, null, System.Globalization.DateTimeStyles.RoundtripKind);
                TimeSpan elapsedTime = DateTime.Now - exitedTimeStamp;

                //hier kan je iets doen met de elapsed time. bijv een property updaten
            }
        }
    }
}