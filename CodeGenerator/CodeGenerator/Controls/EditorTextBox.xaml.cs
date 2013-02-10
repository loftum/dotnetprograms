using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using CodeGenerator.Lib.CSharp;
using CodeGenerator.Lib.Syntax;
using DotNetPrograms.Common.ExtensionMethods;

namespace CodeGenerator.Controls
{
    public partial class EditorTextBox
    {
        public static DependencyProperty SyntaxParserProperty = DependencyProperty.Register("SyntaxParser", typeof (ISyntaxParser), typeof(EditorTextBox));

        public static DependencyProperty SuggestionListKeyProperty = DependencyProperty.Register("SuggestionListKey", typeof(Key), typeof(EditorTextBox),
                                         new FrameworkPropertyMetadata(Key.Space | Key.LeftCtrl, FrameworkPropertyMetadataOptions.AffectsRender));

        public static DependencyProperty LineNumberMarginWidthProperty = DependencyProperty.Register("LineNumberMarginWidth", typeof(double), typeof(EditorTextBox),
                                         new FrameworkPropertyMetadata(15.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public static DependencyProperty LineNumberWidthOffsetProperty = DependencyProperty.Register("LineNumberWidthOffset", typeof(double), typeof(EditorTextBox),
                                         new FrameworkPropertyMetadata(15.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public static DependencyProperty LineNumberBrushProperty = DependencyProperty.Register("LineNumberBrush", typeof(Brush), typeof(EditorTextBox),
                                       new FrameworkPropertyMetadata(new SolidColorBrush(Colors.Black), FrameworkPropertyMetadataOptions.AffectsRender, OnLineNumberBrushChanged));

        public static readonly DependencyProperty HighlightBrushProperty =
                                        DependencyProperty.Register("HighlightBrush", typeof(Brush), typeof(EditorTextBox), new FrameworkPropertyMetadata(new SolidColorBrush(Colors.Yellow), FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty BackgroundBrushProperty =
                                        DependencyProperty.Register("BackgroundBrush", typeof(Brush), typeof(EditorTextBox), new FrameworkPropertyMetadata(new SolidColorBrush(Colors.White), FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty ForegroundBrushProperty =
                                        DependencyProperty.Register("ForegroundBrush", typeof(Brush), typeof(EditorTextBox), new FrameworkPropertyMetadata(new SolidColorBrush(Colors.Black), FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty HighlightStylesProperty =
                                        DependencyProperty.Register("HighlightStyles", typeof(HighlightStyleCollection), typeof(EditorTextBox), new FrameworkPropertyMetadata(new HighlightStyleCollection(), OnHighlightStyleChanged));

        public static readonly DependencyProperty CursorColorProperty =
                                        DependencyProperty.Register("CursorColor", typeof(Color), typeof(EditorTextBox), new FrameworkPropertyMetadata(Colors.Black, OnCursorColorChanged));



        public static readonly RoutedEvent SyntaxRulesChangedEvent = EventManager.RegisterRoutedEvent("SyntaxRulesChanged", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(EditorTextBox));

        private IDictionary<TagType, HighlightStyle> _highlightStyles;
    
        private Canvas _suggestionCanvas;
        private ListBox _suggestionList;
        private double _leftTextBorder = Double.PositiveInfinity;

        public ISyntaxParser SyntaxParser
        {
            get { return (ISyntaxParser) GetValue(SyntaxParserProperty); }
            set
            {
                SetValue(SyntaxParserProperty, value);
                ParseAllText();
            }
        }

        public EditorTextBox()
        {
            ForegroundProperty.OverrideMetadata(typeof(EditorTextBox),
                new FrameworkPropertyMetadata(Brushes.Transparent, OnForegroundChanged));

            BackgroundProperty.OverrideMetadata(typeof(EditorTextBox),
                new FrameworkPropertyMetadata(OnBackgroundChanged));

            Loaded += EditorTextBox_Loaded;
            HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            AcceptsTab = true;
            AcceptsReturn = true;

            HighlightText = new NotifyableWordCollection();
            HighlightText.ListChanged += HighlightText_ListChanged;
            TabSize = 4;

            InitializeComponent();
        }

        public event RoutedEventHandler SyntaxRulesChanged
        {
            add
            { 
                AddHandler(SyntaxRulesChangedEvent, value); 
            }
            remove
            { 
                RemoveHandler(SyntaxRulesChangedEvent, value); 
            }
        }

        public Color CursorColor
        {
            get { return (Color)GetValue(CursorColorProperty); }
            set { SetValue(CursorColorProperty, value); }
        }

        public Brush LineNumberBrush
        {
            get { return (Brush)GetValue(LineNumberBrushProperty); }
            set { SetValue(LineNumberBrushProperty, value); }
        }

        public double LineNumberWidthOffset
        {
            get { return (Double)GetValue(LineNumberWidthOffsetProperty); }
            set { SetValue(LineNumberWidthOffsetProperty, value); }
        }

        public double LineNumberMarginWidth
        {
            get { return (Double)GetValue(LineNumberMarginWidthProperty); }
            set { SetValue(LineNumberMarginWidthProperty, value); }
        }

        public HighlightStyleCollection HighlightStyles
        {
            get { return (HighlightStyleCollection)GetValue(HighlightStylesProperty); }
            set { SetValue(HighlightStylesProperty, value); }
        }

        public Brush BackgroundBrush
        {
            get { return (Brush)GetValue(BackgroundBrushProperty); }
            set { SetValue(BackgroundBrushProperty, value); }
        }

        public Brush ForegroundBrush
        {
            get { return (Brush)GetValue(ForegroundBrushProperty); }
            set { SetValue(ForegroundBrushProperty, value); }
        }

        public Brush HighlightBrush
        {
            get { return (Brush)GetValue(HighlightBrushProperty); }
            set { SetValue(HighlightBrushProperty, value); }
        }
        
        public NotifyableWordCollection HighlightText{ get; set;}

        public int TabSize { get; set; }

        private FormattedText LastLineNumberFormat{ get; set; }
        private string Tab { get { return new String(' ', TabSize); } }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);
            ParseAllText();
        }

        private void ParseAllText()
        {
            Text = Text.Replace("\t", Tab);
            if (SyntaxParser != null)
            {
                SyntaxParser.Parse(Text, 0, Text.Length);
            }
            InvalidateVisual();
        }

        private static bool ShouldShowSuggestionList(KeyEventArgs e)
        {
            return e.Key == Key.Space && e.KeyboardDevice.Modifiers == ModifierKeys.Control;
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            if (_suggestionList.IsVisible && e.Key.In(Key.Tab, Key.Enter, Key.Return))
            {
                var selectedItem = (Suggestion) _suggestionList.SelectedItem;
                if (selectedItem != null)
                {
                    var selectedText = selectedItem.Completion;
                    var index = CaretIndex;
                    Text = Text.Insert(CaretIndex, selectedText);
                    CaretIndex = index + selectedText.Length;
                }
                HideSuggestionList();
                e.Handled = true;
                return;
            }

            if (e.Key == Key.Escape)
            {
                HideSuggestionList();
                e.Handled = true;
            }

            if (ShouldShowSuggestionList(e))
            {
                SyntaxParser.FindSuggestions(Text, SelectionStart);
                ShowSuggestionList();
                e.Handled = true;
            }

            if (e.Key == Key.Tab && e.KeyboardDevice.Modifiers == ModifierKeys.Shift)
            {
                // with selected text
                if (SelectedText != string.Empty)
                {
                    string[] lines = SelectedText.SplitLines();
                    for (int ii = 0; ii < lines.Length; ii++)
                    {
                        if (lines[ii].StartsWith(Tab))
                        {
                            lines[ii] = lines[ii].Substring(Tab.Length);
                        }
                        else
                        {
                            lines[ii] = lines[ii].TrimStart(' ');
                        }
                    }
                    SelectedText = String.Join(Environment.NewLine, lines);
                }
                else
                {
                    var index = CaretIndex;
                    var lastLine = Text.LastIndexOf(Environment.NewLine, index, StringComparison.Ordinal);

                    if (lastLine == -1)
                    {
                        lastLine = Text.Length - 1;
                    }

                    var startLine = Text.IndexOf(Environment.NewLine, lastLine, StringComparison.Ordinal);

                    if (startLine != -1)
                    {
                        startLine += Environment.NewLine.Length;
                    }
                    else
                    {
                        startLine = 0;
                    }

                    // find empty spaces
                    var spaces = 0;
                    for (var i = startLine; i < Text.Length - 1; i++)
                    {
                        if (Text[i] == ' ')
                            spaces++;
                        else
                            break;
                    }

                    if (spaces > TabSize)
                    {
                        spaces = TabSize;
                    }


                    Text = Text.Remove(startLine, spaces);

                    // set position of caret
                    if (index >= startLine + spaces)
                    {
                        CaretIndex = index - spaces;
                    }

                    else if (index >= startLine && index < startLine + spaces)
                    {
                        CaretIndex = startLine;
                    }
                    else
                    {
                        CaretIndex = index;
                    }
                }

                e.Handled = true;
            }

            // tab 
            if (e.Key == Key.Tab && e.KeyboardDevice.Modifiers == ModifierKeys.None)
            {
                if (SelectedText == string.Empty)
                {   
                    var caretPosition = CaretIndex;
                    Text = Text.Insert(caretPosition, Tab);
                    CaretIndex = caretPosition + TabSize;                   
                }
                else
                {
                    if (!SelectedText.Contains(Environment.NewLine))
                    {
                        SelectedText = Tab;
                    }
                    else
                    {
                        SelectedText = Tab + SelectedText.Replace(Environment.NewLine, Environment.NewLine + Tab);
                    }
                }
                e.Handled = true;
            }

            // enter respects indenting
            if (e.Key == Key.Return)
            {
                var index = CaretIndex;
                var lastLine = Text.LastIndexOf(Environment.NewLine, index, StringComparison.Ordinal);
                var spaces = 0;

                if (lastLine != -1)
                {
                    var line = Text.Substring(lastLine, Text.Length - lastLine);
                    var startLine = line.IndexOf(Environment.NewLine, StringComparison.Ordinal);
                    if (startLine != -1)
                    {
                        line = line.Substring(startLine).TrimStart('\r', '\n');
                    }
                        
                    foreach (var c in line)
                    {
                        if (c==' ')
                            spaces++;
                        else
                            break;
                    }
                }
                Text = Text.Insert(index, Environment.NewLine + new String(' ', spaces));
                CaretIndex = index + Environment.NewLine.Length+spaces;

                e.Handled = true;
            }

            base.OnPreviewKeyDown(e);       
        }
        
        // rendering 
        protected override void OnRender(DrawingContext drawingContext)
        {
            // draw background 
            drawingContext.PushClip(new RectangleGeometry(new Rect(0, 0, ActualWidth, ActualHeight)));
            drawingContext.DrawRectangle(BackgroundBrush, new Pen(), new Rect(0, 0, ActualWidth, ActualHeight));

            // draw text
            if (Text == string.Empty)
            {
                return;
            }
            var formattedText = new FormattedText(
                Text,
                System.Globalization.CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                new Typeface(FontFamily.Source),
                FontSize, ForegroundBrush);

            var leftMargin = 4.0 + BorderThickness.Left + LineNumberMarginWidth;
            var topMargin = 2.0 + BorderThickness.Top;

            // Background highlight
            if (HighlightText != null && HighlightText.Any())
            {
                foreach (string text in HighlightText)
                {
                    var index = 0;
                    var lastIndex = Text.LastIndexOf(text, StringComparison.OrdinalIgnoreCase);

                    while (index <= lastIndex)
                    {
                        index = Text.IndexOf(text, index, StringComparison.OrdinalIgnoreCase);

                        Geometry geom = formattedText.BuildHighlightGeometry(new Point(leftMargin, topMargin - VerticalOffset), index, text.Length);
                        if (geom != null)
                        {
                            drawingContext.DrawGeometry(HighlightBrush, null, geom);
                        }
                        index += 1;
                    }
                }
            }

            HighlightSyntax(formattedText);

            // left from first char boundary

            var leftBorder = GetRectFromCharacterIndex(0).Left;
            if (!Double.IsInfinity(leftBorder))
            {
                _leftTextBorder = leftBorder;
            }

            drawingContext.DrawText(formattedText, new Point(_leftTextBorder - HorizontalOffset, topMargin - VerticalOffset));


            // draw lines
            if (GetLastVisibleLineIndex() != -1)
            {
                LastLineNumberFormat = GetLineNumbers();                       
            }
            if (LastLineNumberFormat != null)
            {
                LastLineNumberFormat.SetForegroundBrush(LineNumberBrush);
                drawingContext.DrawText(LastLineNumberFormat, new Point(3, topMargin));
            }
        }

        private void HighlightSyntax(FormattedText formattedText)
        {
            if (SyntaxParser != null)
            {
                foreach (var tag in SyntaxParser.Tags)
                {
                    var style = GetHighlightStyleFor(tag.Type);
                    if (style != null)
                    {
                        formattedText.SetForegroundBrush(style.Foreground, tag.StartPosition, tag.Length);
                    }
                }
            }
            formattedText.Trimming = TextTrimming.None;
        }

        private static void OnCursorColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sh = (EditorTextBox)d;
            var c = (Color)e.NewValue; 
            sh.Background = new SolidColorBrush(sh.GetAlphaColor(c));
        }
        
        private static void OnLineNumberBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((EditorTextBox)d).InvalidateVisual();    
        }

        private static void OnForegroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

            if (e.NewValue != Brushes.Transparent)
            {
                ((EditorTextBox)d).Foreground = Brushes.Transparent;
            }
        }

        

        private static void OnBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sh = (EditorTextBox)d;

            var brush = new SolidColorBrush(sh.GetAlphaColor(sh.CursorColor));
            var a = e.NewValue as SolidColorBrush;
            if (a == null || a.Color != brush.Color)
            {
                sh.Background = brush;
            }
        }

        static void OnHighlightStyleChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            ((EditorTextBox)obj)._highlightStyles = null; // flush cache
            var arg = new RoutedEventArgs(SyntaxRulesChangedEvent);
            ((EditorTextBox)obj).RaiseEvent(arg);         
        }

        private void HighlightText_ListChanged(object source, EventArgs e)
        {
            InvalidateVisual();
        }

        private HighlightStyle GetHighlightStyleFor(TagType tagType)
        {
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                return null;
            }

            if (_highlightStyles == null)
            {
                _highlightStyles = PopulateHighlightStyles();
            }

            return DoGetHighlightStyleFor(tagType);
        }

        private HighlightStyle DoGetHighlightStyleFor(TagType tagType)
        {
            try
            {
                return _highlightStyles[tagType];
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }

        private IDictionary<TagType, HighlightStyle> PopulateHighlightStyles()
        {
            var dictionary =  new Dictionary<TagType, HighlightStyle>();
            foreach (var style in HighlightStyles)
            {
                dictionary[style.Type] = style;
            }
            return dictionary;
        }

        private Color GetAlphaColor(Color color)
        {
            return Color.FromArgb(0, (byte)(255 - color.R), (byte)(255 - color.G), (byte)(255 - color.B));
        }

        private void EditorTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (VisualTreeHelper.GetChildrenCount(this) > 0)
            {
                var border = VisualTreeHelper.GetChild(this, 0); // Border
                var grid = VisualTreeHelper.GetChild(border, 0); // Grid
                var scrollViewer = (ScrollViewer)VisualTreeHelper.GetChild(grid, 0); // Scrollbar
                scrollViewer.ScrollChanged += (s2, e2) => InvalidateVisual();

                _suggestionCanvas = (Canvas)VisualTreeHelper.GetChild(grid, 2); // canvas
                _suggestionList = (ListBox)VisualTreeHelper.GetChild(_suggestionCanvas, 0); // listbox

                HideSuggestionList();

                // refresh 
                InvalidateVisual();
            }
        }

        private void ShowSuggestionList()
        {
            _suggestionList.Items.Clear();
            if (!SyntaxParser.Suggestions.Any())
            {
                return;
            }
            _suggestionList.Items.AddRange(SyntaxParser.Suggestions);
            _suggestionCanvas.IsHitTestVisible = true;
            Point position = GetRectFromCharacterIndex(CaretIndex).BottomRight;

            double left = position.X - LineNumberWidthOffset - LineNumberMarginWidth;
            double top = position.Y;

            if (left + _suggestionList.ActualWidth > _suggestionCanvas.ActualWidth)
            {
                left = _suggestionCanvas.ActualWidth - _suggestionList.ActualWidth;
            }
            if (top + _suggestionList.ActualHeight > _suggestionCanvas.ActualHeight)
            {
                top = _suggestionCanvas.ActualHeight - _suggestionList.ActualHeight;
            }   

            Canvas.SetLeft(_suggestionList, left);
            Canvas.SetTop(_suggestionList, top);
            _suggestionList.Visibility = Visibility.Visible;
            _suggestionList.Focus();
        }

        private void HideSuggestionList()
        {
            _suggestionCanvas.IsHitTestVisible = false;
            _suggestionList.Visibility = Visibility.Hidden;
        }


        private Double[] VisibleLineWidthsIncludingTrailingWhitespace()
        {

            var firstLine = GetFirstVisibleLineIndex();
            var lastLine = Math.Max(GetLastVisibleLineIndex(), firstLine);
            var lineWidths = new Double[lastLine - firstLine + 1];
            if (lineWidths.Length == 1)
            {
                lineWidths[0] = MeasureString(Text);
            }
            else
            {
                for (int i = firstLine; i <= lastLine; i++)
                {
                    string lineString = GetLineText(i);
                    lineWidths[i - firstLine] = MeasureString(lineString);
                }
            }
            return lineWidths;
        }

        private double MeasureString(string str)
        {
            return new FormattedText(
                str,
                System.Globalization.CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                new Typeface(FontFamily.Source),
                FontSize,
                new SolidColorBrush(Colors.Black))
            .WidthIncludingTrailingWhitespace;
        }

        private FormattedText GetLineNumbers()
        {
            var firstLine = GetFirstVisibleLineIndex();
            var lastLine = GetLastVisibleLineIndex();
            var builder = new StringBuilder();
            double maxSize = 0;

            for (var i = firstLine; i <= lastLine; i++)
            {
                var num = (i + 1) + "\n";

                var size = MeasureString(num);
                if (size > maxSize)
                {
                    maxSize = size;
                }

                builder.Append(num);
            }
            
            LineNumberMarginWidth = maxSize + LineNumberWidthOffset;

            var lineNumberString = builder.ToString();
            var lineNumbers = new FormattedText(
                  lineNumberString,
                    System.Globalization.CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight,
                    new Typeface(FontFamily.Source),
                    FontSize,
                    LineNumberBrush);
            return lineNumbers;
        }
    }
}