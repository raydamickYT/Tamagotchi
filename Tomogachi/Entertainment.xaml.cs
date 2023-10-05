using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Timers;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace Tomogachi;

public partial class Entertainment : ContentPage, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    MainPage mainPage;
    private bool isEntertained = false;

    private System.Timers.Timer updateTimer;

    private float _boredomLevel = 0;
    public float BoredomLevel
    {
        get => _boredomLevel;
        set
        {
            if (_boredomLevel != value)
            {
                _boredomLevel = value;
                OnPropertyChanged();
            }
        }
    }

    #region BallPhysics Variables
    private double xVelocity = 1;
    private double yVelocity = 1;

    private double xLastPanStarted = 0;
    private double yLastPanStarted = 0;

    #endregion

    public Entertainment(MainPage _mainPage)
    {
        this.mainPage = _mainPage;
        InitializeComponent();
        BindingContext = this;
        Entertainmentlabel.BindingContext = mainPage;

        ProgressBarHealth.EntertainmentButtonClicked += EntertainmentButton;
        ProgressBarHealth.MainPageButtonClicked += MainPageButton;
        ProgressBarHealth.FeedingButtonClicked += FeedingPageButton;
        ProgressBarHealth.BedRoomButtonClicked += BedRoomButton;
        updateTimer = new System.Timers.Timer
        {
            //in ms
            Interval = 1000,
            AutoReset = true
        };
        updateTimer.Elapsed += OnTimerFinish;
        updateTimer.Start();

        #region BallPhysics Initialization
        var panGesture = new PanGestureRecognizer();
        panGesture.PanUpdated += PanGesturePanUpdated;
        Ball.GestureRecognizers.Add(panGesture);

        #endregion

    }

    private void OnTimerFinish(object sender, EventArgs e)
    {
        BoredomLevel = mainPage.MyCreature.Boredom;
        if (isEntertained)
        {
            Debug.WriteLine(mainPage.MyCreature.Boredom);
            mainPage.MyCreature.Boredom += .1f;
            BoredomLevel = mainPage.MyCreature.Boredom;
            mainPage.MyCreature.Boredom = Math.Min(BoredomLevel, 1);
        }
    }

    #region BallPhysics Functions
    private void PanGesturePanUpdated(object sender, PanUpdatedEventArgs e)
    {
        //hier was een poging tot een systeem waarmee je een bal over het scherm kon laten stuiteren (net als bij poo)
        //zwaar mislukt en niet genoeg tijd om het te fixen. Dus nu kan je de bal bewegen en glitcht hij heel erg.
        switch (e.StatusType)
        {
            case GestureStatus.Started:
                xLastPanStarted = Ball.TranslationX;
                yLastPanStarted += Ball.TranslationY;

                isEntertained = true;

                break;

            case GestureStatus.Running:
                Ball.TranslationX = xLastPanStarted + e.TotalX;
                Ball.TranslationY = yLastPanStarted + e.TotalY;

                break;

            case GestureStatus.Completed:
                xLastPanStarted = Ball.TranslationX;
                yLastPanStarted = Ball.TranslationY;

                isEntertained = false;
                
                break;
        }
    }
    #endregion
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

    public void EnterTainHim(object sender, EventArgs e)
    {
        mainPage.MyCreature.Boredom += .1f;
        Debug.WriteLine(mainPage.EntertainmentLevel);
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}