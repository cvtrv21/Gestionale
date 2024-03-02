using FrameQueues.Interfaces;
using FrameQueues;

namespace BiciclottiWpf.Data
{
    public abstract class BaseRepository
    {
        #region Fields

        static IClient<IBiciclottiClientContracts>? _client;

        #endregion

        #region Properties

        protected static IClient<IBiciclottiClientContracts> Client => _client ??= new QueueManager<IBiciclottiClientContracts>();

        #endregion

        #region Public methods

        public static void Close()
        {
            _client?.Dispose();
            _client = null;
        }

        #endregion
    }
}
