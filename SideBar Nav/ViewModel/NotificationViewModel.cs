using System;
using System.Collections.ObjectModel;
using System.Linq;
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
}
