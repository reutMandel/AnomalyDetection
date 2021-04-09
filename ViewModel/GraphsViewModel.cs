using System.Collections.Generic;
using System.Collections.ObjectModel;
using AnomalyDetection.Model;
using System.Linq;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace AnomalyDetection.ViewModel
{
    public class GraphsViewModel : ViewModel
    {
        private IFGModel fgModel;
        private ObservableCollection<string> fieldsName;
        private PlotModel selectedItemGraph;
        private string selectedField;
        private LineSeries lineSeries;
        private List<double> values;
        private bool isSelected;

        public ObservableCollection<string> FieldsName
        {
            get
            {
                return this.fieldsName;
            }
            set
            {
                this.fieldsName = value;
                NotifyPropertyChanged("FieldsName");
            }
        }

        public PlotModel SelectedItemGraph
        {
            get { return this.selectedItemGraph; }
            set
            {
                this.selectedItemGraph = value;
                NotifyPropertyChanged("SelectedItemGraph");
            }
        }

        public string SelectedField
        {
            get { return this.selectedField; }
            set
            {
                this.selectedField = value;
                NotifyPropertyChanged("SelectedField");
            }
        }

        public bool IsSelected { get; set; }

        public GraphsViewModel(IFGModel fgModel)
        {
            this.fgModel = fgModel;
            FieldsName = new ObservableCollection<string>();
            this.fgModel.LoadXmlCompleted += delegate () { InsertFieldsName(); };
            SelectedItemGraph = new PlotModel();
            this.lineSeries = new LineSeries();
            this.isSelected = false;
            fgModel.CurrentPosition.PositionChanged += delegate () { UpdateGraph(); };
            fgModel.SpeedProperties.SpeedChanged += delegate () { ItemSelected(); };
            SetUpSelectedFieldModel();
        }

        public void InsertFieldsName()
        {
            this.fgModel.CsvNames.Keys.ToList().ForEach(name => FieldsName.Add(name));

        }

        private void SetUpSelectedFieldModel()
        {
            var timeAxis = new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Time In Seconds",
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot
            };
            SelectedItemGraph.Axes.Add(timeAxis);
            var valueAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "Value",
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot
            };
            SelectedItemGraph.Axes.Add(valueAxis);
            SelectedItemGraph.Series.Add(lineSeries);
        }
        

        public void ItemSelected()
        {
            this.isSelected = true;
            lineSeries.Points.Clear();
            values = fgModel.GetValuesByField(selectedField).Select(i=> double.Parse(i)).ToList();
            for (int i = 0; i < fgModel.CurrentPosition.Position; i++)
            {
                lineSeries.Points.Add(new DataPoint(i * fgModel.SpeedProperties.Sleep / 1000, values[i]));
            }
            SelectedItemGraph.InvalidatePlot(true);
        }

        private void UpdateGraph()
        {
            if (!isSelected)
                return;
            int size = lineSeries.Points.Count;
            if (size < fgModel.CurrentPosition.Position)
            {
                for(int i = size; i< fgModel.CurrentPosition.Position; i++)
                {
                    lineSeries.Points.Add(new DataPoint(i * fgModel.SpeedProperties.Sleep / 1000, values[i]));
                }
            }
            else if(size > fgModel.CurrentPosition.Position)
            {
                    ItemSelected();
            }
            SelectedItemGraph.InvalidatePlot(true);
        }
    }
}
