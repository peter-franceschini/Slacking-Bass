using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SlackingBass.Services
{
    public class HmacSha256HashService : IHashService
    {
        /// <summary>
        /// Gets HmacSha256 hash of a Slack request signature using a Slack secret
        /// </summary>
        /// <param name="requestSignature"></param>
        /// <param name="signatureSecret"></param>
        /// <returns></returns>
        public string GetHash(string requestSignature, string signatureSecret)
        {
            var HmacSha256 = new HMACSHA256(Encoding.UTF8.GetBytes(signatureSecret));
            var hashBytes = HmacSha256.ComputeHash(Encoding.UTF8.GetBytes(requestSignature));
            var hash = HashEncode(hashBytes);
            return hash;
        }

        public string GenerateVersionedHash(string hash, string versionNumber)
        {
            return $"{versionNumber}={hash}";
        }

        private string HashEncode(byte[] hash)
        {
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }
}
