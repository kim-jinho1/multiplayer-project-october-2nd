using System.Net.Sockets;

namespace JamesFrowen.SimpleWeb
{
    [System.Serializable]
    public struct TcpConfig
    {
        public readonly bool noDelay;
        public readonly int sendTimeout;
        public readonly int receiveTimeout;

        public TcpConfig(bool noDelay, int sendTimeout, int receiveTimeout)
        {
            this.noDelay = noDelay;
            this.sendTimeout = sendTimeout;
            this.receiveTimeout = receiveTimeout;
        }

        public readonly void ApplyTo(TcpClient client)
        {
            client.SendTimeout = sendTimeout;
            client.ReceiveTimeout = receiveTimeout;
            client.NoDelay = noDelay;
        }
    }
}