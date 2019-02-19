using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchFight.Entity
{
    public class GenericEntity
    {
        //Key para poder usar el servicio
        public string key { get; set; }

        //Url basica a la cual se le agregara los demás parametros.
        public string basicUrl { get; set; }

        //Url que contiene todo lo necesario para el api search engine.
        public string fullUrl { get; set; }

        //Configuración
        public string customConfig { get; set; }

        //Parametro que contiene la palabra(s) a buscar.
        public string q { get; set; }

        //Nombre del motor de busqueda
        public string name { get; set; }

        //
        public int resultado { get; set; }
    }
}
