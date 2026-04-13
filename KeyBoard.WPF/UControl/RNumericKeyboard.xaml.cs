using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using KeyBoard.WPF.Contract;
using KeyBoard.WPF.Helper;

namespace KeyBoard.WPF.UControl
{
    /// <summary>
    /// RNumericKeyboard.xaml 的交互逻辑
    /// </summary>
    public partial class RNumericKeyboard : UserControl, IKeyboardClosable
    {
        public event EventHandler? Closed;

        public RNumericKeyboard()
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
