using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
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
    /// Keyboard.xaml 的交互逻辑
    /// </summary>
    public partial class Keyboard : UserControl, IKeyboardClosable
    {

        public event EventHandler? Closed;

        private readonly HashSet<Key> _isPressedKeys = new HashSet<Key>();

        /// <summary>
        /// 初始化
        /// </summary>
        public Keyboard()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.tgCapsLock.IsChecked = KeyBoardHelper.CapsLock;
            this._isPressedKeys.Clear();
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
            if (btn.Tag is not null && btn.Tag is Key key)
            {

                if (key == Key.Enter)
                {
                    KeyBoardHelper.SendKeyboard(key);
                    KeyBoardHelper.WpfKeyboardPress(key);
                    this.Closed?.Invoke(this, EventArgs.Empty);
                    return;
                }

                else if (_isPressedKeys.Contains(key))
                {
                    KeyBoardHelper.SendKeyboard(key);
                    _isPressedKeys.Remove(key);
                    return;
                }

                if (key == Key.LeftShift || key == Key.RightShift || key == Key.LeftCtrl || key == Key.RightCtrl || key == Key.LeftAlt || key == Key.RightAlt)
                {
                    _isPressedKeys.Add(key);
                    return;
                }
                else
                {
                    foreach (var pressedKey in _isPressedKeys)
                    {
                        KeyBoardHelper.SendKeyboardDown(pressedKey);
                    }
                    KeyBoardHelper.SendKeyboard(key);
                    foreach (var pressedKey in _isPressedKeys)
                    {
                        KeyBoardHelper.SendKeyboardUp(pressedKey);
                    }
                    _isPressedKeys.Clear();
                    this.UpdateKeyboardState();
                    return;
                }
            }
        }

        private void ReleaseKey()
        {
            _isPressedKeys.Clear();
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
