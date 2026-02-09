using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace KeyBoard.WPF.Controls.Attach
{
    public class KeyBoardAttach
    {

        public static Brush GetBackground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(BackgroundProperty);
        }

        public static void SetBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(BackgroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for UCBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.RegisterAttached("Background", typeof(Brush), typeof(KeyBoardAttach), new PropertyMetadata(Brushes.Orange));


        public static double GetAlphabetKeyFontSize(DependencyObject obj)
        {
            return (double)obj.GetValue(AlphabetKeyFontSizeProperty);
        }

        public static void SetAlphabetKeyFontSize(DependencyObject obj, double value)
        {
            obj.SetValue(AlphabetKeyFontSizeProperty, value);
        }

        /// <summary>
        /// 字母键（a-z/A-Z）字号
        /// </summary>
        public static readonly DependencyProperty AlphabetKeyFontSizeProperty =
            DependencyProperty.RegisterAttached("AlphabetKeyFontSize", typeof(double), typeof(KeyBoardAttach), new PropertyMetadata(24.0));



        public static double GetFunctionKeyFontSize(DependencyObject obj)
        {
            return (double)obj.GetValue(FunctionKeyFontSizeProperty);
        }

        public static void SetFunctionKeyFontSize(DependencyObject obj, double value)
        {
            obj.SetValue(FunctionKeyFontSizeProperty, value);
        }

        /// <summary>
        /// 功能键（Esc、Tab、Shift、Ctrl、Enter、Backspace等）字号
        /// </summary>
        public static readonly DependencyProperty FunctionKeyFontSizeProperty =
            DependencyProperty.RegisterAttached("FunctionKeyFontSize", typeof(double), typeof(KeyBoardAttach), new PropertyMetadata(16.0));



        public static double GetFKeyFontSize(DependencyObject obj)
        {
            return (double)obj.GetValue(FKeyFontSizeProperty);
        }

        public static void SetFKeyFontSize(DependencyObject obj, double value)
        {
            obj.SetValue(FKeyFontSizeProperty, value);
        }

        /// <summary>
        /// F1-F12键字号
        /// </summary>
        public static readonly DependencyProperty FKeyFontSizeProperty =
            DependencyProperty.RegisterAttached("FKeyFontSize", typeof(double), typeof(KeyBoardAttach), new PropertyMetadata(16.0));



        public static double GetNumberAndSymbolKeyFontSize(DependencyObject obj)
        {
            return (double)obj.GetValue(NumberAndSymbolKeyFontSizeProperty);
        }

        public static void SetNumberAndSymbolKeyFontSize(DependencyObject obj, double value)
        {
            obj.SetValue(NumberAndSymbolKeyFontSizeProperty, value);
        }

        /// <summary>
        /// 数字+符号键（0-9 + !@#$%^*()等）字号
        /// </summary>
        public static readonly DependencyProperty NumberAndSymbolKeyFontSizeProperty =
            DependencyProperty.RegisterAttached("NumberAndSymbolKeyFontSize", typeof(double), typeof(KeyBoardAttach), new PropertyMetadata(20.0));


    }
}
