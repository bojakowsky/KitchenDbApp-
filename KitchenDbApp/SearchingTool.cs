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
            findRecipes();
        }

        private void tokenizeIngridients()
        {
            //ingridients = initial.Split('-');
            ingridients = Regex.Split(initial, "\r\n");
        }

        private void findRecipes()
        {
            //KitchenEntities dataEntities = new KitchenEntities();
            foreach ( string s in ingridients)
            {

            /*var query =
                from recipe in dataEntities.Potrawies
                where ingridients = Regex.Split(initial, "\r\n")
                select new { recipe.IdPotrawy };
            DataGrid data = new DataGrid()
            data.ItemsSource = query.ToList();
            */
            }
            
                
        }

    }
}
