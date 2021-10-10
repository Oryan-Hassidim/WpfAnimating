using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAnimating
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfAnimating"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfAnimating;assembly=WpfAnimating"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
    [ContentProperty("Content")]
    [DefaultProperty("Content")]
    [Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
    public class AnimatedContentControl : ContentControl
    {
        private static Storyboard defaultOutStoryboard = null;
        private static Storyboard defaultInStoryboard = null;
        public Storyboard OutStoryboard
        {
            get { return (Storyboard)GetValue(OutStoryboardProperty); }
            set { SetValue(OutStoryboardProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OutStoryboard.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OutStoryboardProperty =
            DependencyProperty.Register("OutStoryboard", typeof(Storyboard), typeof(AnimatedContentControl), new PropertyMetadata(defaultOutStoryboard));


        public Storyboard InStoryboard
        {
            get { return (Storyboard)GetValue(InStoryboardProperty); }
            set { SetValue(InStoryboardProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InStoryboard.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InStoryboardProperty =
            DependencyProperty.Register("InStoryboard", typeof(Storyboard), typeof(AnimatedContentControl), new PropertyMetadata(defaultInStoryboard));


        public Grid grid => (Grid)Template?.FindName("PartPanel", this);
        private DependencyProperty[] exclude = new DependencyProperty[]
        {
            NameProperty,
            ContentProperty,
            HasContentProperty
        };
        public async override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ContentControl _new = new()
            {
                Content = this.Content,
                RenderTransformOrigin = new Point(0.5, 0.5),
                RenderTransform = new TransformGroup()
                {
                    Children = new TransformCollection()
                    {
                        new ScaleTransform(),
                        new SkewTransform(),
                        new RotateTransform(),
                        new TranslateTransform()
                    }
                }
            };
            var localValueEnumerator = GetLocalValueEnumerator();
            while (localValueEnumerator.MoveNext())
            {
                if (exclude.Contains(localValueEnumerator.Current.Property)) continue;
                _new.SetBinding(localValueEnumerator.Current.Property,
                    new Binding()
                    {
                        Source = this,
                        Path = new PropertyPath(localValueEnumerator.Current.Property.Name)
                    });
            }
            grid.Children.Insert(grid.Children.Count, _new);

            async Task _in()
            {
                if (InStoryboard is not null)
                {
                    var _in = InStoryboard.Clone();
                    _new.BeginStoryboard(_in);
                    await Task.Delay(_in.Duration.TimeSpan);
                    _in.Remove();
                }
            }

            Task t1 = _in();
            await t1;
        }

        protected override async void OnContentChanged(object oldContent, object newContent)
        {
            if (this.grid is null) return;
            var grid = this.grid;

            ContentControl[] _old = grid.Children.OfType<ContentControl>().ToArray();

            ContentControl _new = new()
            {
                Content = newContent,
                RenderTransformOrigin = new Point(0.5, 0.5),
                RenderTransform = new TransformGroup()
                {
                    Children = new TransformCollection()
                    {
                        new ScaleTransform(),
                        new SkewTransform(),
                        new RotateTransform(),
                        new TranslateTransform()
                    }
                }
            };
            var localValueEnumerator = GetLocalValueEnumerator();
            while (localValueEnumerator.MoveNext())
            {
                if (exclude.Contains(localValueEnumerator.Current.Property)) continue;
                _new.SetBinding(localValueEnumerator.Current.Property,
                    new Binding()
                    {
                        Source = this,
                        Path = new PropertyPath(localValueEnumerator.Current.Property.Name)
                    });
            }
            grid.Children.Insert(grid.Children.Count, _new);

            async Task _out()
            {
                if (OutStoryboard is not null)
                {
                    var _out = OutStoryboard.Clone();
                    foreach (var item in _old)
                    {
                        item.BeginStoryboard(_out);
                    }
                    await Task.Delay(_out.Duration.TimeSpan);
                    foreach (var item in _old)
                    {
                        grid.Children.Remove(item);
                    }
                    _out.Remove();
                }
                else
                {
                    grid.Children.RemoveAt(0);
                }
            }

            async Task _in()
            {
                if (InStoryboard is not null)
                {
                    var _in = InStoryboard.Clone();
                    _new.BeginStoryboard(_in);
                    await Task.Delay(_in.Duration.TimeSpan);
                    _in.Remove();
                }
            }

            Task t1 = _in(), t2 = _out();
            await Task.WhenAll(t1, t2);
            //base.OnContentChanged(oldContent, newContent);
        }


        static AnimatedContentControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AnimatedContentControl), new FrameworkPropertyMetadata(typeof(AnimatedContentControl)));
        }
    }
}
