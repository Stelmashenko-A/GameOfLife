using System;

namespace LifeHost.Models
{
    public class AddHostResponce
    {
        public Guid RequestId { get; set; }
        public string VerificationString { get; set; }
    }
}