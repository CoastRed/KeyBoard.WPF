using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using KeyBoard.WPF.Controls.Attach;
using KeyBoard.WPF.UControl;

namespace KeyBoard.WPF.Behavior
{
    public class RKeyboardBehavior : AbstractKeyboardBehavior<RKeyboard>
    {
        public override double Width { get; set; } = 1200;
        public override double Height { get; set; } = 400;

        public override void SetDependencyProperty()
        {
            Background ??= new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F3F3F3"));
            base.SetDependencyProperty();
        }
    }
}
