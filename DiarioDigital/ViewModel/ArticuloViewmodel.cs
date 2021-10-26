using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DiarioDigital.ViewModel
{
    public class ArticuloViewmodel
    {


        public int IdArticulo { get; set; }
        public string Titulo { get; set; }
        public System.DateTime Fecha { get; set; }
        [AllowHtml]
        public string Contenido { get; set; }
        public int categoriaID { get; set; }
        public byte[] Vista_previa { get; set; }

        public string categoriaNombre { get; set; }

        public IEnumerable<Articulo> articulos { get; set; }
   
        public Comentarios comentario { get; set; }
 
   

    }
}