using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using TheClassMain.Model;
using TheClassMain.Service;
using System.Windows.Data;
using TheClassMain.Query;
using TheClassMain.ViewModel;

namespace TheClassMain.Pages
{
    public partial class Calendrier : Page
    {
        public Calendrier()
        {
            InitializeComponent();
            DataContext = new CalendrierViewModel();
        }
    }
}