using Newtonsoft.Json;

namespace Tomogachi;

public partial class CreateName : ContentPage
{
    MainPage mainPage;
    public CreateName(MainPage _mainpage)
    {
        mainPage = _mainpage;
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var dataStore = DependencyService.Get<IDataStore<Creature>>();

        //kijk ook hier even of we niet wat gemist hebben, zeker weten dat de creature niet al bestaat
        string existingCreatureName = Preferences.Get("CreatureName", null);
        if (string.IsNullOrEmpty(existingCreatureName))
        {
            //als de creature echt niet bestaat, dan laten we de speler de creature een naam geven
            string result = await DisplayPromptAsync("name your Creature", "please input name here:");
            if (!string.IsNullOrEmpty(result))
            {
                mainPage.MyCreature.Name = result;
                // en maken we hem aan
                var makeCreature = await dataStore.CreateItem(mainPage.MyCreature, mainPage.MyCreature.Name);
                Preferences.Set("CreatureName", result);

                //return to main page
                await Navigation.PopToRootAsync();
            }

        }
        else
        {
            mainPage.MyCreature.Name = existingCreatureName;

            await Navigation.PopToRootAsync();
        }
    }
}