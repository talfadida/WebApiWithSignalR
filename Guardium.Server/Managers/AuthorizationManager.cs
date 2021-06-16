using System;
using Guardium.Server.Model;
namespace Guardium.Server
{
    public class AuthorizationManager : IAuthorizationManager
    {

        public int MaxElementsPerDay { get; set; } = 5;
        //other safeguars can be defined

        private readonly IAppManager _gApp;

        public AuthorizationManager(IAppManager gApp)
        {
            this._gApp = gApp;
        }

        public bool AllowAddNewElement(User user)
        {
            if (_gApp.ElementsCreateByUserToday(user) >= MaxElementsPerDay)
                return false;
            return true;

        }

        public bool AllowDeleteElement(User user, Page page)
        {
            return (page.UserCreator == user);
        }
    }
}
