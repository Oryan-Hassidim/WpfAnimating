using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace WpfAnimating.AnimatedProperties
{
    partial class AP
    {
        public static Duration GetColorDuration(DependencyObject obj)
        {
            return (Duration)obj.GetValue(ColorDurationProperty);
        }
        public static void SetColorDuration(DependencyObject obj, Duration value)
        {
            obj.SetValue(ColorDurationProperty, value);
        }
        public static readonly DependencyProperty ColorDurationProperty =
            DependencyProperty.RegisterAttached("ColorDuration", typeof(Duration), typeof(AP), new PropertyMetadata(new Duration(TimeSpan.FromSeconds(3))));

        public static IEasingFunction GetColorEasingFunction(DependencyObject obj) => (IEasingFunction)obj.GetValue(ColorEasingFunctionProperty);
        public static void SetColorEasingFunction(DependencyObject obj, IEasingFunction value) => obj.SetValue(ColorEasingFunctionProperty, value);
        public static readonly DependencyProperty ColorEasingFunctionProperty =
            DependencyProperty.RegisterAttached("ColorEasingFunction", typeof(IEasingFunction), typeof(AP), new PropertyMetadata(null));

        public static Color GetColor(DependencyObject obj) => (Color)obj.GetValue(ColorProperty);
        public static void SetColor(DependencyObject obj, Color value) => obj.SetValue(ColorProperty, value);
        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.RegisterAttached("Color", typeof(Color), typeof(Color),
                new UIPropertyMetadata(Colors.Black,
                    new ColorAnimatedProperty(SolidColorBrush.ColorProperty,
                        ColorDurationProperty,
                        ColorEasingFunctionProperty).PropertyChangedCallback,
                    null, false));

    }
}
