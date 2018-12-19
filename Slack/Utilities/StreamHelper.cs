using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Slack.Utilities
{
    public class StreamHelper
    {
        public async Task<string> ReadAsString(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}
