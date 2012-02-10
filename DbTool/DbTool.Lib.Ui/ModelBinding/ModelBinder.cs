using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DbTool.Lib.ExtensionMethods;
using DbTool.Lib.Ui.Models;

namespace DbTool.Lib.Ui.ModelBinding
{
    public class ModelBinder<TModel> where TModel : IViewModel
    {
        private readonly TModel _model;
        private readonly IDictionary<string, Action> _actions = new Dictionary<string, Action>();

        public ModelBinder(TModel model)
        {
            _model = model;
            _model.ModelChange += HandleModelChange;
        }

        public ModelBinder<TModel> Bind<TProperty>(Expression<Func<TModel, TProperty>> property, Action action)
        {
            _actions[property.GetPropertyId()] = action;
            return this;
        }

        private void HandleModelChange(object sender, ModelChangeEventArgs e)
        {
            var action = TryGetActionFor(e.PropertyId);
            if (action != null)
            {
                action();
            }
        }

        private Action TryGetActionFor(string propertyId)
        {
            try
            {
                return _actions[propertyId];
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }
    }
}