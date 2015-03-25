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
        private DataGrid result;

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
            if (ingridients.Count() > 0)
            {
                using (var dataEntities = new KitchenEntities())
                {
                    var query =
                        from ingr in dataEntities.Skladnikis
                        where ingridients.Contains(ingr.Skladnik)
                        select new { ingr.IdPotrawy };

                    List<int> iList = new List<int>();
                    foreach (var row in query)
                    {
                        iList.Add((int)row.IdPotrawy);
                    }

                    var nextQuery =
                        from recipe in dataEntities.Potrawies
                        where iList.Contains(recipe.IdPotrawy)
                        select new { recipe.IdPotrawy, recipe.NazwaPotrawy, recipe.Skladniki, recipe.Przygotowanie };
                    result = new DataGrid();
                    result.ItemsSource = nextQuery.ToList();
                }
            }
        }

        public DataGrid getSearchResult()
        {
            return result;
        }
    }
}
