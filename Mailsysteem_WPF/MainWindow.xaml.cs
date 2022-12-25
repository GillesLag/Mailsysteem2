using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Mailsysteem_DAL;

namespace Mailsysteem_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Bericht> MailItemsOntvangen = new ObservableCollection<Bericht>();
        private ObservableCollection<Bericht> MailItemsVerzonden = new ObservableCollection<Bericht>();
        private List<Gebruiker> GebruikerList;
        private Gebruiker gebruiker;
        private readonly int keuzeGebruiker = 1;
        public MainWindow()
        {
            InitializeComponent();

            GebruikerList = DatabaseOperations.OphalenGebruikers();
            gebruiker = GebruikerList[keuzeGebruiker];
            OphalenOntvangenBerichten();
            OphalenVerzondenBerichten();
            lbMailItems.DataContext = MailItemsOntvangen;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            ListBoxItem item = GetAncestorOfType<ListBoxItem>(sender as Button);

            item.IsSelected = true;
            if (lbMailItems.SelectedItem is Bericht bericht)
            {
                MessageBox.Show("test");
            }

        }

        private void lbMailItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Bericht b = lbMailItems.SelectedItem as Bericht;
            string ontvangers = "";
            string cc = "";
            DatabaseOperations.OphalenOntvangers(b.id).ForEach(x => ontvangers += x.email + "; ");
            DatabaseOperations.OphalenOntvangersCC(b.id).ForEach(x => cc += x.email + "; ");

            lblOntvangers.Content = ontvangers;
            lblCcOntvangers.Content = cc;
            lblOnderwerpTekst.Content = b.onderwerp;
            tbBerichtBody.Text = b.berichtTekst;

            if (b.verzenderId != null)
            {
                lblGebruiker.Content = DatabaseOperations.OphalenVerzender(b.verzenderId.Value);
            }

            else
            {
                lblGebruiker.Content = "Gebuiker bestaat niet meer";
            }
        }

        private void btnVezondenItems_Click(object sender, RoutedEventArgs e)
        {
            lbMailItems.DataContext = MailItemsVerzonden;
        }

        private void btnInbox_Click(object sender, RoutedEventArgs e)
        {
            lbMailItems.DataContext = MailItemsOntvangen;
        }

        private void btnNieuweMail_Click(object sender, RoutedEventArgs e)
        {
            NieuweMail nieuweMail = new NieuweMail(gebruiker);
            nieuweMail.ShowDialog();

            OphalenOntvangenBerichten();
            OphalenVerzondenBerichten();
        }

        private void btnBeantwoorden_Click(object sender, RoutedEventArgs e)
        {
            Bericht b = lbMailItems.SelectedItem as Bericht;
            string volledigeBody = $"\n\nVan: {lblGebruiker.Content}\n" +
                $"Verzonden: {b.datumVerstuurd}\n" +
                $"Aan: {lblOntvangers.Content}\n" +
                $"CC: {lblCcOntvangers.Content}\n" +
                $"Onderwerp: {lblOnderwerpTekst.Content}\n\n" +
                $"{tbBerichtBody.Text}";

            NieuweMail nieuweMail = new NieuweMail(gebruiker, lblOnderwerpTekst.Content.ToString(), volledigeBody, lblGebruiker.Content.ToString());
            nieuweMail.ShowDialog();

            OphalenOntvangenBerichten();
            OphalenVerzondenBerichten();
        }

        private void btnAllenBeantwoorden_Click(object sender, RoutedEventArgs e)
        {
            Bericht b = lbMailItems.SelectedItem as Bericht;
            string volledigeBody = $"\n\nVan: {lblGebruiker.Content}\n" +
                $"Verzonden: {b.datumVerstuurd}\n" +
                $"Aan: {lblOntvangers.Content}\n" +
                $"CC: {lblCcOntvangers.Content}\n" +
                $"Onderwerp: {lblOnderwerpTekst.Content}\n\n" +
                $"{tbBerichtBody.Text}";

            NieuweMail nieuweMail = new NieuweMail(gebruiker, lblOnderwerpTekst.Content.ToString(), volledigeBody, lblGebruiker.Content.ToString(), lblOntvangers.Content.ToString(), lblCcOntvangers.Content.ToString());
            nieuweMail.ShowDialog();

            OphalenOntvangenBerichten();
            OphalenVerzondenBerichten();
        }

        private void btnDoorsturen_Click(object sender, RoutedEventArgs e)
        {
            Bericht b = lbMailItems.SelectedItem as Bericht;
            string volledigeBody = $"Van: {lblGebruiker.Content}\n" +
                $"Verzonden: {b.datumVerstuurd}\n" +
                $"Aan: {lblOntvangers.Content}\n" +
                $"CC: {lblCcOntvangers.Content}\n" +
                $"Onderwerp: {lblOnderwerpTekst.Content}\n\n" +
                $"{tbBerichtBody.Text}";

            NieuweMail nieuweMail = new NieuweMail(gebruiker, lblOnderwerpTekst.Content.ToString(), volledigeBody);
            nieuweMail.ShowDialog();

            OphalenOntvangenBerichten();
            OphalenVerzondenBerichten();
        }

        private void OphalenOntvangenBerichten()
        {
            DatabaseOperations.OphalenOntvangenBerichten(gebruiker.id).ForEach(x =>
            {
                if (!MailItemsOntvangen.Contains(x))
                {
                    MailItemsOntvangen.Add(x);
                }
            });
        }

        private void OphalenVerzondenBerichten()
        {
            DatabaseOperations.OphalenVerzondenBerichten(gebruiker.id).ForEach(x =>
            {
                x.Gebruiker = gebruiker;
                if (!MailItemsVerzonden.Contains(x))
                    MailItemsVerzonden.Add(x);
            });
        }

        private void btnTaken_Click(object sender, RoutedEventArgs e)
        {
            Taken taken = new Taken(gebruiker);
            taken.Show();
            this.Close();
        }

        private void btnVergaderingen_Click(object sender, RoutedEventArgs e)
        {
            Vergaderingen vergaderingen = new Vergaderingen(gebruiker);
            vergaderingen.Show();
            this.Close();
        }

        private T GetAncestorOfType<T>(FrameworkElement child) where T : FrameworkElement
        {
            var parent = VisualTreeHelper.GetParent(child);
            if (parent != null && !(parent is T))
                return (T)GetAncestorOfType<T>((FrameworkElement)parent);
            return (T)parent;
        }
    }
}
