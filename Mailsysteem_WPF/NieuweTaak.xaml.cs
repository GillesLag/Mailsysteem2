using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Mailsysteem_DAL;

namespace Mailsysteem_WPF
{
    /// <summary>
    /// Interaction logic for NieuweTaak.xaml
    /// </summary>
    public partial class NieuweTaak : Window
    {
        private TaakRepo TaakRepo = new TaakRepo();
        private TaakCategorieRepo taakCategorieRepo = new TaakCategorieRepo();
        private Gebruiker gebruiker;
        public NieuweTaak(Gebruiker g)
        {
            InitializeComponent();
            gebruiker = g;
            lblCategorieën.Content = "";
        }

        private void btnAanmaken_Click(object sender, RoutedEventArgs e)
        {
            if (dpEindDatum.SelectedDate == null)
            {
                MessageBox.Show("Vul een einddatum in!");
                return;
            }
            Taak taak = new Taak()
            {
                naam = tbTitelTekst.Text,
                isVoltooid = false,
                eindDatum = dpEindDatum.SelectedDate.Value,
                gebruikerId = gebruiker.id,
                extraInfo = tbTaakBody.Text
            };

            if (dpHerinneringDatum.DisplayDate != null)
            {
                taak.herinneringDatum = dpHerinneringDatum.SelectedDate;
            }

            if (!taak.IsGeldig())
            {
                MessageBox.Show(taak.Error);
                return;
            }

            if (!TaakRepo.InsertTaak(taak))
            {
                MessageBox.Show("Geen taak kunnen toevoegen!");
            }

            taakCategorieRepo.InsertTaakCategorie(taak, lblCategorieën.Content.ToString());

            this.Close();
        }

        private void btnCategorieToevoegen_Click(object sender, RoutedEventArgs e)
        {
            CategorieWindow cat = new CategorieWindow();
            cat.ShowDialog();

            if (string.IsNullOrWhiteSpace(cat.keuze))
                return;

            if (lblCategorieën.Content.ToString().Contains(cat.keuze))
                return;

            if (string.IsNullOrWhiteSpace(lblCategorieën.Content.ToString()))
                lblCategorieën.Content = cat.keuze;

            else
                lblCategorieën.Content += ", " + cat.keuze;
        }
    }
}
