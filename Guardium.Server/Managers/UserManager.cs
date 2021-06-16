using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Guardium.Server.Model;

namespace Guardium.Server
{
    public class UserManager: IUserManager
    {
        private readonly Dictionary<string, User> _userList = new Dictionary<string, User>();

        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserManager(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }
        public User GetCurrent()
        {
            string ipAddress = ExtractIpAddress();
            if (_userList.ContainsKey(ipAddress))
                return _userList[ipAddress];

            var newUser = new User(ipAddress);
            _userList.Add(ipAddress, newUser);
            return newUser;
        }

        private string ExtractIpAddress()
        {
            //get from connection/context
            //return this._httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

            //get from header
            var headers = this._httpContextAccessor.HttpContext.Request.Headers;
            return headers.ContainsKey("ip") ? headers["ip"] : throw new Exception("User must have ip address");
        }
    }
}
