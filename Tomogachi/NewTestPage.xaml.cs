using System.Diagnostics;

namespace Tomogachi;

public partial class NewTestPage : ContentPage
{
    MainPage mainPage;
    public NewTestPage(MainPage _mainPage)
    {
        InitializeComponent();
        BindingContext = this;
        mainPage = _mainPage;
        HungerBar.BindingContext = _mainPage;
        ThirstBar.BindingContext = _mainPage;
    }

    void OnbuttonClicked(object sender, EventArgs e)
    {
        Debug.WriteLine(mainPage.MyCreature.Hunger);
        if ((mainPage.MyCreature.Hunger - .1f) > 0 && (mainPage.MyCreature.Hunger - .1f) <= .8f)
        {
            mainPage.MyCreature.Hunger += .1f;
        }
    }
}