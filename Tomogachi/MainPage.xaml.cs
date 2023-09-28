using Newtonsoft.Json;
using System.ComponentModel;
using System.Timers;

namespace Tomogachi
{
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        int count = 0;
        public string HeaderTitle { get; set; } = "Why hello there";

        private float Hunger { get; set; } = .0f;
        public string HungerText => Hunger switch
        {
            <= 0 => "Not Hungry",
            < .25f => "A little Hungry",
            < .50f => "Moderately Hungry",
            < .75f => "Hungry",
            < 1 => "Hangry",
            >= 1.0f => "Starving",
            _ => throw new ArgumentException("Not Possible", Hunger.ToString())
        };

        public Creature MyCreature { get; set; } = new Creature();

        public string CreatureName => MyCreature.Name;


        public MainPage()
        {
            BindingContext = this;
            InitializeComponent();
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

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            //Navigation.PushAsync(new MainPage());

            //hunger += 1;

            string creatureString = JsonConvert.SerializeObject(MyCreature);
            Console.Write(creatureString);

            Preferences.Set("MyCreature", creatureString);

            var dataStore = DependencyService.Get<IDataStore<Creature>>();
            MyCreature = null; //dataStore.ReadItem();
            if (MyCreature == null)
            {
                MyCreature = new Creature()
                {
                    Name = "Vincent",
                    Hunger = 1.0f,
                    Thirst = 0.1f,
                };
                var result = await dataStore.CreateItem(MyCreature);
                var testItem = dataStore.ReadItem("1");
            }
        }
    }
}