using System;
using System.Collections.Generic;
using System.Linq;
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
    /// RNumericKeyboard.xaml 的交互逻辑
    /// </summary>
    public partial class RNumericKeyboard : UserControl, IKeyboardClosable
    {
        public event EventHandler? Closed;

        private static Dictionary<string, byte> _keyNameToCodeMap = new Dictionary<string, byte>()
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
            string? content = null;
            if (btn.Content is not null && btn.Content is string)
            {
                content = btn.Content.ToString();
            }
            
            content ??= btn.Tag == null ? string.Empty : btn.Tag.ToString();
            if (string.IsNullOrEmpty(content))
            {
                return;
            }
            if (content == "确认" || content == "Enter")
            {
                this.Closed?.Invoke(this, new EventArgs());
                return;
            }
            //keybd_event(_keyNameToCodeMap[content], 0, 0, 0);
            //keybd_event(_keyNameToCodeMap[content], 0, 2, 0);
            KeyBoardHelper.SendKeyboard(_keyNameToCodeMap[content]);
        }
    }
}
