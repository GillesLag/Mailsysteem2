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
    /// Interaction logic for NieuweMail.xaml
    /// </summary>
    public partial class NieuweMail : Window
    {
        private Gebruiker gebruiker;
        public NieuweMail(Gebruiker g)
        {
            InitializeComponent();
            gebruiker = g;
            tbOnderwerp.Text = "";
            tbOntvangers.Text = "";
            tbOntvangersCc.Text = "";
        }
        public NieuweMail(Gebruiker gebruiker, string onderwerp, string body, string ontvanger, string alleOntvangers = "", string cc = "") : this(gebruiker)
        {
            tbOntvangers.Text = ontvanger + "; " + alleOntvangers.Replace(gebruiker.email + ";", "");
            tbOntvangersCc.Text = cc.Replace(gebruiker.email + ";", "");
            tbOnderwerp.Text = onderwerp;
            tbBerichtBody.Text = body;
        }

        public NieuweMail(Gebruiker gebruiker, string onderwerp, string tekstBody) : this(gebruiker)
        {
            tbOnderwerp.Text = onderwerp;
            tbBerichtBody.Text = tekstBody;
        }

        private void btnVerzenden_Click(object sender, RoutedEventArgs e)
        {
            Bericht b = new Bericht
            {
                berichtTekst = tbBerichtBody.Text,
                bijlage = "",
                datumVerstuurd = DateTime.Now,
                verzenderId = gebruiker.id,
                onderwerp = tbOnderwerp.Text
            };

            if (!b.IsGeldig())
            {
                MessageBox.Show(b.Error);
                return;
            }

            List<string> emailAdressen = (tbOntvangers.Text + ";" + tbOntvangersCc.Text).Split(';').Where(s => !string.IsNullOrWhiteSpace(s)).Select(x => x.Trim()).ToList();
            string fouteEmailAdressen = "";

            foreach (string email in emailAdressen)
            {
                if (DatabaseOperations.GetGebruikerId(email) == -1)
                    fouteEmailAdressen += email + Environment.NewLine;
            }

            if (!string.IsNullOrWhiteSpace(fouteEmailAdressen))
            {
                MessageBox.Show("Volgende email adressen bestaan niet:\n" + fouteEmailAdressen);
                return;
            }

            if (emailAdressen.Count == 0)
            {
                MessageBox.Show("vul een ontvanger in!");
                return;
            }

            if (!DatabaseOperations.InsertBericht(b))
            {
                MessageBox.Show("Bericht kon niet verzonden worden");
            }

            DatabaseOperations.InsertBerichtOntvangers(tbOntvangers.Text, tbOntvangersCc.Text);

            this.Close();
        }
    }
}
