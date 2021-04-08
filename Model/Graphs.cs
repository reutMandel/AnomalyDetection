using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnomalyDetection.Model
{
    public class Graphs : Notify
    {

        List<double> Values { get; }

        public void AddValue(double value)
        {
            Values.Add(value);
        }
    }
}
