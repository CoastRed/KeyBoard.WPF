using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace KeyBoard.WPF.Controls.Attach
{
    public class UCAttach
    {


        public static Brush GetUCBackground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(UCBackgroundProperty);
        }

        public static void SetUCBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(UCBackgroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for UCBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UCBackgroundProperty =
            DependencyProperty.RegisterAttached("UCBackground", typeof(Brush), typeof(UCAttach), new PropertyMetadata(Brushes.Orange));


        public static double GetUCFontSize(DependencyObject obj)
        {
            return (double)obj.GetValue(UCFontSizeProperty);
        }

        public static void SetUCFontSize(DependencyObject obj, double value)
        {
            obj.SetValue(UCFontSizeProperty, value);
        }

        // Using a DependencyProperty as the backing store for UCFontSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UCFontSizeProperty =
            DependencyProperty.RegisterAttached("UCFontSize", typeof(double), typeof(UCAttach), new PropertyMetadata(20.0));




    }
}
