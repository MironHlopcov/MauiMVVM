using MauiMVVM.Model;
using MauiMVVM.Service;
using System.Collections.ObjectModel;


namespace MauiMVVM.ViewModel
{
    public class DataItemListViewModel : BaseViewModel
    {
        DataItemService dataItemService;

        public ObservableCollection<DataItem> DataItems { get; } = new();
        public Command GetDataItemsComand { get; }
        string name;
        public string Name
        {
            get => name;
            set
            {
                if (name == value)
                    return;
                name = value;
                OnPropertyChanged();

            }
        }
        string image;
        public string Image
        {
            get => image;
            set
            {
                if (image == value)
                    return;
                image = value;
                OnPropertyChanged();

            }
        }
        public DataItemListViewModel(DataItemService dataItemService)
        {
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
