using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using KeyBoard.WPF.Controls.Attach;
using KeyBoard.WPF.UControl;
using KeyBoard.WPF.ViewModels;
using Microsoft.Xaml.Behaviors;

namespace KeyBoard.WPF.Behavior
{
    public class NumericKeyboardBehavior : AbstractKeyboardBehavior<NumericKeyboard>
    {
        public override double Width { get; set; } = 300;
        public override double Height { get; set; } = 250;

    }
}
