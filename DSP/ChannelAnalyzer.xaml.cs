using System;
using System.Collections.Generic;
using System.Linq;
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
using LiveCharts.Wpf;

namespace DSP
{
    /// <summary>
    /// Логика взаимодействия для ChannelAnalyzer.xaml
    /// </summary>
    public partial class ChannelAnalyzer : Window
    {
        public int a;
        public ChannelAnalyzer(int channel_num, int samples_num, string[] date_channel, string channels_names)
        {
            InitializeComponent();

            //Ахуительное решение 10 из 10
            if (samples_num > 400000)
            {
                a = samples_num / 300;
            }
            if (samples_num > 212000)
            {
                a = samples_num / 1000;
            }
            if (samples_num > 104000)
            {
                a = samples_num / 419;
            }
            if (samples_num > 41000)
            {
                a = samples_num / 466;
            }
            if (samples_num > 15000)
            {
                a = samples_num / 524;
            }
            if (samples_num > 1200)
            {
                a = samples_num / 404;
            }

            string[] date_channels = new string[channel_num + 1];
            for (int i = 0; i < samples_num; i += a)
            {
                string[] tmp = date_channel[i].Split(' ');
                for (int j = 0; j < channel_num; j++)
                {
                    date_channels[j] += tmp[j] + ',';
                }
            }

            string[] names = channels_names.Split(';');

            for (int i = 0; i < channel_num; i++)
            {
                string tm = date_channels[i];
                string[] results = tm.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                List<double> channel_tmp = new List<double>();
                channel_tmp.AddRange(results.Select(x => Convert.ToDouble(x)));
                TextBlock name = new TextBlock();
                name.Text = names[i];
                Grid.SetRow(name, i);
                TestGrid.Children.Add(name);
                CartesianChart ch = new CartesianChart();
                ch.Series = new SeriesCollection
                {
                    new LineSeries
                    {
                        LineSmoothness = 1,
                        StrokeThickness = 2,
                        DataLabels = false,
                        Fill = Brushes.Transparent,
                        PointGeometrySize = 0,
                        Title = names[i],
                        Values = new ChartValues<double>(channel_tmp),
                    }
                };
                ch.AxisX = new AxesCollection
                {
                    new Axis
                    {
                        ShowLabels = false,
                    }
                };
                ch.AxisY = new AxesCollection
                {
                    new Axis
                    {
                        ShowLabels = false,
                    }
                };
                //ch.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
                RowDefinition rowDef = new RowDefinition();
                TextBlock text = new TextBlock();
                ContextMenu menu = new ContextMenu();
                MenuItem item = new MenuItem();
                text.ContextMenu = menu;
                item.Header = "осцилограмма";
                text.ContextMenu.Items.Add(item);
                TestGrid.RowDefinitions.Add(rowDef);
                item.Click += Open_osc;
                Grid.SetRow(ch, i);

                TestGrid.Children.Add(ch);

                Grid.SetRow(text, i);

                TestGrid.Children.Add(text);
            }
        }
        void Open_osc(object sender, RoutedEventArgs e)
        {
            {
                Window window = new Window
                {
                    Title = "My User Control Dialog",
                    Content = new OSC()
                };

                window.ShowDialog();
            }
        }
    }
}
