using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TheClassMain.Pages;
using System.Data.Entity;
using Microsoft.VisualBasic;

namespace TheClassMain.Composants
{

    public class Factures
    {
        static int cpt = 1;
        public string Name { get; set; }
        public string Description { get; set; }
        public int Montant { get; set; }
        public DateTime Date { get; set; }
        public string Categorie { get; set; }
        public int Num {  get; set; }

        public Factures()
        {
            Num = cpt++;
        }

           public override string ToString()
       {
           return $"#{Num} - {Description} | {Montant}$ | {Categorie} | {Date.ToShortDateString()}";
       }
    }

    public class FacturesContext : DbContext
    {
        public DbSet<Factures> Factures { get; set; }
    }
}
