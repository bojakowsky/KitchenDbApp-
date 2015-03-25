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

namespace KitchenDbApp
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        private Potrawy p;
        private KitchenEntities dataEntities;
        public EditWindow()
        {
            InitializeComponent();
        }

        public EditWindow(Potrawy p, KitchenEntities dataEntities)
        {
            InitializeComponent();
            this.p = p;
            NameTextBox.Text = p.NazwaPotrawy;
            IngTextBox.Text = p.Skladniki;
            PrepTextBox.Text = p.Przygotowanie;
            IdLabel.Content = "" + p.IdPotrawy;

            this.dataEntities = dataEntities;
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            
            dataEntities.Database.ExecuteSqlCommand("DELETE FROM Skladniki WHERE IdPotrawy=" + p.IdPotrawy);

            p.NazwaPotrawy = NameTextBox.Text;
            p.Skladniki = IngTextBox.Text;
            p.Przygotowanie = PrepTextBox.Text;

            dataEntities.Entry(p).State = System.Data.EntityState.Modified;
            dataEntities.SaveChanges();

            Skladniki ingridient = new Skladniki();
            string[] ingr = System.Text.RegularExpressions.Regex.Split(p.Skladniki, "\r\n");

            ingridient.IdPotrawy = p.IdPotrawy;
            foreach (string i in ingr)
            {
                ingridient.Skladnik = i;
                dataEntities.Entry(ingridient).State = System.Data.EntityState.Added;
                dataEntities.SaveChanges();
            }

            this.Close();
        }
    }
}
