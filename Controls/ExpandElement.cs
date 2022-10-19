
using CommunityToolkit.Maui.Converters;
using CommunityToolkit.Maui.Markup;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using static CommunityToolkit.Maui.Markup.GridRowsColumns;

namespace MauiMVVM.Controls
{
    internal class ExpandElement : ContentView
    {

        private static KeyValuePair<string, string[]> filtredFilds = new KeyValuePair<string, string[]>();
        private static string groupName;
        private static string[] groupValues;

        #region KeyValuePair
        public static readonly BindableProperty FiltredFildsProperty = BindableProperty.Create(nameof(FiltredFilds),
          typeof(KeyValuePair<string, string[]>),
          typeof(ExpandableList),
          defaultValue: new KeyValuePair<string, string[]>(),
          defaultBindingMode: BindingMode.OneWay,
          propertyChanged: FiltredFildsPropertyChanged);
        private static void FiltredFildsPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            filtredFilds = (KeyValuePair<string, string[]>)newValue;
            var control = (ExpandElement)bindable;
            control.Initialize();

        }

        public KeyValuePair<string, string[]> FiltredFilds
        {
            get
            {
                return (KeyValuePair<string, string[]>)base.GetValue(FiltredFildsProperty);
            }

            set
            {
                base.SetValue(FiltredFildsProperty, value);
            }
        }
        #endregion
        #region GroupName
        public static readonly BindableProperty GroupNameProperty = BindableProperty.Create(nameof(GroupName),
                                typeof(string),
                                typeof(ExpandableList),
                                defaultValue: "GroupName",
                                defaultBindingMode: BindingMode.OneWay,
                                propertyChanged: GroupNamePropertyChanged);
        private static void GroupNamePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            groupName = (string)newValue ?? "groupName";
        }

        public string GroupName
        {
            get
            {
                return base.GetValue(GroupNameProperty)?.ToString();
            }

            set
            {
                base.SetValue(GroupNameProperty, value);
            }
        }
        #endregion
        #region GroupValues
        public static readonly BindableProperty GroupValuesProperty = BindableProperty.Create(nameof(GroupValues),
                                typeof(List<string>),
                                typeof(ExpandableList),
                                defaultValue: new List<string>(),
                                defaultBindingMode: BindingMode.OneWay,
                                propertyChanged: GroupNamePropertyChanged);
        private static void GroupValuesPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            groupValues = (string[])newValue ;
            var temp = new Dictionary<string, string[]>();
            temp.Add(groupName, groupValues);
            filtredFilds = temp.FirstOrDefault();

            var control = (ExpandElement)bindable;
            control.Initialize();

        }

        public List<string> GroupValues
        {
            get
            {
                return (List<string>)base.GetValue(GroupValuesProperty);
            }

            set
            {
                base.SetValue(GroupValuesProperty, value);
            }
        }
        #endregion




        public ExpandElement()
        {
        }

        private void Initialize()
        {
            KeyValuePair<string, string[]> group = filtredFilds;
            GroupItem groupItem = new GroupItem();
            groupItem.Key = group.Key;

            List<Item> values = new List<Item>();
            foreach (var val in group.Value)
                values.Add(new Item { Key = val });
            groupItem.Items = values.ToArray();

            var groupNameView = new Grid
            {
                ColumnDefinitions = Columns.Define(Auto, Auto, Auto),
                ColumnSpacing = 2,
                Children =
                {
                    new CheckBox{ BindingContext = groupItem}
                    .Column(0).Row(0).Bind(nameof(Item.Value))
                    .Invoke(checbox=>checbox.CheckedChanged+=Checbox_NameChanged),
                    new Label{BindingContext = groupItem,
                        VerticalOptions = LayoutOptions.Center}
                    .Column(1).Row(0).Bind(nameof(Item.Key)),
                    new Image{Source = "expand_more.png"}
                    .Size(15,25)
                     .Bind(Grid.IsVisibleProperty,
                     nameof(Item.IsExpanded),
                     converter: new InvertedBoolConverter())
                     .CenterVertical()
                    .Column(2).Row(0),
                     new Image{Source = "expand_less.png"}
                    .Size(15,25)
                    .Bind(Grid.IsVisibleProperty, nameof(Item.IsExpanded))
                    .CenterVertical()
                    .Column(2).Row(0)
                }
            };
            var itemListView = new CollectionView
            {
                ItemsSource = groupItem.Items,
                ItemTemplate = new DataTemplate(() =>
                {
                    HorizontalStackLayout views = new HorizontalStackLayout
                    {
                        Padding = new Thickness(20, 0, 0, 0),
                        Children =
                             {
                                new CheckBox().Column(0).Bind(nameof(Item.Value))
                                .Invoke(checbox=>checbox.CheckedChanged+=Checbox_ValueChanged),
                                new Label{ VerticalOptions = LayoutOptions.Center}
                                .Column(1).Bind(nameof(Item.Key))
                             }
                    };
                    return views;
                })
            };
            var view = new Grid
            {
                ColumnDefinitions = Columns.Define(Auto, Star),
                RowDefinitions = Rows.Define(Auto, Auto),
                BindingContext = groupItem,
                Children =
                {
                   groupNameView
                   .Row(0)
                   .TapGesture(()=>{
                        groupItem.IsExpanded=!groupItem.IsExpanded;
                    }),
                   itemListView
                   .ColumnSpan(2)
                   .Row(1)
                   .Bind(Grid.IsVisibleProperty, ".IsExpanded")
                }
            };
            Content = view;
            void Checbox_NameChanged(object sender, CheckedChangedEventArgs e)
            {
                if (e.Value == true)
                {
                    if (!values.Any(x => x.Value == true))
                        groupItem.Value = false;
                    return;
                }
                if (e.Value == false)
                    foreach (var v in values)
                        v.Value = false;

            }
            void Checbox_ValueChanged(object sender, CheckedChangedEventArgs e)
            {
                if (e.Value == true)
                {
                    groupItem.Value = true;
                    return;
                }
                if (!values.Any(x => x.Value == true))
                {
                    groupItem.Value = false;
                    return;
                }

            }
        }

        private class Item : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;
            public void OnPropertyChanged([CallerMemberName] string name = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
            public Item()
            {
                value = false;
            }

            private string key;
            public string Key
            {
                get => key;
                set
                {
                    if (key == value)
                        return;
                    key = value;
                    OnPropertyChanged();
                }
            }

            private bool value;
            public bool Value
            {
                get => value;
                set
                {
                    if (this.value == value)
                        return;
                    this.value = value;
                    OnPropertyChanged();
                }
            }

            private bool isExpanded;
            public bool IsExpanded
            {
                get => isExpanded;
                set
                {
                    if (this.isExpanded == value)
                        return;
                    this.isExpanded = value;
                    OnPropertyChanged();
                }
            }
        }
        private class GroupItem : Item
        {
            private Item[] items;
            public Item[] Items
            {
                get => items;
                set
                {
                    if (items == value)
                        return;
                    items = value;
                    OnPropertyChanged();
                }
            }
        }

    }
}