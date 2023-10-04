namespace Tomogachi;

public partial class CreateName : ContentPage
{
	public CreateName()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        string creaturename = Preferences.Get("Creature_name", null);

        if (string.IsNullOrEmpty(creaturename))
        {
            string result = await DisplayPromptAsync("name your Creature", "please input name here:");
            if (!string.IsNullOrEmpty(result))
            {
                Preferences.Set("creature_name", result);

                //return to main page
                Navigation.PopToRootAsync();
            }
        }
    }
}