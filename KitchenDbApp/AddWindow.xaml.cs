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
using System.Data.Objects;
using System.Data;

namespace KitchenDbApp
{
    /// <summary>
    /// Interaction logic for AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        public AddWindow()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Potrawy recipe = new Potrawy();
            recipe.NazwaPotrawy = NameTextBox.Text;
            recipe.Skladniki = IngTextBox.Text;
            recipe.Przygotowanie = PrepTextBox.Text;

            try
            {
                using (var dbCtx = new KitchenEntities())
                {
                    dbCtx.Entry(recipe).State = System.Data.EntityState.Added;
                    dbCtx.SaveChanges();

                    Skladniki ingridient = new Skladniki();
                    string[] ingr = System.Text.RegularExpressions.Regex.Split(recipe.Skladniki, "\r\n");

                    ingridient.IdPotrawy = recipe.IdPotrawy;
                    foreach (string i in ingr)
                    {
                        ingridient.Skladnik = i;
                        dbCtx.Entry(ingridient).State = System.Data.EntityState.Added;
                        dbCtx.SaveChanges();
                    }

                }
                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Added failed");
            }
        }
    }
}
