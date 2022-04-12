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
    public class GrupoController : Controller
    {
        private readonly BAMBUContext context;
        private readonly IConstante constante;
        public GrupoController(BAMBUContext context, IConstante constante)
        {
            this.context = context;
            this.constante = constante;
        }
        // GET: GrupoController
        public async Task<ActionResult> Index(string pBuscar = "")
        {
            var lst = new List<Grupo>();
            if (!string.IsNullOrEmpty(pBuscar) && pBuscar.Trim().Length > 0)
                lst = await context.Grupo.Where(x => x.Denominacion.Contains(pBuscar)).ToListAsync();
            else
                lst = await context.Grupo.Take(10).ToListAsync();
            return View(lst);
        }

        // GET: GrupoController/Create
        public async Task<ActionResult> Create(int id = 0)
        {
            ViewBag.TokenApiPeru = constante.TokenApiPeru;
            var grupo = new Grupo();
            ViewBag.id = id;
            var lstMiembro = new List<ListarMiembroDto>();
            if (id > 0)
            {

                grupo = await context.Grupo.FindAsync(id);
                //grupo.Miembro = await context.Miembro.Include(x => x.Persona)
                //    .Where(x => x.GrupoId == id).ToListAsync();                
                lstMiembro = await (from x in context.Miembro
                                    join vt in (context.ValorTabla.Where(x => x.TablaId == Constante.Tabla.EstadoMiembro)) on x.EstadoId equals vt.ItemId
                                    join n in (context.ValorTabla.Where(x => x.TablaId == Constante.Tabla.Nivel)) on x.NivelId equals n.ItemId
                                    where x.GrupoId == id
                                    select new ListarMiembroDto
                                    {
                                        MiembroId = x.MiembroId,
                                        PersonaId = x.PersonaId,
                                        Dni = x.Persona.NumeroDocumento,
                                        Miembro = x.Persona.NombreCompleto,
                                        Celular = x.Persona.Celular,
                                        Correo = x.Persona.Email,
                                        Grupo = x.Grupo.Denominacion,
                                        Nivel = n.Denominacion,
                                        Estado = vt.Denominacion,
                                        EstadoId = x.EstadoId,
                                        NivelId = x.NivelId
                                    }).OrderBy(x => x.Miembro).ToListAsync();
            }
            ViewBag.lstMiembro = lstMiembro;
            ViewBag.cboTaller = await context.ValorTabla.Where(x => x.TablaId == Constante.Tabla.Taller && x.ItemId > 0)
                .Select(x => new SelectListItem
                {
                    Text = x.Denominacion,
                    Value = x.ItemId.ToString()
                }).ToListAsync();
            string tallerid = grupo.TallerId.ToString();
            ViewBag.cboNivel = await context.ValorTabla
                .Where(x => x.TablaId == Constante.Tabla.Nivel && x.ItemId > 0 && x.Valor == tallerid)
                .Select(x => new SelectListItem
                {
                    Text = x.Denominacion,
                    Value = x.ItemId.ToString()
                }).ToListAsync();
            ViewBag.cboEstado = await context.ValorTabla.Where(x => x.TablaId == Constante.Tabla.EstadoMiembro && x.ItemId > 0)
                .Select(x => new SelectListItem
                {
                    Text = x.Denominacion,
                    Value = x.ItemId.ToString()
                }).ToListAsync();
            return View(grupo);
        }

        // POST: GrupoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Grupo grupo)
        {
            try
            {
                if (grupo.GrupoId == 0)
                {
                    context.Grupo.Add(grupo);
                }
                else
                {
                    context.Grupo.Update(grupo);
                }
                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public async Task<ActionResult> AgregarMiembro(int pGrupoId, string pDni, string pPaterno,
            string pMaterno, string pNombres)
        {
            pDni = pDni.Trim();
            pPaterno = pPaterno.ToUpper();
            pMaterno = pMaterno.ToUpper();
            pNombres = pNombres.ToUpper();

            var persona = await context.Persona.FirstOrDefaultAsync(x => x.NumeroDocumento == pDni);
            if (persona == null)
            {
                persona = new Persona
                {
                    NumeroDocumento = pDni,
                    ApePaterno = pPaterno,
                    ApeMaterno = pMaterno,
                    Nombre = pNombres,
                    NombreCompleto = pPaterno + " " + pMaterno + " " + pNombres,
                    Sexo = "M",
                    Activo = true,
                    TipoAlimentacionId = 1,
                    FechaReg = DateTime.Now
                };
                context.Persona.Add(persona);
                await context.SaveChangesAsync();
            }

            var miembro = context.Miembro.FirstOrDefault(x => x.PersonaId == persona.PersonaId && x.GrupoId == pGrupoId);
            if (miembro == null)
            {
                var grupo = context.Grupo.Find(pGrupoId);
                string tallerid = grupo.TallerId.ToString();
                var nivel = context.ValorTabla.First(x => x.TablaId == Constante.Tabla.Nivel && x.Valor == tallerid);
                miembro = new Miembro
                {
                    PersonaId = persona.PersonaId,
                    GrupoId = pGrupoId,
                    Fecha = DateTime.Now,
                    EstadoId = Constante.EstadoPersona.Pendiente,
                    NivelId = nivel.ItemId
                };
                context.Miembro.Add(miembro);
                await context.SaveChangesAsync();
            }


            return RedirectToAction(nameof(Create), new { id = pGrupoId });
        }



        public async Task<ActionResult> EliminarMiembro(int pGrupoId, int pMiembroId)
        {
            var cliente = await context.Miembro.FindAsync(pMiembroId);

            if (cliente != null)
            {
                context.Miembro.Remove(cliente);
                await context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Create), new { id = pGrupoId });
        }

        [HttpPost]
        public async Task<IActionResult> GuardarEstadoNivel(int MiembroId, int EstadoId, int NivelId)
        {
            var miembro = await context.Miembro.FindAsync(MiembroId);
            if (miembro != null)
            {
                miembro.EstadoId = EstadoId;
                miembro.NivelId = NivelId;
                await context.SaveChangesAsync();
            }
            return Json(true);
        }

    }
}
