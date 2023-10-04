using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Tomogachi
{

    public partial class Sleep : ContentPage, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        MainPage mainPage;
        private Color _backgroundColorSetter = Color.FromArgb("#dcdcdc");
        private bool SleepButtonClickedBool = false;
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

        }

        private void Sleep_Clicked(object sender, EventArgs e)
        {
            if (SleepButtonClickedBool)
            {
                //awake
                BackgroundColorSetter = Color.FromArgb("#dcdcdc");
                SleepButtonClickedBool = false;
                mainPage.IsSleeping = false;
            }
            else
            {
                //sleep
                BackgroundColorSetter = Color.FromArgb("#000000");
                SleepButtonClickedBool = true;
                mainPage.IsSleeping = true;
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
