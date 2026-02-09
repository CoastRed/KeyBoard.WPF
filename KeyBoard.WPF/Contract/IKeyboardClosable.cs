using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyBoard.WPF.Contract
{
    public interface IKeyboardClosable
    {
        public event EventHandler? Closed;
    }
}
