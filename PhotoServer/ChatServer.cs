using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using ExitGames.Diagnostics.Counter;
using ExitGames.Diagnostics.Monitoring;
using ExitGames.Logging;
using ExitGames.Logging.Log4Net;

using log4net.Config;
using MSQL.Model;
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
            var clientPeer = new ClientPeer(initRequest.Protocol,initRequest.PhotonPeer);
            return clientPeer;
        }

        protected override void Setup()
        {
            
            
              log4net.GlobalContext.Properties["Photon:ApplicationLogPath"] = Path.Combine(this.ApplicationRootPath, "log");
              log4net.GlobalContext.Properties["LogFileName"] = "XX" + this.ApplicationName;
              var file = new FileInfo(Path.Combine(this.BinaryPath, "log4net.config"));
              if (file.Exists)
              {
                  LogManager.SetLoggerFactory(Log4NetLoggerFactory.Instance);
                  XmlConfigurator.ConfigureAndWatch(file);
              }
              log.InfoFormat("Create application Instance: Type={0}", Instance.GetType());
              log.Debug("!!!!!!!!!!!!!!!!! Application setup complete");
              var set = ConfigurationManager.AppSettings["ooxx"];
              log.Debug(" setting : " + set);
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

    public class ClientPeer : PeerBase
    {
        private static readonly ILogger log = LogManager.GetCurrentClassLogger();

        #region VARIABLES
/// <summary>
/// save current user account info for login
/// </summary>
        public MUser LoginUser { get; set; }
//TODO        public Role LoginRole { get; set; } 
//TODO        public Team Team { get; set; }

        #endregion
//        public ClientPeer(InitRequest initRequest) : base(initRequest)
//        {
//        }

        public ClientPeer(IRpcProtocol protocol, IPhotonPeer unmanagedPeer) : base(protocol, unmanagedPeer)
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
            
            //TODO 
            
        }
    }
}