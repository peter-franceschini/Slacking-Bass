using Microsoft.VisualStudio.TestTools.UnitTesting;
using SlackingBass.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackingBass.Test.Utilities.Slack
{
    [TestClass]
    public class SlackSignatureValidationServiceTest
    {
        [TestMethod]
        public void SignatureValid_ExpiredTimestamp_ReturnSignatureInvalid()
        {
            var hashService = new HmacSha256HashService();
            var slackSignatureValidationService = new SlackSignatureValidationService(hashService);

            // Dumby data from slack documentation
            string xSlackSignature = "v0=30fce689f9150236dd75fcfbec40374a7495fb8b7b179fa0112cec871cef5c5e";
            string xSlackRequestTimestamp = "1545336933";
            string requestBody = "token=xyzz0WbapA4vBCDEFasx0q6G&team_id=T1DC2JH3J&team_domain=testteamnow&channel_id=G8PSS9T3V&channel_name=foobar&user_id=U2CERLKJA&user_name=roadrunner&command=%2Fwebhook-collect&text=&response_url=https%3A%2F%2Fhooks.slack.com%2Fcommands%2FT1DC2JH3J%2F397700885554%2F96rGlfmibIGlgcZRskXaIFfN&trigger_id=398738663015.47445629121.803a0bc887a14d10d2c447fce8b6703c";
            string slackSecret = "8f742231b10e8888abcd99yyyzzz85a5";

            Assert.IsFalse(slackSignatureValidationService.SignatureValid(xSlackSignature, xSlackRequestTimestamp, requestBody, slackSecret));
        }

        [TestMethod]
        public void SignatureValid_ValidSignature_ReturnSignatureValid()
        {
            var hashService = new HmacSha256HashService();
            var slackSignatureValidationService = new SlackSignatureValidationService(hashService);

            // Dumby data from slack documentation
            var xSlackRequestTimestamp = ((long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds).ToString();
            var requestBody = "token=xyzz0WbapA4vBCDEFasx0q6G&team_id=T1DC2JH3J&team_domain=testteamnow&channel_id=G8PSS9T3V&channel_name=foobar&user_id=U2CERLKJA&user_name=roadrunner&command=%2Fwebhook-collect&text=&response_url=https%3A%2F%2Fhooks.slack.com%2Fcommands%2FT1DC2JH3J%2F397700885554%2F96rGlfmibIGlgcZRskXaIFfN&trigger_id=398738663015.47445629121.803a0bc887a14d10d2c447fce8b6703c";
            var slackSecret = "8f742231b10e8888abcd99yyyzzz85a5";

            // Build validHashedSlackSignature to compare against
            var validSlackSignature = $"v0:{xSlackRequestTimestamp}:token=xyzz0WbapA4vBCDEFasx0q6G&team_id=T1DC2JH3J&team_domain=testteamnow&channel_id=G8PSS9T3V&channel_name=foobar&user_id=U2CERLKJA&user_name=roadrunner&command=%2Fwebhook-collect&text=&response_url=https%3A%2F%2Fhooks.slack.com%2Fcommands%2FT1DC2JH3J%2F397700885554%2F96rGlfmibIGlgcZRskXaIFfN&trigger_id=398738663015.47445629121.803a0bc887a14d10d2c447fce8b6703c";
            var validHashedSlackSignature = "v0=" + hashService.GetHash(validSlackSignature, slackSecret);

            Assert.IsTrue(slackSignatureValidationService.SignatureValid(validHashedSlackSignature, xSlackRequestTimestamp, requestBody, slackSecret));
        }

        [TestMethod]
        public void SignatureValid_InvalidRequestBody_ReturnSignatureInvalid()
        {
            var hashService = new HmacSha256HashService();
            var slackSignatureValidationService = new SlackSignatureValidationService(hashService);

            // Dumby data from slack documentation
            var xSlackRequestTimestamp = ((long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds).ToString();
            var requestBody = string.Empty;
            var slackSecret = "8f742231b10e8888abcd99yyyzzz85a5";

            // Build validHashedSlackSignature to compare against
            var validSlackSignature = $"v0:{xSlackRequestTimestamp}:token=xyzz0WbapA4vBCDEFasx0q6G&team_id=T1DC2JH3J&team_domain=testteamnow&channel_id=G8PSS9T3V&channel_name=foobar&user_id=U2CERLKJA&user_name=roadrunner&command=%2Fwebhook-collect&text=&response_url=https%3A%2F%2Fhooks.slack.com%2Fcommands%2FT1DC2JH3J%2F397700885554%2F96rGlfmibIGlgcZRskXaIFfN&trigger_id=398738663015.47445629121.803a0bc887a14d10d2c447fce8b6703c";
            var validHashedSlackSignature = "v0=" + hashService.GetHash(validSlackSignature, slackSecret);

            Assert.IsFalse(slackSignatureValidationService.SignatureValid(validHashedSlackSignature, xSlackRequestTimestamp, requestBody, slackSecret));
        }

        [TestMethod]
        public void SignatureValid_InvalidSecret_ReturnSignatureInvalid()
        {
            var hashService = new HmacSha256HashService();
            var slackSignatureValidationService = new SlackSignatureValidationService(hashService);

            // Dumby data from slack documentation
            var xSlackRequestTimestamp = ((long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds).ToString();
            var requestBody = "token=xyzz0WbapA4vBCDEFasx0q6G&team_id=T1DC2JH3J&team_domain=testteamnow&channel_id=G8PSS9T3V&channel_name=foobar&user_id=U2CERLKJA&user_name=roadrunner&command=%2Fwebhook-collect&text=&response_url=https%3A%2F%2Fhooks.slack.com%2Fcommands%2FT1DC2JH3J%2F397700885554%2F96rGlfmibIGlgcZRskXaIFfN&trigger_id=398738663015.47445629121.803a0bc887a14d10d2c447fce8b6703c";
            var slackSecret = "8f742231b10e8888abcd99yyyzzz85a5";

            // Build validHashedSlackSignature to compare against
            var validSlackSignature = $"v0:{xSlackRequestTimestamp}:token=xyzz0WbapA4vBCDEFasx0q6G&team_id=T1DC2JH3J&team_domain=testteamnow&channel_id=G8PSS9T3V&channel_name=foobar&user_id=U2CERLKJA&user_name=roadrunner&command=%2Fwebhook-collect&text=&response_url=https%3A%2F%2Fhooks.slack.com%2Fcommands%2FT1DC2JH3J%2F397700885554%2F96rGlfmibIGlgcZRskXaIFfN&trigger_id=398738663015.47445629121.803a0bc887a14d10d2c447fce8b6703c";
            var validHashedSlackSignature = "v0=" + hashService.GetHash(validSlackSignature, slackSecret);

            Assert.IsFalse(slackSignatureValidationService.SignatureValid(validHashedSlackSignature, xSlackRequestTimestamp, requestBody, string.Empty));
        }
    }
}
