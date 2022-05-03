using Hra.App.Models;
using Hra.App.Servicio;
using Hra.Application.DTO;
using Hra.Domain.Entity;
using Hra.Infraestructure.Data;
using Hra.Transversal.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hra.App.Controllers
{
    public class ProcesoController : Controller
    {
        private readonly BAMBUContext contexto;
        private readonly IConstante constante;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly string _contenedor = "storage";
        public ProcesoController(BAMBUContext contexto, IConstante constante, IAlmacenadorArchivos almacenadorArchivos)
        {
            this.contexto = contexto;
            this.constante = constante;
            this.almacenadorArchivos = almacenadorArchivos;
        }
        public async Task<IActionResult> Index(int pMiembroId)
        {
            var miembro = await contexto.Miembro.Include(x => x.Persona).Include(x => x.Grupo)
                .FirstOrDefaultAsync(x => x.MiembroId == pMiembroId);
            var nivel = await contexto.ValorTabla.FirstOrDefaultAsync(x => x.TablaId == Constante.Tabla.Nivel && x.ItemId == miembro.NivelId);
            var Estado = await contexto.ValorTabla.FirstOrDefaultAsync(x => x.TablaId == Constante.Tabla.EstadoMiembro && x.ItemId == miembro.EstadoId);

            var mensaje = await (from p in contexto.Mensaje
                                 join vt in (contexto.ValorTabla.Where(x => x.TablaId == Constante.Tabla.Nivel)) on p.NivelId equals vt.ItemId
                                 where p.MiembroId == pMiembroId
                                 orderby p.Fecha descending
                                 select new ListarMensajeDto()
                                 {
                                     Fecha = p.Fecha,
                                     Nota = p.Nota,
                                     MensajeId = p.MensajeId,
                                     Nivel = vt.Denominacion
                                 }).ToListAsync();

            var archivos = await (from p in contexto.Archivo
                                  join vt in (contexto.ValorTabla.Where(x => x.TablaId == Constante.Tabla.Evidencia)) on p.EvidenciaId equals vt.ItemId
                                  where p.MiembroId == pMiembroId
                                  orderby p.Fecha descending
                                  select new ListarArchivoDto()
                                  {
                                      Fecha = p.Fecha,
                                      ArchivoId = p.ArchivoId,
                                      Evidencia = vt.Denominacion,
                                      Archivo = p.Nombre
                                  }).ToListAsync();

            ViewBag.MiembroId = pMiembroId;
            ViewBag.NombreCompleto = miembro.Persona.NombreCompleto
                + " [" + miembro.Grupo.Denominacion + "-" + nivel.Denominacion + "]"
                + " ESTADO: " + Estado.Denominacion;
            var tallerid = miembro.Grupo.TallerId.ToString();
            ViewBag.cboNivel = await contexto.ValorTabla
                .Where(x => x.TablaId == Constante.Tabla.Nivel && x.ItemId > 0 && x.Valor == tallerid)
                .Select(x => new SelectListItem
                {
                    Text = x.Denominacion,
                    Value = x.ItemId.ToString()
                }).ToListAsync();
            ViewBag.cboEvidencia = await contexto.ValorTabla.Where(x => x.TablaId == Constante.Tabla.Evidencia && x.ItemId > 0)
                .Select(x => new SelectListItem
                {
                    Text = x.Denominacion,
                    Value = x.ItemId.ToString()
                }).ToListAsync();

            return View(new ProcesoMiembro { Mensajes = mensaje, Archivos = archivos });
        }
        public class ProcesoMiembro
        {
            public List<ListarMensajeDto> Mensajes { get; set; }
            public List<ListarArchivoDto> Archivos { get; set; }
        }
        [HttpPost]
        public async Task<IActionResult> GuardarMensaje(Mensaje mensaje)
        {
            mensaje.Fecha = DateTime.Now;
            contexto.Mensaje.Add(mensaje);
            await contexto.SaveChangesAsync();
            return RedirectToAction("Index", new { pMiembroId = mensaje.MiembroId });
        }

        public async Task<ActionResult> EliminarMensaje(int pMensajeId)
        {
            var m = await contexto.Mensaje.FindAsync(pMensajeId);
            if (m != null)
            {
                contexto.Mensaje.Remove(m);
                await contexto.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index), new { pMiembroId = m.MiembroId });
        }
        public async Task<ActionResult> EliminarArchivo(int pArchivoId)
        {
            var m = await contexto.Archivo.FindAsync(pArchivoId);
            if (m != null)
            {
                if (!string.IsNullOrEmpty(m.Nombre))
                    await almacenadorArchivos.BorrarArchivo(m.Nombre, _contenedor);

                contexto.Archivo.Remove(m);
                await contexto.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index), new { pMiembroId = m.MiembroId });
        }

        [HttpPost]
        public async Task<IActionResult> GuardarArchivo(IFormFile file, Archivo archivo)
        {
            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName).ToLower();
                using (var memorystream = new MemoryStream())
                {
                    await file.CopyToAsync(memorystream);
                    var contenido = memorystream.ToArray();
                    archivo.Nombre = await almacenadorArchivos.GuardarArchivo(contenido, extension, _contenedor, file.ContentType);
                }
            }

            archivo.Fecha = DateTime.Now;
            contexto.Archivo.Add(archivo);
            await contexto.SaveChangesAsync();
            return RedirectToAction("Index", new { pMiembroId = archivo.MiembroId });
        }
    }
}
