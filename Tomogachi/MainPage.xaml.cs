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
        public bool IsSleeping = false;

        App app;

        int count = 0;
        public string HeaderTitle { get; set; } = "Why hello there";

        public Creature MyCreature { get; set; } = new Creature();

        IDataStore<Creature> TestDataStore;

        Creature CreatureFromDatabase;

        public string LonelyLevel => MyCreature.Loneliness switch
        {
            <= 0 => "Lonely",
            < .50f => "Comfortable",
            < 1 => "Overstimulated",
            >= 1.0f => "Idk what comes after overstimulated ",
            _ => throw new ArgumentException("Not Possible", MyCreature.Loneliness.ToString())
        };
        public string EntertainmentLevel => MyCreature.boredom switch
        {
            <= 0 => "Amused",
            < .25f => "Slightly amused",
            < .50f => "Indifferent",
            < .75f => "Slightly Interested",
            < 1 => "Nonchalant",
            >= 1.0f => "Bored",
            _ => throw new ArgumentException("Not Possible", MyCreature.boredom.ToString())
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
            //vergeet deze niet weg te halen
            Preferences.Clear();
            BindingContext = this;
            InitializeComponent();

            //string creatureString = JsonConvert.SerializeObject(MyCreature);

            //Preferences.Set("MyCreature", creatureString);

            var dataStore = DependencyService.Get<IDataStore<Creature>>();

            updateTimer = new System.Timers.Timer
            {
                //in ms
                Interval = 1000,
                AutoReset = true
            };

            updateTimer.Elapsed += OnUpdateTimerElapsed;
            updateTimer.Start();

            ProgressBarHealth.EntertainmentButtonClicked += EntertainmentButton;
            ProgressBarHealth.FeedingButtonClicked += FeedingPageButton;
            ProgressBarHealth.MainPageButtonClicked += MainPageButton;
            ProgressBarHealth.BedRoomButtonClicked += BedRoomButton;

        }
        private async void OnUpdateTimerElapsed(object sender, ElapsedEventArgs e)
        {
            //CreatureFromDatabase = await TestDataStore.ReadItem("2");

            //ik wil dat de stat na ongeveer 10 minuten vol is, dus door 1/600 e toe te voegen iedere seconde kom ik daar op uit
            MyCreature.Hunger -= 1f / 600f;
            HungerLevel = MyCreature.Hunger;
            MyCreature.Thirst -= 1 / 600f;
            ThirstLevel = MyCreature.Thirst;
            MyCreature.boredom -= 1 / 600f;

            if (IsSleeping)
            {
                MyCreature.tired += .1f;
            }
            else
            {
                MyCreature.tired -= .1f;
            }



        }
        private void NavigateToPage<TPage>(Func<MainPage, TPage> pageFactory) where TPage : Page
        {
            var existingPage = Navigation.NavigationStack.OfType<TPage>().FirstOrDefault();
            if (existingPage != null)
            {
                var index = Navigation.NavigationStack
                    .Select((page, idx) => new { page, idx })
                    .FirstOrDefault(x => x.page == existingPage)?.idx;

                if (index.HasValue)
                {
                    // Pop pages until you reach the existing page
                    for (int i = Navigation.NavigationStack.Count - 1; i > index.Value; i--)
                    {
                        Navigation.RemovePage(Navigation.NavigationStack[i]);
                    }
                }
            }
            else
            {
                Navigation.PushAsync(pageFactory(this));
            }
        }

        private void FeedingPageButton()
        {
            NavigateToPage(mainPage => new NewTestPage(mainPage));
        }

        private void EntertainmentButton()
        {
            NavigateToPage(mainPage => new Entertainment(mainPage));
        }

        private void MainPageButton()
        {
            Navigation.PopToRootAsync();
        }

        private void BedRoomButton()
        {
            NavigateToPage(mainpage => new Sleep(mainpage));
        }


        private void OnEntertainmentButtonClicked(object sender, EventArgs e)
        {
            Debug.WriteLine("test");
            Navigation.PushAsync(new Entertainment(this));
        }

        //private async void OnCounterClicked(object sender, EventArgs e)
        //{
        //    count += 1;

        //    if (count == 1)
        //    {
        //        CounterBtn.Text = $"Clicked {count} time";
        //    }
        //    else
        //    {
        //        CounterBtn.Text = $"Clicked {count} times";
        //    }

        //    //50 is in a unit relative to the device's display.
        //    await CounterBtn.RelRotateTo(90.0, 1000, Easing.SpringIn);
        //    await CounterBtn.TranslateTo(.0, 50.0, 1000);
        //    await CounterBtn.TranslateTo(.0, -50.0, 1000);
        //    await CounterBtn.ScaleTo(10, 1000, Easing.BounceIn);

        //    SemanticScreenReader.Announce(CounterBtn.Text);
        //}

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

            var result = await dataStore.CreateItem(MyCreature, MyCreature.Name);
            var testItem = await dataStore.ReadItem(MyCreature.Name);
            Debug.WriteLine(testItem.Name);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var dataStore = DependencyService.Get<IDataStore<Creature>>();
            // string CreatureName = dataStore.Get("creature_name", null);

            if (MyCreature.Name == "Vincent")
            {
                await Navigation.PushAsync(new CreateName(MyCreature, this));
            }
            var result = await dataStore.CreateItem(MyCreature, MyCreature.Name);
            //dataStore.UpdateItem(MyCreature, IsSleeping);
            var ReadName = await dataStore.ReadItem(MyCreature.Name);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}