using System;
using MonoMac.AppKit;
using MonoMac.Foundation;
using DbTool.Lib.ExtensionMethods;
using DotNetPrograms.Common.ExtensionMethods;

namespace DbToolMac.ExtensionMethods
{
    public static class NSTextViewExtensions
    {
        public static string GetSelectedOrAllText(this NSTextView textView)
        {
            var range = textView.SelectedRange;

            return range.Length == 0
                ? textView.GetAllText()
                : textView.GetTextRange(range);
        }

        public static string GetTextRange(this NSTextView textView, NSRange range)
        {
            var data = textView.RtfFromRange(range);
            NSError error;
            NSDictionary dictionary;
            var attributedString = new NSAttributedString(data, new NSDictionary(), out dictionary, out error);
            return attributedString.Value;
        }

        public static string GetAllText(this NSTextView textView)
        {
            return textView.TextStorage.Value;
        }

        public static void ClearText(this NSTextView textView)
        {
            textView.TextStorage.SetString(new NSAttributedString(string.Empty));
        }

        public static void SetText(this NSTextView textView, string text)
        {
            textView.ClearText();
            textView.InsertText(text.ValueOrEmpty().ToNSString());
        }
    }
}

