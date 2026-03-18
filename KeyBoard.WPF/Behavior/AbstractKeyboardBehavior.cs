using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using KeyBoard.WPF.Contract;
using KeyBoard.WPF.Controls.Attach;
using KeyBoard.WPF.UControl;
using KeyBoard.WPF.ViewModels;
using Microsoft.Xaml.Behaviors;

namespace KeyBoard.WPF.Behavior
{
    public abstract class AbstractKeyboardBehavior<TView> : Behavior<Control> where TView : UserControl, IKeyboardClosable
    {
        public Panel? Panel { get; set; }

        public Popup? Popup { get; set; }

        public UserControl? Keyboard { get; set; }

        public IKeyboardClosable? KeyboardClosable => this.Keyboard as IKeyboardClosable;



        /// <summary>
        /// 键盘背景色
        /// </summary>
        public virtual Brush? Background { get; set; }

        /// <summary>
        /// 字母键（a-z/A-Z）字号
        /// </summary>
        public virtual double AlphabetKeyFontSize { get; set; } = 24;
        /// <summary>
        /// 功能键（Esc、Tab、Shift、Ctrl、Enter、Backspace等）字号
        /// </summary>
        public virtual double FunctionKeyFontSize { get; set; } = 16;

        /// <summary>
        /// F1-F12键字号
        /// </summary>
        public virtual double FKeyFontSize { get; set; } = 16;

        /// <summary>
        /// 数字+符号键（0-9 + !@#$%^*()等）字号
        /// </summary>
        public virtual double NumberAndSymbolKeyFontSize { get; set; } = 20;

        public virtual double Width { get; set; } = 800;

        public virtual double Height { get; set; } = 600;



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

