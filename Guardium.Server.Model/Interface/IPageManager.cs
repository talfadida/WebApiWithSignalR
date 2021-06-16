using System.Threading.Tasks;

namespace Guardium.Server.Model
{
    public interface IPageManager
    {
        Task AddElement(Page page, User user, ElementContent elementContent);
        Task DeleteElement(Page page, User user, string elementIdentifier);
    }
}