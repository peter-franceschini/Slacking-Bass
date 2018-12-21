using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlackingBass.Services
{
    public class SlackSignatureValidationService : ISignatureValidationService
    {
        private const string VersionNumber = "v0";
        private IHashService HashService { get; set; }

        public SlackSignatureValidationService(IHashService hashService)
        {
            HashService = hashService;
        }

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
            // Validate request timestamp
            if (!DateValid(xSlackRequestTimestamp))
            {
                return false;
            }

            // Build request signature
            var requestSignature = BuildRequestSignature(xSlackRequestTimestamp, requestBody);

            // Hash request signature
            var hashedSignature = HashService.GetHash(requestSignature, signatureSecret);
            var versionedHash = HashService.GenerateVersionedHash(hashedSignature, VersionNumber);

            return versionedHash == xSlackSignature;
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
    }
}
