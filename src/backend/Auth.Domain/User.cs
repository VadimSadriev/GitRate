﻿using System;
using Microsoft.AspNetCore.Identity;

namespace Auth.Domain
{
    /// <summary>
    /// Application user
    /// </summary>
    public class User : IdentityUser
    {
        public DateTimeOffset CreateDate { get; set; }
    }
}