using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace MauiApp1;


public partial class MainPage : ContentPage, INotifyPropertyChanged
{
	int count = 0;
	public string HeaderTIrle {get; set;} = "hehehe hoi";
	private float Hunger {get; set;} = .0f;
	public string HungetText => Hunger switch {
		<= 0 => "Not hungry",
		< .25f => "lil hongry",
		<.50f => "pwetty hongry",
		< .75f => "food",
		< 1 => "GIMME FOOD",
		>= 1.0f => "ded",
		_ => throw new ArgumentException("not possible", Hunger.ToString())
	};

	public MainPage()
	{
		InitializeComponent();
		BindingContext = this;
	}

	//async method wordt in de achtergrond gerund dus je kan het in de achtergrond uitvoeren
	private async void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);

		//animaties
		// je moet await erbij zetten omdat het een async method is
		var result = await CounterBtn.RelRotateTo(90.0, 1000, Easing.SpringIn);
		await CounterBtn.TranslateTo(.0, 50.0, 1000);
	}
	
	private void TestFunction(object sender, EventArgs e){
		Hunger += .1f;
	}

}

