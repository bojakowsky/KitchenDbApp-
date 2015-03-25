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

        public EditWindow()
        {
            InitializeComponent();
        }

        public EditWindow(Potrawy p)
        {
            InitializeComponent();
            this.p = p;
            NameTextBox.Text = p.NazwaPotrawy;
            IngTextBox.Text = p.Skladniki;
            PrepTextBox.Text = p.Przygotowanie;
            IdLabel.Content = "" + p.IdPotrawy;
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {

            p.NazwaPotrawy = NameTextBox.Text;
            p.Skladniki = IngTextBox.Text;
            p.Przygotowanie = PrepTextBox.Text;
            using (var dbCtx = new KitchenEntities())
            {
                dbCtx.Entry(p).State = System.Data.EntityState.Modified;
                dbCtx.SaveChanges();
            }
            this.Close();
        }
    }
}
