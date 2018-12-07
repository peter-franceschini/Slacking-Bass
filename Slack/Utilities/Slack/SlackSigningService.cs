using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Slack.Utilities.Slack
{
    public class SlackSigningUtil
    {
        public bool ValidateSignature(string xSlackSignature, string xSlackRequestTimestamp, string command)
        {
            var baseString = BuildBaseString(xSlackRequestTimestamp, command);
            //var HMACHash = HashHMAC()

            return true;
        }

        private string BuildBaseString(string xSlackRequestTimestamp, string command)
        {
            var versionNumber = "v0";
            var baseString = $"{versionNumber}:{xSlackRequestTimestamp}:command={command}";
            return baseString;
        }

        private static string HashHMACHex(string keyHex, string message)
        {
            byte[] hash = HashHMAC(HexDecode(keyHex), StringEncode(message));
            return HashEncode(hash);
        }

        private static byte[] HashHMAC(byte[] key, byte[] message)
        {
            var hash = new HMACSHA256(key);
            return hash.ComputeHash(message);
        }

        private static byte[] StringEncode(string text)
        {
            var encoding = new ASCIIEncoding();
            return encoding.GetBytes(text);
        }

        private static string HashEncode(byte[] hash)
        {
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }

        private static byte[] HexDecode(string hex)
        {
            var bytes = new byte[hex.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = byte.Parse(hex.Substring(i * 2, 2), NumberStyles.HexNumber);
            }
            return bytes;
        }
    }
}
