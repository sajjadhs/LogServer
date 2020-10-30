using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogServer
{
    public class Class : DbContext
    {
        public Class(DbContextOptions<Class> options) : base(options)
        {

        }
        public DbSet<Data> Datas { get; set; }
    }

    public class Data
    {
        public int ID { get; set; }
        public string Content { get; set; }
    }
}
