using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Media.Animation;


namespace WpfAnimating.AnimatedProperties
{
    public partial class AP
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
            DependencyProperty.RegisterAttached("InitialClickedPoint", typeof(bool), typeof(AP), new PropertyMetadata(false, null, registerClickedPoint));

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
            DependencyProperty.RegisterAttachedReadOnly("ClickedPoint", typeof(Point), typeof(AP), new PropertyMetadata(new Point(0, 0)));
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

    }
}
