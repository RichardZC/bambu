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
    public class ClienteController : Controller
    {
        private readonly BAMBUContext contexto;
        private readonly IConstante constante;

        public ClienteController(BAMBUContext contexto, IConstante constante)
        {
            this.contexto = contexto;
            this.constante = constante;
        }
        public IActionResult Index(int pPersonaId = 0)
        {
            ViewBag.PersonaId = pPersonaId;
            return View();
        }
        public async Task<IActionResult> Listar(string pBuscar = "")
        {
            var lstPersona = new List<ListarMiembroDto>();
            if (!string.IsNullOrEmpty(pBuscar) && pBuscar.Trim().Length > 0)
                lstPersona = await (from x in contexto.Cliente
                                    join vt in (contexto.ValorTabla.Where(x => x.TablaId == Constante.Tabla.EstadoMiembro)) on x.Estado equals vt.ItemId
                                    where x.Persona.NombreCompleto.Contains(pBuscar) || x.Grupo.Denominacion.Contains(pBuscar)
                                    select new ListarMiembroDto
                                    {
                                        PersonaId = x.PersonaId,
                                        Dni = x.Persona.NumeroDocumento,
                                        Miembro = x.Persona.NombreCompleto,
                                        Celular = x.Persona.Celular,
                                        Correo = x.Persona.Email,
                                        Grupo = x.Grupo.Denominacion,
                                        Estado = vt.Denominacion
                                    }).ToListAsync();
            else
                lstPersona = await (from x in contexto.Cliente
                                    join vt in (contexto.ValorTabla.Where(x => x.TablaId == Constante.Tabla.EstadoMiembro)) on x.Estado equals vt.ItemId
                                    select new ListarMiembroDto
                                    {
                                        PersonaId = x.PersonaId,
                                        Dni = x.Persona.NumeroDocumento,
                                        Miembro = x.Persona.NombreCompleto,
                                        Celular = x.Persona.Celular,
                                        Correo = x.Persona.Email,
                                        Grupo = x.Grupo.Denominacion,
                                        Estado = vt.Denominacion
                                    }).Take(10).OrderByDescending(x => x.PersonaId).ToListAsync();

            return View(lstPersona);
        }
        public async Task<IActionResult> Editar(int id = 0)
        {
            var persona = new Persona()
            {
                Estado = true
            };
            if (id > 0)
                persona = await contexto.Persona.FirstOrDefaultAsync(x => x.PersonaId == id);

            ViewBag.TokenApiPeru = constante.TokenApiPeru;
            ViewBag.cboEstadoCivil = await contexto.ValorTabla.Where(x => x.TablaId == Constante.Tabla.EstadoCivil && x.ItemId > 0)
                .Select(x => new SelectListItem
                {
                    Text = x.Denominacion,
                    Value = x.ItemId.ToString()
                }).ToListAsync();
            ViewBag.cboApoderadoParentesco = await contexto.ValorTabla.Where(x => x.TablaId == Constante.Tabla.ApoderadoParentesco && x.ItemId > 0)
                .Select(x => new SelectListItem
                {
                    Text = x.Denominacion,
                    Value = x.ItemId.ToString()
                }).ToListAsync();
            ViewBag.cboTipoAlimentacion = await contexto.ValorTabla.Where(x => x.TablaId == Constante.Tabla.TipoAlimentacion && x.ItemId > 0)
                .Select(x => new SelectListItem
                {
                    Text = x.Denominacion,
                    Value = x.ItemId.ToString()
                }).ToListAsync();
            ViewBag.cboEstado = await contexto.ValorTabla.Where(x => x.TablaId == Constante.Tabla.EstadoMiembro && x.ItemId > 0)
                .Select(x => new SelectListItem
                {
                    Text = x.Denominacion,
                    Value = x.ItemId.ToString()
                }).ToListAsync();
            ViewBag.cboGrupo = await contexto.Grupo
                .Select(x => new SelectListItem
                {
                    Text = x.Denominacion,
                    Value = x.GrupoId.ToString()
                }).ToListAsync();
            var miembro = new Miembro()
            {
                Persona = persona,
                Cliente = await contexto.Cliente.FirstOrDefaultAsync(x => x.PersonaId == id)
            };
            if (miembro.Cliente == null)
                miembro.Cliente = new Cliente() { Activo = true };
            if (miembro.Cliente.PersonaReferenciaId == null)
                ViewBag.PersonaReferencia = string.Empty;
            else
                ViewBag.PersonaReferencia = contexto.Persona
                .FirstOrDefault(x => x.PersonaId == miembro.Cliente.PersonaReferenciaId).NombreCompleto;

            return View(miembro);
        }
        public class Miembro
        {
            public Persona Persona { get; set; }
            public Cliente Cliente { get; set; }
        }
        [HttpGet]
        public async Task<JsonResult> BuscarClienteAutocomplete(string term)
        {
            var qry = from p in contexto.Persona
                      where p.Estado && (p.NombreCompleto.Contains(term) || p.NumeroDocumento.Contains(term))
                      orderby p.NombreCompleto
                      select new { id = p.PersonaId, text = p.NumeroDocumento + " " + p.NombreCompleto };
            return Json(await qry.Take(10).ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Guardar(Miembro miembro)
        {
            miembro.Persona.ApePaterno = miembro.Persona.ApePaterno.Trim().ToUpper();
            miembro.Persona.ApeMaterno = miembro.Persona.ApeMaterno.Trim().ToUpper();
            miembro.Persona.Nombre = miembro.Persona.Nombre.Trim().ToUpper();
            miembro.Persona.NombreCompleto = miembro.Persona.ApePaterno + " " + miembro.Persona.ApeMaterno + " " + miembro.Persona.Nombre;

            if (miembro.Persona.PersonaId > 0)
                contexto.Persona.Update(miembro.Persona);
            else
                contexto.Persona.Add(miembro.Persona);

            await contexto.SaveChangesAsync();

            if (miembro.Cliente.ClienteId > 0)
            {
                miembro.Cliente.FechaReg = DateTime.Now;
                miembro.Cliente.Activo = miembro.Persona.Estado;
                contexto.Cliente.Update(miembro.Cliente);
            }
            else
            {
                miembro.Cliente.ClienteId = miembro.Persona.PersonaId;
                miembro.Cliente.PersonaId = miembro.Persona.PersonaId;
                miembro.Cliente.FechaReg = DateTime.Now;
                miembro.Cliente.Activo = miembro.Persona.Estado;
                contexto.Cliente.Add(miembro.Cliente);
            }
            await contexto.SaveChangesAsync();


            return RedirectToAction("Listar");
        }

        [HttpGet]
        public async Task<IActionResult> ValidarClienteDNI(string pDniAnterior, string pDni)
        {
            if (pDniAnterior == pDni)
                return Json(true);

            var existe = await contexto.Persona.AnyAsync(x => x.NumeroDocumento == pDni
                                            && x.NumeroDocumento != pDniAnterior);
            return Json(!existe);
        }
    }
}
