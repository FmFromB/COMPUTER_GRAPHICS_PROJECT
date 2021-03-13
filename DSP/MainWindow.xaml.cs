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
                string data = File.ReadAllText(openFileDialog.FileName);
                ChanelsPage сhanelsPage = new ChanelsPage();
                сhanelsPage.Show();
                сhanelsPage.txtEditor.Text = data;
            }
        }
    }
}
