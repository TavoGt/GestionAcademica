//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GestionNotasCunor.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class calificacion
    {
        public int id_actividad { get; set; }
        public int id_alumno { get; set; }
        public Nullable<decimal> calif_obtenida { get; set; }
    
        public virtual actividad actividad { get; set; }
        public virtual alumno alumno { get; set; }
    }
}
