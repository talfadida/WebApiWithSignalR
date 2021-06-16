using System.Threading.Tasks;
using Guardium.Server.Model;

namespace Guardium.Server
{
    public interface INotifier
    {
        Task NotifyClientsOnAdd(string uuid, ElementContent elementContent);
        Task NotifyClientsOnDelete(string uuid, string elementIdentifier);
    }
}
