﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.DataModel.Mappers;
using BusinessLogic.DataModel.Repositories;
using Common.DataTransferObjects;
using Resistencia.Database;

namespace BusinessLogic.Controllers
{
    public class ControllerProyecto
    {
        private ProyectoMapper _mapper;
        private CategoriaMapper _mappercategoria;
        private HerramientasMapper _mapperherramientas;
        private EtiquetasMapper _mapperetiquetas;
        public ControllerProyecto()
        {
            _mapper = new ProyectoMapper();
            _mappercategoria = new CategoriaMapper();
            _mapperherramientas = new HerramientasMapper();
            _mapperetiquetas = new EtiquetasMapper();
        }


        public void CrearProyecto(DTOProyecto DTOP)
        {
            try
            {
                using (design_proEntities context = new design_proEntities())
                {
                    ProyectoRepository ProyectoRepositorio = new ProyectoRepository(context);
                    UsuariosRepository usuariosRepositorio = new UsuariosRepository(context); 
                    if (ProyectoRepositorio.Any(DTOP.Titulo))
                    {
                        throw new Exception("El proyecto ya existe.");
                    }
                    ProyectoRepositorio.Create(_mapper.MapFromDTOProyecto(DTOP));
                    context.SaveChanges();
                    Proyecto P = ProyectoRepositorio.get(DTOP.Titulo);
                    List<DTOHerramientas> herramientas = DTOP.Herramientas.ToList();
                    List<DTOEtiquetas> etiquetas = DTOP.Etiquetas.ToList();
                    List<DTOProyecto_categorias> pro = DTOP.Proyecto_categorias.ToList();
                    foreach (var her in herramientas)
                    {
                        P.Herramientas.Add(CrearHerramienta(her.Herramienta, DTOP.Titulo));
                    }
                    foreach (var eti in etiquetas)
                    {
                        P.Etiquetas.Add(CrearEtiqueta(eti.Etiquetas1, DTOP.Titulo));
                    }
                    foreach (var cat in pro)
                    {
                        P.Proyecto_categorias.Add(UnirCategoria(cat.Categoria,DTOP.Titulo));
                    }
                    int ID = CrearPortfolio(P.Titulo);
                    P.ID_Portfolio = ID;
                    P.Portada = CrearVisual(DTOP.P);
                    Usuarios User = usuariosRepositorio.get(DTOP.Autor);
                    User.Proyecto.Add(P);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int CrearVisual(string base64)
        {
            using (design_proEntities context = new design_proEntities())
            {
                Visual v = new Visual();
                VisualRepository visualRepository = new VisualRepository(context);
                v.Path = base64;
                v.Tipo = 0;
                visualRepository.Create(v);
                context.SaveChanges();
                return v.ID;
            }
        }

        public void CrearPaginas(DTOProyecto DTOP)
        {
            using (design_proEntities context = new design_proEntities())
            {
                ProyectoRepository proyectoRepository = new ProyectoRepository(context);
                PortfolioRepository portfolioRepository = new PortfolioRepository(context);
                PagesRepository pagesRepository = new PagesRepository(context);
                VisualRepository visualRepository = new VisualRepository(context);
                DTOProyecto dto = _mapper.MapToDTOProyecto(proyectoRepository.get(DTOP.Titulo));
                int ID = dto.ID_Portfolio ?? default(int);
                Pages page = new Pages();
                Visual v = new Visual();
                Portfolio P = portfolioRepository.get(ID);       
                if(DTOP.Texto == null)
                {
                    v.Path = DTOP.Imagen;
                    page.ID_Visual = v.ID;
                    page.ID_Portfolio = ID;
                    pagesRepository.Create(page);
                    visualRepository.Create(v);
                    context.SaveChanges();
                    P.Pages.Add(page);
                }
                else
                {
                        page.ID_Portfolio = ID;
                        page.Contenido = DTOP.Texto;
                        pagesRepository.Create(page);
                        context.SaveChanges();
                        P.Pages.Add(page);
                }
            }
        }

        public Herramientas CrearHerramienta(string Herram, string titulo)
        {
            try
            {
                using (design_proEntities context = new design_proEntities())
                {
                    int ID;
                    HerramientasRepository HeRepositorio = new HerramientasRepository(context);
                    Herramientas herras = new Herramientas();
                    herras.Titulo_proyecto = titulo;
                    herras.Herramienta = Herram.ToLower();
                    ID = herras.ID;
                    HeRepositorio.Create(herras);
                    context.SaveChanges();
                    return HeRepositorio.get(ID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Etiquetas CrearEtiqueta(string etiq, string titulo)
        {
            try
            {
                using (design_proEntities context = new design_proEntities())
                {
                    int ID;
                    EtiquetasRepository HeRepositorio = new EtiquetasRepository(context);
                    Etiquetas eti = new Etiquetas();
                    eti.Titulo_proyecto = titulo;
                    eti.Etiquetas1 = etiq.ToLower();
                    HeRepositorio.Create(eti);
                    ID = eti.ID;
                    context.SaveChanges();
                    return HeRepositorio.get(ID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Proyecto_categorias UnirCategoria(string cat,string Titulo)
        {
            try
            {
                using (design_proEntities context = new design_proEntities())
                {
                    int ID;
                    Proyecto_categoriasRepository larepo = new Proyecto_categoriasRepository(context);
                    Proyecto_categorias proca = new Proyecto_categorias();
                    proca.Titulo_proyecto = Titulo;
                    proca.Categoria = cat;
                    ID = proca.ID;
                    larepo.Create(proca);
                    context.SaveChanges();
                    return larepo.get(ID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        

        public int CrearPortfolio(string Titulo)
        {
            try
            {
                using (design_proEntities context = new design_proEntities())
                {
                    PortfolioRepository PortfolioRepositorio = new PortfolioRepository(context);
                    Portfolio portfolio = new Portfolio();
                    portfolio.Titulo_Proyecto = Titulo;
                    PortfolioRepositorio.Create(portfolio);
                    context.SaveChanges();
                    return portfolio.ID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BorrarProyecto(string Titulo)
        {
            try
            {
                using (design_proEntities context = new design_proEntities())
                {
                    ProyectoRepository repositorio = new ProyectoRepository(context);
                    repositorio.Delete(Titulo);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOProyecto GetProyect(string Titulo_proyecto)
        {
            using (design_proEntities context = new design_proEntities())
            {
                ProyectoRepository repositorio = new ProyectoRepository(context);
                VisualRepository visualRepository = new VisualRepository(context);
                PortfolioRepository portfolioRepository = new PortfolioRepository(context);
                var entity = repositorio.get(Titulo_proyecto);
                var DTOentity = _mapper.MapToDTOProyecto(entity);
                List<string> paginas = new List<string>();
                List<int> IDS = new List<int>();
                Visual v = visualRepository.get(DTOentity.Portada);
                DTOentity.P = v.Path;
                Portfolio port = portfolioRepository.get(DTOentity.ID_Portfolio ?? default(int));
                if(port != null)
                {
                    foreach (var p in port.Pages)
                    {
                        if (p.ID_Visual == null)
                        {
                            paginas.Add(p.Contenido);
                            IDS.Add(p.ID);
                        }
                        else
                        {
                            Visual visual = visualRepository.get(p.ID_Visual ?? default(int));
                            paginas.Add("Imagen" + visual.Path);
                            IDS.Add(p.ID);
                        }
                    }
                    DTOentity.IDPages = IDS;
                    DTOentity.paginas = paginas;
                }
                return DTOentity;   
            }
        }

        public void BorrarPagina(int ID)
        {
            using (design_proEntities context = new design_proEntities())
            {
                VisualRepository visualRepository = new VisualRepository(context);
                PagesRepository pagesRepository = new PagesRepository(context);
                Pages page = pagesRepository.get(ID);
                if(page.ID_Visual == null)
                {
                    pagesRepository.Delete(ID);
                }
                else
                {
                    visualRepository.Delete(page.ID_Visual ?? default(int));
                    pagesRepository.Delete(ID);
                }
                context.SaveChanges();
            }
        }


        public void EditarPagina(int ID,string cadena,int texto)
        {
            using (design_proEntities context = new design_proEntities())
            {
                VisualRepository visualRepository = new VisualRepository(context);
                PagesRepository pagesRepository = new PagesRepository(context);
                Pages page = pagesRepository.get(ID);
                if (texto==1)
                {
                    if (page.ID_Visual != null)
                    {
                        Visual v = visualRepository.get(page.ID_Visual ?? default(int));
                        visualRepository.Delete(v.ID);
                    }
                    page.Contenido = cadena;
                }
                else
                {
                    if (page.ID_Visual != null)
                    {
                        Visual v = visualRepository.get(page.ID_Visual ?? default(int));
                        v.Path = cadena;
                    }
                    else
                    {
                        page.Contenido = null;
                        page.ID_Visual = CrearVisual(cadena);
                    }
                }
                context.SaveChanges();
            }
        }

        public void SumarVistas(string Titulo_proyecto)
        {
            using (design_proEntities context = new design_proEntities())
            {
                ProyectoRepository repositorio = new ProyectoRepository(context);
                var entity = repositorio.get(Titulo_proyecto);
                entity.Vistas = entity.Vistas+1;
                context.SaveChanges();
            }
        }

        public List<DTOProyecto> GetAllProyects()
        {
            List<DTOProyecto> proyectos = new List<DTOProyecto>();

            using (design_proEntities context = new design_proEntities())
            {
                ProyectoRepository repositorio = new ProyectoRepository(context);
                var entityList = repositorio.GetAll();
                foreach (var entity in entityList)
                {
                    proyectos.Add(_mapper.MapToDTOProyecto(entity));
                }
            }
            return proyectos;
                
        }

        public List<DTOProyecto> FilterByCat(string Nombre_Cat)
        {
            List<DTOProyecto> proyectos_porcat = new List<DTOProyecto>();

            using (design_proEntities context = new design_proEntities())
            {
                ProyectoRepository repositorio = new ProyectoRepository(context);
                Proyecto_categoriasRepository pro_cat = new Proyecto_categoriasRepository(context);
                var pro_cats = pro_cat.GetAll();
                var entityList = repositorio.GetAll();
                foreach (var entity in entityList)
                {
                    foreach (var p_cat in pro_cats)
                    {
                        if(p_cat.Categoria == Nombre_Cat && p_cat.Titulo_proyecto == entity.Titulo)
                        {
                            proyectos_porcat.Add(_mapper.MapToDTOProyecto(entity));
                        }
                    }   
                }
            }
            return proyectos_porcat;            
        }

        public List<DTOProyecto> FilterByLike(string Email)
        {
            List<DTOProyecto> proyectos_porlike = new List<DTOProyecto>();

            using (design_proEntities context = new design_proEntities())
            {
                ProyectoRepository repositorio = new ProyectoRepository(context);
                UsuariosRepository UserRepositorio = new UsuariosRepository(context);
                Usuarios User = UserRepositorio.get(Email);
                List<Proyecto> Proyectos_likeados = User.Proyecto1.ToList();
                var entityList = repositorio.GetAll();
                foreach (var entity in entityList)
                {
                    foreach (var likeado in Proyectos_likeados)
                    {
                        if(likeado.Titulo == entity.Titulo) 
                        {
                            proyectos_porlike.Add(_mapper.MapToDTOProyecto(entity));
                        }
                    }    
                }
            }
            return proyectos_porlike;            
        }

        public List<DTOProyecto> FilterByMio(string Email)
        {
            List<DTOProyecto> proyectos_pormio = new List<DTOProyecto>();

            using (design_proEntities context = new design_proEntities())
            {
                ProyectoRepository repositorio = new ProyectoRepository(context);
                UsuariosRepository UserRepositorio = new UsuariosRepository(context);
                Usuarios User = UserRepositorio.get(Email);
                List<Proyecto> Proyectos_mios = User.Proyecto.ToList();
                var entityList = repositorio.GetAll();
                foreach (var entity in entityList)
                {
                    foreach (var mio in Proyectos_mios)
                    {
                        if(mio.Titulo == entity.Titulo) 
                        {
                            proyectos_pormio.Add(_mapper.MapToDTOProyecto(entity));
                        }
                    }    
                }
            }
            return proyectos_pormio;            
        }

        public List<DTOCategoria> GetAllCategos()
        {
            List<DTOCategoria> cats = new List<DTOCategoria>();

            using (design_proEntities context = new design_proEntities())
            {
                CategoriaRepository repositorio = new CategoriaRepository(context);
                var entityList = repositorio.GetAll();
                foreach (var entity in entityList)
                {
                    cats.Add(_mappercategoria.MapToDTOCategoria(entity));
                }
            }
            return cats;
                
        }

        public List<DTOProyecto> Search(string Buscar)
        {

            using (design_proEntities context = new design_proEntities())
            {
                ProyectoRepository repositorio = new ProyectoRepository(context);
                HerramientasRepository repo_herra = new HerramientasRepository(context);
                EtiquetasRepository repo_etiquetas = new EtiquetasRepository(context);
                List<DTOProyecto> lista = new List<DTOProyecto>();
                var proyectito = new DTOProyecto();
                var entityList = repositorio.GetAll();
                var herramientas = repo_herra.GetAll();
                var etiquetas = repo_etiquetas.GetAll();
                foreach (var proyecto in entityList)
                {
                    if ((proyecto.Titulo.Contains(Buscar)) || (proyecto.Autor.Contains(Buscar)))
                    {
                        lista.Add(_mapper.MapToDTOProyecto(proyecto));
                    }
                }
                foreach (var herra in herramientas)
                {
                    if (herra.Herramienta.Contains(Buscar.ToLower()))
                    {
                        proyectito = _mapper.MapToDTOProyecto(repositorio.get(herra.Titulo_proyecto));
                        if (!lista.Contains(proyectito))
                        {
                            lista.Add(proyectito);
                        }
                    }
                }
                foreach (var eti in etiquetas)
                {
                    if (eti.Etiquetas1.Contains(Buscar.ToLower()))
                    {
                        proyectito = _mapper.MapToDTOProyecto(repositorio.get(eti.Titulo_proyecto));
                        if (!lista.Contains(proyectito))
                        {
                            lista.Add(proyectito);
                        }
                    }
                }
                return lista;
            }

        }
    }
}
