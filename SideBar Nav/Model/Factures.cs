using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TheClassMain.Pages;

namespace TheClassMain.Composants
{
    class Factures
    {
        private string name;
        private string description;
        private int montant;
        private Calendar date;
        private RappelsCategories rappels;
        private List<Factures> factures;

        public Factures()
        {
        }

        public String Name { get; set; }
        public String Description { get; set; }
        public int Montant { get; set; }
        public Calendar Date { get; set; }
    }
}
