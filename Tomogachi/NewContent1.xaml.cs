namespace Tomogachi;

public partial class NewContent1 : ContentView
{
    public static readonly BindableProperty MyAmazingProperty = BindableProperty.Create(nameof(ViewName), typeof(string), typeof(NewContent1)/*, propertyChanging:*/);

    //Ctrl + .

    public string ViewName
    {
        get => GetValue(MyAmazingProperty) as string;
        set => SetValue(MyAmazingProperty, value);
    }
    public NewContent1()
    {
        InitializeComponent();
        this.BindingContext = this;
    }
}