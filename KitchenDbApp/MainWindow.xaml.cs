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
using System.Data.Entity;
namespace KitchenDbApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public class Val
    {
        public int IdPotrawy { get; set; }
        public string NazwaPotrawy { get; set; }
        public string Skladniki { get; set; }
        public string Przygotowanie { get; set; }

        public Val(int id, string name, string ingrid, string prep)
        {
            IdPotrawy = id;
            NazwaPotrawy = name;
            Skladniki = ingrid;
            Przygotowanie = prep;
        }
    }

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //ObjectQuery<Potrawy> recipes = dataEntities.Potrawy;
            using (var dataEntities = new KitchenEntities1())
            {
                var query =
                from recipe in dataEntities.Potrawy
                where recipe.Skladniki != "NULL"
                select new  { recipe.IdPotrawy, recipe.NazwaPotrawy, recipe.Skladniki, recipe.Przygotowanie };
                DataGrid.ItemsSource = query.ToList();
            }

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

        
        public int GetSelectedId()
        {
            DataGridRow row = (DataGridRow)DataGrid.ItemContainerGenerator.ContainerFromIndex(DataGrid.SelectedIndex);
            DataGridCell RowColumn = DataGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;
            string CellValue = ((TextBlock)RowColumn.Content).Text;
            return Convert.ToInt32(CellValue);    
        }


        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Potrawy recipeRow = new Potrawy();
                //int dataGridSelection = DataGrid.SelectedIndex + 1;
                //recipeRow.IdPotrawy = dataGridSelection;
                using (var dataEntities = new KitchenEntities1())
                {
                    

                    var original = dataEntities.Potrawy.Find(GetSelectedId());
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

                            dataEntities.Entry(original).State = EntityState.Modified;
                            dataEntities.SaveChanges();

                            dataEntities.Database.ExecuteSqlCommand("DELETE FROM Skladniki WHERE IdPotrawy=" + original.IdPotrawy);

                            Window_Loaded(sender, e);
                            break;
                        case MessageBoxResult.No:
                            break;
                        case MessageBoxResult.Cancel:
                            break;
                    }
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
                using (var dataEntities = new KitchenEntities1())
                {
                    var original = dataEntities.Potrawy.Find(GetSelectedId());
                    var newWindow = new EditWindow(original, dataEntities);
                    newWindow.ShowDialog();
                }
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
            SearchingTool search = new SearchingTool(SearchTextRecipe.Text);
            DataGrid.ItemsSource = search.getSearchResult().ItemsSource;
            //
        }

        private void ResetSearchButton_Click(object sender, RoutedEventArgs e)
        {
            Window_Loaded(sender, e);
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("To edit the row double click on it. \n"
                            + "To delete the row right select it and then right click.\n"
                            + "Have a nice day ;) !");
        }

        
    }
}
