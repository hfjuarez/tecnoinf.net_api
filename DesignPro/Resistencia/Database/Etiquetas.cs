//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Resistencia.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class Etiquetas
    {
        public string Titulo_proyecto { get; set; }
        public string Etiquetas1 { get; set; }
        public int ID { get; set; }
    
        public virtual Proyecto Proyecto { get; set; }
    }
}
