namespace TheClassMain.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Categories
    {
        [Key]
        public int CategorieId { get; set; }
        public int UserCategorieId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
        public virtual ICollection<Factures> Factures { get; set; }
    }
}
