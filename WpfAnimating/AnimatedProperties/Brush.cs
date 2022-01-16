using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace WpfAnimating.AnimatedProperties
{
    partial class AP
    {
        public static Duration GetBackgroundDuration(DependencyObject obj)
        {
            return (Duration)obj.GetValue(BackgroundDurationProperty);
        }
        public static void SetBackgroundDuration(DependencyObject obj, Duration value)
        {
            obj.SetValue(BackgroundDurationProperty, value);
        }
        public static readonly DependencyProperty BackgroundDurationProperty =
            DependencyProperty.RegisterAttached("BackgroundDuration", typeof(Duration), typeof(AP), new PropertyMetadata(new Duration(TimeSpan.FromSeconds(3))));

        public static IEasingFunction GetBackgroundEasingFunction(DependencyObject obj) => (IEasingFunction)obj.GetValue(BackgroundEasingFunctionProperty);
        public static void SetBackgroundEasingFunction(DependencyObject obj, IEasingFunction value) => obj.SetValue(BackgroundEasingFunctionProperty, value);
        public static readonly DependencyProperty BackgroundEasingFunctionProperty =
            DependencyProperty.RegisterAttached("BackgroundEasingFunction", typeof(IEasingFunction), typeof(AP), new PropertyMetadata(null));

        public static Brush GetBackground(DependencyObject obj) => (Brush)obj.GetValue(BackgroundProperty);
        public static void SetBackground(DependencyObject obj, Brush value) => obj.SetValue(BackgroundProperty, value);
        public static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.RegisterAttached("Background", typeof(Brush), typeof(AP),
                new UIPropertyMetadata(Brushes.Transparent,
                    new BrushAnimatedProperty(Panel.BackgroundProperty,
                        BackgroundDurationProperty,
                        BackgroundEasingFunctionProperty
                        ).PropertyChangedCallback,
                    null, false));

    }
}
