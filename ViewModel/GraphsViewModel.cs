using AnomalyDetection.Model;
using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AnomalyDetection.ViewModel
{
    public class GraphsViewModel : ViewModel
    {
        private IFGModel fgModel;
        private ObservableCollection<string> fieldsName;
        private ObservableCollection<string> anomaliesStrList;
        private PlotModel selectedItemGraph;
        private PlotModel correlatedGraph;
        private PlotModel algorithmGraph;
        private string selectedField;
        private LineSeries selectedItemLineSeries;
        private LineSeries correlatedLineSeries;
        private Annotation algorithmAnnotation;
        private ScatterSeries scatterSeries;
        private List<double> values;
        private List<double> correlatedValues;
        private List<Point> algorithmPoints;
        private List<Point> anomalyPoints;
        private ScatterSeries anomalyScatter;
        private string correlatedField;
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

        public ObservableCollection<string> AnomaliesStrList
        {
            get
            {
                return this.anomaliesStrList;
            }
            set
            {
                this.anomaliesStrList = value;
                NotifyPropertyChanged("AnomaliesStrList");
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

        public PlotModel CorrelatedGraph
        {
            get => this.correlatedGraph;
            set
            {
                this.correlatedGraph = value;
                NotifyPropertyChanged("CorrelatedGraph");
            }
        }

        public PlotModel AlgorithmGraph
        {
            get => this.algorithmGraph;
            set
            {
                this.algorithmGraph = value;
                NotifyPropertyChanged("AlgorithmGraph");
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

        public string CorrelatedField
        {
            get { return this.correlatedField; }
            set
            {
                this.correlatedField = value;
                NotifyPropertyChanged("CorrelatedField");
            }
        }

        public bool IsSelected { get; set; }

        public GraphsViewModel(IFGModel fgModel)
        {
            this.isSelected = false;
            this.fgModel = fgModel;
            this.fgModel.CurrentPosition.PositionChanged += delegate () { UpdateGraph(); };
            this.fgModel.SpeedProperties.SpeedChanged += delegate () { SpeedChangeHandler(); };
            this.fgModel.LoadXmlCompleted += delegate () { InsertFieldsName(); };
            this.fgModel.GraphsLogic.LoadDllCompleted += delegate () { InsertAnomaliesStr(); };
            this.FieldsName = new ObservableCollection<string>();
            this.AnomaliesStrList = new ObservableCollection<string>();
            this.SelectedItemGraph = new PlotModel();
            this.CorrelatedGraph = new PlotModel();
            this.AlgorithmGraph = new PlotModel();
            this.selectedItemLineSeries = new LineSeries();
            this.correlatedLineSeries = new LineSeries();
            this.scatterSeries = new ScatterSeries();
            this.anomalyScatter = new ScatterSeries();
            SetUpSelectedFieldModel();
            SetUpCorrelatedFieldModel();
            SetUpAlgorithmModel();
        }

        public void InsertFieldsName()
        {
            this.fgModel.CsvNames.Keys.ToList().ForEach(name => FieldsName.Add(name));
        }

        public void InsertAnomaliesStr()
        {
            this.fgModel.GraphsLogic.GetAnomaliesAsString().ForEach(name => AnomaliesStrList.Add(name));
        }

        private void SetUpSelectedFieldModel()
        {
            SelectedItemGraph.Title = "Selected Item";
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
            SelectedItemGraph.Series.Add(selectedItemLineSeries);
        }

        private void SetUpCorrelatedFieldModel()
        {
            CorrelatedGraph.Title = "Correlated Item";
            var timeAxis = new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Time In Seconds",
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot
            };
            CorrelatedGraph.Axes.Add(timeAxis);
            var valueAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "Value",
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
            };
            CorrelatedGraph.Axes.Add(valueAxis);
            CorrelatedGraph.Series.Add(correlatedLineSeries);
        }

        private void SetUpAlgorithmModel()
        {
            var fieldAxis = new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Selected Item"
            };
            AlgorithmGraph.Axes.Add(fieldAxis);
            var valueAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "Correlated Item"
            };
            AlgorithmGraph.Axes.Add(valueAxis);
            this.scatterSeries.MarkerType = MarkerType.Circle;
            this.scatterSeries.MarkerSize = 1.5;
            this.anomalyScatter.MarkerType = MarkerType.Circle;
            this.anomalyScatter.MarkerSize = 1.5;
            this.anomalyScatter.MarkerFill = OxyColors.Red;
            AlgorithmGraph.Series.Add(this.scatterSeries);
            AlgorithmGraph.Series.Add(this.anomalyScatter);
        }

        public void ItemSelected()
        {
            this.CorrelatedField = this.fgModel.GetCorrelatedField(selectedField);
            this.correlatedValues = this.fgModel.GetValuesByField(this.CorrelatedField);
            CorrelatedGraph.Axes.FirstOrDefault(x => x.Position == AxisPosition.Left).Title = this.correlatedField;
            SelectedItemGraph.Axes.FirstOrDefault(x => x.Position == AxisPosition.Left).Title = this.selectedField;
            this.values = fgModel.GetValuesByField(selectedField);
            SetAllLineSeries();
            SetAlgorithmGraph();
            this.isSelected = true;
        }

        private void SetScatterSeries()
        {
            this.scatterSeries.Points.Clear();
            int currentPosition = this.fgModel.CurrentPosition.Position;
            double time = this.fgModel.CurrentPosition.Position * this.fgModel.SpeedProperties.Sleep / 1000;
            if (time < 30)
            {
                int i;
                for (i = 0; i < currentPosition; i++)
                {
                    this.scatterSeries.Points.Add(new ScatterPoint(this.algorithmPoints[i].X, this.algorithmPoints[i].Y));
                }
            }
            if (time >= 30)
            {
                int startTime = (int)(time - 30) * 1000 / this.fgModel.SpeedProperties.Sleep;
                int i;
                for (i = startTime; i < currentPosition; i++)
                {
                    this.scatterSeries.Points.Add(new ScatterPoint(this.algorithmPoints[i].X, this.algorithmPoints[i].Y));
                }
            }
        }

        private void SetAlgorithmGraph()
        {
            AlgorithmProperties properties = this.fgModel.GetAlgorithmProperties(this.SelectedField, this.CorrelatedField);
            this.algorithmAnnotation = properties.AnnotationShape;
            this.algorithmPoints = properties.Points;
            this.anomalyPoints = this.fgModel.GraphsLogic.GetAnnomaliesPoints(this.SelectedField, this.CorrelatedField, this.algorithmPoints);
            AlgorithmGraph.Annotations.Clear();
            AlgorithmGraph.Annotations.Add(this.algorithmAnnotation);
            SetScatterSeries();
            AlgorithmGraph.InvalidatePlot(true);

            double minX = values.Min();
            double minY = correlatedValues.Min();
            double maxX = values.Max();
            double maxY = correlatedValues.Max();
            var xAxis = this.AlgorithmGraph.Axes.FirstOrDefault(x => x.Position == AxisPosition.Bottom);
            xAxis.Minimum = minX;
            xAxis.Maximum = maxX;
            var yAxis = this.AlgorithmGraph.Axes.FirstOrDefault(x => x.Position == AxisPosition.Left);
            yAxis.Minimum = minY;
            yAxis.Maximum = maxY;
            SetScatterSeries();
        }

        private void SetLineSeries(LineSeries lineSeries, List<double> currentValues)
        {
            lineSeries.Points.Clear();
            for (int i = 0; i < fgModel.CurrentPosition.Position; i++)
            {
                lineSeries.Points.Add(new DataPoint(i * fgModel.SpeedProperties.Sleep / 1000, currentValues[i]));
            }
        }

        private void SpeedChangeHandler()
        {
            SetAllLineSeries();
            SetScatterSeries();
        }

        private void SetAllLineSeries()
        {
            SetLineSeries(this.selectedItemLineSeries, this.values);
            SetLineSeries(this.correlatedLineSeries, this.correlatedValues);
            SelectedItemGraph.InvalidatePlot(true);
            CorrelatedGraph.InvalidatePlot(true);
        }

        private void UpdateGraph()
        {
            if (!isSelected)
                return;
            int size = selectedItemLineSeries.Points.Count;
            if (size < fgModel.CurrentPosition.Position)
            {
                for (int i = size; i < fgModel.CurrentPosition.Position; i++)
                {
                    selectedItemLineSeries.Points.Add(new DataPoint(i * fgModel.SpeedProperties.Sleep / 1000, values[i]));
                    correlatedLineSeries.Points.Add(new DataPoint(i * fgModel.SpeedProperties.Sleep / 1000, correlatedValues[i]));
                    scatterSeries.Points.Add(new ScatterPoint(algorithmPoints[i].X, algorithmPoints[i].Y));
                    scatterSeries.Points.RemoveAt(0);
                }
            }
            else if (size > fgModel.CurrentPosition.Position)
            {
                SetAllLineSeries();
                SetScatterSeries();
            }
            SelectedItemGraph.InvalidatePlot(true);
            CorrelatedGraph.InvalidatePlot(true);
            AlgorithmGraph.InvalidatePlot(true);
        }
    }
}
