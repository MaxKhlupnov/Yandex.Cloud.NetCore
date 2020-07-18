using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Yandex.Cloud.NetCore.Sample.Common.Models;
namespace Yandex.Cloud.NetCore.Sample.Common.Framework
{
    public sealed class AuthContext : IdentityDbContext<Member>
    {

        public AuthContext(DbContextOptions<AuthContext> options) : base(options)
        {
        }

        public DbSet<Member> Members { get; set; }

    }
}
