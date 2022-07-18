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
            {"0", 48 },
            {"1", 49 },
            {"2", 50 },
            {"3", 51 },
            {"4", 52 },
            {"5", 53 },
            {"6", 54 },
            {"7", 55 },
            {"8", 56 },
            {"9", 57 },
            {"A", 65 },
            {"B", 66 },
            {"C", 67 },
            {"D", 68 },
            {"E", 69 },
            {"F", 70 },
            {"G", 71 },
            {"H", 72 },
            {"I", 73 },
            {"J", 74 },
            {"K", 75 },
            {"L", 76 },
            {"M", 77 },
            {"N", 78 },
            {"O", 79 },
            {"P", 80 },
            {"Q", 81 },
            {"R", 82 },
            {"S", 83 },
            {"T", 84 },
            {"U", 85 },
            {"V", 86 },
            {"W", 87 },
            {"X", 88 },
            {"Y", 89 },
            {"Z", 90 },
            {"`", 192 },
            {"Space", 32 },
            {"Backspace", 8 },
            {"Tab", 9 },
            {"[", 219 },
            {"]", 221 },
            {"\\", 220 },
            {"CapsLock", 20 },
            {";", 186 },
            {"'", 222 },
            {"Shift", 16 },
            {",", 188 },
            {".", 190 },
            {"/", 191 },
            {"Enter", 108 },
            {"Ctrl", 17 },
            {"Alt", 18 },
            {"=", 187 },
            {"-", 189 }
        };

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
                return (bs[0x14] == 1);
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

            if (this.CapsLock)
            {
                this.tgCapsLock.IsChecked = true;
            }
        }

        private void Grid_Click(object sender, RoutedEventArgs e)
        {
            ButtonBase? btn = e.OriginalSource as ButtonBase;
            if (btn == null)
            {
                return;
            }
            string? content = btn.Content?.ToString();
            if (string.IsNullOrEmpty(content))
            {
                return;
            }
            if (content == "Enter")
            {
                keybd_event(keycode[content], 0, 0, 0);
                keybd_event(keycode[content], 0, 2, 0);
                this.ClosedEvent?.Invoke(this, new EventArgs());
                return;
            }


            keybd_event(keycode[content], 0, 0, 0);
            keybd_event(keycode[content], 0, 2, 0);


        }


    }
}
