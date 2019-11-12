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
using PhotoServer.Handlers;


namespace PhotoServer
{
    public class ServerApplication:ApplicationBase
    {
        private static ServerApplication _instance;
        private static readonly ILogger log = LogManager.GetCurrentClassLogger();
        public Dictionary<byte,BaseHandler> handlers = new Dictionary<byte, BaseHandler>();

        public static ServerApplication Instance
        {
            get { return _instance; }
        }
        public List<ClientPeer> ClientPeers = new List<ClientPeer>();

        public ServerApplication()
        {
            
        }
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

    
}