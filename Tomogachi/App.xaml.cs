using System.Diagnostics;
using Tomogachi;

namespace Tomogachi
{
    public partial class App : Application
    {
        string ExitedTimeStampKey = "ExitedTimeStamp";


        public App()
        {
            DependencyService.RegisterSingleton<IDataStore<Creature>>(new CreatureDataStore());

            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override void OnSleep()
        {
            Debug.Write("app sleep");
            base.OnSleep();
            {

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
            if (exitTimestampString != null)
            {
                DateTime exitedTimeStamp = DateTime.Parse(exitTimestampString, null, System.Globalization.DateTimeStyles.RoundtripKind);
                TimeSpan elapsedTime = DateTime.Now - exitedTimeStamp;
                Debug.WriteLine(message: "Elapsed Time: " + elapsedTime.ToString());

                //hier kan je iets doen met de elapsed time. bijv een property updaten  
                UpdateCreatureStats(elapsedTime);
            }
        }

        protected override void OnStart()
        {
            base.OnStart();

            string exitTimestampString = Preferences.Get(ExitedTimeStampKey, null);
            Debug.WriteLine("Exited Time: " + exitTimestampString);

            if (exitTimestampString != null)
            {
                DateTime exitedTimeStamp = DateTime.Parse(exitTimestampString, null, System.Globalization.DateTimeStyles.RoundtripKind);
                TimeSpan elapsedTime = DateTime.Now - exitedTimeStamp;
                Debug.WriteLine("Elapsed Time: " + elapsedTime.ToString());

                // Update the creature's stats based on the elapsed time.
                UpdateCreatureStats(elapsedTime);
            }
        }


        private async void UpdateCreatureStats(TimeSpan elapsedTime)
        {
            // Obtain the current creature's stats.
            var dataStore = DependencyService.Get<IDataStore<Creature>>();
            string CreatedCreature = Preferences.Get("CreatureName", null);
            if (CreatedCreature != null)
            {

                Debug.WriteLine("Creature name: " + CreatedCreature);
                var myCreature = await dataStore.ReadItem(CreatedCreature); // Replace with how you fetch your creature.

                // Define decay rates per hour.
                const float hungerDecayPerHour = 0.1f;
                const float thirstDecayPerHour = 0.15f;

                // kijk hoeveel er van af moet. als er een nog op seconde staat dan kan je goed zien of het werkt
                float hungerDecay = (float)elapsedTime.TotalSeconds * hungerDecayPerHour;
                float thirstDecay = (float)elapsedTime.TotalHours * thirstDecayPerHour;

                Debug.WriteLine("hungerdecay" +  hungerDecay);

                //verander de stats
                myCreature.Hunger -= hungerDecay;
                myCreature.Thirst -= thirstDecay;

                // zorg dat ze niet onder de 0 komen
                myCreature.Hunger = Math.Max(0, myCreature.Hunger);
                myCreature.Thirst = Math.Max(0, myCreature.Thirst);

                // Update creature in de database
                dataStore.UpdateItem(myCreature);
            }
        }

    }
}