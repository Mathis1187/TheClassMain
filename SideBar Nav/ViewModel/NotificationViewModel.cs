using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using TheClassMain.Model;
using TheClassMain.Query;

namespace TheClassMain.ViewModel;

public partial class NotificationViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Factures> upcomingFactures = new();
    
    [ObservableProperty]
    private ObservableCollection<Factures> expiredFactures = new();

    public NotificationViewModel()
    {
        using var context = new TableContext();
        LoadUpcomingFacturesFromDatabase();
        LoadExpiredFacturesFromDatabase();
        CheckAndNotify();
    }

    private void LoadUpcomingFacturesFromDatabase()
    {
        var today = DateTime.Today;
        using var context = new TableContext();

        var allFactures = context.FacturesT.ToList();

        var facturesAvenir = allFactures
            .Where(f => f.Date.Date >= today)
            .OrderBy(f => f.Date)
            .ToList();

        UpcomingFactures = new ObservableCollection<Factures>(facturesAvenir);
    }
    
    private void LoadExpiredFacturesFromDatabase()
    {
        var today = DateTime.Today;
        using var context = new TableContext();
        
        var allFactures = context.FacturesT.ToList();
        
        var facturesPassees = allFactures
            .Where(f => f.Date.Date < today)
            .OrderByDescending(f => f.Date)
            .ToList();

        ExpiredFactures = new ObservableCollection<Factures>(facturesPassees); 
    }

    private List<Factures> GetFacturesALAvenirDansTroisJours()
    {
        var today = DateTime.Today;
        var limitDate = today.AddDays(3);

        using var context = new TableContext();
        var facturesProchaines = context.FacturesT
            .Where(f => f.Date.Date >= today && f.Date.Date <= limitDate)
            .OrderBy(f => f.Date)
            .ToList();

        return facturesProchaines;
    }
    
    
    public void CheckAndNotify()
    {
        var facturesProchaines = GetFacturesALAvenirDansTroisJours();

        if (facturesProchaines.Any())
        {
            string message = "Factures à venir dans les 3 prochains jours :\n";
            foreach (var f in facturesProchaines)
            {
                message += $"{f.Description} - Date: {f.Date.ToShortDateString()}\n"; 
            }
            System.Windows.MessageBox.Show(message, "Notification Factures Prochaines", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

}
