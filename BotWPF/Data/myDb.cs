using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotWPF.Data
{
    public class myDb:DbContext
    {
        public DbSet<Video> Videos { get; set; }
        public DbSet<Category> Categories { get; set; }
        public myDb():base("PornDb")
        {


        }
       
    }
}
