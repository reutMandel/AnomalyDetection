using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    
    }
}
