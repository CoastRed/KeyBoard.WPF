using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using KeyBoard.WPF.Contract;
using KeyBoard.WPF.Helper;

namespace KeyBoard.WPF.UControl
{
    /// <summary>
    /// NumericKeyboard.xaml 的交互逻辑
    /// </summary>
    public partial class NumericKeyboard : UserControl, IKeyboardClosable
    {
        public event EventHandler? Closed;

        public NumericKeyboard()
        {
            InitializeComponent();
        }

        private void Grid_Click(object sender, RoutedEventArgs e)
        {
            Button? btn = e.OriginalSource as Button;
            if (btn == null)
            {
                return;
            }

            if (btn.Tag is not null && btn.Tag is Key key)
            {

                KeyBoardHelper.SendKeyboard(key);
                if (key == Key.Separator)
                {
                    KeyBoardHelper.WpfKeyboardPress(key);
                    this.Closed?.Invoke(this, new EventArgs());
                    return;
                }
            }
        }

    }
}
