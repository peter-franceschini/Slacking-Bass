using System;
using System.Security.Cryptography;
using System.Text;

namespace SlackingBass.Utilities.Slack
{
    public class SlackSigningUtil
    {
        private const string VersionNumber = "v0";

        /// <summary>
        /// Checks the validity of a Slack request using the requests signature
        /// </summary>
        /// <param name="xSlackSignature"></param>
        /// <param name="xSlackRequestTimestamp"></param>
        /// <param name="requestBody"></param>
        /// <param name="signatureSecret"></param>
        /// <returns></returns>
        public bool SignatureValid(string xSlackSignature, string xSlackRequestTimestamp, string requestBody, string signatureSecret)
        {
            if (!DateValid(xSlackRequestTimestamp))
            {
                return false;
            }

            var requestSignature = BuildRequestSignature(xSlackRequestTimestamp, requestBody);
            var hmacHash = GetHmacSha256Hash(requestSignature, signatureSecret);

            return hmacHash == xSlackSignature;
        }

        /// <summary>
        /// Gets HmacSha256 hash of a Slack request signature using a Slack secret
        /// </summary>
        /// <param name="requestSignature"></param>
        /// <param name="signatureSecret"></param>
        /// <returns></returns>
        private string GetHmacSha256Hash(string requestSignature, string signatureSecret)
        {
            var HmacSha256 = new HMACSHA256(Encoding.UTF8.GetBytes(signatureSecret));
            var hashBytes = HmacSha256.ComputeHash(Encoding.UTF8.GetBytes(requestSignature));
            var hash = HashEncode(hashBytes);
            var versionedHash = $"{VersionNumber}={hash}";
            return versionedHash;
        }

        /// <summary>
        /// Validates the request is valid based on its requestTimestamp
        /// </summary>
        /// <param name="xSlackRequestTimestamp"></param>
        /// <returns></returns>
        private bool DateValid(string xSlackRequestTimestamp)
        {
            var time = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(Convert.ToInt64(xSlackRequestTimestamp));
            var difference = time.Subtract(DateTime.UtcNow);

            const int secondsInMinute = 60;
            const int minutesRequestValid = 5;
            if (Math.Abs(difference.TotalSeconds) > secondsInMinute * minutesRequestValid)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Build Slack request signature from request timestamp and raw request body
        /// </summary>
        /// <param name="xSlackRequestTimestamp"></param>
        /// <param name="requestBody"></param>
        /// <returns></returns>
        private string BuildRequestSignature(string xSlackRequestTimestamp, string requestBody)
        {
            var baseString = $"{VersionNumber}:{xSlackRequestTimestamp}:{requestBody}";
            return baseString;
        }

        private string HashEncode(byte[] hash)
        {
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }
}
