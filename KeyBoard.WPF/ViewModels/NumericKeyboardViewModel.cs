using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyBoard.WPF.Common;
using KeyBoard.WPF.Helper;

namespace KeyBoard.WPF.ViewModels
{
    public class NumericKeyboardViewModel : ViewModelBase
    {
        public RelayCommand<string> ButtonCommand { get; }


        public NumericKeyboardViewModel()
        {
            ButtonCommand = new RelayCommand<string>(OnButtonCommand);
        }

        public void OnButtonCommand(string keyName)
        {
            KeyBoardHelper.SendKeyboard(keyName);
        }
    }
}
