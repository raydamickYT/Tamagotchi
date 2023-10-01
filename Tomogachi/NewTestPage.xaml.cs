namespace Tomogachi;

public partial class NewTestPage : ContentPage
{
    public NewTestPage(MainPage mainPage)
    {
        InitializeComponent();
        BindingContext = this;
        HungerText.BindingContext = mainPage;
        HungerBar.BindingContext = mainPage;
        ThirstBar.BindingContext = mainPage;
        ThirstText.BindingContext = mainPage;
    }

    void OnbuttonClicked(object sender, EventArgs e)
    {
        if (MainPage.MyCreature.Hunger - .1f! < 0)
        {
            MainPage.MyCreature.Hunger -= .1f;
        }
    }
}