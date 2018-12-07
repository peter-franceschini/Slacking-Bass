using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slack.Services
{
    public interface INotificationService
    {
        void Notify(string title, string body);
    }
}
