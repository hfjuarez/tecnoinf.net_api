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
    
    public partial class Mensajes
    {
        public int ID { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Cuerpo { get; set; }
        public short Visto { get; set; }
        public string Emisor { get; set; }
        public string Remitente { get; set; }
    
        public virtual Usuarios Usuarios { get; set; }
        public virtual Usuarios Usuarios1 { get; set; }
    }
}
