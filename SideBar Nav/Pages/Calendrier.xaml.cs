namespace TheClassMain.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for Page2.xaml
    /// </summary>
    public partial class Calendrier : Page
    {
        /// <summary>
        /// Defines the listedObjets
        /// </summary>
        private List<ObjetCool> listedObjets;

        /// <summary>
        /// Initializes a new instance of the <see cref="Calendrier"/> class.
        /// </summary>
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

        /// <summary>
        /// The dateChoisi
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="SelectionChangedEventArgs"/></param>
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

    /// <summary>
    /// Defines the <see cref="ObjetCool" />
    /// </summary>
    public class ObjetCool
    {
        /// <summary>
        /// Gets or sets the date
        /// </summary>
        public DateTime date { get; set; }

        /// <summary>
        /// Gets or sets the titre
        /// </summary>
        public string titre { get; set; }
    }
}
