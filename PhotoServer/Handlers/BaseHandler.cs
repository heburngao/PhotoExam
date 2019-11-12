using Common;
using ExitGames.Logging;
using Photon.SocketServer;

namespace PhotoServer.Handlers
{
    public abstract class BaseHandler
    {
        private static readonly ILogger log = LogManager.GetCurrentClassLogger();

        public BaseHandler()
        {
            log.Debug("basehander : "+ this.GetType().Name + "is register.");
        }

        public abstract void OnHandlerMessage(OperationRequest request, OperationResponse response,
            ClientPeer clientPeer, SendParameters sendParameters);

        public abstract OpCode OpCode { get; }
    }
}