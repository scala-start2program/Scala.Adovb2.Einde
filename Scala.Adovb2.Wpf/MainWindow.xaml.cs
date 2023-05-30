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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Scala.Adovb2.Core;
using Scala.Adovb2.Core.Entities;
using Scala.Adovb2.Core.Services;

namespace Scala.Adovb2.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PersoonService persoonService = new PersoonService();
        bool isNew;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            VulCombos();
            LinksActief();
            VulListbox();
        }

        private void LstPersonen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearControls();
            if(lstPersonen.SelectedItem != null)
            {
                Persoon persoon = (Persoon)lstPersonen.SelectedItem;
                txtNaam.Text = persoon.Naam;
                txtVoornaam.Text = persoon.Voornaam;
                txtAdres.Text = persoon.Adres;
                txtGemeente.Text = persoon.Gemeente;
                dtpGeboortedatum.SelectedDate = persoon.Geboortedatum;
                cmbSoort.SelectedItem = persoon.Soort;
            }
        }

        private void CmbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearControls();
            cmbFilter.SelectedIndex = -1;
            VulListbox();
        }
        private void BtnClearFilter_Click(object sender, RoutedEventArgs e)
        {
            ClearControls();
            VulListbox();
        }
        private void BtnNieuw_Click(object sender, RoutedEventArgs e)
        {
            isNew = true;
            RechtsActief();
            ClearControls();
            txtNaam.Focus();
        }

        private void BtnWijzig_Click(object sender, RoutedEventArgs e)
        {
            if (lstPersonen.SelectedItem != null)
            {
                isNew = false;
                RechtsActief();
                txtNaam.Focus();
            }
        }

        private void BtnVerwijder_Click(object sender, RoutedEventArgs e)
        {
            if(lstPersonen.SelectedItem != null)
            {
                if(MessageBox.Show("Ben je zeker?", "Persoon verwijderen", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Persoon persoon = (Persoon)lstPersonen.SelectedItem;
                    persoonService.PersoonVerwijderen(persoon);
                    ClearControls();
                    VulListbox();
                }
            }
        }

        private void BtnBewaren_Click(object sender, RoutedEventArgs e)
        {
            string naam = txtNaam.Text.Trim();
            string voornaam = txtVoornaam.Text.Trim();
            string adres = txtAdres.Text.Trim();
            string gemeente = txtGemeente.Text.Trim();
            if(naam.Length ==0 )
            {
                MessageBox.Show("Naam invoeren", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                txtNaam.Focus();
                return;
            }
            if (voornaam.Length == 0)
            {
                MessageBox.Show("Voornaam invoeren", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                txtVoornaam.Focus();
                return;
            }
            if(dtpGeboortedatum.SelectedDate == null)
            {
                MessageBox.Show("Geboortedatum invoeren", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                dtpGeboortedatum.Focus();
                return;
            }
            DateTime geboortedatum = (DateTime)dtpGeboortedatum.SelectedDate;
            if(cmbSoort.SelectedIndex == -1)
            {
                MessageBox.Show("Soort opgeven", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                cmbSoort.Focus();
                return;
            }
            string soort = cmbSoort.SelectedItem.ToString();

            Persoon persoon;
            if(isNew)
            {
                persoon = new Persoon(naam, voornaam, adres, gemeente, geboortedatum, soort);
                persoonService.PersoonToevoegen(persoon);
            }
            else
            {
                persoon = (Persoon)lstPersonen.SelectedItem;
                persoon.Naam = naam;
                persoon.Voornaam = voornaam;
                persoon.Adres = adres;
                persoon.Gemeente = gemeente;
                persoon.Geboortedatum = geboortedatum;
                persoon.Soort = soort;
                persoonService.PersoonWijzigen(persoon);
            }
            VulListbox();
            LinksActief();
            lstPersonen.SelectedValue = persoon.Id;
            LstPersonen_SelectionChanged(null, null);
        }

        private void BtnAnnuleren_Click(object sender, RoutedEventArgs e)
        {
            ClearControls();
            LinksActief();
            LstPersonen_SelectionChanged(null, null);
        }
        private void LinksActief()
        {
            grpPersonen.IsEnabled = true;
            grpDetails.IsEnabled = false;
            btnBewaren.Visibility = Visibility.Hidden;
            btnAnnuleren.Visibility = Visibility.Hidden;
        }
        private void RechtsActief()
        {
            grpPersonen.IsEnabled = false;
            grpDetails.IsEnabled = true;
            btnBewaren.Visibility = Visibility.Visible;
            btnAnnuleren.Visibility = Visibility.Visible;
        }
        private void VulListbox()
        {
            if (cmbFilter.SelectedIndex == -1)
                lstPersonen.ItemsSource = persoonService.GetPersonen();
            else
                lstPersonen.ItemsSource = persoonService.GetPersonen(cmbFilter.SelectedItem.ToString());
            lstPersonen.Items.Refresh();
        }
        private void ClearControls()
        {
            txtNaam.Text = "";
            txtVoornaam.Text = "";
            txtAdres.Text = "";
            txtGemeente.Text = "";
            cmbSoort.SelectedIndex = -1;
            dtpGeboortedatum.SelectedDate = null;
        }
        private void VulCombos()
        {
            List<string> soorten = new List<string>();
            soorten.Add("Familie");
            soorten.Add("Vrienden");
            soorten.Add("Anderen");
            cmbFilter.ItemsSource = soorten;
            cmbSoort.ItemsSource = soorten;

        }


    }
}
