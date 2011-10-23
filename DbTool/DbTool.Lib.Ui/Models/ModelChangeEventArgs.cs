using System;

namespace DbTool.Lib.Ui.Models
{
    public class ModelChangeEventArgs : EventArgs
    {
        public string PropertyId { get; private set; }

        public ModelChangeEventArgs(string propertyId)
        {
            PropertyId = propertyId;
        }
    }
}