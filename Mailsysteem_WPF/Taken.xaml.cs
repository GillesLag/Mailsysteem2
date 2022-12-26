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
using System.Windows.Shapes;
using Mailsysteem_DAL;

namespace Mailsysteem_WPF
{
    /// <summary>
    /// Interaction logic for Taken.xaml
    /// </summary>
    public partial class Taken : Window
    {
        private TaakRepo taakRepo = new TaakRepo();
        private Gebruiker gebruiker;
        private ObservableCollection<Taak> taken = new ObservableCollection<Taak>();
        private ObservableCollection<Taak> takenKlaar = new ObservableCollection<Taak>();

        public Taken(Gebruiker g)
        {
            InitializeComponent();

            gebruiker = g;
            OphalenTaken();
        }

        private void btnKlaar_Click(object sender, RoutedEventArgs e)
        {
            ListBoxItem item = GetAncestorOfType<ListBoxItem>(sender as Button);

            item.IsSelected = true;
            if (lbTaakItems.SelectedItem is Taak taak)
            {
                taak.isVoltooid = true;

                if (!taakRepo.UpdateTaak(taak))
                {
                    MessageBox.Show("Er is iets mis met de database!");
                    return;
                }

                taken.Remove(taak);
                if (!takenKlaar.Contains(taak))
                    takenKlaar.Add(taak);
            }
        }

        private void btnTaken_Click(object sender, RoutedEventArgs e)
        {
            lbTaakItems.DataContext = taken;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            ListBoxItem item = GetAncestorOfType<ListBoxItem>(sender as Button);

            item.IsSelected = true;
            if (lbTaakItems.SelectedItem is Taak taak)
            {
                taken.Remove(taak);
                takenKlaar.Remove(taak);
                taakRepo.DeleteTaak(taak);
            }
        }

        private void btnNieuweTaak_Click(object sender, RoutedEventArgs e)
        {
            NieuweTaak taak = new NieuweTaak(gebruiker);
            taak.ShowDialog();

            OphalenTaken();
        }

        private void lbMailItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbTaakItems.SelectedItem is Taak taak)
            {
                lblTaakNaam.Content = taak.naam;
                lblEindDatumTekst.Content = taak.eindDatum;
                tbTaakBody.Text = taak.extraInfo;

                string categorieën = "";

                foreach (TaakCategorie tc in taak.TaakCategorie)
                {
                    categorieën += tc.Categorie.naam + ", ";
                }

                categorieën = categorieën.Trim().Trim(',');
                lblCategorieën.Content = categorieën;
            }
        }

        private void btnMails_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void btnAfgerond_Click(object sender, RoutedEventArgs e)
        {
            lbTaakItems.DataContext = takenKlaar;
        }

        private void OphalenTaken()
        {
            taken.Clear();
            takenKlaar.Clear();
            taakRepo.OphalenTaken(gebruiker.id).ForEach(x =>
            {
                if (!x.isVoltooid)
                    taken.Add(x);

                else
                    takenKlaar.Add(x);
            });

            lbTaakItems.DataContext = taken;
        }

        private T GetAncestorOfType<T>(FrameworkElement child) where T : FrameworkElement
        {
            var parent = VisualTreeHelper.GetParent(child);
            if (parent != null && !(parent is T))
                return (T)GetAncestorOfType<T>((FrameworkElement)parent);
            return (T)parent;
        }

        private void btnVergaderingen_Click(object sender, RoutedEventArgs e)
        {
            Vergaderingen vergaderingen = new Vergaderingen(gebruiker);
            vergaderingen.Show();
            this.Close();
        }
    }
}
