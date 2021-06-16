using System;
using System.Linq;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Guardium.Server.Utils;
using Guardium.Server.Model;

namespace Guardium.Server
{
    public class AppManager : IAppManager
    {

        public Dictionary<string, Page> Pages { get; set; } = new Dictionary<string, Page>();

        public AppManager()
        {

        }


        public Page CreateOrGetExistingPage(User user, string uuid)
        {
            return Pages.GetOrAdd(uuid, new Page(uuid, user));
        }

        public int ElementsCreateByUserToday(User user)
        {
            //user can create elements accross different pages, so we should traverse accross all pages
            return Pages
                    .Values
                    .SelectMany(p => p.ListOfElements)
                    .Where(p => p.CreatedBy == user && p.CreatedWhen.Date == DateTime.Today)
                    .Count();

        }

        public void ResetApp()
        {
            Pages.Clear();
        }
    }
}
