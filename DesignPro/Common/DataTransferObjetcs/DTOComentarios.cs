//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Common.DataTransferObjects
{
    using System;
    using System.Collections.Generic;
    
    public partial class DTOComentarios
    {
        public int ID { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Cuerpo { get; set; }
        public string Usuario { get; set; }
        public string Proyecto { get; set; }
    
        public  DTOProyecto Proyecto1 { get; set; }
        public  DTOUsuarios Usuarios { get; set; }
    }
}
