using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlackingBass.Services
{
    public interface IHashService
    {
        string GetHash(string requestSignature, string signatureSecret);
        string GenerateVersionedHash(string hash, string versionNumber);
    }
}
