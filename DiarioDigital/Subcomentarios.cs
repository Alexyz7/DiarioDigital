//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DiarioDigital
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Subcomentarios
    {
        public int IdSubcoment { get; set; }

        [Required(ErrorMessage = "El comentario no puede ser vacio")]
        public string subComentario { get; set; }
        public System.DateTime fechaSub { get; set; }
        public Nullable<int> ComID { get; set; }
        public Nullable<int> Usuario_id { get; set; }
    
        public virtual Comentarios Comentarios { get; set; }
        public virtual Usuarios Usuarios { get; set; }
    }
}
