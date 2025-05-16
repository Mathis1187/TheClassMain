namespace TheClassMain.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Pwd { get; set; }
        public virtual ICollection<Factures> Factures { get; set; }
        public virtual ICollection<Categories> Categories { get; set; }
    }
}
