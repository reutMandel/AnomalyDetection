namespace AnomalyDetection.Model
{
    public interface IClient
    {
        void Connect();
        void Send(byte[] buffer);
        void Disconnect();
    }
}