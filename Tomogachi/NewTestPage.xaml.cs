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

        ProgressBarHealth.ButtonClicked += EntertainmentButton;
        ProgressBarHealth.ButtonClicked2 += FeedingPageButton;
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
            Navigation.PushAsync(pageFactory(mainPage));
        }
    }

    private void FeedingPageButton()
    {
        NavigateToPage(page => new NewTestPage(mainPage));
    }

    private void EntertainmentButton()
    {
        NavigateToPage(page => new Entertainment(mainPage));
    }
    void OnbuttonClicked(object sender, EventArgs e)
    {
        Debug.WriteLine(mainPage.MyCreature.Hunger);
        if ((mainPage.MyCreature.Hunger - .1f) > 0 && (mainPage.MyCreature.Hunger + .1f) <= 1f)
        {
            Debug.WriteLine("works");
            mainPage.MyCreature.Hunger += .1f;
        }
    }
}