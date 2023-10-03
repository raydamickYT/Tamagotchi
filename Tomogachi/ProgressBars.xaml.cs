using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Tomogachi;

public partial class ProgressBarHealth : ContentView, INotifyPropertyChanged
{
    private MainPage _mainPage;
    public event PropertyChangedEventHandler PropertyChanged;
    public delegate void ButtonClickedDelegate();

    public event ButtonClickedDelegate ButtonClicked;
    public event ButtonClickedDelegate ButtonClicked2;
    //public static readonly BindableProperty HealthProperty =
    //   BindableProperty.Create(nameof(Health), typeof(double), typeof(ProgressBarHealth));


    //public static readonly BindableProperty ThirstProperty =
    //    BindableProperty.Create(nameof(Thirst), typeof(double), typeof(ProgressBarHealth));

    public ProgressBarHealth() { 
        InitializeComponent();
    }
    public ProgressBarHealth(MainPage mainPage)
    {
        _mainPage = mainPage;
        this.BindingContext = this;
    }


    //public double Health
    //{
    //    get => (double)GetValue(HealthProperty);
    //    set => SetValue(HealthProperty, value);
    //}


    private void OnEntertainmentButtonClicked(object sender, EventArgs e)
    {
        // Navigation.PushAsync(new Entertainment(_mainPage));
        ButtonClicked?.Invoke();
    }
    void TestButtonClicked(object sender, EventArgs e)
    {
       // Navigation.PushAsync(new NewTestPage(_mainPage));
       ButtonClicked2?.Invoke();

    }
    //public double Thirst
    //{
    //    get => (double)GetValue(ThirstProperty);
    //    set
    //    {
    //        if ((Double)GetValue(ThirstProperty) != value)
    //        {
    //            SetValue(ThirstProperty, value);
    //            Debug.WriteLine("werkt");
    //            OnPropertyChanged();  // This notifies the UI of the change.
    //        }

    //    }
    //}

    //protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    //{
    //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    //}
}