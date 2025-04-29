using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheClassMain.Composants;

namespace TheClassMain.Model
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Pwd { get; set; }

        [ForeignKey("CategorieId")]
        public int CategorieId { get; set; }

        [ForeignKey("FactureId")]
        public int FactureId { get; set; }

        public virtual Categories Categorie { get; set; }
        public virtual Factures Facture { get; set; }
    }
}
