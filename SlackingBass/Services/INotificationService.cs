namespace SlackingBass.Services
{
    public interface INotificationService
    {
        bool Notify(string title, string body);
    }
}
