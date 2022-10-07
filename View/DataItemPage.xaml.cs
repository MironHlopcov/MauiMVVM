using MauiMVVM.ViewModel;
using MauiMVVM.Service;

namespace MauiMVVM;

public partial class DataItemPage : ContentPage
{
	public DataItemPage()
	{
        DataItemService dataItemService = new DataItemService();
        InitializeComponent();
		BindingContext = new DataItemListViewModel()
		{
			Navigation = this.Navigation,
			DataItemService = dataItemService
		};
	}
}

