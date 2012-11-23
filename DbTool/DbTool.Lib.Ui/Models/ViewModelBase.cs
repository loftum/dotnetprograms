using System;
using System.Linq.Expressions;
using DotNetPrograms.Common.ExtensionMethods;

namespace DbTool.Lib.Ui.Models
{
    public abstract class ViewModelBase : IViewModel
    {
        public event ModelChangeEventHandler ModelChange;

        protected void OnPropertyChange<TProperty>(Expression<Func<TProperty>> expression)
        {
            if (ModelChange != null && expression != null)
            {
                ModelChange(this, new ModelChangeEventArgs(expression.GetPropertyName()));
            }
        }
    }
}