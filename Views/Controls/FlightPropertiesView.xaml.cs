using AnomalyDetection.Model;
using AnomalyDetection.ViewModel;
using System.Windows.Controls;


namespace AnomalyDetection.Views.Controls
{
    /// <summary>
    /// Interaction logic for FlightPropertiesView.xaml
    /// </summary>
    public partial class FlightPropertiesView : UserControl
    {
        private FlightPropertiesViewModel flightPropertiesViewModel;
        public FlightPropertiesView()
        {
            InitializeComponent();
            flightPropertiesViewModel = new FlightPropertiesViewModel(FGModel.Instance);
            DataContext = flightPropertiesViewModel;
        }
    }
}
