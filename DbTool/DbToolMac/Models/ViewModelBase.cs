using System;
using DbTool.Lib.Ui.Models;
using MonoMac.Foundation;
using System.Linq.Expressions;
using DbTool.Lib.Ui.ExtensionMethods;

namespace DbToolMac
{
    public abstract class ViewModelBase : NSObject, IViewModel
    {
        public event ModelChangeEventHandler ModelChange;

        protected void OnPropertyChange<TProperty>(Expression<Func<TProperty>> expression)
        {
            if (ModelChange != null && expression != null)
            {
                ModelChange(this, new ModelChangeEventArgs(expression.GetPropertyId()));
            }
        }
    }
}

