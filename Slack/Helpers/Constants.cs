using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Slack.Helpers
{
    public class Constants
    {
        public static IConfiguration Configuration { get; set; }

        public Constants()
        {
            var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();
        }

        public string SlackSigningSignature
        {
            get
            {
                return Configuration["Slack_SigningSecret"];
            }
        }
    }
}
