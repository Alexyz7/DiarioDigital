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
    using System.Linq;

    public partial class Usuarios
    {
        private DiarioOnlineEntities db = new DiarioOnlineEntities();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuarios()
        {
            this.Comentarios = new HashSet<Comentarios>();
            this.Subcomentarios = new HashSet<Subcomentarios>();
        }
    
        public int IdUser { get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }
        public string Nombre { get; set; }

        public bool autenticar()
        {
            return db.Usuarios.Where(x => x.Email == this.Email && x.Contraseña == this.Contraseña).FirstOrDefault() != null;

            
        }

    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comentarios> Comentarios { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Subcomentarios> Subcomentarios { get; set; }
    }
}
