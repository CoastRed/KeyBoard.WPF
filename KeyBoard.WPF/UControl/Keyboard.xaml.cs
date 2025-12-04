using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace KeyBoard.WPF.UControl
{
    /// <summary>
    /// Keyboard.xaml 的交互逻辑
    /// </summary>
    public partial class Keyboard : UserControl
    {
        private static Dictionary<string, byte> keycode = new Dictionary<string, byte>()
        {
            {"`", 0xC0 },
            {"1", 0x31 },
            {"2", 0x32 },
            {"3", 0x33 },
            {"4", 0x34 },
            {"5", 0x35 },
            {"6", 0x36 },
            {"7", 0x37 },
            {"8", 0x38 },
            {"9", 0x39 },
            {"0", 0x30 },
            {"=", 0xBB },
            {"-", 0xBD },



            {"A", 0x41 },
            {"B", 0x42 },
            {"C", 0x43 },
            {"D", 0x44 },
            {"E", 0x45 },
            {"F", 0x46 },
            {"G", 0x47 },
            {"H", 0x48 },
            {"I", 0x49 },
            {"J", 0x4A },
            {"K", 0x4B },
            {"L", 0x4C },
            {"M", 0x4D },
            {"N", 0x4E },
            {"O", 0x4F },
            {"P", 0x50 },
            {"Q", 0x51 },
            {"R", 0x52 },
            {"S", 0x53 },
            {"T", 0x54 },
            {"U", 0x55 },
            {"V", 0x56 },
            {"W", 0x57 },
            {"X", 0x58 },
            {"Y", 0x59 },
            {"Z", 0x5A },

            {"F1", 0x70 },
            {"F2", 0x71 },
            {"F3", 0x72 },
            {"F4", 0x73 },
            {"F5", 0x74 },
            {"F6", 0x75 },
            {"F7", 0x76 },
            {"F8", 0x77 },
            {"F9", 0x78 },
            {"F10", 0x79 },
            {"F11", 0x7A },
            {"F12", 0x7B },

            {"[", 0xDB },
            {"]", 0xDD },
            {"\\", 0xDC },
            {";", 0xBA },
            {"'", 0xDE },
            {",", 0xBC },
            {".", 0xBE },
            {"/", 0xBF },

            {"Esc", 0x1B },
            
            {"Space", 0x20 },
            {"Backspace", 0x08 },
            {"Tab", 0x09 },
            
           
            {"CAPS", 0x14 },
            
            {"Shift", 0x10 },
            
            {"Enter", 0x6C },
            {"Ctrl", 0x11 },
            {"Alt", 0x12 },
            
        };

        private HashSet<string> _isPressedKey = new HashSet<string>();

        /// <summary>
        /// 键盘输入
        /// </summary>
        /// <param name="bVK"></param>
        /// <param name="bScan"></param>
        /// <param name="dwFlags"></param>
        /// <param name="dwExtraInfo"></param>
        [DllImport("User32.dll")]
        public static extern void keybd_event(byte bVK, byte bScan, Int32 dwFlags, int dwExtraInfo);

        /// <summary>
        /// 获取键盘状态
        /// </summary>
        /// <param name="pbKeyState"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "GetKeyboardState")]
        public static extern int GetKeyboardState(byte[] pbKeyState);

        /// <summary>
        /// CapsLock按键状态
        /// </summary>
        public bool CapsLock 
        { 
            get
            {
                byte[] bs = new byte[256];
                GetKeyboardState(bs);
                this.tgCapsLock.IsChecked = (bs[0x14] == 1);
                return (bs[0x14] == 1);
            }
        }

        /// <summary>
        /// Shift按键状态
        /// </summary>
        public bool Shift
        {
            get
            {
                byte[] bs = new byte[256];
                GetKeyboardState(bs);
                this.tgCapsLock.IsChecked = (bs[0x10] == 1);
                return (bs[0x10] == 1);
            }
        }


        /// <summary>
        /// 键盘关闭事件
        /// </summary>
        public event EventHandler? ClosedEvent;

        

        /// <summary>
        /// 初始化
        /// </summary>
        public Keyboard()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.tgCapsLock.IsChecked = this.CapsLock;
        }

        private void Grid_Click(object sender, RoutedEventArgs e)
        {
            ButtonBase? btn = e.OriginalSource as ButtonBase;
            if (btn == null)
            {
                return;
            }
            string? content = btn.Content?.ToString();
            if (string.IsNullOrWhiteSpace(content))
            {
                return;
            }

            if (keycode.TryGetValue(content!, out byte code))
            {
                if (content == "Enter")
                {
                    keybd_event(keycode[content], 0, 0, 0);
                    keybd_event(keycode[content], 0, 2, 0);
                    ReleaseKey();
                    this.ClosedEvent?.Invoke(this, new EventArgs());
                    return;
                }
                if (_isPressedKey.Contains(content!))
                {
                    keybd_event(code, 0, 2, 0);
                    _isPressedKey.Remove(content!);
                    return;
                }
                if(content == "Shift" || content == "Ctrl" || content == "Alt")
                {
                    keybd_event(code, 0, 0, 0);
                    _isPressedKey.Add(content!);
                    return;
                }
                else
                {
                    keybd_event(code, 0, 0, 0);
                    keybd_event(code, 0, 2, 0);
                    ReleaseKey();
                }
            }
        }

        private void ReleaseKey()
        {
            foreach (var key in _isPressedKey)
            {
                if (keycode.TryGetValue(key, out byte code))
                {
                    keybd_event(code, 0, 2, 0);
                }
            }
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
