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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TrackMapAnimationTest
{
    class TestPointAnimation : PointAnimationUsingPath
    {
        private Point ov;
        private Point dv;

        protected override Freezable CreateInstanceCore()
        {
            return new TestPointAnimation();
        }
        protected override Point GetCurrentValueCore(Point defaultOriginValue, Point defaultDestinationValue, AnimationClock animationClock)
        {
            Point ret = base.GetCurrentValueCore(defaultOriginValue, defaultDestinationValue, animationClock);
            ov = defaultOriginValue;
            dv = defaultDestinationValue;

            var it = animationClock.CurrentProgress;
            var t = animationClock.CurrentTime;

            Point pos, tg;
            PathGeometry.GetPointAtFractionLength(it.Value, out pos, out tg);
            return pos;
        }

        protected override void OnChanged()
        {
            base.OnChanged();
        }
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Windows.Threading.DispatcherTimer _timer;
        float pos;
        float delta;
        TestPointAnimation pa;
        public MainWindow()
        {
            InitializeComponent();
            _timer = new System.Windows.Threading.DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(16)
            };
            _timer.Tick += _timer_Tick;
            pos = 0;
            delta = 3.0118780942341358733505261374546e-4f;

            path1.Freeze();
            pa = new TestPointAnimation();
            pa.PathGeometry = path1;
            TestPointAnimation.SetDesiredFrameRate(pa, 60);

            pa.Duration = TimeSpan.FromSeconds(53.123f);

        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            if (pos >= 1f)
                _timer.Stop();

            pos += delta;
            Point p;
            Point t;
            path1.GetPointAtFractionLength(pos, out p, out t);

            circ.SetValue(EllipseGeometry.CenterProperty, p);
            //circ.InvalidateProperty(EllipseGeometry.CenterProperty);
        }

        private void Path_Loaded(object sender, RoutedEventArgs e)
        {
            circ.BeginAnimation(EllipseGeometry.CenterProperty, pa);
            //_timer.Start();
        }
    }
}
