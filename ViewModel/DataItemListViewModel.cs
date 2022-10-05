using MauiMVVM.Model;
using MauiMVVM.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiMVVM.ViewModel
{
    public class DataItemListViewModel:BaseViewModel
    {
        public ObservableCollection<DataItemViewModel> DataItems { get; } = new();
        public Command GetDataItemsComand { get; }
       
        DataItemService dataItemService;
        public DataItemListViewModel(DataItemService dataItemService)
        {
            this.dataItemService = dataItemService;
            GetDataItemsComand = new Command(async () => await GetDataItemAsync());
        }
        async Task GetDataItemAsync()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                var dataItems = await dataItemService.GetDataItems();
                if (DataItems.Count != 0)
                    DataItems.Clear();
                foreach (var it in dataItems)
                {
                    DataItems.Add(new DataItemViewModel(it));
                }
            }
            catch (Exception ex)
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
