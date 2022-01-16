using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using WpfAnimating.AnimatedProperties;

namespace WpfAnimating.AnimatedProperties
{
    public partial class AP
    {
        public static Duration GetHeightWidthDuration(DependencyObject obj)
        {
            return (Duration)obj.GetValue(HeightWidthDurationProperty);
        }
        public static void SetHeightWidthDuration(DependencyObject obj, Duration value)
        {
            obj.SetValue(HeightWidthDurationProperty, value);
        }
        public static readonly DependencyProperty HeightWidthDurationProperty =
            DependencyProperty.RegisterAttached("HeightWidthDuration", typeof(Duration), typeof(AP), new PropertyMetadata(new Duration(TimeSpan.FromSeconds(3))));

        public static IEasingFunction GetHeightWidthEasingFunction(DependencyObject obj) => (IEasingFunction)obj.GetValue(HeightWidthEasingFunctionProperty);
        public static void SetHeightWidthEasingFunction(DependencyObject obj, IEasingFunction value) => obj.SetValue(HeightWidthEasingFunctionProperty, value);
        public static readonly DependencyProperty HeightWidthEasingFunctionProperty =
            DependencyProperty.RegisterAttached("HeightWidthEasingFunction", typeof(IEasingFunction), typeof(AP), new PropertyMetadata(null));

        public static double GetHeight(DependencyObject obj)
        {
            return (double)obj.GetValue(HeightProperty);
        }
        public static void SetHeight(DependencyObject obj, double value)
        {
            obj.SetValue(HeightProperty, value);
        }
        public static readonly DependencyProperty HeightProperty =
            DependencyProperty.RegisterAttached("Height", typeof(double), typeof(AP),
                new UIPropertyMetadata(
                    0.0,
                    new DoubleAnimatedProperty(FrameworkElement.HeightProperty,
                        HeightWidthDurationProperty,
                        HeightWidthEasingFunctionProperty).PropertyChangedCallback,
                    null, false));
        public static double GetWidth(DependencyObject obj) => (double)obj.GetValue(WidthProperty);
        public static void SetWidth(DependencyObject obj, double value) => obj.SetValue(WidthProperty, value);
        public static readonly DependencyProperty WidthProperty =
            DependencyProperty.RegisterAttached("Width", typeof(double), typeof(AP),
                new UIPropertyMetadata(
                    0.0,
                    new DoubleAnimatedProperty(
                        FrameworkElement.WidthProperty,
                        HeightWidthDurationProperty,
                        HeightWidthEasingFunctionProperty).PropertyChangedCallback,
                    null, false));

    }
}
