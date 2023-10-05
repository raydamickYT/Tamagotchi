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

        ProgressBarHealth.EntertainmentButtonClicked += EntertainmentButton;
        ProgressBarHealth.FeedingButtonClicked += FeedingPageButton;
        ProgressBarHealth.MainPageButtonClicked += MainPageButton;
        ProgressBarHealth.BedRoomButtonClicked += BedRoomButton;
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
        NavigateToPage(page => new NewTestPage(page));
    }

    private void EntertainmentButton()
    {
        NavigateToPage(page => new Entertainment(page));
    }
    private void MainPageButton()
    {
        //NavigateToPage(mainpage => new MainPage());
        Navigation.PopToRootAsync();

    }
    private void BedRoomButton()
    {
        NavigateToPage(mainpage => new Sleep(mainpage));
    }
    void OnbuttonClicked(object sender, EventArgs e)
    {
        Debug.WriteLine(mainPage.MyCreature.Hunger);
        mainPage.MyCreature.Hunger += .1f;
        mainPage.MyCreature.Hunger = Math.Min(mainPage.MyCreature.Hunger, 1f);
    }

    void OnDrinkingbuttonClicked(object sender, EventArgs e)
    {
        Debug.WriteLine(mainPage.MyCreature.Thirst);
        mainPage.MyCreature.Thirst += .1f;
        mainPage.MyCreature.Thirst = Math.Min(mainPage.MyCreature.Thirst, 1f);
    }
}