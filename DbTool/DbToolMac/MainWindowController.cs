using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;
using DbToolMac.Models;
using DbToolMac.ExtensionMethods;
using DbToolMac.Delegation;

namespace DbToolMac
{
    public partial class MainWindowController : MonoMac.AppKit.NSWindowController
    {
        public new MainWindow Window { get { return (MainWindow)base.Window; } }
        private IDbToolControllerDelegate _controllerDelegate;

        [Export("model")]
        public MainWindowViewModel Model { get; private set; }

        // Called when created from unmanaged code
        public MainWindowController(IntPtr handle) : base (handle)
        {
            Initialize();
        }
		
        // Called when created directly from a XIB file
        [Export ("initWithCoder:")]
        public MainWindowController(NSCoder coder) : base (coder)
        {

            Initialize();
        }
		
        // Call to load from the XIB/NIB file
        public MainWindowController() : base ("MainWindow")
        {
            Initialize();
        }

        public MainWindowController(IDbToolControllerDelegate controllerDelegate) : base ("MainWindow")
        {
            _controllerDelegate = controllerDelegate;
            Model = _controllerDelegate.Model;
        }
		
        // Shared initialization code
        void Initialize()
        {
            Model = new MainWindowViewModel();
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            EditorBox.Font = NSFont.FromFontName("Monaco", 12);
        }

        partial void Connection_Click(NSObject sender)
        {
            Window.Title = "DbTool - Connected";
            var statement = EditorBox.GetSelectedOrAllText();
            ResultTextBox.SetText(statement);
        }
    }
}

