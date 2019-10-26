using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;

public class Main : MonoBehaviour ,IPhotonPeerListener
{
    private bool Status = false;
        PhotonPeer peer;
    // Start is called before the first frame update
    void Start()
    {
        
        peer = new PhotonPeer(this, ConnectionProtocol.Tcp);
        peer.Connect("127.0.0.1:4530", "ChatServer");
        
    }

    private void OnGUI()
    {
        if (GUILayout.Button("send"))
        {
            if (this.Status)
            {
                peer.OpCustom(1, new Dictionary<byte, object>() {{10, " hello world "}}, true);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        peer?.Service();
    }

    public void DebugReturn(DebugLevel level, string message)
    {
        Debug.Log(" Debug Return : " +level +" : " + message);
    }

    public void OnOperationResponse(OperationResponse operationResponse)
    {
        switch (operationResponse.OperationCode)
        {
            case 2 :
                if (operationResponse.Parameters.TryGetValue(20, out var obj))
                {
                    Debug.Log(" rcv : " + obj);
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
                Debug.Log(" Server Connected !");
                break;
        }
    }

    public void OnEvent(EventData eventData)
    {
        
    }
}
