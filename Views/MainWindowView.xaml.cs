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

        public MainWindowView()
        {
            InitializeComponent();
            this.mainWindowViewModel = new MainWindowViewModel(new FGModel());
            DataContext = mainWindowViewModel;
        }
    }
}
