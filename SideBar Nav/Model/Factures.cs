using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheClassMain.Composants
{
    public class Factures
    {

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Montant { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("CategorieId")]
        public int CategorieId { get; set; }

        public string CategorieName { get; set; }
        public virtual Categories Categorie { get; set; }
    }
}
