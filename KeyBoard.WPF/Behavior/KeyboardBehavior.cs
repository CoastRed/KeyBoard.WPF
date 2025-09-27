using KeyBoard.WPF.Controls.Attach;
using KeyBoard.WPF.UControl;
using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace KeyBoard.WPF.Behavior
{
    /// <summary>
    /// 全键盘
    /// </summary>
    public class KeyboardBehavior : Behavior<Control>
    {
        /// <summary>
        /// 触发键盘的控件所在的容器
        /// </summary>
        public Panel? Panel { get; set; }

        public Popup? Popup { get; set; }

        public Keyboard? Keyboard { get; set; }

        /// <summary>
        /// 小键盘背景色
        /// </summary>
        public Brush? UCBackground { get; set; }

        public double UCFontSize { get; set; } = 15;

        public KeyboardBehavior()
        {
            //this.popup.AllowsTransparency= true;
            //this.popup.AllowDrop = true;
        }

        protected override void OnAttached()
        {
            this.AssociatedObject.GotFocus += AssociatedObject_GotFocus;
            this.AssociatedObject.LostFocus += AssociatedObject_LostFocus;
            base.OnAttached();
        }

        protected override void OnDetaching()
        {
            this.AssociatedObject.GotFocus -= AssociatedObject_GotFocus;
            this.AssociatedObject.LostFocus -= AssociatedObject_LostFocus;
            base.OnDetaching();
        }

        private void AssociatedObject_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.Popup is not null)
            {
                this.Popup.IsOpen = false;
            }
            this.Panel?.Children.Remove(this.Popup);
        }

        private void AssociatedObject_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            //获取Panel
            this.Panel ??= this.GetParent<Panel>(this.AssociatedObject);
            if (this.Panel is null)
            {
                return;
            }

            if (this.Keyboard is null)
            {
                this.Keyboard = new Keyboard();
                if (this.UCBackground != null)
                {
                    Keyboard.SetValue(UCAttach.UCBackgroundProperty, this.UCBackground);
                }
                Keyboard.SetValue(UCAttach.UCFontSizeProperty, this.UCFontSize);
                Keyboard.ClosedEvent += Keyboard_ClosedEvent;
            }

            if (this.Popup is null)
            {
                this.Popup = new Popup();
                Popup.Child = Keyboard;
                Popup.StaysOpen = true;
                Popup.PlacementTarget = this.AssociatedObject;
                Popup.Placement = PlacementMode.Bottom;
            }

            if (this.Panel.Children.Contains(this.Popup) is false)
            {
                this.Panel.Children.Add(Popup);
            }
            Popup.IsOpen = true;

        }

        private void Keyboard_ClosedEvent(object? sender, EventArgs e)
        {
            if (this.Panel is not null)
            {
                this.Panel.Focusable = true;
                this.Panel.Focus();
            }
        }

        private T? GetParent<T>(DependencyObject dependencyObject) where T : DependencyObject
        {
            dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
            if (dependencyObject == null)
            {
                return default;
            }
            if (dependencyObject is Window)
            {
                return default;
            }
            if (dependencyObject is T)
            {
                return dependencyObject as T;
            }
            else
            {
                return this.GetParent<T>(dependencyObject);
            }
        }
    }
}
