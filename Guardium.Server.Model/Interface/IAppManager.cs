namespace Guardium.Server.Model
{
    public interface IAppManager
    {
        Page CreateOrGetExistingPage(User user, string uuid);
        int ElementsCreateByUserToday(User user);
        void ResetApp();
    }
}