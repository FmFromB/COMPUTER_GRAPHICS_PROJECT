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
using System.Windows.Shapes;
using LiveCharts;

namespace DSP
{
    /// <summary>
    /// Логика взаимодействия для ChannelAnalyzer.xaml
    /// </summary>
    public partial class ChannelAnalyzer : Window
    {
        public ChannelAnalyzer()
        {
            InitializeComponent();
            Values = new ChartValues<double> { };
            Values2 = new ChartValues<double> { };
            Values3 = new ChartValues<double> { };
            Values4 = new ChartValues<double> { };
            Values5 = new ChartValues<double> { };
            Values6 = new ChartValues<double> { };
            DataContext = this;
        }
        public ChartValues<double> Values { get; set; }
        public ChartValues<double> Values2 { get; set; }
        public ChartValues<double> Values3 { get; set; }
        public ChartValues<double> Values4 { get; set; }
        public ChartValues<double> Values5 { get; set; }
        public ChartValues<double> Values6 { get; set; }


        private void UpdateOnclick(object sender, RoutedEventArgs e)
        {
            Chart.Update(true);
            Chart2.Update(true);
            Chart3.Update(true);
            Chart4.Update(true);
            Chart5.Update(true);
            Chart6.Update(true);
        }
    }
}
