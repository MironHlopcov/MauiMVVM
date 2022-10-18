using MauiMVVM.Service;
using MauiMVVM.ViewModel;

namespace MauiMVVM.Controls;

public partial class FilterPanel
{
	public FilterPanel()
	{

        InitializeComponent();

		NameGr.GroupItem = "Names";
		NameGr.Values = new List<string>();
        
    }
}