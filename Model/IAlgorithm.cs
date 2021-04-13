namespace AnomalyDetection.Model
{
    public interface IAlgorithm
    {
        AlgorithmProperties GetAlgorithmProperties(Point[] points);
    }
}
