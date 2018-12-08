namespace Slack.Services
{
    public interface INotificationService
    {
        bool Notify(string title, string body);
    }
}
