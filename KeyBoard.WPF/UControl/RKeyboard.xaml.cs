using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// RKeyboard.xaml 的交互逻辑
    /// </summary>
    public partial class RKeyboard : UserControl, IKeyboardClosable
    {

        public event EventHandler? Closed;

        private HashSet<string> _isPressedKey = new HashSet<string>();

        /// <summary>
        /// 初始化
        /// </summary>
        public RKeyboard()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.tgCapsLock.IsChecked = KeyBoardHelper.CapsLock;
            this._isPressedKey.Clear();
        }

        public void UpdateKeyboardState()
        {
            this.tgAlt1.IsChecked = KeyBoardHelper.Alt;
            this.tgCtrl1.IsChecked = KeyBoardHelper.Ctrl;
            this.tgShift1.IsChecked = KeyBoardHelper.Shift;
        }

        private void Grid_Click(object sender, RoutedEventArgs e)
        {
            ButtonBase? btn = e.OriginalSource as ButtonBase;
            if (btn == null)
            {
                return;
            }
            string? content = btn.Content?.ToString() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(content))
            {
                return;
            }

            if (content == "Enter")
            {
                KeyBoardHelper.SendKeyboard(content);
                this.Closed?.Invoke(this, EventArgs.Empty);
                return;
            }

            else if (_isPressedKey.Contains(content))
            {
                KeyBoardHelper.SendKeyboard(content);
                _isPressedKey.Remove(content);
                return;
            }

            if (content == "Shift" || content == "Ctrl" || content == "Alt")
            {
                _isPressedKey.Add(content!);
                return;
            }
            else
            {
                foreach (var key in _isPressedKey)
                {
                    KeyBoardHelper.SendKeyboardDown(key);
                }
                KeyBoardHelper.SendKeyboard(content);
                foreach (var key in _isPressedKey)
                {
                    KeyBoardHelper.SendKeyboardUp(key);
                }
                _isPressedKey.Clear();
                this.UpdateKeyboardState();
                return;
            }
        }

        private void ReleaseKey()
        {
            _isPressedKey.Clear();
            this.tgAlt1.IsChecked = false;
            this.tgCtrl1.IsChecked = false;
            this.tgShift1.IsChecked = false;
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            ReleaseKey();
        }


    }
}
