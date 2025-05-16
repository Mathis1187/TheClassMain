namespace TheClassMain.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Factures
    {
        [Key]
        public int FactureId { get; set; }
        public int UserFactureId { get; set; }

        public string Description { get; set; }
        public decimal Montant { get; set; }
        public DateTime Date { get; set; }

        // public bool IsPayer { get; set; }
        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
        public int CategorieId { get; set; }

        [ForeignKey("CategorieId")]
        public virtual Categories Categorie { get; set; }

        public string CategorieName { get; set; }
    }
}
