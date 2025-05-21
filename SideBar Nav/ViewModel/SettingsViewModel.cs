using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.Input;
using TheClassMain.Query;
using TheClassMain.Service;

namespace TheClassMain.ViewModel;

public partial class SettingsViewModel : ViewModelBase
{
    [RelayCommand]
    public async Task DeleteAllFactures()
    {
        string message = "Are you sure you want to delete ALL factures?";
        string title = "Delete All";

        var result = MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Warning);
     

        if (result == MessageBoxResult.Yes)
        {
            using var context = new TableContext();

            var allFactures = context.FacturesT.ToList();
            var allHistorique = context.HistoriquesT.ToList();

            if (allFactures.Any())
            {
                context.FacturesT.RemoveRange(allFactures);
                context.HistoriquesT.RemoveRange(allHistorique);
                await context.SaveChangesAsync();
                MessageBox.Show("All factures deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("There are no factures to delete.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
    
    [RelayCommand]
    public async Task DeleteAllCategories()
    {
        string message = "Are you sure you want to delete ALL categories?";
        string title = "Delete All Categories";

        var result = MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Warning);
        if (result != MessageBoxResult.Yes) return;
  
        using var context = new TableContext();
        
        var allFactures = context.FacturesT.ToList();
        var allHistorique = context.HistoriquesT.ToList();

        var categoriesToDelete = context.CategoriesT
            .Where(c => c.CustomerId == Session.CurrentCustomer.CustomerId)
            .ToList();
        

        if (!categoriesToDelete.Any())
        {
            MessageBox.Show("There are no categories to delete.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        if (allFactures.Any())
        {
            MessageBox.Show("You cant delete all categories if there still factures.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }
        
        if (categoriesToDelete.Any())
            context.CategoriesT.RemoveRange(categoriesToDelete);
        
        if (allHistorique.Any())
            context.HistoriquesT.RemoveRange(allHistorique);
        
        await context.SaveChangesAsync();
        
        MessageBox.Show("All categories were deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
      
    }
    
    [RelayCommand]
    public async Task DeleteAllFacturesAndCategories()
    {
        string message = "Are you sure you want to delete ALL factures and categories?";
        string title = "Delete All Factures and Categories";

        var result = MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Warning);
        if (result != MessageBoxResult.Yes) return;

        using var context = new TableContext();
        
        var allFactures = context.FacturesT.ToList();
        
        var allHistorique = context.HistoriquesT.ToList();
        
        var categoriesToDelete = context.CategoriesT
            .Where(c => c.CustomerId == Session.CurrentCustomer.CustomerId)
            .ToList();

        if (!allFactures.Any() && !categoriesToDelete.Any())
        {
            MessageBox.Show("There are no factures or categories to delete.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }
        
        if (allFactures.Any())
            context.FacturesT.RemoveRange(allFactures);
        
        if (categoriesToDelete.Any())
            context.CategoriesT.RemoveRange(categoriesToDelete);
        
        if (allHistorique.Any())
            context.HistoriquesT.RemoveRange(allHistorique);

        await context.SaveChangesAsync();

        MessageBox.Show("All factures and categories were deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    
    [RelayCommand]
    public void Logout()
    {
        Session.CurrentCustomer = null;
        
        foreach (Window window in Application.Current.Windows)
        {
                window.Close();
        }
        

    }

    public void activateNotifications()
    {
        
    }
}

