using System.Collections.Generic;
using ExitGames.Logging;
using MSQL.Model;
using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;

namespace PhotoServer
{
    public class ClientPeer : PeerBase
        {
            private static readonly ILogger log = LogManager.GetCurrentClassLogger();
    
            #region VARIABLES
    /// <summary>
    /// save current user account info for login
    /// </summary>
            public MUser LoginUser { get; set; }
    //TODO        public Role LoginRole { get; set; } 
            public Team Team { get; set; }
    
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