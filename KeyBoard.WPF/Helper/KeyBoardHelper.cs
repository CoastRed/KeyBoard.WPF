using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace KeyBoard.WPF.Helper
{
    public static class KeyBoardHelper
    {
        private const int KEYEVENTF_KEYDOWN = 0x0;
        private const int KEYEVENTF_EXTENDEDKEY = 0x1;
        private const int KEYEVENTF_KEYUP = 0x2;
        private static readonly Dictionary<string, byte> _keyNameToCodeMap = new Dictionary<string, byte>()
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

        /// <summary>
        /// 键盘输入SendKeyboardEvent
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

        public static bool Shift => System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.LeftShift) ||
               System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.RightShift);

        public static bool Ctrl => System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.LeftCtrl) ||
               System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.RightCtrl);

        public static bool Alt => System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.LeftAlt) ||
               System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.RightAlt);

        public static bool CapsLock => System.Windows.Input.Keyboard.IsKeyToggled(System.Windows.Input.Key.CapsLock);


        public static void SendKeyboard(string keyName)
        {
            if (_keyNameToCodeMap.TryGetValue(keyName, out byte keyCode))
            {
                // 模拟按键按下
                keybd_event(keyCode, 0, KEYEVENTF_KEYDOWN, 0);
                // 模拟按键释放
                keybd_event(keyCode, 0, KEYEVENTF_KEYUP, 0);
            }
            else
            {
                //throw new ArgumentException($"未找到键名对应的键码: {keyName}");
            }
        }

        public static void SendKeyboardDown(string keyName)
        {
            if (_keyNameToCodeMap.TryGetValue(keyName, out byte keyCode))
            {
                // 模拟按键按下
                keybd_event(keyCode, 0, KEYEVENTF_KEYDOWN, 0);
            }
            else
            {
                //throw new ArgumentException($"未找到键名对应的键码: {keyName}");
            }
        }

        public static void SendKeyboardUp(string keyName)
        {
            if (_keyNameToCodeMap.TryGetValue(keyName, out byte keyCode))
            {
                // 模拟按键释放
                keybd_event(keyCode, 0, KEYEVENTF_KEYUP, 0);
            }
            else
            {
                //throw new ArgumentException($"未找到键名对应的键码: {keyName}");
            }
        }

        public static void SendKeyboard(byte keyCode)
        {
            // 模拟按键按下
            keybd_event(keyCode, 0, KEYEVENTF_KEYDOWN, 0);
            // 模拟按键释放
            keybd_event(keyCode, 0, KEYEVENTF_KEYUP, 0);
        }

        public static void SendKeyboardDown(byte keyCode)
        {
            // 模拟按键按下
            keybd_event(keyCode, 0, KEYEVENTF_KEYDOWN, 0);
        }

        public static void SendKeyboardUp(byte keyCode)
        {
            // 模拟按键释放
            keybd_event(keyCode, 0, KEYEVENTF_KEYUP, 0);
        }


    }
}