        protected virtual void AssociatedObject_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.Popup is not null)
            {
                this.Popup.IsOpen = false;
            }
            this.Panel?.Children.Remove(this.Popup);
        }

        protected virtual void AssociatedObject_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            //获取Panel
            this.Panel ??= this.GetParent<Panel>(this.AssociatedObject);
            if (this.Panel is null)
            {
                return;
            }

            if (this.Keyboard is null)
            {
                this.Keyboard = Activator.CreateInstance<TView>() as UserControl;
                this.KeyboardClosable?.Closed += UserControl_ClosedEvent;
                this.SetDependencyProperty();
            }

            if (this.Popup is null)
            {
                this.Popup = new Popup();
                Popup.Width = this.Width;
                Popup.Height = this.Height;
                Popup.AllowsTransparency = true;
                Popup.AllowDrop = true;
                Popup.Child = Keyboard;
                Popup.StaysOpen = true;
                Popup.PlacementTarget = this.AssociatedObject;
                Popup.Placement = PlacementMode.Bottom;

                //Popup.PopupAnimation = PopupAnimation.None;
                //Popup.MouseLeftButtonDown += Popup_MouseLeftButtonDown;
                //Popup.MouseMove += Popup_MouseMove;
                //Popup.MouseLeftButtonUp += Popup_MouseLeftButtonUp;
                //Popup.MouseLeave += Popup_MouseLeave;
                //Popup.CustomPopupPlacementCallback = CalculatePopupPlacement;
            }

            if (this.Panel.Children.Contains(this.Popup) is false)
            {
                if(this.Panel is DockPanel)
                {
                    // DockPanel需要特殊处理，放在最前面
                    this.Panel.Children.Insert(0, Popup);
                }
                else
                {
                    this.Panel.Children.Add(Popup);
                }
            }
            Popup.IsOpen = true;
        }

        


        protected void UserControl_ClosedEvent(object? sender, EventArgs e)
        {
            if (this.Panel is not null)
            {
                this.Panel.Focusable = true;
                this.Panel.Focus();
            }
            //bool? re = this.Panel?.Focus();
        }

        public virtual void SetDependencyProperty()
        {
            if (Background is not null)
            {
                Keyboard?.SetValue(KeyBoardAttach.BackgroundProperty, this.Background);
            }
            Keyboard?.SetValue(KeyBoardAttach.AlphabetKeyFontSizeProperty, this.AlphabetKeyFontSize);
            Keyboard?.SetValue(KeyBoardAttach.FunctionKeyFontSizeProperty, this.FunctionKeyFontSize);
            Keyboard?.SetValue(KeyBoardAttach.FKeyFontSizeProperty, this.FKeyFontSize);
            Keyboard?.SetValue(KeyBoardAttach.NumberAndSymbolKeyFontSizeProperty, this.NumberAndSymbolKeyFontSize);
        }

        protected T? GetParent<T>(DependencyObject dependencyObject) where T : DependencyObject
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


        #region 拖动相关


        //// 拖动状态变量（适配PlacementMode.Bottom）
        //private bool _isDragging; // 是否正在拖动
        //private Point _mouseOffset; // 鼠标按下时，相对于PlacementTarget的偏移
        //private double _initHorizontalOffset; // Popup初始水平偏移（拖动前）
        //private double _initVerticalOffset; // Popup初始垂直偏移（拖动前）

        //private void Popup_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        //{
        //    if(Popup is null)
        //    {
        //        return;
        //    }
        //    _isDragging = true;
        //    Popup.CaptureMouse(); // 捕获鼠标，防止拖动中断

        //    // 记录Popup拖动前的初始偏移（基于PlacementTarget）
        //    _initHorizontalOffset = Popup.HorizontalOffset;
        //    _initVerticalOffset = Popup.VerticalOffset;

        //    // 计算鼠标按下时，相对于PlacementTarget（targetBtn）的坐标
        //    // 关键：基准是PlacementTarget，而非屏幕/Popup
        //    _mouseOffset = e.GetPosition(this.AssociatedObject);
        //}
        //private void Popup_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        //{
        //    if (_isDragging && Popup is not null)
        //    {
        //        // 获取当前鼠标相对于PlacementTarget（targetBtn）的坐标
        //        Point currentMousePos = e.GetPosition(this.AssociatedObject);

        //        // 计算新偏移：初始偏移 + (当前鼠标位置 - 按下时鼠标位置)
        //        double newHorizontalOffset = _initHorizontalOffset + (currentMousePos.X - _mouseOffset.X);
        //        double newVerticalOffset = _initVerticalOffset + (currentMousePos.Y - _mouseOffset.Y);

        //        // 更新Popup的相对偏移（适配PlacementMode.Bottom的核心）
        //        Popup.HorizontalOffset = newHorizontalOffset;
        //        Popup.VerticalOffset = newVerticalOffset;
        //    }
        //}
        //private void Popup_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        //{
        //    _isDragging = false;
        //    Popup?.ReleaseMouseCapture();
        //}
        //private void Popup_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        //{
        //    _isDragging = false;
        //    Popup?.ReleaseMouseCapture();
        //}
        //public CustomPopupPlacement[] CalculatePopupPlacement(Size popupSize, Size targetSize, Point offset)
        //{
        //    //return new[]
        //    //{
        //    //    new CustomPopupPlacement(new Point(offset.X, offset.Y), PopupPrimaryAxis.None)
        //    //};
        //    // 基于PlacementTarget的Bottom位置计算固定偏移，不自动调整
        //    Point fixedPos = new Point(
        //        targetSize.Width / 2 + offset.X, // 水平居中+偏移
        //        targetSize.Height + offset.Y);   // 底部+垂直偏移
        //    return new[] { new CustomPopupPlacement(fixedPos, PopupPrimaryAxis.None) };
        //}


        #endregion
    }

    //// 禁用Popup自动调整的回调类
    //public class FixedPopupPlacementCallback : CustomPopupPlacementCallback
    //{
    //    public override CustomPopupPlacement[] CalculatePopupPlacement(Size popupSize, Size targetSize, Point offset)
    //    {
    //        return new[]
    //        {
    //            new CustomPopupPlacement(new Point(offset.X, offset.Y), PopupPrimaryAxis.None)
    //        };
    //    }

    //    // 适配PlacementTarget+Bottom的固定位置回调
    //    public override CustomPopupPlacement[] CalculatePopupPlacement(Size popupSize, Size targetSize, Point offset)
    //    {
    //        // 基于PlacementTarget的Bottom位置计算固定偏移，不自动调整
    //        Point fixedPos = new Point(
    //            targetSize.Width / 2 + offset.X, // 水平居中+偏移
    //            targetSize.Height + offset.Y);   // 底部+垂直偏移
    //        return new[] { new CustomPopupPlacement(fixedPos, PopupPrimaryAxis.None) };
    //    }
    //}

    //public abstract class AbstractKeyboardBehavior<TView, TViewModel> : AbstractKeyboardBehavior<TView> where TView : UserControl where TViewModel : ViewModelBase, new()
    //{
    //    protected new virtual void AssociatedObject_GotFocus(object sender, System.Windows.RoutedEventArgs e)
    //    {
    //        //获取Panel
    //        this.Panel ??= base.GetParent<Panel>(this.AssociatedObject);
    //        if (this.Panel is null)
    //        {
    //            return;
    //        }

    //        if (this.UserControl is null)
    //        {
    //            this.UserControl = Activator.CreateInstance<TView>() as UserControl;
    //            this.UserControl.DataContext = Activator.CreateInstance<TViewModel>();
    //            this.SetDependencyProperty();
    //        }

    //        if (this.Popup is null)
    //        {
    //            this.Popup = new Popup();
    //            Popup.Child = UserControl;
    //            Popup.StaysOpen = true;
    //            Popup.PlacementTarget = this.AssociatedObject;
    //            Popup.Placement = PlacementMode.Bottom;
    //        }

    //        if (this.Panel.Children.Contains(this.Popup) is false)
    //        {
    //            this.Panel.Children.Add(Popup);
    //        }
    //        Popup.IsOpen = true;
    //    }
    //}

}
