using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace NewApp.Entities
{
    public class User : IdentityUser<string>, IBaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public string Password { get; set; }
        public string SocialId { get; set; }
        public string AvatarUrl { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
    }
}
