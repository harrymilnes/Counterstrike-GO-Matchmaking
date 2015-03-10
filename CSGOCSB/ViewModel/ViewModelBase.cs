using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace CSGOMM.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        protected ViewModelBase()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(Expression<Func<object>> property)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(property.Name);
                handler(this, e);
            }
        }
    }
}
