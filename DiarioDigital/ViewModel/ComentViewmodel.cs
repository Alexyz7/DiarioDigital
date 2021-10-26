using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DiarioDigital.ViewModel
{
    public class ComentViewmodel
    {
   




        public int Idcoment { get; set; }
        [Required(ErrorMessage = "El comentario no puede ser vacio")]


        [MaxLength(250, ErrorMessage = "El maximo de caracteres es de 250")]
        public string comentario { get; set; }
        public System.DateTime DateComent { get; set; }
        public Nullable<int> postID { get; set; }
        public Nullable<int> userID { get; set; }
        public Nullable<int> comentsubID { get; set; }

        public string autor { get; set; }

        public virtual Articulo Articulo { get; set; }
   
        public Usuarios usuario { get; set; }

    

    }
}