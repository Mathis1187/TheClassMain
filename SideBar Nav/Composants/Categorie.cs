using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheClassMain.Composants
{
    class RappelsCategories
    {
        private List<string> categorie = new List<string>();
        private bool favorit;
        private double id;

        public static double autoIncrement = 1;

        public RappelsCategories(List<string> categorie, bool favorit, double id)
        {
            this.categorie = categorie;
            this.favorit = favorit;
            this.id = autoIncrement++;
        }

        public List<String> Categorie { get; set; }
        public bool Favorit { get; set; }
        public double Id { get; set; }

    }
}
