using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Media.Animation;


namespace WpfAnimating.ViewHelpers
{
    public class AnimatedProperties
    {


        public static bool GetInitialClickedPoint(DependencyObject obj)
        {
            return (bool)obj.GetValue(InitialClickedPointProperty);
        }

        public static void SetInitialClickedPoint(DependencyObject obj, bool value)
        {
            obj.SetValue(InitialClickedPointProperty, value);
        }

        // Using a DependencyProperty as the backing store for InitialClickedPoint.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InitialClickedPointProperty =
            DependencyProperty.RegisterAttached("InitialClickedPoint", typeof(bool), typeof(AnimatedProperties), new PropertyMetadata(false, null, registerClickedPoint));

        private static object registerClickedPoint(DependencyObject d, object baseValue)
        {
            if (d is FrameworkElement element && !elements.Contains(element))
            {
                element.MouseLeftButtonDown += element_MouseLeftButtonDown;
                element.Unloaded += element_Unloaded;
            }
            return true;
        }


        private static HashSet<FrameworkElement> elements = new HashSet<FrameworkElement>();
        public static Point GetClickedPoint(DependencyObject obj)
        {
            return (Point)obj.GetValue(ClickedPointProperty);
        }
        private static void SetClickedPoint(DependencyObject obj, Point value)
            => obj.SetValue(ClickedPointPropertyKey, value);
        private static readonly DependencyPropertyKey ClickedPointPropertyKey =
            DependencyProperty.RegisterAttachedReadOnly("ClickedPoint", typeof(Point), typeof(AnimatedProperties), new PropertyMetadata(new Point(0, 0)));
        public static readonly DependencyProperty ClickedPointProperty =
            ClickedPointPropertyKey.DependencyProperty;

        private static void element_Unloaded(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element)
            {
                element.MouseLeftButtonDown -= element_MouseLeftButtonDown;
                element.Unloaded -= element_Unloaded;
            }
        }

        private static void element_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is FrameworkElement element)
            {
                SetClickedPoint(element, e.GetPosition(element));
            }
        }




        public static double GetHeight(DependencyObject obj)
        {
            return (double)obj.GetValue(HeightProperty);
        }
        public static void SetHeight(DependencyObject obj, double value)
        {
            obj.SetValue(HeightProperty, value);
        }
        public static readonly DependencyProperty HeightProperty =
            DependencyProperty.RegisterAttached("Height", typeof(double), typeof(AnimatedProperties),
                new UIPropertyMetadata(
                    0.0,
                    new DoubleAnimatedProperty(FrameworkElement.HeightProperty,
                        new Duration(TimeSpan.FromSeconds(3)),
                        new PowerEase() { EasingMode = EasingMode.EaseIn }).PropertyChangedCallback,
                    null, false));

        public static Duration GetDuration(DependencyObject obj)
        {
            return (Duration)obj.GetValue(DurationProperty);
        }
        public static void SetDuration(DependencyObject obj, Duration value)
        {
            obj.SetValue(DurationProperty, value);
        }
        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.RegisterAttached("Duration", typeof(Duration), typeof(AnimatedProperties), new PropertyMetadata(new Duration(TimeSpan.FromSeconds(3))));

        public static IEasingFunction GetEasingFunction(DependencyObject obj) => (IEasingFunction)obj.GetValue(EasingFunctionProperty);
        public static void SetEasingFunction(DependencyObject obj, IEasingFunction value) => obj.SetValue(EasingFunctionProperty, value);
        public static readonly DependencyProperty EasingFunctionProperty =
            DependencyProperty.RegisterAttached("EasingFunction", typeof(IEasingFunction), typeof(AnimatedProperties), new PropertyMetadata(null));

        public static double GetWidth(DependencyObject obj) => (double)obj.GetValue(WidthProperty);
        public static void SetWidth(DependencyObject obj, double value) => obj.SetValue(WidthProperty, value);
        public static readonly DependencyProperty WidthProperty =
            DependencyProperty.RegisterAttached("Width", typeof(double), typeof(AnimatedProperties),
                new UIPropertyMetadata(
                    0.0,
                    new DoubleAnimatedProperty(
                        FrameworkElement.WidthProperty,
                        DurationProperty,
                        EasingFunctionProperty).PropertyChangedCallback,
                    null, false));

        public static Color GetColor(DependencyObject obj) => (Color)obj.GetValue(ColorProperty);
        public static void SetColor(DependencyObject obj, Color value) => obj.SetValue(ColorProperty, value);
        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.RegisterAttached("Color", typeof(Color), typeof(Color),
                new UIPropertyMetadata(Colors.Black, 
                    new ColorAnimatedProperty(SolidColorBrush.ColorProperty, 
                        DurationProperty, EasingFunctionProperty).PropertyChangedCallback, null, false));
    }
}
