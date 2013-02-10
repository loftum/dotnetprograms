using System.Windows;
using Caliburn.Micro;

namespace CodeGenerator
{
    public class CodeGeneratorWindowManager : WindowManager
    {
        protected override Window EnsureWindow(object model, object view, bool isDialog)
        {
            var window = base.EnsureWindow(model, view, isDialog);
            window.Height = 600;
            window.Width = 800;
            window.SizeToContent = SizeToContent.Manual;
            return window;
        }
    }
}