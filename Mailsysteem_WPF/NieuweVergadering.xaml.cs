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
    /// Interaction logic for NieuweVergadering.xaml
    /// </summary>
    public partial class NieuweVergadering : Window
    {
        private VergaderingRepo vergaderingRepo = new VergaderingRepo();
        private VergaderingGenodigdeRepo vergaderingGenodigdeRepo = new VergaderingGenodigdeRepo();
        private Gebruiker gebruiker;
        public NieuweVergadering(Gebruiker g)
        {
            InitializeComponent();
            gebruiker = g;
        }

        private void btnAanmaken_Click(object sender, RoutedEventArgs e)
        {
            if (dpDatum.SelectedDate == null)
            {
                MessageBox.Show("vul een datum in!");
                return;
            }

            Vergadering vergadering = new Vergadering()
            {
                onderwerp = tbTitelTekst.Text,
                datum = dpDatum.SelectedDate.Value,
                plaats = tbLocatieTekst.Text,
                organisatorId = gebruiker.id,
                Gebruiker = gebruiker,
            };

            List<string> emailAdressen = tbDeelnemeersTekst.Text.Split(';').Where(s => !string.IsNullOrWhiteSpace(s)).Select(x => x.Trim()).ToList();
            emailAdressen.Add(gebruiker.email);
            string fouteEmailAdressen = "";

            foreach (string email in emailAdressen)
            {
                if (vergaderingGenodigdeRepo.GetGebruikerId(email) == -1)
                    fouteEmailAdressen += email + Environment.NewLine;
            }

            if (!string.IsNullOrWhiteSpace(fouteEmailAdressen))
            {
                MessageBox.Show("Volgende email adressen bestaan niet:\n" + fouteEmailAdressen);
                return;
            }

            if (emailAdressen.Count == 0)
            {
                MessageBox.Show("vul een deelnemer in!");
                return;
            }

            try
            {
                vergadering.beginTijd = TimeSpan.ParseExact(tbBegintijd.Text, "hh\\:mm", System.Globalization.CultureInfo.InvariantCulture);
                vergadering.eindTijd = TimeSpan.ParseExact(tbEindtijd.Text, "hh\\:mm", System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                MessageBox.Show("Vul een geldige begin -en eindtijd in!");
                return;
            }

            if (!vergadering.IsGeldig())
            {
                MessageBox.Show(vergadering.Error);
                return;
            }

            if (!vergaderingRepo.InsertVergadering(vergadering))
            {
                MessageBox.Show("Vergadering kon niet opgslagen worden");
                return;
            }

            vergaderingGenodigdeRepo.InsertVergaderingGenodigde(emailAdressen);

            this.Close();
        }
    }
}
