using System.Collections.Generic;
using System.IO;
using ExitGames.Diagnostics.Counter;
using ExitGames.Diagnostics.Monitoring;
using ExitGames.Logging;
using ExitGames.Logging.Log4Net;

using log4net.Config;
using Photon.SocketServer;
using Photon.SocketServer.Diagnostics;
using PhotonHostRuntimeInterfaces;



namespace PhotoServer
{
    public class ChatServer:ApplicationBase
    {
        private static readonly ILogger log = LogManager.GetCurrentClassLogger();
        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            var clientPeer = new ChatPeer(initRequest.Protocol,initRequest.PhotonPeer);
            return clientPeer;
        }

        protected override void Setup()
        {
              log4net.GlobalContext.Properties["Photon:ApplicationLogPath"] = Path.Combine(this.ApplicationRootPath, "log");
              log4net.GlobalContext.Properties["LogFileName"] = "TD" + this.ApplicationName;
              var file = new FileInfo(Path.Combine(this.BinaryPath, "log4net.config"));
              if (file.Exists)
              {
                  LogManager.SetLoggerFactory(Log4NetLoggerFactory.Instance);
                  XmlConfigurator.ConfigureAndWatch(file);
              }
              log.InfoFormat("Create application Instance: Type={0}", Instance.GetType());
              log.Debug("!!!!!!!!!!!!!!!!! Application setup complete");

              Initialize();
        }

        private void Initialize()
        {
             CounterPublisher.DefaultInstance.AddStaticCounterClass(typeof(Counter),"PhotoServer");
             Protocol.AllowRawCustomValues = true;
        }

        protected override void TearDown()
        {
             log.Debug("!!!!!!!!!!!!!!!!!!! Application tear down");
        }
    }

    public static class Counter
    {
        [PublishCounter("Photo")]
        public static readonly NumericCounter Games = new NumericCounter("Photo");
    }

    public class ChatPeer : PeerBase
    {
        private static readonly ILogger log = LogManager.GetCurrentClassLogger();
        public ChatPeer(InitRequest initRequest) : base(initRequest)
        {
        }

        public ChatPeer(IRpcProtocol protocol, IPhotonPeer unmanagedPeer) : base(protocol, unmanagedPeer)
        {
        }

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            switch (operationRequest.OperationCode)
            {
                case 1:
                    if (operationRequest.Parameters.TryGetValue(10, out var obj))
                    {
                        log.Debug("!!!!!!!!!! rcv from client ： ");

                        SendOperationResponse(
                            new OperationResponse(2,
                                new Dictionary<byte, object>() {{20, operationRequest.OperationCode + "|:)" + obj}}),
                            sendParameters);
                    }

                    break;
            }
        }

        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            log.Debug("!!!!!!!!!!!!! disconnect ");
        }
    }
}