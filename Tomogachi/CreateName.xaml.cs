namespace Tomogachi;

public partial class CreateName : ContentPage
{
    Creature Creature;
    MainPage mainPage;
	public CreateName(Creature TheCreature, MainPage _mainpage)
	{
        mainPage = _mainpage;
        Creature = TheCreature;
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        string creaturename = Preferences.Get(Creature.Name, null);

            string result = await DisplayPromptAsync("name your Creature", "please input name here:");
            if (!string.IsNullOrEmpty(result))
            {
                Preferences.Set(Creature.Name, result);
                mainPage.MyCreature.Name = result;
                //return to main page
                await Navigation.PopToRootAsync();
            }
    }
}