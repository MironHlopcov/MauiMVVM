using MauiMVVM.ViewModel;

namespace MauiMVVM;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage(DataItemViewModel dataItemViewModel)
	{
		InitializeComponent();
		BindingContext = dataItemViewModel;
	}

	
}

