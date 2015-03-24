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
            Potrawy recepie = new Potrawy();
            recepie.NazwaPotrawy = NameTextBox.Text;
            recepie.Skladniki = IngTextBox.Text;

            using (var dbCtx = new KitchenEntities())
            {
                dbCtx.Entry(recepie).State = System.Data.EntityState.Added;
                dbCtx.SaveChanges();
            }
            this.Close();
        }
    }
}
