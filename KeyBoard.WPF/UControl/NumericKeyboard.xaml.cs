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
            {"0", 96 },
            {"1", 97 },
            {"2", 98 },
            {"3", 99 },
            {"4", 100 },
            {"5", 101 },
            {"6", 102 },
            {"7", 103 },
            {"8", 104 },
            {"9", 105 },
            {"←", 8 },
            {".", 110 },
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
            if (content == "确认" || content == "Enter")
            {
                this.ClosedEvent?.Invoke(this, new EventArgs());
                return;
            }
            keybd_event(keycode[content], 0, 0, 0);
            keybd_event(keycode[content], 0, 2, 0);
        }

    }
}
