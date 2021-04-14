﻿using OxyPlot.Annotations;
using System.Collections.Generic;

namespace AnomalyDetection.Model
{
    public class AlgorithmProperties
    {
        public Annotation AnnotationShape { get; set; }

        public List<Point> Points { get; set; }
    }
}