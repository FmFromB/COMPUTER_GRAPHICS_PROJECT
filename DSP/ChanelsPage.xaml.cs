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
using OxyPlot;
using System.Globalization;

namespace DSP
{
    public partial class ChanelsPage : Window
    {
        public static string path = "D://testData.txt";
        public DataPoint[] TestPoints { get; }
            = DataLoader.loadData(path)
               .Select(x => new DataPoint(x.Rx, x.Rz))
               .ToArray();
        public ChanelsPage()
        {
            InitializeComponent();
            this.DataContext = this;
        }
    }

    public class DataLoader
    {
        public static IEnumerable<Data> loadData(string path)
        {
            return
                File.ReadLines(path)
                .Select(x => x.Split(new[] { ';' }))
                .Select(x => new Data
                {
                    Rx = double.Parse(x[1], CultureInfo.InvariantCulture),
                    Rz = double.Parse(x[0], CultureInfo.InvariantCulture)
                });
        }
    }

    public class Data
    {
        public double Rx { get; set; }
        public double Rz { get; set; }
    }
}
