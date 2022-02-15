﻿using Hra.App.Models;
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
                lstPersona = await (from x in contexto.Persona
                                    join vt in (contexto.ValorTabla.Where(x => x.TablaId == Constante.Tabla.EstadoMiembro)) on x.EstadoId equals vt.ItemId
                                    where x.NombreCompleto.Contains(pBuscar) || x.Grupo.Denominacion.Contains(pBuscar)
                                    select new ListarMiembroDto
                                    {
                                        PersonaId = x.PersonaId,
                                        Dni = x.NumeroDocumento,
                                        Miembro = x.NombreCompleto,
                                        Celular = x.Celular,
                                        Correo = x.Email,
                                        Grupo = x.Grupo.Denominacion,
                                        Estado = vt.Denominacion
                                    }).ToListAsync();
            else
                lstPersona = await (from x in contexto.Persona
                                    join vt in (contexto.ValorTabla.Where(x => x.TablaId == Constante.Tabla.EstadoMiembro)) on x.EstadoId equals vt.ItemId
                                    select new ListarMiembroDto
                                    {
                                        PersonaId = x.PersonaId,
                                        Dni = x.NumeroDocumento,
                                        Miembro = x.NombreCompleto,
                                        Celular = x.Celular,
                                        Correo = x.Email,
                                        Grupo = x.Grupo.Denominacion,
                                        Estado = vt.Denominacion
                                    }).Take(10).OrderByDescending(x => x.PersonaId).ToListAsync();

            return View(lstPersona);
        }
        public async Task<IActionResult> Editar(int id = 0)
        {
            var persona = new Persona()
            {
                Activo = true
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



            if (persona.PersonaReferenciaId == null)
                ViewBag.PersonaReferencia = string.Empty;
            else
                ViewBag.PersonaReferencia = contexto.Persona
                .FirstOrDefault(x => x.PersonaId == persona.PersonaReferenciaId).NombreCompleto;

            return View(persona);
        }

        [HttpGet]
        public async Task<JsonResult> BuscarClienteAutocomplete(string term)
        {
            var qry = from p in contexto.Persona
                      where p.Activo && (p.NombreCompleto.Contains(term) || p.NumeroDocumento.Contains(term))
                      orderby p.NombreCompleto
                      select new { id = p.PersonaId, text = p.NumeroDocumento + " " + p.NombreCompleto };
            return Json(await qry.Take(10).ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Guardar(Persona miembro)
        {
            miembro.ApePaterno = miembro.ApePaterno.Trim().ToUpper();
            miembro.ApeMaterno = miembro.ApeMaterno.Trim().ToUpper();
            miembro.Nombre = miembro.Nombre.Trim().ToUpper();
            miembro.NombreCompleto = miembro.ApePaterno + " " + miembro.ApeMaterno + " " + miembro.Nombre;
            miembro.FechaReg = DateTime.Now;

            if (miembro.PersonaId > 0)
                contexto.Persona.Update(miembro);
            else
                contexto.Persona.Add(miembro);

            await contexto.SaveChangesAsync();


            return RedirectToAction("Listar");
        }
        [HttpPost]
        public async Task<IActionResult> CrearPersona(string pDni, string pNombre, string pPaterno, string pMaterno)
        {
            pPaterno = pPaterno.Trim().ToUpper();
            pMaterno = pMaterno.Trim().ToUpper();
            pNombre = pNombre.Trim().ToUpper();

            var existepersona = await contexto.Persona.AnyAsync(x => x.NumeroDocumento == pDni);
            if (existepersona)
                return Json("Ya se encuentra registrado el Dni");

            existepersona = await contexto.Persona.AnyAsync(x =>
                         x.ApePaterno == pPaterno && x.ApeMaterno == pMaterno && x.Nombre == pNombre
            );
            if (existepersona)
                return Json("Ya se encuentra registrado " + pPaterno + " " + pMaterno + " " + pNombre);

            var persona = new Persona
            {
                NumeroDocumento = pDni,
                ApePaterno = pPaterno,
                ApeMaterno = pMaterno,
                Nombre = pNombre,
                NombreCompleto = pPaterno + " " + pMaterno + " " + pNombre,
                Sexo = "M",
                Activo = true,
                EstadoId = 9
            };
            contexto.Persona.Add(persona);
            await contexto.SaveChangesAsync();

            return Json(string.Empty);
        }
        [HttpGet]
        public async Task<IActionResult> ValidarClienteDNI(string pDniAnterior, string pDni,
            string pPaterno, string pMaterno, string pNombre)
        {
            try
            {
                if (!String.IsNullOrEmpty(pDniAnterior) && pDniAnterior == pDni)
                    return Json(string.Empty);

                var persona = await contexto.Persona
                    .FirstOrDefaultAsync(x => x.NumeroDocumento == pDni && x.NumeroDocumento != pDniAnterior);
                if (persona != null)
                    return Json("Ya existe una persona registrado con este DNI:" + pDni);

                var persona1 = await contexto.Persona
                    .FirstOrDefaultAsync(x => x.ApePaterno == pPaterno
                                && x.ApeMaterno == pMaterno
                                && x.Nombre == pNombre && x.NumeroDocumento != pDniAnterior);

                if (persona1 != null)
                    return Json("Ya existe una persona registrado con: "
                        + persona1.ApePaterno + " " + persona1.ApeMaterno + " " + persona1.Nombre);

            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }

            return Json(string.Empty);

        }
        public async Task<IActionResult> BuscarPersona(string pDni)
        {
            var persona = await contexto.Persona.FirstOrDefaultAsync(x => x.NumeroDocumento == pDni);
            return Json(persona);
        }
    }
}
