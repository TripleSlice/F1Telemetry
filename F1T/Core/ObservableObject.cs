using System.Collections.Generic;
using System.ComponentModel;

namespace F1T.Core
{
    /// <summary>
    /// Provides an object which implements the <see cref="INotifyPropertyChanged"/> interface
    /// </summary>
    public class ObservableObject : INotifyPropertyChanged
    {
        // boiler-plate
        // https://stackoverflow.com/questions/1315621/implementing-inotifypropertychanged-does-a-better-way-exist
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        protected bool SetField<T>(ref T field, T value, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
