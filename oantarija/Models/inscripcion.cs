//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace oantarija.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class inscripcion
    {
        public int id { get; set; }
        public int usuario { get; set; }
        public int curso { get; set; }
        public bool estado { get; set; }
    
        public virtual curso curso1 { get; set; }
        public virtual usuario usuario1 { get; set; }
    }
}
