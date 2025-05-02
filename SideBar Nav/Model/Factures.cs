namespace TheClassMain.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Defines the <see cref="Factures" />
    /// </summary>
    public class Factures
    {
        /// <summary>
        /// Gets or sets the FactureId
        /// </summary>
        [Key]
        public int FactureId { get; set; }

        /// <summary>
        /// Gets or sets the Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the Montant
        /// </summary>
        public decimal Montant { get; set; }

        /// <summary>
        /// Gets or sets the Date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the CustomerId
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the Customer
        /// </summary>
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        /// <summary>
        /// Gets or sets the CategorieId
        /// </summary>
        public int CategorieId { get; set; }

        /// <summary>
        /// Gets or sets the Categorie
        /// </summary>
        [ForeignKey("CategorieId")]
        public virtual Categories Categorie { get; set; }

        /// <summary>
        /// Gets or sets the CategorieName
        /// </summary>
        public string CategorieName { get; set; }
    }
}
