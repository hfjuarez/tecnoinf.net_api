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
    
    public partial class DTOProyecto
    {
        
        public DTOProyecto()
        {
            this.Comentarios = new HashSet<DTOComentarios>();
            this.Etiquetas = new HashSet<DTOEtiquetas>();
            this.Herramientas = new HashSet<DTOHerramientas>();
            this.Portfolio = new HashSet<DTOPortfolio>();
            this.Proyecto_categorias = new HashSet<DTOProyecto_categorias>();
            this.Usuarios1 = new HashSet<DTOUsuarios>();
        }
    
        public string Titulo { get; set; }
        public int Vistas { get; set; }
        public System.DateTime Fecha_publicada { get; set; }
        public string Autor { get; set; }
        public Nullable<int> ID_Portfolio { get; set; }
        public int Portada { get; set; }

        public  ICollection<DTOComentarios> Comentarios { get; set; }
        public  ICollection<DTOEtiquetas> Etiquetas { get; set; }
        public  ICollection<DTOHerramientas> Herramientas { get; set; }
        public  ICollection<DTOPortfolio> Portfolio { get; set; }
        public  DTOPortfolio Portfolio1 { get; set; }
        public  ICollection<DTOProyecto_categorias> Proyecto_categorias { get; set; }
        public  DTOUsuarios Usuarios { get; set; }
        public  ICollection<DTOUsuarios> Usuarios1 { get; set; }
    }
}
