using System;
using System.Collections.Generic;
using System.Web.Mvc;
using StuffLibrary.HtmlTools.Dropdowns;

namespace StuffLibrary.Models.GuiElements
{
    public class GuiElementsViewModel
    {
        public IEnumerable<SelectListItem> AvailableValues { get; private set; }
        public string SelectedValue { get; set; }

        public string SuccessMessage { get; set; }
        public string InfoMessage { get; set; }
        public string WarningMessage { get; set; }
        public string ErrorMessage { get; set; }

        public GuiElementsViewModel()
        {
            AvailableValues = SelectableList.OfEnum<DayOfWeek>().Items;
        }
    }
}