namespace Guardium.Server.Model
{
    public interface IAuthorizationManager
    {
        int MaxElementsPerDay { get; }
        bool AllowAddNewElement(User user);
        bool AllowDeleteElement(User user, Page page);
    }
}