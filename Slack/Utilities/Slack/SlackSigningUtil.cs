using System;
using System.Security.Cryptography;
using System.Text;

namespace Slack.Utilities.Slack
{
    public class SlackSigningUtil
    {

        public bool SignatureValid(string xSlackSignature, string xSlackRequestTimestamp, string requestBody, string signatureSecret)
        {
            if (!DateValid(xSlackRequestTimestamp))
            {
                return false;
            }

            var hmacHash = GetHmacSha256Hash(xSlackRequestTimestamp, requestBody, signatureSecret);

            return hmacHash == xSlackSignature;
        }

        private string GetHmacSha256Hash(string xSlackRequestTimestamp, string requestBody, string signatureSecret)
        {
            var requestSignature = BuildRequestSignature(xSlackRequestTimestamp, requestBody);

            var HmacSha256 = new HMACSHA256(Encoding.UTF8.GetBytes(signatureSecret));
            var hashBytes = HmacSha256.ComputeHash(Encoding.UTF8.GetBytes(requestSignature));
            var hash = HashEncode(hashBytes);
            var versionedHash = $"v0={hash}";
            return versionedHash;
        }

        private bool DateValid(string xSlackRequestTimestamp)
        {
            var time = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(Convert.ToInt64(xSlackRequestTimestamp));
            var difference = time.Subtract(DateTime.UtcNow);
            if (Math.Abs(difference.TotalSeconds) > 60 * 5)
            {
                return false;
            }

            return true;
        }

        private string BuildRequestSignature(string xSlackRequestTimestamp, string requestBody)
        {
            var versionNumber = "v0";
            var baseString = $"{versionNumber}:{xSlackRequestTimestamp}:{requestBody}";
            return baseString;
        }

        private static string HashEncode(byte[] hash)
        {
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }
}
