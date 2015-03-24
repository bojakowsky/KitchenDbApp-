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

namespace KitchenDbApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        KitchenEntities dataEntities = new KitchenEntities();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //ObjectQuery<Potrawy> recepies = dataEntities.Potrawy;

            var query =
            from recepie in dataEntities.Potrawies
            select new { recepie.IdPotrawy, recepie.NazwaPotrawy, recepie.Skladniki };
            
            DataGrid.ItemsSource = query.ToList();
        }

        private void AddRecepie_Click(object sender, RoutedEventArgs e)
        {
            var newWindow = new AddWindow();
            //newWindow.Show();
            newWindow.ShowDialog();
            Window_Loaded(sender, e);
        }
    }
}
