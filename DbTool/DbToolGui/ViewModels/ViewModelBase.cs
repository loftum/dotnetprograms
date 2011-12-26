using System;
using System.ComponentModel;
using System.Linq.Expressions;
using DbTool.Lib.Ui.ExtensionMethods;

namespace DbToolGui.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertiesChanged(params string[] propertyNames)
        {
            foreach (var propertyName in propertyNames)
            {
                OnPropertyChanged(propertyName);
            }
        }

        protected void OnPropertiesChanged<TProperty>(params Expression<Func<TProperty>>[] expressions)
        {
            foreach (var expression in expressions)
            {
                OnPropertyChanged(expression);
            }
        }

        protected void OnPropertyChanged<TProperty>(Expression<Func<TProperty>> property)
        {
            OnPropertyChanged(property.GetPropertyId());
        }

        protected void OnPropertyChanged(string propertyName)
        {
            ValidatePropertyName(propertyName);
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void ValidatePropertyName(string propertyName)
        {
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                throw new ViewModelException(propertyName + " is not a valid property.");
            }
        }
    }
}