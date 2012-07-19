using System;
using System.Drawing;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;
using Ninject;
using DbTool.Lib.Modules;
using DbToolMac.Modules;
using DbTool.Lib.Ui.Modules;
using DbTool.Lib.Configuration;

namespace DbToolMac
{
    public partial class AppDelegate : NSApplicationDelegate
    {
        private MainWindowController _mainWindowController;
        private IKernel _kernel;

        public AppDelegate()
        {
            _kernel = CreateKernel();
        }

        public override void FinishedLaunching(NSObject notification)
        {
            _mainWindowController = _kernel.Get<MainWindowController>();
            _mainWindowController.Window.MakeKeyAndOrderFront(this);
        }

        private IKernel CreateKernel()
        {
            return new StandardKernel(new ConfigModule(), new TaskModule(), new DatabaseModule(), new UiModule(), new MetaModule(), new ApplicationModule());
        }

        public override void WillTerminate(NSNotification notification)
        {
            if (_kernel != null && !_kernel.IsDisposed)
            {
                _kernel.Get<IDbToolConfig>().SaveSettings();
                _kernel.Dispose();
            }
            _kernel = null;
        }
    }
}

