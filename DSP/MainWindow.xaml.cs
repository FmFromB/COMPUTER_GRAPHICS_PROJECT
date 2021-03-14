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
                string[] data = File.ReadAllText(openFileDialog.FileName).Split('\n');
                //ChanelsPage сhanelsPage = new ChanelsPage();
                //сhanelsPage.Show();
                
                chanel_num = data[1];
                samples_num = data[3];
                sampling_rate = data[5];
                start_date = data[7];
                start_time = data[9];
                channels_names = data[11];
                
                text.Text = chanel_num + samples_num + sampling_rate + start_date + start_time + channels_names;
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
