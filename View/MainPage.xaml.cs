using MauiMVVM.ViewModel;

namespace MauiMVVM;

public partial class MainPage : ContentPage
{
	public MainPage(DataItemViewModel dataItemViewModel)
	{
		InitializeComponent();
		BindingContext = dataItemViewModel;
	}

	
}

