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

        public int postID { get; set; }
        public int userID { get; set; }

    }
}