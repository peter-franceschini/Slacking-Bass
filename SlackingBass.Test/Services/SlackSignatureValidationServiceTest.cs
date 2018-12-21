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
        public void SignatureValid_ExpiredTimestamp_Fail()
        {
            var hashService = new HmacSha256HashService();
            var slackSigningUtil = new SlackSignatureValidationService(hashService);
            string xSlackSignature = "v0=30fce689f9150236dd75fcfbec40374a7495fb8b7b179fa0112cec871cef5c5e";
            string xSlackRequestTimestamp = "1545336933";
            string requestBody = "token=IYrLqos4BxfSVvVe0BcqW36D&team_id=T3VCSKSN7&team_domain=octopusunicorn&channel_id=C3W5PQGFR&channel_name=general&user_id=U3W5PQE0P&user_name=peter&command=%2Ftest&text=testing&response_url=https%3A%2F%2Fhooks.slack.com%2Fcommands%2FT3VCSKSN7%2F509522938821%2FJxEFYrz7ug6oY8Hj0WF9T0cL&trigger_id=510375801943.131434672755.c7167f537e169a70ad39ef06059a8322";
            string slackSecret = "8f742231b10e8888abcd99yyyzzz85a5";

            Assert.IsFalse(slackSigningUtil.SignatureValid(xSlackSignature, xSlackRequestTimestamp, requestBody, slackSecret));
        }
    }
}
