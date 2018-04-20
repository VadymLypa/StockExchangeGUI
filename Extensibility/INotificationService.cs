namespace Extensibility
{
    public interface INotificationService
    {
        void WrongInput();
        void SocketError(string message);
    }
}
