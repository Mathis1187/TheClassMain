namespace TheClassMain.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Defines the <see cref="Customer" />
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Gets or sets the CustomerId
        /// </summary>
        [Key]
        public int CustomerId { get; set; }

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
        /// Gets or sets the Factures
        /// </summary>
        public virtual ICollection<Factures> Factures { get; set; }

        /// <summary>
        /// Gets or sets the Categories
        /// </summary>
        public virtual ICollection<Categories> Categories { get; set; }
    }
}
