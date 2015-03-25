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
            //ObjectQuery<Potrawy> recipes = dataEntities.Potrawy;

            var query =
            from recipe in dataEntities.Potrawies
            select new { recipe.IdPotrawy, recipe.NazwaPotrawy, recipe.Skladniki, recipe.Przygotowanie };
            DataGrid.ItemsSource = query.ToList();

            /*KitchenDbApp.KitchenDataSet kitchenDataSet = ((KitchenDbApp.KitchenDataSet)(this.FindResource("kitchenDataSet")));
            // Load data into the table Potrawy. You can modify this code as needed.
            KitchenDbApp.KitchenDataSetTableAdapters.PotrawyTableAdapter kitchenDataSetPotrawyTableAdapter = new KitchenDbApp.KitchenDataSetTableAdapters.PotrawyTableAdapter();
            kitchenDataSetPotrawyTableAdapter.Fill(kitchenDataSet.Potrawy);
            System.Windows.Data.CollectionViewSource potrawyViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("potrawyViewSource")));
            potrawyViewSource.View.MoveCurrentToFirst();*/
        }

        private void AddRecipe_Click(object sender, RoutedEventArgs e)
        {
            var newWindow = new AddWindow();
            //newWindow.Show();
            newWindow.ShowDialog();
            Window_Loaded(sender, e);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Potrawy recipeRow = new Potrawy();
                int dataGridSelection = DataGrid.SelectedIndex + 1;
                recipeRow.IdPotrawy = dataGridSelection;
                var original = dataEntities.Potrawies.Find(recipeRow.IdPotrawy);

                string msgtext = "Do you really want to delete that row?";
                string txt = "Deleting row";
                MessageBoxButton button = MessageBoxButton.YesNoCancel;
                MessageBoxResult result = MessageBox.Show(msgtext, txt, button);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                            original.NazwaPotrawy = "NULL";
                            original.Skladniki = "NULL";
                            original.Przygotowanie = "NULL";
                            using (var dbCtx = new KitchenEntities())
                            {
                                dbCtx.Entry(original).State = System.Data.EntityState.Modified;
                                dbCtx.SaveChanges();
                            }
                            Window_Loaded(sender, e);
                        break;
                    case MessageBoxResult.No:
                        break;
                    case MessageBoxResult.Cancel:
                        break;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                return;
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Potrawy recipeRow = new Potrawy();
                recipeRow.IdPotrawy = ++(DataGrid.SelectedIndex);
                var original = dataEntities.Potrawies.Find(recipeRow.IdPotrawy);
                var newWindow = new EditWindow(original);
                newWindow.ShowDialog();
                Window_Loaded(sender, e);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                return;
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {

        }

        
    }
}
