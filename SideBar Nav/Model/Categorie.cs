using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheClassMain.Composants
{
    public class Categories
    {
        public string Name { get; set; }
        public bool Favorit { get; set; }
        public int Id { get; set; }

        static int cpt = 1;

        public Categories()
        {
            Id = cpt++;
        }
    }
}
