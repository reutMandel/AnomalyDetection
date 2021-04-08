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
using AnomalyDetection.Model;
using AnomalyDetection.ViewModel;

namespace AnomalyDetection.Views.Controls
{
    /// <summary>
    /// Interaction logic for GraphsView.xaml
    /// </summary>
    public partial class GraphsView : UserControl
    {
        private GraphsViewModel graphsViewModel;
        public GraphsView()
        {
            InitializeComponent();
            graphsViewModel = new GraphsViewModel(FGModel.Instance);
            DataContext = graphsViewModel;
        }
    }
}
