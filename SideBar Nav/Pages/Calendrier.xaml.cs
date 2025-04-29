using System.Collections.Generic;
using System;
using System.Windows.Controls;
using System.Linq;

namespace TheClassMain.Pages
{
    /// <summary>
    /// Interaction logic for Page2.xaml
    /// </summary>
    public partial class Calendrier : Page
    {

        private List<ObjetCool> listedObjets;
        public Calendrier()
        {
            InitializeComponent();

            listedObjets = new List<ObjetCool>{
                new ObjetCool{ date = DateTime.Today, titre ="ce rendre a LURSSAF" },
                new ObjetCool { date = DateTime.Today.AddDays(4), titre = "truc a faire2" },
            };
            //https://learn.microsoft.com/en-us/dotnet/api/system.windows.controls.calendar.blackoutdates?view=windowsdesktop-9.0
            //quand on clique sur une date
            CalendrierCool26x.SelectedDatesChanged += dateChoisi;
        }
        private void dateChoisi(object sender, SelectionChangedEventArgs e)
        {
            if (CalendrierCool26x.SelectedDate != null)
            {
                DateTime dateee = CalendrierCool26x.SelectedDate.Value;
                ObjetCool evenementTrouverr = null;
                foreach (ObjetCool obj in listedObjets)
                {
                    if (obj.date.Date == dateee.Date)
                    {
                        evenementTrouverr = obj;
                        break;
                    }
                }
                //pour montrer si ya un truc a faire ! 
                if (evenementTrouverr != null)
                {
                    rapelleDujour.Text = "TA UN TRUC A FAIRE !!! : " + evenementTrouverr.titre;
                }
                else
                {
                    rapelleDujour.Text = "rien a faire ! ";
                }
            }
        }



    }

    //juste pour listant pour testerrrr! 
    public class ObjetCool{
        public DateTime date{get;set;}
        public string titre{get;set;}
    }
}
