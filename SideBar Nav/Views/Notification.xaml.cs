using System.Windows.Controls;
using TheClassMain.ViewModel;

namespace TheClassMain.Views;

public partial class Notification : Page
{
    public Notification()
    {
        InitializeComponent();
        DataContext = new NotificationViewModel();
    }
}