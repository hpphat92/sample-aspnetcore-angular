using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace NewApp.Entities
{
    public class Role : IdentityRole<string>, IBaseEntity
    {
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
    }
}
