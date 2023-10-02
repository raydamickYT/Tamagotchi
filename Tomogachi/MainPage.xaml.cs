using Newtonsoft.Json;
using System.ComponentModel;
using System.Timers;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Tomogachi
{
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        //timer
        private System.Timers.Timer updateTimer;

        App app;

        int count = 0;
        public string HeaderTitle { get; set; } = "Why hello there";

        public Creature MyCreature { get; set; } = new Creature();
        public string HungerText => MyCreature.Hunger switch
        {
            <= 0 => "Not Hungry",
            < .25f => "A little Hungry",
            < .50f => "Moderately Hungry",
            < .75f => "Hungry",
            < 1 => "Hangry",
            >= 1.0f => "Starving",
            _ => throw new ArgumentException("Not Possible", MyCreature.Hunger.ToString())
        };

        public string ThirstText => MyCreature.Thirst switch
        {
            <= 0 => "Not Thirsty",
            < .25f => "A little Thirsty",
            < .50f => "Moderately thristy",
            < .75f => "THIRSTY",
            < 1 => "THARSTY",
            >= 1.0f => "Dehydrated",
            _ => throw new ArgumentException("Not Possible", MyCreature.Thirst.ToString())
        };

        private float _hungerLevel;

        public float HungerLevel
        {
            get => _hungerLevel;
            set
            {
                if (_hungerLevel != value)
                {
                    _hungerLevel = value;
                    OnPropertyChanged();  // This notifies the UI of the change.
                }
            }
        }
        private float _thirstLevel;

        public float ThirstLevel
        {
            get => _thirstLevel;
            set
            {
                if (_thirstLevel != value)
                {
                    _thirstLevel = value;
                    OnPropertyChanged();  // This notifies the UI of the change.
                }
            }
        }

        public string CreatureName => MyCreature.Name;

        public MainPage()
        {
            BindingContext = this;
            InitializeComponent();

            string creatureString = JsonConvert.SerializeObject(MyCreature);

            Preferences.Set("MyCreature", creatureString);

            var dataStore = DependencyService.Get<IDataStore<Creature>>();

            updateTimer = new System.Timers.Timer
            {
                //in ms
                Interval = 1000,
                AutoReset = true
            };

            updateTimer.Elapsed += OnUpdateTimerElapsed;
            updateTimer.Start();

        }

        private void OnUpdateTimerElapsed(object sender, ElapsedEventArgs e)
        {
            //ik wil dat de stat na ongeveer 10 minuten vol is, dus door 1/600 e toe te voegen iedere seconde kom ik daar op uit
            MyCreature.Hunger -= 1f / 600f;
            HungerLevel = MyCreature.Hunger;
            MyCreature.Thirst -= 1 / 600f;
            ThirstLevel = MyCreature.Thirst;
            Debug.WriteLine(ThirstText);

        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            count += 1;

            if (count == 1)
            {
                CounterBtn.Text = $"Clicked {count} time";
            }
            else
            {
                CounterBtn.Text = $"Clicked {count} times";
            }

            //50 is in a unit relative to the device's display.
            await CounterBtn.RelRotateTo(90.0, 1000, Easing.SpringIn);
            await CounterBtn.TranslateTo(.0, 50.0, 1000);
            await CounterBtn.TranslateTo(.0, -50.0, 1000);
            await CounterBtn.ScaleTo(10, 1000, Easing.BounceIn);

            SemanticScreenReader.Announce(CounterBtn.Text);
        }

        void TestButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NewTestPage(this));

        }

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            var dataStore = DependencyService.Get<IDataStore<Creature>>();
            //MyCreature = null; //dataStore.ReadItem();
            //if (MyCreature == null)
            //{
            //    MyCreature = new Creature()
            //    {
            //        Name = "Mannetje",
            //        Hunger = 1.0f,
            //        Thirst = 0.1f,
            //    };
            //    //MyCreature = testItem.Result;

            //    // kom hier later op terug
            //}
            var result = await dataStore.CreateItem(MyCreature);
            var testItem = await dataStore.ReadItem("1");
            Debug.WriteLine(testItem.Name);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}