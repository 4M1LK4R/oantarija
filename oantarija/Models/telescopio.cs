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
    
    public partial class telescopio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public telescopio()
        {
            this.actividad_solar = new HashSet<actividad_solar>();
        }
    
        public int id { get; set; }
        public string nombre { get; set; }
        public string marca { get; set; }
        public string tipo { get; set; }
        public string diametro { get; set; }
        public string dis_focal { get; set; }
        public string montura { get; set; }
        public bool estado { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<actividad_solar> actividad_solar { get; set; }
    }
}