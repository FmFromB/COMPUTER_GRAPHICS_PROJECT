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
            DataContext = this;
        }
        public ChartValues<double> Values { get; set; }

        private void UpdateOnclick(object sender, RoutedEventArgs e)
        {
            Chart.Update(true);
        }
    }
}
