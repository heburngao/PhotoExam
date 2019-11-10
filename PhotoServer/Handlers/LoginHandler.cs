using System.Collections.Generic;
using Common;
using Common.Tools;
using LitJson;
using MSQL.Manager;
using MSQL.Model;
using Photon.SocketServer;

namespace PhotoServer.Handlers
{
    public class LoginHandler:BaseHandler
    {
        private MySQL_userManager mgr;
        public LoginHandler()
        {
           
            mgr = new MySQL_userManager();
        }

        public override void OnHandlerMessage(OperationRequest request, OperationResponse response, ClientPeer clientPeer,
            SendParameters sendParameters)
        {
            Dictionary<byte, object> parameters;
            parameters = request.Parameters;
            object jsonObj = null;
            parameters.TryGetValue((byte) ParameterCode.User, out jsonObj);
            MUser user;
            user = JsonMapper.ToObject<MUser>(jsonObj.ToString());
            IList<MUser> userDB;
            userDB = mgr.GetUserByName(user.UserName);
            foreach (MUser mUser in userDB)
            {
                if (mUser.PassWord == MD5Tool.GetMD5(user.PassWord))
                {
                    //username and password is correct, success , login
                    response.ReturnCode = (short) ReturnCode.Success;
                    clientPeer.LoginUser = mUser;
                }
                else
                {
                    response.ReturnCode = (short) ReturnCode.Fail;
                    response.DebugMessage = "username or password error !";
                }
            }
        }

        public override OpCode OpCode
        {
            get
            {
                return OpCode.Login;
            }
        }
    }
}