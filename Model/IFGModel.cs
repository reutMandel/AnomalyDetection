using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnomalyDetection.Model
{
    public interface IFGModel : INotifyPropertyChanged
    {
        string XmlPath { get; set;}

        string CsvPath { get; set;}

        string FgPath { get; set; }

        void StartStimulate();

        void ReadCsvFile();
    }
}
