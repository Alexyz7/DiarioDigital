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
    
    public partial class Articulo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Articulo()
        {
            this.Comentarios = new HashSet<Comentarios>();
        }
    
        public int IdArticulo { get; set; }
        public string Titulo { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public string Contenido { get; set; }
        public int categoriaID { get; set; }
        public byte[] Vista_previa { get; set; }
    
        public virtual Categoria Categoria { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comentarios> Comentarios { get; set; }
    }
}