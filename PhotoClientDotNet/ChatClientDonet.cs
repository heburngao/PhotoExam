using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;

namespace PhotoClientDotNet
{
    public   class ChatClientDonet
    {
        public static void Main(string[] args)
        {
            PeerListener listener = new PeerListener();
            var peer = new PhotonPeer(listener, ConnectionProtocol.Tcp);
            peer.Connect("127.0.0.1:4530", "ChatServer");
            while (!listener.Status)
            {
                peer.Service();
            }

            peer.OpCustom(1, new Dictionary<byte, object>() {{10, " hello world "}}, true);

            while (true)
            {
                peer.Service();
            }
        }
    }

    public class PeerListener : IPhotonPeerListener
    {
        public bool Status = false;
        public void DebugReturn(DebugLevel level, string message)
        {
            Console.WriteLine(" Debug Return : " +level +" : " + message);
        }

        public void OnOperationResponse(OperationResponse operationResponse)
        {
            switch (operationResponse.OperationCode)
            {
                case 2 :
                    if (operationResponse.Parameters.TryGetValue(20, out var obj))
                    {
                        Console.WriteLine(" rcv : " + obj);
                    }

                    break;
            }

            
        }

        public void OnStatusChanged(StatusCode statusCode)
        {
            switch (statusCode)
            {
                case StatusCode.Connect:
                    Status = true;
                    Console.WriteLine(" Server Connected !");
                    break;
            }
        }

        public void OnEvent(EventData eventData)
        {
        }
    }
}