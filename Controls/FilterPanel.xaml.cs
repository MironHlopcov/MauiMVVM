using MauiMVVM.Service;
using MauiMVVM.ViewModel;

namespace MauiMVVM.Controls;

public partial class FilterPanel
{
	public FilterPanel()
	{

        InitializeComponent();

        var filtredFilds = new Dictionary<string, string[]>();
        filtredFilds.Add("test", new string[] { "123", "321", "123", "321" });
        filtredFilds.Add("test2", new string[] { "123", "321", "123", "321" });
        filtredFilds.Add("test3", new string[] { "123", "321", "123", "321" });

       // ExpandElemt.FiltredFilds = filtredFilds.FirstOrDefault();
    }
}