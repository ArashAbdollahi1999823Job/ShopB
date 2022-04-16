namespace Application.Visitors.VisitorOnline
{
    public interface IVisitorOnlineService
    {
        void ConnectUser(string clidntId);
        void DisConnectUser(string clidntId);
        int GetCount();
    }
}
