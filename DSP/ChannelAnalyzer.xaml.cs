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
        public ChannelAnalyzer(int channel_num, string[] date_channels, string[] names)
        {
            InitializeComponent();

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
                CartesianChart ch = new CartesianChart {Name = names[i]};
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
                window.MaxHeight = 400;
                window.MaxWidth = 800;
                window.Top = 120;
                window.ShowDialog();

            }
        }
    }
}
