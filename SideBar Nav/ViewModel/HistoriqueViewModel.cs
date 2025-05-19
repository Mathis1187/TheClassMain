using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TheClassMain.Model;
using TheClassMain.Query;
using TheClassMain.ViewModel;

public class HistoriqueViewModel : ViewModelBase
{
    public ObservableCollection<Historique> HistoryEntries { get; set; } = new();

    public HistoriqueViewModel()
    {
        _ = InitializeHistorique();
    }

    private async Task InitializeHistorique()
    {
        await CleanupOldHistorique();
        await LoadHistorique();
    }

    public async Task LoadHistorique()
    {
        try
        {
            using var db = new TableContext();
            var list = await db.HistoriquesT
                .OrderByDescending(h => h.Timestamp)
                .ToListAsync();

            HistoryEntries.Clear();
            foreach (var entry in list)
                HistoryEntries.Add(entry);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors du chargement de l'historique : {ex.Message}");
        }
    }

    public async Task AddHistorique(string action, string description, int? factureId = null)
    {
        try
        {
            var newEntry = new Historique
            {
                Timestamp = DateTime.Now,
                Action = action,
                Description = description,
                FactureId = factureId
            };

            using var db = new TableContext();
            db.HistoriquesT.Add(newEntry);
            await db.SaveChangesAsync();

            HistoryEntries.Insert(0, newEntry);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors de l'ajout de l'historique : {ex.Message}");
        }
    }

    private async Task CleanupOldHistorique()
    {
        try
        {
            using var db = new TableContext();
            var thirtyDaysAgo = DateTime.Now.AddDays(-30);
            var oldEntries = await db.HistoriquesT
                .Where(h => h.Timestamp < thirtyDaysAgo)
                .ToListAsync();

            if (oldEntries.Any())
            {
                db.HistoriquesT.RemoveRange(oldEntries);
                await db.SaveChangesAsync();
                Console.WriteLine($"{oldEntries.Count} anciennes entrées supprimées de l'historique.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors du nettoyage de l'historique : {ex.Message}");
        }
    }
}
