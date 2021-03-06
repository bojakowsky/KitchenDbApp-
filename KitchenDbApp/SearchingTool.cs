﻿using System;
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
        private List<int> ID = new List<int>();
        private List<int> bufID = new List<int>();
        //private List<string> INGR = new List<string>();
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
                using (var dataEntities = new KitchenEntities1())
                {
                    //var query =
                    //    from ingr in dataEntities.Skladniki
                    //    where ingridients.Contains(ingr.Skladnik)
                    //    select new { ingr.IdPotrawy };

                    //var quer =
                    //    from ingr in dataEntities.Skladniki
                    //    select new { ingr.Skladnik, ingr.IdPotrawy };


                    //bool[,] found = new bool[ingridients.Length, quer.Count()];
                    //for (int i = 0; i < ingridients.Length ; i++) 
                    //{
                    //    for (int j = 0 ; j < quer.Count(); j++)
                    //    {
                    //        found[i, j] = false;
                    //    }
                    //}

                    //int iI = 0;
                    //int iQ = 0;
                    //foreach (var ingrid in ingridients)
                    //{
                    //    foreach (var que in quer)
                    //    {
                    //        if (que.Skladnik.Contains(ingrid))
                    //        {
                    //            ID.Add((int)que.IdPotrawy);
                    //            INGR.Add(que.Skladnik);
                    //            found[iI,iQ] = true;
                    //            continue;
                    //        }
                    //        iQ++;
                    //    }
                    //    iQ = 0;
                    //    iI++;
                    //}

                    foreach (var ingrid in ingridients)
                    {
                        var quer =
                            from ingr in dataEntities.Skladniki
                            where ingr.Skladnik.Contains(ingrid)
                            group ingr by ingr.IdPotrawy into IdPotrawy
                            select new { IDpotrawy = IdPotrawy.Key };
                        try
                        {
                            foreach (var el in quer)
                            {
                                bufID.Add((int)el.IDpotrawy);
                            }
                            
                        }
                        catch(Exception)
                        {
                        }
                        
                    }

                    var q = 
                        from data in dataEntities.Skladniki
                        group data by data.IdPotrawy into g
                        select new { Id = g.Key };

                        
                    foreach(var item in q)
                    {
                        
                        int howMany = bufID.Count<int>(x => x == (int)item.Id);
                        if (howMany == ingridients.Count()) ID.Add((int)item.Id); 
                    }

                    
                    //for (int i = 0; i < quer.Count(); i++)
                    //{
                    //    if (found[0, i] == true)
                    //    {
                    //        for (int j = 0; j < ingridients.Length; j++)
                    //        {
                    //            if (found[j, i] == false)
                    //            {
                    //                try
                    //                {
                    //                    int f = INGR.FindIndex(x => x == ingridients[j]);
                    //                    ID.RemoveAt(f);
                    //                    break;
                    //                }
                    //                catch (Exception) {/*...*/ }
                    //            }


                    //        }
                    //    }
                    //}

                    //var query = dataEntities.Skladniki
                    //    .GroupBy(x => x.IdPotrawy)
                    //    .Select(x => x.FirstOrDefault());

                    var query =
                        from ingr in dataEntities.Skladniki
                        where ingr.IdPotrawy == ID.FirstOrDefault(x=>x==ingr.IdPotrawy)
                        select new { ingr.IdPotrawy };

                    List<int> iList = new List<int>();
                    foreach (var row in query)
                    {
                        iList.Add((int)row.IdPotrawy);
                        //int counter = 0;
                        //foreach (var r in query)
                        //{
                        //    if (r.IdPotrawy == row.IdPotrawy) counter++;
                        //}
                        //if (counter == ingridients.Count()) 
                    }



                    var nextQuery =
                        from recipe in dataEntities.Potrawy
                        where iList.Contains(recipe.IdPotrawy)
                        where recipe.Skladniki != "NULL"
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
