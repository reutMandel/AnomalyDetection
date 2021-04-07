using System.Windows;
using System.Windows.Controls;
using AnomalyDetection.Model;
using AnomalyDetection.ViewModel;

namespace AnomalyDetection.Views.Controls
{
    /// <summary>
    /// Interaction logic for ToolBar.xaml
    /// </summary>
    public partial class ToolBar : UserControl
    {

        private ToolBarViewModel toolBarViewModel;

        public ToolBar()
        {
            InitializeComponent();
            this.toolBarViewModel = new ToolBarViewModel(FGModel.Instance);
            DataContext = toolBarViewModel;
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //if (IsLoaded)
            //{
            //    toolBarViewModel.SliderHandler();
            //}
        }
    }
}
