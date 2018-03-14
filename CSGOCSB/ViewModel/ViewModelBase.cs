using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace CSGOCSB.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected ViewModelBase() { }

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