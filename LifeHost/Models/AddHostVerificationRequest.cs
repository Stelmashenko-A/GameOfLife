using System;

namespace LifeHost.Models
{
    public class AddHostVerificationRequest
    {
        public Guid RequestId { get; set; }
        public string EncodedString { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
    }
}