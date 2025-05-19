using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheClassMain.Model;

public class Historique
{
    [Key]
    public int Id { get; set; }
    public DateTime Timestamp { get; set; }
    public string Action { get; set; }
    public string Description { get; set; }
    public int? FactureId { get; set; }

    [ForeignKey("FactureId")]
    public Factures Facture { get; set; }
}