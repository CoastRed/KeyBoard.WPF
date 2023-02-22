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

namespace KeyBoard.WPF.UControl
{
    /// <summary>
    /// NumericKeyboard.xaml 的交互逻辑
    /// </summary>
    public partial class NumericKeyboard : UserControl
    {

        [DllImport("User32.dll")]
        public static extern void keybd_event(byte bVK, byte bScan, Int32 dwFlags, int dwExtraInfo);

        public event EventHandler? ClosedEvent;

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
            {"←", 8 },
            {".", 190 },
        };

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
            string? content = btn.Content == null ? string.Empty : btn.Content.ToString();
            if (string.IsNullOrEmpty(content))
            {
                return;
            }
            if (content == "确认")
            {
                this.ClosedEvent?.Invoke(this, new EventArgs());
                return;
            }
            keybd_event(keycode[content], 0, 0, 0);
            keybd_event(keycode[content], 0, 2, 0);
        }

    }
}
