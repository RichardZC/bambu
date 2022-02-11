﻿using Hra.App.Models;
using Hra.Domain.Entity;
using Hra.Infraestructure.Data;
using Microsoft.AspNetCore.Mvc;
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
            grupo.Cliente = new List<Cliente>();
            if (id > 0)
            {
                grupo = await context.Grupo.FindAsync(id);
                grupo.Cliente = await context.Cliente.Include(x => x.Persona)
                    .Where(x => x.GrupoId == id).ToListAsync();
            }
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
                    Estado = true
                };
                context.Persona.Add(persona);
                await context.SaveChangesAsync();
            }

            var cliente = await context.Cliente.FirstOrDefaultAsync(x => x.PersonaId == persona.PersonaId);
            if (cliente == null)
            {
                cliente = new Cliente
                {
                    ClienteId = persona.PersonaId,
                    PersonaId = persona.PersonaId,
                    GrupoId = pGrupoId,
                    TipoAlimentacionId = 1,
                    FechaReg = DateTime.Now,
                    Activo = true,
                    Estado = 1
                };
                context.Cliente.Add(cliente);
                await context.SaveChangesAsync();
            }
            else
            {
                cliente.GrupoId = pGrupoId;
                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Create), new { id = pGrupoId });
        }



        public async Task<ActionResult> EliminarMiembro(int pGrupoId, int pPersonaId)
        {
            var cliente = await context.Cliente.FirstOrDefaultAsync(x => x.PersonaId == pPersonaId);
            if (cliente != null)
            {
                cliente.GrupoId = null;
                await context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Create), new { id = pGrupoId });
        }



    }
}