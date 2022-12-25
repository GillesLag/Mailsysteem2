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
using System.Collections.ObjectModel;

namespace Mailsysteem_WPF
{
    /// <summary>
    /// Interaction logic for Vergaderingen.xaml
    /// </summary>
    public partial class Vergaderingen : Window
    {
        private Gebruiker gebruiker;
        private ObservableCollection<Vergadering> vergaderingen = new ObservableCollection<Vergadering>();
        public Vergaderingen(Gebruiker g)
        {
            InitializeComponent();
            gebruiker = g;
            OphalenVergaderingen();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            ListBoxItem item = GetAncestorOfType<ListBoxItem>(sender as Button);

            item.IsSelected = true;
            if (lbVergaderingItems.SelectedItem is Vergadering vergadering)
            {
                //TODO delete vergadering nog implementeren
                MessageBox.Show("nog implementeren");
                //vergaderingen.Remove(vergadering);
               // DatabaseOperations.DeleteVergadering(vergadering);
            }
        }

        private void btnMails_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void btnNieuweVergadering_Click(object sender, RoutedEventArgs e)
        {
            NieuweVergadering nv = new NieuweVergadering(gebruiker);
            nv.ShowDialog();

            OphalenVergaderingen();
        }

        private void btnInboxVergaderingen_Click(object sender, RoutedEventArgs e)
        {
            lbVergaderingItems.DataContext = vergaderingen;
        }

        private void lbVergaderingItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbVergaderingItems.SelectedItem is Vergadering vergadering)
            {
                lblTitelTekst.Content = vergadering.onderwerp;
                lblDatumTekst.Content = vergadering.KorteDatum;
                lblBeginTijdTekst.Content = vergadering.beginTijd;
                lblEindTijdTekst.Content = vergadering.eindTijd;
                lblLocatieTekst.Content = vergadering.plaats;
                lblDeelnemersTekst.Content = "";

                foreach (VergaderingGenodigde vg in vergadering.VergaderingGenodigde)
                {
                    lblDeelnemersTekst.Content += vg.Gebruiker.email + "; ";
                }
            }
        }

        private void btnTaken_Click(object sender, RoutedEventArgs e)
        {
            Taken taken = new Taken(gebruiker);
            taken.Show();
            this.Close();
        }

        private T GetAncestorOfType<T>(FrameworkElement child) where T : FrameworkElement
        {
            var parent = VisualTreeHelper.GetParent(child);
            if (parent != null && !(parent is T))
                return (T)GetAncestorOfType<T>((FrameworkElement)parent);
            return (T)parent;
        }

        private void OphalenVergaderingen()
        {
            DatabaseOperations.OphalenVergaderingen(gebruiker.id).ForEach(x =>
            {
                if (!vergaderingen.Contains(x))
                    vergaderingen.Add(x);
            });

            lbVergaderingItems.DataContext = vergaderingen;
        }
    }
}
