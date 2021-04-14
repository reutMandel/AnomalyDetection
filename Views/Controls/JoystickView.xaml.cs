using System;
using System.Windows.Controls;
using AnomalyDetection.Model;
using AnomalyDetection.ViewModel;

namespace AnomalyDetection.Views.Controls
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
