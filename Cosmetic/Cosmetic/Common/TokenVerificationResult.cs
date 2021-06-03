using System;
using System.Collections.Generic;
using System.Text;

namespace EC.SecurityService.Common
{
    public class TokenVerificationResult
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public TokenVerificationResult(string message, bool succeeded = true)
        {
            this.Message = message;
            this.Succeeded = succeeded;
        }
    }

    public class AccessCodeVerifyResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string CellPhone { get; set; }
    }
}