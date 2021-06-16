using System;

namespace Guardium.Server.Model
{
    public class User
    {
        public User(string userIp)
        {
            
            UserIP = userIp;
        }

        public override bool Equals(object obj)
        {
            User target = obj as User;
            if (target == null) return false;
            return this.UserIP.ToLower() == target.UserIP.ToLower();
        }

        public override int GetHashCode()
        {
            return UserIP.GetHashCode();
        }

        public string UserIP { get; set; }         
        public string Username { get; set; }
    }
}
