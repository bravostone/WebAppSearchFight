using Newtonsoft.Json;
using SearchFight.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SearchFight.Logic
{
    public class SearchEngine
    {
        /// <summary>
        /// Método que arma la consulta para que enviar al servicio según el motor de busqueda
        /// </summary>
        /// <param name="data">Contiene la data a buscar y el motor a utilizar.</param>
        public void SearchCall(GenericEntity data)
        {
            GenericEntity genericEntity = new GenericEntity();

            try
            {
                if (data.name == "Google")
                {
                    genericEntity.basicUrl     = "https://www.googleapis.com/customsearch/v1?key=";
                    genericEntity.customConfig = "001242270951922749449:jwoekadisuw";
                    genericEntity.key          = "AIzaSyBomoxFfbe73-y7xsbascQjJiC_ObF0ze4";
                    genericEntity.q            = data.q;
                    genericEntity.name         = data.name;

                    var url = genericEntity.basicUrl + genericEntity.key + "&cx=" + genericEntity.customConfig + "&q=" + genericEntity.q;

                    genericEntity.fullUrl = url;

                    data.resultado = Convert.ToInt32(callApi(genericEntity));
                }

                if (data.name == "Bing")
                {
                    genericEntity.basicUrl      = "https://api.cognitive.microsoft.com/bingcustomsearch/v7.0/search?q=";
                    genericEntity.customConfig  = "14fa5e0d-5848-4f3e-b4ab-e59bcab6b551";
                    genericEntity.key           = "5c68e3ef607c46a0a267e37dd967c14c";
                    genericEntity.q             = data.q;
                    genericEntity.name          = data.name;

                    var url = genericEntity.basicUrl + genericEntity.q + "&customconfig=" + genericEntity.customConfig + "&mkt=en-US";

                    genericEntity.fullUrl = url;

                    data.resultado = Convert.ToInt32(callApi(genericEntity));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Método que llama al servicio
        /// </summary>
        /// <param name="datos">parametro que contiene los datos que necesita el servicio</param>
        /// <returns>retorna el total de coincidencias</returns>
        public string callApi(GenericEntity datos)
        {
            var matches = string.Empty;

            try
            {
                WebRequest request = HttpWebRequest.Create(datos.fullUrl);
                if (datos.name == "Bing")
                {
                    request.Headers["Ocp-Apim-Subscription-Key"] = datos.key;
                }
                HttpWebResponse response = (HttpWebResponse)request.GetResponseAsync().Result;
                string json = new StreamReader(response.GetResponseStream()).ReadToEnd();

                if (datos.name == "Bing")
                {
                    BingEntity.BingObject bingJson = JsonConvert.DeserializeObject<BingEntity.BingObject>(json);
                    matches = bingJson.webPages.totalEstimatedMatches.ToString();
                }
                else
                {
                    GoogleEntity.GoogleObject googleJson = JsonConvert.DeserializeObject<GoogleEntity.GoogleObject>(json);
                    matches = googleJson.queries.request[0].totalResults;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return matches;
        }
    }
}