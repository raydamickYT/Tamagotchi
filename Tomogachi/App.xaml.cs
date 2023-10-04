using System.Diagnostics;
using Tomogachi;

namespace Tomogachi
{
    public partial class App : Application
    {
        string ExitedTimeStampKey = "ExitedTimeStamp";


        public App()
        {
            DependencyService.RegisterSingleton<IDataStore<Creature>>(new RemoteCreatureDataStore());

            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override void OnSleep()
        {
            Debug.Write("app sleep");
            base.OnSleep();{

            }
            DateTime currentDateTime = DateTime.Now;
            Preferences.Set(ExitedTimeStampKey, currentDateTime.ToString("o")); //o is a round trip format specifier 
        }

        protected override void OnResume()
        {
            Debug.Write("app awake");
            base.OnResume();
            string exitTimestampString = Preferences.Get(ExitedTimeStampKey, null);
            Debug.WriteLine("Exited Time: " + ExitedTimeStampKey);
            if(exitTimestampString != null)
            {
                DateTime exitedTimeStamp = DateTime.Parse(exitTimestampString, null, System.Globalization.DateTimeStyles.RoundtripKind);
                TimeSpan elapsedTime = DateTime.Now - exitedTimeStamp;
                // Debug.WriteLine(message: "Elapsed Time: " + elapsedTime.ToString());
                //hier kan je iets doen met de elapsed time. bijv een property updaten  
            }
        }

        protected override void OnStart()
        {
            base.OnStart();
        }

    }
}