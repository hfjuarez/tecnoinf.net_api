//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BussinesLogic.Models
{
    using System;
    using System.Collections.Generic;
    
    public class Proyecto
    {
        
        public Proyecto()
        {
            this.Comentarios = new HashSet<Comentarios>();
            this.Etiquetas = new HashSet<Etiquetas>();
            this.Herramientas = new HashSet<Herramientas>();
            this.Portfolio = new HashSet<Portfolio>();
            this.Proyecto_categorias = new HashSet<Proyecto_categorias>();
            this.Usuarios1 = new HashSet<Usuarios>();
        }
    
        private string Titulo { get; set; }
        private int Vistas { get; set; }
        private System.DateTime Fecha_publicada { get; set; }
        private string Autor { get; set; }
        private int ID_Portfolio { get; set; }
    
        private ICollection<Comentarios> Comentarios { get; set; }
        private ICollection<Etiquetas> Etiquetas { get; set; }
        private ICollection<Herramientas> Herramientas { get; set; }
        private ICollection<Portfolio> Portfolio { get; set; }
        private Portfolio Portfolio1 { get; set; }
        private ICollection<Proyecto_categorias> Proyecto_categorias { get; set; }
        private Usuarios Usuarios { get; set; }
        private ICollection<Usuarios> Usuarios1 { get; set; }
    }
}