using System.Collections.Generic;
using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;

namespace PhotoServer
{
    public class ChatServer:ApplicationBase
    {
        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            var clientPeer = new ChatPeer(initRequest.Protocol,initRequest.PhotonPeer);
            return clientPeer;
        }

        protected override void Setup()
        {
              
        }

        protected override void TearDown()
        {
             
        }
    }

    public class ChatPeer : PeerBase
    {
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


                        SendOperationResponse(
                            new OperationResponse(2,
                                new Dictionary<byte, object>() {{20, operationRequest.OperationCode + "|" + obj}}),
                            sendParameters);
                    }

                    break;
            }
        }

        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
             
        }
    }
}