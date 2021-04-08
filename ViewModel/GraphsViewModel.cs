using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using AnomalyDetection.Model;
using System.Linq;
using OxyPlot;

namespace AnomalyDetection.ViewModel
{
    public class GraphsViewModel : ViewModel
    {
        private IFGModel fgModel;
        private ObservableCollection<string> fieldsName;

        public GraphsViewModel(IFGModel fgModel)
        {
            this.fgModel = fgModel;
            FieldsName = new ObservableCollection<string>();
            this.fgModel.LoadXmlCompleted += delegate () { InsertFieldsName(); };
        }
        public IList<DataPoint> Points { get; private set; }

        public string Title { get; private set; }

        public void InsertFieldsName()
        {
            this.fgModel.CsvNames.Keys.ToList().ForEach(name => FieldsName.Add(name));

        }

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
        public Collection<CollectionDataValue> Data { get; set; }

        public class CollectionDataValue
        {
            public double xData { get; set; }
            public double yData { get; set; }
        }
    }
}
