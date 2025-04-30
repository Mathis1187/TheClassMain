namespace TheClassMain.Composants
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
        /// Gets or sets the Id
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string Name { get; set; }

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
        /// Gets or sets the CategorieId
        /// </summary>
        [ForeignKey("CategorieId")]
        public int CategorieId { get; set; }

        /// <summary>
        /// Gets or sets the CategorieName
        /// </summary>
        public string CategorieName { get; set; }

        /// <summary>
        /// Gets or sets the Categorie
        /// </summary>
        public virtual Categories Categorie { get; set; }
    }
}
