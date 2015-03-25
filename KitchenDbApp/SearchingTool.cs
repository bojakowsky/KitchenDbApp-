using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Objects;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
namespace KitchenDbApp
{
    public class SearchingTool
    {
        private string[] ingridients;
        private string initial;

        public SearchingTool(string x)
        {
            initial = x;
            tokenizeIngridients();
        }

        private void tokenizeIngridients()
        {
            ingridients = initial.Split('-');
        }

        private void findRecipes()
        {
            KitchenEntities dataEntities = new KitchenEntities();
            DataGrid searchResult = new DataGrid();
                var query =
                from recipe in dataEntities.Potrawies
                //where ..
                select new { recipe.IdPotrawy, recipe.NazwaPotrawy, recipe.Skladniki, recipe.Przygotowanie };
            searchResult.ItemsSource = query.ToList();
        }

    }
}
