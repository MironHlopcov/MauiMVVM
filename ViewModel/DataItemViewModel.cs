using MauiMVVM.Model;
using MauiMVVM.Service;
using System.Collections.ObjectModel;


namespace MauiMVVM.ViewModel
{
    public class DataItemViewModel : BaseViewModel
    {
        public DataItem DataItem { get; private set; }
        
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

        public DataItemViewModel(DataItem dataItem)
        {
            DataItem = new();
        }
      
    }
}
