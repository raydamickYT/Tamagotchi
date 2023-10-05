using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Tomogachi
{

    public partial class Sleep : ContentPage, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ImageSource DayNightImage { get; set; }
        public string SleepingStatusTekst => SleepButtonClickedBool switch
        {
            true => "Sleeping",
            false => "awake",
        };

        MainPage mainPage;
        private Color _backgroundColorSetter = Color.FromArgb("#dcdcdc");

        private bool _sleepButtonClickedBool = false;

        public bool SleepButtonClickedBool
        {
            get => _sleepButtonClickedBool;
            set
            {
                if (_sleepButtonClickedBool != value)
                {
                    _sleepButtonClickedBool = value;
                    OnPropertyChanged(nameof(SleepButtonClickedBool));
                    OnPropertyChanged(nameof(SleepingStatusTekst));
                }
            }
        }

        public Color BackgroundColorSetter
        {
            get => _backgroundColorSetter;
            set
            {
                if (_backgroundColorSetter != value)
                {
                    _backgroundColorSetter = value;
                    OnPropertyChanged(nameof(BackgroundColorSetter));
                }
            }
        }
        public Sleep(MainPage _mainpage)
        {
            this.mainPage = _mainpage;
            this.BindingContext = this;
            InitializeComponent();

            ProgressBarHealth.EntertainmentButtonClicked += EntertainmentButton;
            ProgressBarHealth.FeedingButtonClicked += FeedingPageButton;
            ProgressBarHealth.MainPageButtonClicked += MainPageButton;
            ProgressBarHealth.BedRoomButtonClicked += BedRoomButton;

            if (mainPage.MyCreature.Sleeping)
            {
                //still sleeping and needs to be woken up
                BackgroundColorSetter = Color.FromArgb("#000000");
                SleepButtonClickedBool = true;
                mainPage.IsSleeping = true;
            }

            DayNightImage = ImageSource.FromFile("day.png");
            OnPropertyChanged(nameof(DayNightImage));

        }

        private void Sleep_Clicked(object sender, EventArgs e)
        {
            if (SleepButtonClickedBool)
            {
                //awake
                BackgroundColorSetter = Color.FromArgb("#dcdcdc");
                SleepButtonClickedBool = false;
                mainPage.IsSleeping = false;


                DayNightImage = ImageSource.FromFile("day.png");
                OnPropertyChanged(nameof(DayNightImage));
            }
            else
            {
                //sleep
                BackgroundColorSetter = Color.FromArgb("#000000");
                SleepButtonClickedBool = true;
                mainPage.IsSleeping = true;


                DayNightImage = ImageSource.FromFile("night.png");
                OnPropertyChanged(nameof(DayNightImage));
            }
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            mainPage.IsSleeping = false;
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
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
