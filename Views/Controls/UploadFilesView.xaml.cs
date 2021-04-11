using AnomalyDetection.Model;
using AnomalyDetection.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AnomalyDetection.Views.Controls
{
    /// <summary>
    /// Interaction logic for UploadFilesView.xaml
    /// </summary>
    public partial class UploadFilesView : UserControl
    {
        private UploadFilesViewModel uploadFilesViewModel;
        public UploadFilesView()
        {
            InitializeComponent();
            this.uploadFilesViewModel = new UploadFilesViewModel(FGModel.Instance);
            DataContext = this.uploadFilesViewModel;
        }
    }
}
