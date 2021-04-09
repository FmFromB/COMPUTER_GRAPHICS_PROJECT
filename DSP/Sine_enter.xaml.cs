using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using Microsoft.Win32;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;
using System.Collections;
using System.Globalization;
using System.Text.RegularExpressions;
using LiveCharts.Wpf;

namespace DSP
{
    /// <summary>
    /// Логика взаимодействия для Sine_enter.xaml
    /// </summary>
    public partial class Sine_enter : Window
    {
        public Sine_enter()
        {
            InitializeComponent();
        }
        public double number;
        public double phi;
        public double w;
        public double cycle_value = 0;
        oscillogram Oscillogram = new oscillogram();
        private void sine_modultaion(object sender, RoutedEventArgs e)
        {
            string amp = amplitude.Text;
            string phs = phase.Text;
            string freq = round_freq.Text;
            number = Convert.ToDouble(amp);
            phi = Convert.ToDouble(phs);
            w = Convert.ToDouble(freq);
            this.Close();
            Oscillogram = new oscillogram { Height = 300, Width = 250 };
            Oscillogram.Show();


            List<double> modulation_values = new List<double>();
            for (int i = 0; i <= number; i++)
            {
                cycle_value = number * Math.Sin(i * w + phi);
                modulation_values.Add(cycle_value);
            }

            int k = 1;
            TextBlock name = new TextBlock();
            Grid.SetRow(name, k);
            CartesianChart ch = new CartesianChart();

            ch.Series = new SeriesCollection
            {
                new LineSeries
                {
                    LineSmoothness = 2,
                    StrokeThickness = 2,
                    DataLabels = false,
                    Fill = Brushes.Transparent,
                    PointGeometrySize = 0,
                    Values = new ChartValues<double>(modulation_values),
                }
            };


            RowDefinition rowDef = new RowDefinition();
            TextBlock text = new TextBlock();

            Oscillogram.oscil.RowDefinitions.Add(rowDef);
            Grid.SetRow(ch, k);

            Oscillogram.oscil.Children.Add(ch);

            Grid.SetRow(text, k);

            Oscillogram.oscil.Children.Add(text);
        }
    }
}

