using System.Threading.Tasks;
using Guardium.Server.Model;

namespace Guardium.Server
{

    public class PageManager : IPageManager
    {
        private readonly IAuthorizationManager _authMgr;
        private readonly INotifier _notifier;
 
        public PageManager(IAuthorizationManager authMgr, INotifier notifier)  
        {
            this._authMgr = authMgr;
            this._notifier  = notifier;
        }

        public async Task AddElement(Page page, User user, ElementContent elementContent)
        {

            if (!_authMgr.AllowAddNewElement(user))
                throw new PageManagerException("Maximum elements per day exceeded");
            

            if (!page.TryAddElement(user, elementContent, out var status))
                throw new PageManagerException($"Couldn't add element: {status}");
            
            await _notifier.NotifyClientsOnAdd(page.PageUUID, elementContent);
            
            
        }

        public async Task DeleteElement(Page page, User user, string elementIdentifier)
        {
            if (!_authMgr.AllowDeleteElement(user, page))
                throw new PageManagerException("You are not allowed to delete elements from this page");

            if (!page.TryDeleteElement(elementIdentifier, out var status))
                throw new PageManagerException($"Something went wrong when trying to delete your element: {status}");

            await _notifier.NotifyClientsOnDelete(page.PageUUID, elementIdentifier);
            
            
        }
    }
}
