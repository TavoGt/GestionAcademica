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
    
    public partial class detalle_asign
    {
        public int id_detalle_asign { get; set; }
        public int id_asign_alumno { get; set; }
        public int id_carrera { get; set; }
        public int id_curso { get; set; }
        public string seccion { get; set; }
    
        public virtual asign_alumno asign_alumno { get; set; }
        public virtual asign_curso asign_curso { get; set; }
    }
}
