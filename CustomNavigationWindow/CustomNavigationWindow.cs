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

namespace CustomNavigationWindow
{
    /// <summary>
    /// このカスタム コントロールを XAML ファイルで使用するには、手順 1a または 1b の後、手順 2 に従います。
    ///
    /// 手順 1a) 現在のプロジェクトに存在する XAML ファイルでこのカスタム コントロールを使用する場合
    /// この XmlNamespace 属性を使用場所であるマークアップ ファイルのルート要素に
    /// 追加します:
    ///
    ///     xmlns:MyNamespace="clr-namespace:CustomNavigationWindow"
    ///
    ///
    /// 手順 1b) 異なるプロジェクトに存在する XAML ファイルでこのカスタム コントロールを使用する場合
    /// この XmlNamespace 属性を使用場所であるマークアップ ファイルのルート要素に
    /// 追加します:
    ///
    ///     xmlns:MyNamespace="clr-namespace:CustomNavigationWindow;assembly=CustomNavigationWindow"
    ///
    /// また、XAML ファイルのあるプロジェクトからこのプロジェクトへのプロジェクト参照を追加し、
    /// リビルドして、コンパイル エラーを防ぐ必要があります:
    ///
    ///     ソリューション エクスプローラーで対象のプロジェクトを右クリックし、
    ///     [参照の追加] の [プロジェクト] を選択してから、このプロジェクトを選択します。
    ///
    ///
    /// 手順 2)
    /// コントロールを XAML ファイルで使用します。
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
    public class CustomNavigationWindow : NavigationWindow
    {
        //初期化処理
        static CustomNavigationWindow()
        {
            
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomNavigationWindow), new FrameworkPropertyMetadata(typeof(CustomNavigationWindow)));

        }
        
        //イベントの関連付け
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            Button button_exit = GetTemplateChild("Exit_BUTTON") as Button; //閉じるボタンを探して割り当て
            /*
            Trigger trigger = new Trigger();
            trigger.Property = Button.IsMouseOverProperty;
            trigger.Value = true;
            trigger.Setters.Add(new Setter(Button.ForegroundProperty, new SolidColorBrush(Colors.White)));

            trigger.Setters.Add(new Setter(Button.BackgroundProperty, new SolidColorBrush(Colors.Red)));
            Trigger trigger2 = new Trigger();
            trigger2.Property = Button.IsMouseOverProperty;
            trigger2.Value = false;
            trigger2.Setters.Add(new Setter(Button.ForegroundProperty, new SolidColorBrush(Colors.Black)));

            trigger2.Setters.Add(new Setter(Button.BackgroundProperty, new SolidColorBrush(Colors.White)));
            Style style = new Style(typeof(Button));
            style.Triggers.Add(trigger2);
            style.Triggers.Add(trigger);
            button_exit.Style=style;

            button_exit.HorizontalAlignment = HorizontalAlignment.Right;
            button_exit.Width = 43;
            */
            
            button_exit.Click += exit_button_click; //閉じるボタンのクリックイベントの割り当て
            Button back_button = GetTemplateChild("BackButton") as Button;
            back_button.Click += back_click;
            this.SizeChanged += Window_SizeChanged; //Windowリサイズイベントの割り当て
            Grid grid1= GetTemplateChild("LayoutRoot1") as Grid;
            DockPanel dockPanel = GetTemplateChild("MainDockPanel") as DockPanel;
            //grid1.Width = DockPanel.WidthProperty.;
        }
        //閉じるボタンの実装
        private void exit_button_click(object sender,RoutedEventArgs e)
        {
            this.Close();
        }
        private void back_click(object sender,RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
        }
        //ウィンドウリサイズイベント
        private void Window_SizeChanged(object sender,RoutedEventArgs e)
        {
            switch (WindowState)
            {
                case WindowState.Maximized:
                    Grid layoutrootkun = GetTemplateChild("LayoutRoot1") as Grid;
                    layoutrootkun.Margin = new Thickness(9);
                    break;
                default:
                    Grid layoutrootkun2 = GetTemplateChild("LayoutRoot1") as Grid;
                    layoutrootkun2.Margin = new Thickness(1);
                    break;
            }
        }
        public static readonly DependencyProperty MarginRightProperty = DependencyProperty.RegisterAttached(
    "MarginRight",
    typeof(string),
    typeof(CustomNavigationWindow),
    new UIPropertyMetadata(OnMarginRightPropertyChanged));
        public static readonly DependencyProperty MarginLeftProperty = DependencyProperty.RegisterAttached(
"MarginLeft",
typeof(string),
typeof(CustomNavigationWindow),
new UIPropertyMetadata(OnMarginLeftPropertyChanged));
        public static string GetMarginRight(FrameworkElement element)
        {
            return (string)element.GetValue(MarginRightProperty);
        }

        public static void SetMarginRight(FrameworkElement element, string value)
        {
            element.SetValue(MarginRightProperty, value);
        }

        private static void OnMarginRightPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var element = obj as FrameworkElement;

            if (element != null)
            {
                int value;
                if (Int32.TryParse((string)args.NewValue, out value))
                {
                    var margin = element.Margin;
                    margin.Right = value;
                    element.Margin = margin;
                }
            }
        }
        public static string GetMarginLeft(FrameworkElement element)
        {
            return (string)element.GetValue(MarginLeftProperty);
        }

        public static void SetMarginLeft(FrameworkElement element, string value)
        {
            element.SetValue(MarginLeftProperty, value);
        }

        private static void OnMarginLeftPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var element = obj as FrameworkElement;

            if (element != null)
            {
                int value;
                if (Int32.TryParse((string)args.NewValue, out value))
                {
                    var margin = element.Margin;
                    margin.Left = value;
                    element.Margin = margin;
                }
            }
        }
    }
}
