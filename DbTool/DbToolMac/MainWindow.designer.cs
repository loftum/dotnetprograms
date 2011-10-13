// WARNING
//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoMac.Foundation;

namespace DbToolMac
{
	[Register ("MainWindowController")]
	partial class MainWindowController
	{
		[Outlet]
		MonoMac.AppKit.NSComboBox ConnectionBox { get; set; }

		[Outlet]
		MonoMac.AppKit.NSButton ConnectionButton { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextView EditorBox { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextView ResultTextBox { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTableView ResultTable { get; set; }

		[Action ("Connection_Click:")]
		partial void Connection_Click (MonoMac.Foundation.NSObject sender);
	}

	[Register ("MainWindow")]
	partial class MainWindow
	{
	}
}
