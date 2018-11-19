using System;

namespace AuthAPI.Models
{
    public class UserClaim
    {
        public Guid? UserClaimId { get; set; }

        public string UserId { get; set; }

        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }
    }
}
