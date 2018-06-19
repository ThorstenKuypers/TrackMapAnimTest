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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Windows.Threading.DispatcherTimer _timer;
        float pos;
        float delta;

        public MainWindow()
        {
            InitializeComponent();
            _timer = new System.Windows.Threading.DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(100)
            };
            _timer.Tick += _timer_Tick;
            pos = 0;
            delta = 1.0f / 100.0f;

            path1.Freeze();
            PointAnimationUsingPath pa = new PointAnimationUsingPath();
            pa.PathGeometry = path1;
            pa.Duration = TimeSpan.FromSeconds(10);

            _timer.Start();
            //circ.BeginAnimation(EllipseGeometry.CenterProperty, pa);
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
    }
}
