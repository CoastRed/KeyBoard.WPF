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
    public class RNumericKeyboardBehavior : AbstractKeyboardBehavior<RNumericKeyboard>
    {
        public override double Width { get; set; } = 525;
        public override double Height { get; set; } = 253;

        public override void SetDependencyProperty()
        {
            Background ??= new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EEEEEE"));
            base.SetDependencyProperty();
        }
    }
}
