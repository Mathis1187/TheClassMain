using System;
using System.Windows.Controls;

namespace TheClassMain.Pages
{
    /// <summary>
    /// Interaction logic for Page4.xaml
    /// </summary>
    public partial class Rappels : Page
    {
        public Rappels()
        {
            InitializeComponent();
        }

        private void btnADDrapelle_click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (txtAJOUTRAPELLE.Text != "" && dpDateRappel.SelectedDate != null)
            {
                lstRAPELLE.Items.Add(txtAJOUTRAPELLE.Text + " - " + dpDateRappel.SelectedDate.Value.ToShortDateString());
                txtAJOUTRAPELLE.Clear();
                dpDateRappel.SelectedDate = null;
                ATTENTION();
            }
        }
        private void ATTENTION()
        {
            DateTime? dateProche = null;
            string nomProche = "";

            foreach (string item in lstRAPELLE.Items)
            {
                string[] DATE = item.Split(" - ");
                if (DATE.Length == 2 && DateTime.TryParse(DATE[1], out DateTime date))
                {
                    if (dateProche == null || date < dateProche)
                    {
                        dateProche = date;
                        nomProche = DATE[0];
                    }
                }
            }

            if (dateProche != null)
                txtPROCHAIN.Text = $"ATTENTION : PROCHAIN PAIEMENT DE {nomProche} LE {dateProche.Value.ToShortDateString()}";
            else
                txtPROCHAIN.Text = "ATTENTION : PROCHAIN PAIEMENT - AUCUN";




        }
    }
}
