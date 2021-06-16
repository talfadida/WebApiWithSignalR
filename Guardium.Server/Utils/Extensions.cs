using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Guardium.Server.Model;

namespace Guardium.Server.Utils
{
    public static class Extensions
    {
        public static Page GetOrAdd(this Dictionary<string, Page> dic, string uuid, Page newPage)
        {
            if (dic.ContainsKey(uuid))
                return dic[uuid];
            
            lock(dic)                
            {
                if (dic.ContainsKey(uuid))
                    return dic[uuid];
                dic.Add(uuid, newPage);
                return dic[uuid];
            }
        }
    }
}
