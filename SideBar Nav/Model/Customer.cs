namespace TheClassMain.Model
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using TheClassMain.Composants;

    /// <summary>
    /// Defines the <see cref="Customer" />
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the UserName
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the Pwd
        /// </summary>
        public string Pwd { get; set; }

        /// <summary>
        /// Gets or sets the CategorieId
        /// </summary>
        [ForeignKey("CategorieId")]
        public int CategorieId { get; set; }

        /// <summary>
        /// Gets or sets the FactureId
        /// </summary>
        [ForeignKey("FactureId")]
        public int FactureId { get; set; }

        /// <summary>
        /// Gets or sets the Categorie
        /// </summary>
        public virtual Categories Categorie { get; set; }

        /// <summary>
        /// Gets or sets the Facture
        /// </summary>
        public virtual Factures Facture { get; set; }
    }
}
