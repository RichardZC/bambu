using Hra.App.Models;
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

        public ProcesoController(BAMBUContext contexto, IConstante constante)
        {
            this.contexto = contexto;
            this.constante = constante;
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

            return View(mensaje);
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
    }
}
