using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlackingBass.Services
{
    public interface ISignatureValidationService
    {
        bool SignatureValid(string xSlackSignature, string xSlackRequestTimestamp, string requestBody, string signatureSecret);
    }
}
