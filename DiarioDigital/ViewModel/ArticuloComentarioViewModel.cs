using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiarioDigital.ViewModel
{
    public class ArticuloComentarioViewModel
    {

        public Articulo Articulo { get; set; }
        public Comentarios comentarios { get; set; }
        public Usuarios Usuarios { get; set; }


        //Llaves foraneas de la tabla comentarios
        public Nullable<int> postID { get; set; }
        public Nullable<int> userID { get; set; }
        public Nullable<int> comentSubID { get; set; }


        // Llaves foraneas de la tabla Subcomentarios

        public Nullable<int> Usuario_id { get; set; }

    }
}