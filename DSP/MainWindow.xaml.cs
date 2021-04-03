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

namespace DSP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string chanel_num;
        public string samples_num;
        public string sampling_rate;
        public string start_date;
        public string start_time;
        public string channels_names;
        public string aaaaa;


        public MainWindow()
        {
            InitializeComponent();
        }

        private void loading_file(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Текстовый файл (*.txt)|*.txt|Звуковой файл (*.wav)|*.wav|Dat-файл (*.dat)|*.dat";
            if (openFileDialog.ShowDialog() == true)
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                string[] data = File.ReadAllLines(openFileDialog.FileName);

                ChannelAnalyzer analyzer = new ChannelAnalyzer();
                analyzer.Show();
                
                for (int i = 12; i < 1215; i++)
                {
                    string inputString = data[i];
                    string[] results = inputString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    List<double> d = new List<double>();
                    d.AddRange(results.Select(x => Convert.ToDouble(x)));
                    double[] values = d.ToArray();
                    analyzer.Values.Add(values[0]);

                }
                for (int i = 12; i < 1215; i++)
                {
                    string inputString = data[i];
                    string[] results = inputString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    List<double> d = new List<double>();
                    d.AddRange(results.Select(x => Convert.ToDouble(x)));
                    double[] values = d.ToArray();
                    analyzer.Values2.Add(values[1]);

                }
                for (int i = 12; i < 1215; i++)
                {
                    string inputString = data[i];
                    string[] results = inputString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    List<double> d = new List<double>();
                    d.AddRange(results.Select(x => Convert.ToDouble(x)));
                    double[] values = d.ToArray();
                    analyzer.Values3.Add(values[2]);
                }
                for (int i = 12; i < 1215; i++)
                {
                    string inputString = data[i];
                    string[] results = inputString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    List<double> d = new List<double>();
                    d.AddRange(results.Select(x => Convert.ToDouble(x)));
                    double[] values = d.ToArray();
                    analyzer.Values4.Add(values[3]);
                }
                for (int i = 12; i < 1215; i++)
                {
                    string inputString = data[i];
                    string[] results = inputString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    List<double> d = new List<double>();
                    d.AddRange(results.Select(x => Convert.ToDouble(x)));
                    double[] values = d.ToArray();
                    analyzer.Values5.Add(values[4]);

                }
                for (int i = 12; i < 1215; i++)
                {
                    string inputString = data[i];
                    string[] results = inputString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    List<double> d = new List<double>();
                    d.AddRange(results.Select(x => Convert.ToDouble(x)));
                    double[] values = d.ToArray();
                    analyzer.Values6.Add(values[5]);

                }


                samples_num = data[3];
                sampling_rate = data[5];
                start_date = data[7];
                start_time = data[9];
                for (int i = 12; i < 1215; i++)
                {
                   

                    //double[] result = data3[i].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries).Select(x => double.Parse(x)).ToArray();
                }
                
            }

        }
        private void signal_information(object sender, RoutedEventArgs e)
        {
            Signal_information signal_Information = new Signal_information();

            signal_Information.Show();

            signal_Information.chanels_num.Text = chanel_num;
            signal_Information.samples_num.Text = samples_num;
            signal_Information.sampling_rate.Text = sampling_rate;
            signal_Information.start.Text = start_date + start_time;
        }
    }
}
