using AnomalyDetection.Model;
using AnomalyDetection.ViewModel;
using System.Windows.Controls;

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
