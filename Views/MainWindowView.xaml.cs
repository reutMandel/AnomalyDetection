﻿
using System.Windows;
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

       private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(IsLoaded)
            {
               // mainWindowViewModel.SliderHandler(System.Int32.Parse(SlideController.Value.ToString()));
                mainWindowViewModel.SliderHandler();
            }
        }
    }

}
