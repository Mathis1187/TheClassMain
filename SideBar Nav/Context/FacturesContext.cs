using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheClassMain.Composants;
using Microsoft.EntityFrameworkCore;

namespace TheClassMain.Context
{
    public class FacturesContext : DbContext
    {
        public DbSet<Factures> Factures { get; set; }
    }
}
