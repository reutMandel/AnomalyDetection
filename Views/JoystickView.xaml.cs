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

namespace AnomalyDetection.Views
{
    /// <summary>
    /// Interaction logic for JoystickView.xaml
    /// </summary>
    public partial class JoystickView : UserControl
    {
        private JoystickViewModel joystickViewModel; 
        public JoystickView()
        {
            InitializeComponent();
            joystickViewModel = new JoystickViewModel(FGModel.Instance);
            DataContext = joystickViewModel;
        }

        private void centerKnob_Completed(object sender, EventArgs e)
        {

        }
    }
}
