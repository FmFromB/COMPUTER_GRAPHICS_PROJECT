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
using LiveCharts;
using LiveCharts.Wpf;

namespace DSP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        public static int channel_num;
        public int samples_num;
        public long sampling_rate;
        public string start_date;
        public string start_time;
        public string channels_names;
        public string[] date_channel;
        public string[] date_channels;
        public int a;
        string[] data_all;
        string[] names;

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

                System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                data_all = File.ReadAllLines(openFileDialog.FileName, Encoding.UTF8);

                // Важные переменные
                channel_num = Convert.ToInt32(data_all[1]);
                samples_num = Convert.ToInt32(data_all[3]);
                double k = Double.Parse(data_all[5]);
                sampling_rate = Convert.ToInt32(k);
                start_date = data_all[7];
                start_time = data_all[9];
                channels_names = data_all[11];
                string[] date_channel = new string[samples_num];
                Array.Copy(data_all, 12, date_channel, 0, samples_num);

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

                date_channels = new string[channel_num + 1];
                for (int i = 0; i < samples_num; i += a)
                {
                    string[] tmp = date_channel[i].Split(' ');
                    for (int j = 0; j < channel_num; j++)
                    {
                        date_channels[j] += tmp[j] + ',';
                    }
                }

                names = channels_names.Split(';');

                ChannelAnalyzer analyzer = new ChannelAnalyzer(channel_num, date_channels, names);

                // добавление подменю каналов
                oscillograms.Items.Clear();
  

                // фиксирование размера и положения окна
                // Owner - определяет окно как дочернее
                analyzer.Owner = this;
                analyzer.MaxHeight = 85 * channel_num;
                analyzer.MinHeight = analyzer.MaxHeight;
                analyzer.Top = this.Top + 60;
                analyzer.Left = this.Left + 4;

                analyzer.Show();

                // фиксирование дочернего окна при перемещении главного
                LocationChanged += new EventHandler((object sender, EventArgs e) => {
                    foreach (Window win in this.OwnedWindows)
                    {
                        win.Top = this.Top + 60;
                        win.Left = this.Left + 4;
                    } 
                });
            }
        }

        //Далее идёт дохрена (дохуя) копипасты
        //Почему бы и нет

        oscillogram Oscillogram = new oscillogram { Height = 100 };
        Enter enter = new Enter();
        square_enter S_enter = new square_enter();
        Exponent_enter exp_enter = new Exponent_enter();
        Sine_enter sine_enter = new Sine_enter();
        Impulse impulse_enter = new Impulse();
        Jump jump_enter = new Jump();

        private void show_saw_enter(object sender, RoutedEventArgs e)
        {
            enter = new Enter();
            enter.Show();
        }
        private void show_square_enter(object sender, RoutedEventArgs e)
        {
            S_enter = new square_enter();
            S_enter.Show();
        }
        private void show_exponent_enter(object sender, RoutedEventArgs e)
        {
            exp_enter = new Exponent_enter();
            exp_enter.Show();
        }
        private void show_sine_enter(object sender, RoutedEventArgs e)
        {
            sine_enter = new Sine_enter();
            sine_enter.Show();
        }
        private void show_impulse_enter(object sender, RoutedEventArgs e)
        {
            impulse_enter = new Impulse();
            impulse_enter.Show();
        }
        private void show_jump_enter(object sender, RoutedEventArgs e)
        {
            jump_enter = new Jump();
            jump_enter.Show();
        }

        // добавление осциллограмм через основное меню

        private void signal_information(object sender, RoutedEventArgs e)
        {
            Signal_information signal_Information = new Signal_information();
            signal_Information.Owner = this;
            signal_Information.Show();

            signal_Information.chanels_num.Text = channel_num.ToString();
            signal_Information.samples_num.Text = samples_num.ToString();
            signal_Information.sampling_rate.Text = sampling_rate.ToString();
            signal_Information.start.Text = start_date + " " + start_time;
            
            // длительность в секундах
            var ts = TimeSpan.FromSeconds(samples_num);
            
            int hours = ts.Hours;
            int minutes = ts.Minutes % 60;
            int seconds = ts.Seconds % 60;
            signal_Information.duration.Text = ts.Days.ToString() + "суток " +
                                               hours.ToString() + "часов " +
                                               minutes.ToString() + "минут " +
                                               seconds.ToString() + "секунд";

            
            string[] date_start = start_date.Split('-');
            string[] time_end = start_time.Split(':');
            DateTime date_s = new DateTime(Convert.ToInt32(date_start[2]),
                                          Convert.ToInt32(date_start[1]),
                                          Convert.ToInt32(date_start[0]),
                                          Convert.ToInt32(time_end[0]),
                                          Convert.ToInt32(time_end[1]),
                                          (int)Double.Parse(time_end[2]));

            DateTime date_e = date_s + ts;
            signal_Information.end.Text = date_e.ToString();
        }
    }
}
