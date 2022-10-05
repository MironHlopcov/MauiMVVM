using MauiMVVM.Model;
using MauiMVVM.Service;
using System.Collections.ObjectModel;


namespace MauiMVVM.ViewModel
{
    public class DataItemViewModel : BaseViewModel
    {
        DataItemService dataItemService;

        public ObservableCollection<DataItem> DataItems { get; } = new();
        public Command GetDataItemsComand { get; }

        public DataItemViewModel(DataItemService dataItemService)
        {
            Title = "Data Items";
            this.dataItemService = dataItemService;
            GetDataItemsComand = new Command(async () => await GetDataItemAsync());
        }
        async Task GetDataItemAsync()
        {
            if(IsBusy)
                return;
            try
            {
                IsBusy = true;
                var dataItems = await dataItemService.GetDataItems();
                if (DataItems.Count != 0)
                    DataItems.Clear();
                foreach (var it in dataItems)
                    DataItems.Add(it);

            }
            catch(Exception ex)
            {
                Console.WriteLine($"Unable to get DataItems: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error", $"Unable to get DataItems: {ex.Message}", "Ok");
            }
            finally
            { 
                IsBusy = false;
            }
        }
    }
}
