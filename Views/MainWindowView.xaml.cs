using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;
using AnomalyDetection.ViewModel;
using AnomalyDetection.Model;

namespace AnomalyDetection.Views
{
    /// <summary>
    /// Interaction logic for MainWindowView.xaml
    /// </summary>
    public partial class MainWindowView : Window
    {
        private MainWindowViewModel mainWindowViewModel;
        private bool doneEnable;
        private bool isClickFg;
        private bool isClickXml;
        private bool isClickCsv;

        public MainWindowView()
        {
            InitializeComponent();
            this.mainWindowViewModel = new MainWindowViewModel(new FGModel());
            DataContext = mainWindowViewModel;
            doneEnable = false;
            isClickFg = false;
            isClickXml = false;
            isClickCsv = false;
        }

        private void CsvButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                // Path.GetFileName(openFileDialog.FileName());
                mainWindowViewModel.CsvFile = openFileDialog.FileName;
                isClickCsv = true;
                doneEnable = isClickCsv && isClickFg && isClickXml;
            }
        }

        private void XmlButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                // Path.GetFileName(openFileDialog.FileName());
                mainWindowViewModel.XmlFile = openFileDialog.FileName;
                isClickXml = true;
                doneEnable = isClickCsv && isClickFg && isClickXml;
            }
        }

        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindowViewModel.MVStartSimulator();
        }

        private void FGButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                // Path.GetFileName(openFileDialog.FileName());
                mainWindowViewModel.FgPath = openFileDialog.FileName;
                isClickFg = true;
                doneEnable = isClickCsv && isClickFg && isClickXml;
            }
        }
    }
}
