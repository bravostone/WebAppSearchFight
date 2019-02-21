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
        public string Search(string text)
        {
            var words = validateString(text);
            var result = evaluateSearch(words);

            return result;
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

        /// <summary>
        /// Método que valida el valor de la caja de texto
        /// </summary>
        /// <param name="text">texto</param>
        /// <returns>retorno una lista con las palabras a buscar en el motoro de busqueda.</returns>
        public List<string> validateString (string text)
        {
            List<string> result = new List<string>();

            var lenght = text.Length;

            var start = text.Substring(0, 1);
            var end   = text.Substring(lenght-1, 1);

            if(start == "\"" && end == "\"")
            {
                result.Add(text.Replace( "\""," "));
            }
            else
            {
                result = text.Split(new[] { " " },StringSplitOptions.RemoveEmptyEntries).ToList();
            }

            return result;
        }

        /// <summary>
        /// Método que evaluar el resultado de la busqueda
        /// </summary>
        /// <param name="words">words</param>
        /// <returns>Devuelvo la cadena a mostrar</returns>
        public string evaluateSearch(List<string> words)
        {
            SearchEngine searchEngine = new SearchEngine();
            List<GenericEntity> FinalList = new List<GenericEntity>();

            string finalSentence = string.Empty;
            string sentenceA = string.Empty;
            string sentenceB = string.Empty;

            foreach (var word in words)
            {
                List<GenericEntity> listSearchEngine = new List<GenericEntity>();
                listSearchEngine.Add(addList("Google", word));
                listSearchEngine.Add(addList("Bing", word));

                finalSentence = string.Format("{0}: ", word);

                foreach (var item in listSearchEngine)
                {
                    searchEngine.SearchCall(item);
                    finalSentence = finalSentence + string.Format("{0}: {1} ", item.name, item.resultado);

                    FinalList.Add(new GenericEntity() { name = item.name, resultado = item.resultado, q = item.q });
                }

                sentenceA = sentenceA + finalSentence;
                sentenceA = sentenceA + "<br />";

                finalSentence = string.Empty;

                sentenceB = sentenceB + string.Format("{0}", listSearchEngine.First(x => x.resultado == listSearchEngine.Max(y => y.resultado)).name) + " winner: " +
                                       string.Format("{0} ", listSearchEngine.First(x => x.resultado == listSearchEngine.Max(y => y.resultado)).q);
                sentenceB = sentenceB + "<br />";
            }

            finalSentence = sentenceA + sentenceB;

            return finalSentence = finalSentence + string.Format("Total winner: " + "{0}", FinalList.First(x => x.resultado == FinalList.Max(y => y.resultado)).q);
        }

    }
}