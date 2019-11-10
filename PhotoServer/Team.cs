using System.Collections.Generic;

namespace PhotoServer
{
    public class Team
    {
        public List<ClientPeer> ClientPeers = new List<ClientPeer>();
        public int masterRoleId = 0;

        public Team()
        {
            
        }

        public Team(ClientPeer peer1, ClientPeer peer2, ClientPeer peer3)
        {
            ClientPeers.Add(peer1);
            ClientPeers.Add(peer2);
            ClientPeers.Add(peer3);
            peer1.Team = this;
            peer2.Team = this;
            peer3.Team = this;
            masterRoleId = peer3.LoginUser.Id;
        }

        public void Dissmiss()
        {
            masterRoleId = 0;
            ClientPeers.ForEach(p => { p.Team = null; });
            ClientPeers = null;
        }
    }
}