using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace KeyBoard.WPF.UControl
{
    public class BaseKeyBoard : UserControl
    {
        public Brush UCBackground
        {
            get { return (Brush)GetValue(UCBackgroundProperty); }
            set { SetValue(UCBackgroundProperty, value); }
        }

        // 键盘背景色
        public static readonly DependencyProperty UCBackgroundProperty =
            DependencyProperty.Register("UCBackground", typeof(Brush), typeof(NumericKeyboard), new PropertyMetadata(new SolidColorBrush(Colors.Green)));


    }
}
