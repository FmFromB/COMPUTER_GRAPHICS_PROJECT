using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Events;
using LiveCharts.Geared;
using System.ComponentModel;
using LiveCharts.Defaults;

namespace DSP
{
    /// <summary>
    /// Логика взаимодействия для OSC.xaml
    /// </summary>
    public partial class OSC : UserControl
    {
        public OSC()
        {
            InitializeComponent();
        }
        private void Axis_OnRangeChanged(RangeChangedEventArgs eventargs)
        {
            var vm = (ScrollableViewModel)DataContext;

            var currentRange = eventargs.Range;

            if (currentRange < TimeSpan.TicksPerDay * 2)
            {
                vm.Formatter = x => new DateTime((long)x).ToString("t");
                return;
            }

            if (currentRange < TimeSpan.TicksPerDay * 60)
            {
                vm.Formatter = x => new DateTime((long)x).ToString("dd MMM yy");
                return;
            }

            if (currentRange < TimeSpan.TicksPerDay * 540)
            {
                vm.Formatter = x => new DateTime((long)x).ToString("MMM yy");
                return;
            }

            vm.Formatter = x => new DateTime((long)x).ToString("yyyy");
        }

        public void Dispose()
        {
            var vm = (ScrollableViewModel)DataContext;
            vm.Values.Dispose();
        }
    }

    public class ScrollableViewModel : INotifyPropertyChanged
    {
        private Func<double, string> _formatter;
        private double _from;
        private double _to;

        public ScrollableViewModel()
        {
            var now = DateTime.Now;
            var trend = -30000d;
            var l = new List<DateTimePoint>();
            var r = new Random();

            for (var i = 0; i < 50000; i++)
            {
                now = now.AddHours(1);
                l.Add(new DateTimePoint(now.AddDays(i), trend));

                if (r.NextDouble() > 0.4)
                {
                    trend += r.NextDouble() * 10;
                }
                else
                {
                    trend -= r.NextDouble() * 10;
                }
            }

            Formatter = x => new DateTime((long)x).ToString("yyyy");

            Values = l.AsGearedValues().WithQuality(Quality.High);

            From = DateTime.Now.AddHours(10000).Ticks;
            To = DateTime.Now.AddHours(90000).Ticks;
        }

        public object Mapper { get; set; }
        public GearedValues<DateTimePoint> Values { get; set; }
        public double From
        {
            get { return _from; }
            set
            {
                _from = value;
                OnPropertyChanged("From");
            }
        }
        public double To
        {
            get { return _to; }
            set
            {
                _to = value;
                OnPropertyChanged("To");
            }
        }

        public Func<double, string> Formatter
        {
            get { return _formatter; }
            set
            {
                _formatter = value;
                OnPropertyChanged("Formatter");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}