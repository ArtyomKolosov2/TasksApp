using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace TasksApp.Modules
{
    public class ObservableCollectionModifed<T> : ObservableCollection<T> 
        where T: INotifyPropertyChanged
    {
        public ObservableCollectionModifed()
        {
            CollectionChanged += NewCollectionChange;
        }

        private void NewCollectionChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (object item in e.NewItems)
                {
                    ((INotifyPropertyChanged)item).PropertyChanged += ItemPropertyChanged;
                }
            }
            if (e.OldItems != null)
            {
                foreach (object item in e.OldItems)
                {
                    ((INotifyPropertyChanged)item).PropertyChanged -= ItemPropertyChanged;
                }
            }
        }

        private void ItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyCollectionChangedEventArgs args = new NotifyCollectionChangedEventArgs
                (
                NotifyCollectionChangedAction.Replace,
                sender,
                sender,
                IndexOf((T)sender)
                );
            OnCollectionChanged(args);
        }
    }
}
