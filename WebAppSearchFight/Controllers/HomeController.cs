using System.Web.Mvc;
using SearchFight.Logic;
using System.Collections.Generic;
using SearchFight.Entity;
using System.Linq;
using System;

namespace WebAppSearchFight.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /// <summary>
        /// Método de busqueda
        /// </summary>
        /// <param name="word">palabra a buscar</param>
        /// <returns>Total de coincidencias y ganadores</returns>
        [HttpPost]
        public string Search(string chain)
        {
            SearchEngine searchEngine = new SearchEngine();
            
            string pintar  = string.Empty;
            string pintarA = string.Empty;
            string pintarB = string.Empty;

            string[] words = chain.Split(' ');

            foreach (var word in words)
            {
                List<GenericEntity> listSearchEngine = new List<GenericEntity>();
                listSearchEngine.Add(addList("Google", word));
                listSearchEngine.Add(addList("Bing", word));

                pintar = string.Format("{0}: ", word);

                foreach (var item in listSearchEngine)
                {
                    searchEngine.SearchCall(item);
                    pintar = pintar + string.Format("{0}: {1} ", item.name, item.resultado);
                }

                pintarA = pintarA + pintar;
                pintarA = pintarA + "<br />";

                pintar = string.Empty;

                pintarB =   pintarB +  string.Format("{0}", listSearchEngine.First(x => x.resultado == listSearchEngine.Max(y => y.resultado)).name) + " winner: " +
                                       string.Format("{0} ", listSearchEngine.First(x => x.resultado == listSearchEngine.Max(y => y.resultado)).q);
                pintarB =   pintarB + "<br />";
            }

            pintar = pintarA + pintarB;

            //pintar = pintar + string.Format("{0}", listSearchEngine.First(x => x.resultado == listSearchEngine.Max(y => y.resultado)).name) + " winner: " +
            //                  string.Format("{0}", listSearchEngine.First(x => x.resultado == listSearchEngine.Max(y => y.resultado)).q);

            return pintar;
        }

        /// <summary>
        /// Método que ayuda a añadir motores de busqueda
        /// </summary>
        /// <param name="name">nombre del motor de busqueda</param>
        /// <param name="word">palabra a buscar</param>
        /// <returns>retorna el tipo  que se añadira a la lista</returns>
        public GenericEntity addList(string name, string word)
        {
            GenericEntity genericEntity = new GenericEntity();

            genericEntity.name = name;
            genericEntity.q = word;

            return genericEntity;
        }

    }
}