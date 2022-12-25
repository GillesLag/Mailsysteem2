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
    /// Interaction logic for CategorieWindow.xaml
    /// </summary>
    public partial class CategorieWindow : Window
    {
        public string keuze;
        private List<Categorie> categorieën;
        public CategorieWindow()
        {
            InitializeComponent();
            categorieën = DatabaseOperations.OphalenCategorieën();
            lbCategorieën.ItemsSource = categorieën;
        }

        private void btnToevoegen_Click(object sender, RoutedEventArgs e)
        {
            if (lbCategorieën.SelectedItem is Categorie categorie)
            {
                keuze = categorie.naam;
                this.Close();
            }

            else
                MessageBox.Show("Selecteer een categorie");
        }
    }
}
