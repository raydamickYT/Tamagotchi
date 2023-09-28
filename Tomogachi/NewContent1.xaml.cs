namespace Tomogachi;

public partial class NewContent1 : ContentView
{
    public static readonly BindableProperty MyAmazingProperty = BindableProperty.Create(nameof(MyAmazing), typeof(string), typeof(NewContent1)/*, propertyChanging:*/);

    //Ctrl + .

    public string MyAmazing
    {
        get => GetValue(MyAmazingProperty) as string;
        set => SetValue(MyAmazingProperty, value);
    }
    public NewContent1()
    {
        InitializeComponent();
    }
}