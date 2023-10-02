using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Tomogachi;

public partial class ProgressBarHealth : ContentView, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    public static readonly BindableProperty HealthProperty =
       BindableProperty.Create(nameof(Health), typeof(double), typeof(ProgressBarHealth));


    public static readonly BindableProperty ThirstProperty =
        BindableProperty.Create(nameof(Thirst), typeof(double), typeof(ProgressBarHealth));

    public double Health
    {
        get => (double)GetValue(HealthProperty);
        set => SetValue(HealthProperty, value);
    }

    public double Thirst
    {
        get => (double)GetValue(ThirstProperty);
        set {
            if ((Double)GetValue(ThirstProperty) != value)
            {
                SetValue(ThirstProperty, value);
                Debug.WriteLine("werkt");
                OnPropertyChanged();  // This notifies the UI of the change.
            }

        } 
    }
    public ProgressBarHealth()
	{
		InitializeComponent();
        this.BindingContext = this;
        Debug.WriteLine((double)GetValue(ThirstProperty));
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}