using MauiMVVM.ViewModel;

namespace MauiMVVM;

public partial class MainPage : ContentPage
{
	public MainPage(DataItemListViewModel dataItemViewModel)
	{
		InitializeComponent();
		BindingContext = dataItemViewModel;
	}

	
}

