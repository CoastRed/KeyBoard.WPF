using KeyBoard.WPF.Controls.Attach;
using KeyBoard.WPF.UControl;
using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace KeyBoard.WPF.Behavior
{
    /// <summary>
    /// 全键盘
    /// </summary>
    public class KeyboardBehavior : AbstractKeyboardBehavior<Keyboard>
    {
        public override double Width { get; set; } = 800;
        public override double Height { get; set; } = 350;
    }
}
