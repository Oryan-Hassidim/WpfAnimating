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
    internal static class AnimatedPropertiesSet
    {
        internal static Dictionary<(DependencyObject, DependencyProperty), int> AnimatedProperties = new();
    }

    public class DoubleAnimatedProperty
    {
        private readonly DependencyProperty animatedProperty;
        private readonly DependencyProperty easingFunctionProperty;
        private readonly DependencyProperty durationProperty;
        private DoubleAnimation animation;

        public DoubleAnimatedProperty(DependencyProperty animatedProperty, Duration duration, IEasingFunction easingFunction)
        {
            this.animatedProperty = animatedProperty ?? throw new ArgumentNullException(nameof(animatedProperty));
            if (animatedProperty.PropertyType != typeof(double)) throw new ArgumentException(nameof(animatedProperty));
            if (!duration.HasTimeSpan) throw new ArgumentOutOfRangeException(nameof(duration));
            animation = new()
            {
                Duration = duration,
                EasingFunction = easingFunction
            };
            easingFunctionProperty = null;
            durationProperty = null;
        }
        public DoubleAnimatedProperty(DependencyProperty animatedProperty, DependencyProperty durationProperty, IEasingFunction easingFunction)
        {
            this.animatedProperty = animatedProperty ?? throw new ArgumentNullException(nameof(animatedProperty));
            if (animatedProperty.PropertyType != typeof(double)) throw new ArgumentException(nameof(animatedProperty));
            animation = new()
            {
                EasingFunction = easingFunction
            };
            easingFunctionProperty = null;
            this.durationProperty = durationProperty ?? throw new ArgumentNullException(nameof(durationProperty));
            if (durationProperty?.PropertyType != typeof(Duration)) throw new ArgumentException(nameof(durationProperty));
        }
        public DoubleAnimatedProperty(DependencyProperty animatedProperty, DependencyProperty durationProperty, DependencyProperty easingFunctionProperty)
        {
            this.animatedProperty = animatedProperty ?? throw new ArgumentNullException(nameof(animatedProperty));
            if (animatedProperty.PropertyType != typeof(double)) throw new ArgumentException(nameof(animatedProperty));
            animation = new();
            this.easingFunctionProperty = easingFunctionProperty ?? throw new ArgumentNullException(nameof(easingFunctionProperty));
            if (easingFunctionProperty?.PropertyType != typeof(IEasingFunction)) throw new ArgumentException(nameof(easingFunctionProperty));
            this.durationProperty = durationProperty ?? throw new ArgumentNullException(nameof(durationProperty));
            if (durationProperty?.PropertyType != typeof(Duration)) throw new ArgumentException(nameof(durationProperty));
        }
        public DoubleAnimatedProperty(DependencyProperty animatedProperty, Duration duration, DependencyProperty easingFunctionProperty)
        {
            this.animatedProperty = animatedProperty ?? throw new ArgumentNullException(nameof(animatedProperty));
            if (animatedProperty.PropertyType != typeof(double)) throw new ArgumentException(nameof(animatedProperty));
            if (!duration.HasTimeSpan) throw new ArgumentOutOfRangeException(nameof(duration));
            animation = new()
            {
                Duration = duration
            };
            this.easingFunctionProperty = easingFunctionProperty ?? throw new ArgumentNullException(nameof(easingFunctionProperty));
            if (easingFunctionProperty?.PropertyType != typeof(IEasingFunction)) throw new ArgumentException(nameof(easingFunctionProperty));
            durationProperty = null;
        }

        private async void PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is IConvertible && d is IAnimatable animatable)
            {
                double newValue = Convert.ToDouble(e.NewValue);
                var key = (d, animatedProperty);
                AnimatedPropertiesSet.AnimatedProperties.TryAdd(key, 0);
                AnimatedPropertiesSet.AnimatedProperties[key]++;
                animation.To = newValue;
                animatable.ApplyAnimationClock(animatedProperty, animation.CreateClock());
                await Task.Delay((int)animation.Duration.TimeSpan.TotalMilliseconds + 100);
                AnimatedPropertiesSet.AnimatedProperties[key]--;
                if (AnimatedPropertiesSet.AnimatedProperties[key] == 0)
                {
                    d.SetValue(animatedProperty, newValue);
                    animatable.ApplyAnimationClock(animatedProperty, null);
                    AnimatedPropertiesSet.AnimatedProperties.Remove(key);
                }
            }
        }
        private async void PropertyChanged_Duration(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is IConvertible && d is IAnimatable animatable)
            {
                double newValue = Convert.ToDouble(e.NewValue);
                var key = (d, animatedProperty);
                AnimatedPropertiesSet.AnimatedProperties.TryAdd(key, 0);
                AnimatedPropertiesSet.AnimatedProperties[key]++;
                animation.To = Convert.ToDouble(newValue);
                animation.Duration = (Duration)d.GetValue(durationProperty);
                animatable.ApplyAnimationClock(animatedProperty, animation.CreateClock());
                await Task.Delay((int)animation.Duration.TimeSpan.TotalMilliseconds + 100);
                AnimatedPropertiesSet.AnimatedProperties[key]--;
                if (AnimatedPropertiesSet.AnimatedProperties[key] == 0)
                {
                    d.SetValue(animatedProperty, newValue);
                    animatable.ApplyAnimationClock(animatedProperty, null);
                    AnimatedPropertiesSet.AnimatedProperties.Remove(key);
                }
            }
        }
        private async void PropertyChanged_EasingFunction(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is IConvertible && d is IAnimatable animatable)
            {
                double newValue = Convert.ToDouble(e.NewValue);
                var key = (d, animatedProperty);
                AnimatedPropertiesSet.AnimatedProperties.TryAdd(key, 0);
                AnimatedPropertiesSet.AnimatedProperties[key]++;
                animation.To = Convert.ToDouble(newValue);
                animation.EasingFunction = (IEasingFunction)d.GetValue(easingFunctionProperty);
                animatable.ApplyAnimationClock(animatedProperty, animation.CreateClock());
                await Task.Delay((int)animation.Duration.TimeSpan.TotalMilliseconds + 100);
                AnimatedPropertiesSet.AnimatedProperties[key]--;
                if (AnimatedPropertiesSet.AnimatedProperties[key] == 0)
                {
                    d.SetValue(animatedProperty, newValue);
                    animatable.ApplyAnimationClock(animatedProperty, null);
                    AnimatedPropertiesSet.AnimatedProperties.Remove(key);
                }
            }
        }
        private async void PropertyChanged_DurationAndEasingFunction(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is IConvertible && d is IAnimatable animatable)
            {
                double newValue = Convert.ToDouble(e.NewValue);
                var key = (d, animatedProperty);
                AnimatedPropertiesSet.AnimatedProperties.TryAdd(key, 0);
                AnimatedPropertiesSet.AnimatedProperties[key]++;
                animation.To = Convert.ToDouble(newValue);
                animation.Duration = (Duration)d.GetValue(durationProperty);
                animation.EasingFunction = (IEasingFunction)d.GetValue(easingFunctionProperty);
                animatable.ApplyAnimationClock(animatedProperty, animation.CreateClock());
                await Task.Delay((int)animation.Duration.TimeSpan.TotalMilliseconds + 100);
                AnimatedPropertiesSet.AnimatedProperties[key]--;
                if (AnimatedPropertiesSet.AnimatedProperties[key] == 0)
                {
                    d.SetValue(animatedProperty, newValue);
                    animatable.ApplyAnimationClock(animatedProperty, null);
                    AnimatedPropertiesSet.AnimatedProperties.Remove(key);
                }
            }
        }
        public PropertyChangedCallback PropertyChangedCallback => (durationProperty, easingFunctionProperty) switch
        {
            (null, null) => PropertyChanged,
            (_, null) => PropertyChanged_Duration,
            (null, _) => PropertyChanged_EasingFunction,
            (_, _) => PropertyChanged_DurationAndEasingFunction
        };
    }
    public class ColorAnimatedProperty
    {
        private readonly DependencyProperty animatedProperty;
        private readonly DependencyProperty easingFunctionProperty;
        private readonly DependencyProperty durationProperty;
        private ColorAnimation animation;

        public ColorAnimatedProperty(DependencyProperty animatedProperty, Duration duration, IEasingFunction easingFunction)
        {
            this.animatedProperty = animatedProperty ?? throw new ArgumentNullException(nameof(animatedProperty));
            if (animatedProperty.PropertyType != typeof(Color)) throw new ArgumentException(nameof(animatedProperty));
            if (!duration.HasTimeSpan) throw new ArgumentOutOfRangeException(nameof(duration));
            animation = new()
            {
                Duration = duration,
                EasingFunction = easingFunction
            };
            easingFunctionProperty = null;
            durationProperty = null;
        }
        public ColorAnimatedProperty(DependencyProperty animatedProperty, DependencyProperty durationProperty, IEasingFunction easingFunction)
        {
            this.animatedProperty = animatedProperty ?? throw new ArgumentNullException(nameof(animatedProperty));
            if (animatedProperty.PropertyType != typeof(Color)) throw new ArgumentException(nameof(animatedProperty));
            animation = new()
            {
                EasingFunction = easingFunction
            };
            easingFunctionProperty = null;
            this.durationProperty = durationProperty ?? throw new ArgumentNullException(nameof(durationProperty));
            if (durationProperty?.PropertyType != typeof(Duration)) throw new ArgumentException(nameof(durationProperty));
        }
        public ColorAnimatedProperty(DependencyProperty animatedProperty, DependencyProperty durationProperty, DependencyProperty easingFunctionProperty)
        {
            this.animatedProperty = animatedProperty ?? throw new ArgumentNullException(nameof(animatedProperty));
            if (animatedProperty.PropertyType != typeof(Color)) throw new ArgumentException(nameof(animatedProperty));
            animation = new();
            this.easingFunctionProperty = easingFunctionProperty ?? throw new ArgumentNullException(nameof(easingFunctionProperty));
            if (easingFunctionProperty?.PropertyType != typeof(IEasingFunction)) throw new ArgumentException(nameof(easingFunctionProperty));
            this.durationProperty = durationProperty ?? throw new ArgumentNullException(nameof(durationProperty));
            if (durationProperty?.PropertyType != typeof(Duration)) throw new ArgumentException(nameof(durationProperty));
        }
        public ColorAnimatedProperty(DependencyProperty animatedProperty, Duration duration, DependencyProperty easingFunctionProperty)
        {
            this.animatedProperty = animatedProperty ?? throw new ArgumentNullException(nameof(animatedProperty));
            if (animatedProperty.PropertyType != typeof(Color)) throw new ArgumentException(nameof(animatedProperty));
            if (!duration.HasTimeSpan) throw new ArgumentOutOfRangeException(nameof(duration));
            animation = new()
            {
                Duration = duration
            };
            this.easingFunctionProperty = easingFunctionProperty ?? throw new ArgumentNullException(nameof(easingFunctionProperty));
            if (easingFunctionProperty?.PropertyType != typeof(IEasingFunction)) throw new ArgumentException(nameof(easingFunctionProperty));
            durationProperty = null;
        }

        private async void PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is Color c && d is IAnimatable animatable)
            {
                var key = (d, animatedProperty);
                AnimatedPropertiesSet.AnimatedProperties.TryAdd(key, 0);
                AnimatedPropertiesSet.AnimatedProperties[key]++;
                animation.To = c;
                animatable.ApplyAnimationClock(animatedProperty, animation.CreateClock());
                await Task.Delay((int)animation.Duration.TimeSpan.TotalMilliseconds + 100);
                AnimatedPropertiesSet.AnimatedProperties[key]--;
                if (AnimatedPropertiesSet.AnimatedProperties[key] == 0)
                {
                    d.SetValue(animatedProperty, c);
                    animatable.ApplyAnimationClock(animatedProperty, null);
                    AnimatedPropertiesSet.AnimatedProperties.Remove(key);
                }
            }
        }
        private async void PropertyChanged_Duration(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is Color c && d is IAnimatable animatable)
            {
                var key = (d, animatedProperty);
                AnimatedPropertiesSet.AnimatedProperties.TryAdd(key, 0);
                AnimatedPropertiesSet.AnimatedProperties[key]++;
                animation.To = c;
                animation.Duration = (Duration)d.GetValue(durationProperty);
                animatable.ApplyAnimationClock(animatedProperty, animation.CreateClock());
                await Task.Delay((int)animation.Duration.TimeSpan.TotalMilliseconds + 100);
                AnimatedPropertiesSet.AnimatedProperties[key]--;
                if (AnimatedPropertiesSet.AnimatedProperties[key] == 0)
                {
                    d.SetValue(animatedProperty, c);
                    animatable.ApplyAnimationClock(animatedProperty, null);
                    AnimatedPropertiesSet.AnimatedProperties.Remove(key);
                }
            }
        }
        private async void PropertyChanged_EasingFunction(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is Color c && d is IAnimatable animatable)
            {
                var key = (d, animatedProperty);
                AnimatedPropertiesSet.AnimatedProperties.TryAdd(key, 0);
                AnimatedPropertiesSet.AnimatedProperties[key]++;
                animation.To = c;
                animation.EasingFunction = (IEasingFunction)d.GetValue(easingFunctionProperty);
                animatable.ApplyAnimationClock(animatedProperty, animation.CreateClock());
                await Task.Delay((int)animation.Duration.TimeSpan.TotalMilliseconds + 100);
                AnimatedPropertiesSet.AnimatedProperties[key]--;
                if (AnimatedPropertiesSet.AnimatedProperties[key] == 0)
                {
                    d.SetValue(animatedProperty, c);
                    animatable.ApplyAnimationClock(animatedProperty, null);
                    AnimatedPropertiesSet.AnimatedProperties.Remove(key);
                }
            }
        }
        private async void PropertyChanged_DurationAndEasingFunction(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is Color c && d is IAnimatable animatable)
            {
                var key = (d, animatedProperty);
                AnimatedPropertiesSet.AnimatedProperties.TryAdd(key, 0);
                AnimatedPropertiesSet.AnimatedProperties[key]++;
                animation.To = c;
                animation.Duration = (Duration)d.GetValue(durationProperty);
                animation.EasingFunction = (IEasingFunction)d.GetValue(easingFunctionProperty);
                animatable.ApplyAnimationClock(animatedProperty, animation.CreateClock());
                await Task.Delay((int)animation.Duration.TimeSpan.TotalMilliseconds + 100);
                AnimatedPropertiesSet.AnimatedProperties[key]--;
                if (AnimatedPropertiesSet.AnimatedProperties[key] == 0)
                {
                    d.SetValue(animatedProperty, c);
                    animatable.ApplyAnimationClock(animatedProperty, null);
                    AnimatedPropertiesSet.AnimatedProperties.Remove(key);
                }
            }
        }
        public PropertyChangedCallback PropertyChangedCallback => (durationProperty, easingFunctionProperty) switch
        {
            (null, null) => PropertyChanged,
            (_, null) => PropertyChanged_Duration,
            (null, _) => PropertyChanged_EasingFunction,
            (_, _) => PropertyChanged_DurationAndEasingFunction
        };
    }
    public class BrushAnimatedProperty
    {
        private readonly DependencyProperty animatedProperty;
        private readonly DependencyProperty easingFunctionProperty;
        private readonly DependencyProperty durationProperty;
        private DoubleAnimation animation;

        public BrushAnimatedProperty(DependencyProperty animatedProperty, Duration duration, IEasingFunction easingFunction)
        {
            this.animatedProperty = animatedProperty ?? throw new ArgumentNullException(nameof(animatedProperty));
            if (animatedProperty.PropertyType != typeof(Brush)) throw new ArgumentException(nameof(animatedProperty));
            if (!duration.HasTimeSpan) throw new ArgumentOutOfRangeException(nameof(duration));
            animation = new()
            {
                Duration = duration,
                EasingFunction = easingFunction
            };
            easingFunctionProperty = null;
            durationProperty = null;
        }
        public BrushAnimatedProperty(DependencyProperty animatedProperty, DependencyProperty durationProperty, IEasingFunction easingFunction)
        {
            this.animatedProperty = animatedProperty ?? throw new ArgumentNullException(nameof(animatedProperty));
            if (animatedProperty.PropertyType != typeof(Brush)) throw new ArgumentException(nameof(animatedProperty));
            animation = new()
            {
                EasingFunction = easingFunction
            };
            easingFunctionProperty = null;
            this.durationProperty = durationProperty ?? throw new ArgumentNullException(nameof(durationProperty));
            if (durationProperty?.PropertyType != typeof(Duration)) throw new ArgumentException(nameof(durationProperty));
        }
        public BrushAnimatedProperty(DependencyProperty animatedProperty, DependencyProperty durationProperty, DependencyProperty easingFunctionProperty)
        {
            this.animatedProperty = animatedProperty ?? throw new ArgumentNullException(nameof(animatedProperty));
            if (animatedProperty.PropertyType != typeof(Brush)) throw new ArgumentException(nameof(animatedProperty));
            animation = new();
            this.easingFunctionProperty = easingFunctionProperty ?? throw new ArgumentNullException(nameof(easingFunctionProperty));
            if (easingFunctionProperty?.PropertyType != typeof(IEasingFunction)) throw new ArgumentException(nameof(easingFunctionProperty));
            this.durationProperty = durationProperty ?? throw new ArgumentNullException(nameof(durationProperty));
            if (durationProperty?.PropertyType != typeof(Duration)) throw new ArgumentException(nameof(durationProperty));
        }
        public BrushAnimatedProperty(DependencyProperty animatedProperty, Duration duration, DependencyProperty easingFunctionProperty)
        {
            this.animatedProperty = animatedProperty ?? throw new ArgumentNullException(nameof(animatedProperty));
            if (animatedProperty.PropertyType != typeof(Brush)) throw new ArgumentException(nameof(animatedProperty));
            if (!duration.HasTimeSpan) throw new ArgumentOutOfRangeException(nameof(duration));
            animation = new()
            {
                Duration = duration
            };
            this.easingFunctionProperty = easingFunctionProperty ?? throw new ArgumentNullException(nameof(easingFunctionProperty));
            if (easingFunctionProperty?.PropertyType != typeof(IEasingFunction)) throw new ArgumentException(nameof(easingFunctionProperty));
            durationProperty = null;
        }

        private async void PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is Brush && d is IAnimatable animatable)
            {
                var newValue = e.NewValue as Brush;
                var key = (d, animatedProperty);
                AnimatedPropertiesSet.AnimatedProperties.TryAdd(key, 0);
                AnimatedPropertiesSet.AnimatedProperties[key]++;
                var border = new Border()
                {
                    BorderThickness = new Thickness(0),
                    Width = 1,
                    Height = 1,
                    Background = newValue,
                    Opacity = 0
                };
                Brush brush = new VisualBrush()
                {
                    Visual = new Border()
                    {
                        BorderThickness = new Thickness(0),
                        Width = 1,
                        Height = 1,
                        Background = e.OldValue as Brush,
                        Child = border
                    }
                };
                d.SetValue(animatedProperty, brush);
                animation.From = 0;
                animation.To = 1;
                border.ApplyAnimationClock(UIElement.OpacityProperty, animation.CreateClock());
                await Task.Delay((int)animation.Duration.TimeSpan.TotalMilliseconds + 100);
                AnimatedPropertiesSet.AnimatedProperties[key]--;
                if (AnimatedPropertiesSet.AnimatedProperties[key] == 0)
                {
                    d.SetValue(animatedProperty, newValue);
                    border.ApplyAnimationClock(UIElement.OpacityProperty, null);
                    AnimatedPropertiesSet.AnimatedProperties.Remove(key);
                }
            }
        }
        private async void PropertyChanged_Duration(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is Brush && d is IAnimatable animatable)
            {
                var newValue = e.NewValue as Brush;
                var key = (d, animatedProperty);
                AnimatedPropertiesSet.AnimatedProperties.TryAdd(key, 0);
                AnimatedPropertiesSet.AnimatedProperties[key]++;
                var border = new Border()
                {
                    BorderThickness = new Thickness(0),
                    Width = 1,
                    Height = 1,
                    Background = newValue,
                    Opacity = 0
                };
                Brush brush = new VisualBrush()
                {
                    Visual = new Border()
                    {
                        BorderThickness = new Thickness(0),
                        Width = 1,
                        Height = 1,
                        Background = e.OldValue as Brush,
                        Child = border
                    }
                };
                d.SetValue(animatedProperty, brush);
                animation.From = 0;
                animation.To = 1;
                animation.Duration = (Duration)d.GetValue(durationProperty);
                border.ApplyAnimationClock(UIElement.OpacityProperty, animation.CreateClock());
                await Task.Delay((int)animation.Duration.TimeSpan.TotalMilliseconds + 100);
                AnimatedPropertiesSet.AnimatedProperties[key]--;
                if (AnimatedPropertiesSet.AnimatedProperties[key] == 0)
                {
                    d.SetValue(animatedProperty, newValue);
                    border.ApplyAnimationClock(UIElement.OpacityProperty, null);
                    AnimatedPropertiesSet.AnimatedProperties.Remove(key);
                }
            }
        }
        private async void PropertyChanged_EasingFunction(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is Brush && d is IAnimatable animatable)
            {
                var newValue = e.NewValue as Brush;
                var key = (d, animatedProperty);
                var border = new Border()
                {
                    BorderThickness = new Thickness(0),
                    Width = 1,
                    Height = 1,
                    Background = newValue,
                    Opacity = 0
                };
                Brush brush = new VisualBrush()
                {
                    Visual = new Border()
                    {
                        BorderThickness = new Thickness(0),
                        Width = 1,
                        Height = 1,
                        Background = e.OldValue as Brush,
                        Child = border
                    }
                };
                d.SetValue(animatedProperty, brush);
                animation.From = 0;
                animation.To = 1;
                animation.EasingFunction = (IEasingFunction)d.GetValue(easingFunctionProperty);
                border.ApplyAnimationClock(UIElement.OpacityProperty, animation.CreateClock());
                await Task.Delay((int)animation.Duration.TimeSpan.TotalMilliseconds + 100);
                AnimatedPropertiesSet.AnimatedProperties[key]--;
                if (AnimatedPropertiesSet.AnimatedProperties[key] == 0)
                {
                    d.SetValue(animatedProperty, newValue);
                    border.ApplyAnimationClock(UIElement.OpacityProperty, null);
                    AnimatedPropertiesSet.AnimatedProperties.Remove(key);
                }
            }
        }
        private async void PropertyChanged_DurationAndEasingFunction(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is Brush && d is IAnimatable animatable)
            {
                var newValue = e.NewValue as Brush;
                var key = (d, animatedProperty);
                AnimatedPropertiesSet.AnimatedProperties.TryAdd(key, 0);
                AnimatedPropertiesSet.AnimatedProperties[key]++;
                var border = new Border()
                {
                    BorderThickness = new Thickness(0),
                    Width = 1,
                    Height = 1,
                    Background = newValue,
                    Opacity = 0
                };
                Brush brush = new VisualBrush()
                {
                    Visual = new Border()
                    {
                        BorderThickness = new Thickness(0),
                        Width = 1,
                        Height = 1,
                        Background = e.OldValue as Brush,
                        Child = border
                    }
                };
                d.SetValue(animatedProperty, brush);
                animation.From = 0;
                animation.To = 1;
                animation.Duration = (Duration)d.GetValue(durationProperty);
                animation.EasingFunction = (IEasingFunction)d.GetValue(easingFunctionProperty);
                border.ApplyAnimationClock(UIElement.OpacityProperty, animation.CreateClock());
                await Task.Delay((int)animation.Duration.TimeSpan.TotalMilliseconds + 100);
                AnimatedPropertiesSet.AnimatedProperties[key]--;
                if (AnimatedPropertiesSet.AnimatedProperties[key] == 0)
                {
                    d.SetValue(animatedProperty, newValue);
                    border.ApplyAnimationClock(UIElement.OpacityProperty, null);
                    AnimatedPropertiesSet.AnimatedProperties.Remove(key);
                }
            }
        }
        public PropertyChangedCallback PropertyChangedCallback => (durationProperty, easingFunctionProperty) switch
        {
            (null, null) => PropertyChanged,
            (_, null) => PropertyChanged_Duration,
            (null, _) => PropertyChanged_EasingFunction,
            (_, _) => PropertyChanged_DurationAndEasingFunction
        };
    }

}
