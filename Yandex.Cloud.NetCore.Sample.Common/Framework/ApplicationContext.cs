using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Yandex.Cloud.NetCore.Sample.Common.Models;

namespace Yandex.Cloud.NetCore.Sample.Common.Framework
{
    public sealed class ApplicationContext : DbContext
    {

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<Member> Members { get; set; }

    }
}
